using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Day18
{
    class Program
    {
        private static Dictionary<char, (int, int)> _charToPosition;
        private static List<string> _map;
        private static Dictionary<(HashSet<char>, char), int> _shortestPath;

        static void Main(string[] args)
        {
            _map = new List<string>();
            _charToPosition = new Dictionary<char, (int, int)>();
            _shortestPath = new Dictionary<(HashSet<char>, char), int>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"input2.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                _map.Add(line);
            }

            for (int i = 0; i < _map.Count; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    _charToPosition[_map[i][j]] = (i,j);
                }
            }

            Part1();
        }

        public static void Part1()
        {
            var keysMap = new List<Dictionary<(string, char, char, char, char), int>>();
            keysMap.Add(new Dictionary<(string, char, char, char, char), int> { [("", '@', '&', '|', '^')] = 0});

            int i = 0;
            while (keysMap[i].First().Key.Item1.Length != _charToPosition.Count(c => char.IsLower(c.Key)))
            {
                // at position i+1
                keysMap.Add(new Dictionary<(string, char, char, char, char), int>());
                foreach (KeyValuePair<(string visitedKeys, char lastKey1, char lastKey2, char lastKey3, char lastKey4), int> previousState in keysMap[i])
                {
                    // lastKey1
                    (int y, int x) = _charToPosition[previousState.Key.lastKey1];
                    Dictionary<char, int> accessiblekeys = GetAllAvailableKeys(y, x, new HashSet<(int y, int x)>(), previousState.Key.visitedKeys);
                    foreach (KeyValuePair<char, int> accessibleKey in accessiblekeys)
                    {
                        char[] newKeysNotInOrder = (previousState.Key.visitedKeys + accessibleKey.Key).ToCharArray();
                        Array.Sort(newKeysNotInOrder);
                        string newKeys = new string(newKeysNotInOrder);
                        var newKeyTuple = (newKeys, accessibleKey.Key, previousState.Key.lastKey2,
                            previousState.Key.lastKey3, previousState.Key.lastKey4);
                        CheckAccessibleKeys(keysMap, i, newKeyTuple, accessibleKey, previousState);
                    }

                    // lastKey2
                    (y, x) = _charToPosition[previousState.Key.lastKey2];
                    accessiblekeys = GetAllAvailableKeys(y, x, new HashSet<(int y, int x)>(), previousState.Key.visitedKeys);
                    foreach (KeyValuePair<char, int> accessibleKey in accessiblekeys)
                    {
                        char[] newKeysNotInOrder = (previousState.Key.visitedKeys + accessibleKey.Key).ToCharArray();
                        Array.Sort(newKeysNotInOrder);
                        string newKeys = new string(newKeysNotInOrder);
                        var newKeyTuple = (newKeys, previousState.Key.lastKey1, accessibleKey.Key,
                            previousState.Key.lastKey3, previousState.Key.lastKey4);
                        CheckAccessibleKeys(keysMap, i, newKeyTuple, accessibleKey, previousState);
                    }

                    // lastKey3
                    (y, x) = _charToPosition[previousState.Key.lastKey3];
                    accessiblekeys = GetAllAvailableKeys(y, x, new HashSet<(int y, int x)>(), previousState.Key.visitedKeys);
                    foreach (KeyValuePair<char, int> accessibleKey in accessiblekeys)
                    {
                        char[] newKeysNotInOrder = (previousState.Key.visitedKeys + accessibleKey.Key).ToCharArray();
                        Array.Sort(newKeysNotInOrder);
                        string newKeys = new string(newKeysNotInOrder);
                        var newKeyTuple = (newKeys, previousState.Key.lastKey1, previousState.Key.lastKey2,
                            accessibleKey.Key, previousState.Key.lastKey4);
                        CheckAccessibleKeys(keysMap, i, newKeyTuple, accessibleKey, previousState);
                    }

                    // lastKey4
                    (y, x) = _charToPosition[previousState.Key.lastKey4];
                    accessiblekeys = GetAllAvailableKeys(y, x, new HashSet<(int y, int x)>(), previousState.Key.visitedKeys);
                    foreach (KeyValuePair<char, int> accessibleKey in accessiblekeys)
                    {
                        char[] newKeysNotInOrder = (previousState.Key.visitedKeys + accessibleKey.Key).ToCharArray();
                        Array.Sort(newKeysNotInOrder);
                        string newKeys = new string(newKeysNotInOrder);
                        var newKeyTuple = (newKeys, previousState.Key.lastKey1, previousState.Key.lastKey2,
                            previousState.Key.lastKey3, accessibleKey.Key);
                        CheckAccessibleKeys(keysMap, i, newKeyTuple, accessibleKey, previousState);
                    }
                }
                i++;
            }
        }

        private static void CheckAccessibleKeys(List<Dictionary<(string, char, char, char, char), int>> keysMap, int i,
            (string newKeys, char Key, char lastKey2, char lastKey3, char lastKey4) newKeyTuple, KeyValuePair<char, int> accessibleKey,
            KeyValuePair<(string visitedKeys, char lastKey1, char lastKey2, char lastKey3, char lastKey4), int> previousState)
        {
            // check if this state has already been encountered and store it if better
            if (keysMap[i + 1].ContainsKey(newKeyTuple))
            {
                if (accessibleKey.Value + previousState.Value < keysMap[i + 1][newKeyTuple])
                {
                    keysMap[i + 1][newKeyTuple] = accessibleKey.Value + previousState.Value;
                }
            }
            else
            {
                keysMap[i + 1][newKeyTuple] = accessibleKey.Value + previousState.Value;
            }
        }

        public static Dictionary<char, int> GetAllAvailableKeys(int y, int x, HashSet<(int y, int x)> visited, string availableKeys)
        {
            var result = new Dictionary<char, int>();
            visited.Add((y, x));

            foreach (var (newX, newY) in new List<(int, int)> {(x + 1, y), (x - 1, y), (x, y + 1), (x, y - 1)})
            {
                char newChar = _map[newY][newX];
                if (char.IsLower(newChar) && !availableKeys.Contains(newChar))
                {
                    result[newChar] = 1;
                }
                else if (!visited.Contains((newY, newX)) && newChar != '#' && !(char.IsUpper(newChar) && !availableKeys.Contains(char.ToLower(newChar))))
                {
                    Dictionary<char, int> allAvailableKeys = GetAllAvailableKeys(newY, newX, visited, availableKeys);

                    foreach (KeyValuePair<char, int> kv in allAvailableKeys)
                    {
                        if (result.ContainsKey(kv.Key))
                        {
                            if (result[kv.Key] > kv.Value + 1)
                            {
                                result[kv.Key] = kv.Value + 1;
                            }
                        }
                        else
                        {
                            result[kv.Key] = kv.Value + 1;
                        }
                    }
                }

            }
            visited.Remove((y, x));

            return result;
        }
    }
}
