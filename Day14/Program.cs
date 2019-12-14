using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {

            var reactions = new Dictionary<string, (int producedQuantity,IEnumerable<(string chemical, int quantity)>)>();

            string line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                string[] decomposedReaction = 
                    line.Trim()
                        .Split(" => ").ToArray();

                string[] produits = decomposedReaction[1].Split(' ');
                string produit = produits[1];
                int numberProduit = Int32.Parse(produits[0]);

                IEnumerable<(string chemical, int quantity)> reactifs = decomposedReaction[0].Split(", ").Select(reactif =>
                {
                    string[] r = reactif.Split(' ');
                    return (chemical: r[1], quantity: int.Parse(r[0]));
                });

                reactions[produit] = (numberProduit, reactifs);

                line = Console.ReadLine();
            }

            long min = 1;
            long max = 1000000000000;


            while (max - min > 1)
            {
                long response = NeededOre(reactions, (max + min) >> 1);

                if (response > 1000000000000)
                {
                    max = (max + min) >> 1;
                }
                else
                {
                    min = (max + min) >> 1;
                }
                Console.WriteLine($"{min} {max}");
            }

        }

        private static long NeededOre(Dictionary<string, (int producedQuantity, IEnumerable<(string chemical, int quantity)>)> reactions, long neededFuel)
        {
            Dictionary<string, long> neededQuantities = new Dictionary<string, long>();

            neededQuantities["FUEL"] = neededFuel;

            while (!(neededQuantities.Count(_ => _.Value > 0) == 1 && neededQuantities.ContainsKey("ORE")))
            {
                KeyValuePair<string, long> neededQuantity = neededQuantities.Where(_ => _.Value > 0).First(_ => _.Key != "ORE");

                (int producedQuantity, IEnumerable<(string chemical, int quantity)> reactifs) correspondingReaction =
                    reactions[neededQuantity.Key];

                var reactionTimes = (neededQuantity.Value - 1) / correspondingReaction.producedQuantity + 1;

                neededQuantities[neededQuantity.Key] -= correspondingReaction.producedQuantity * reactionTimes;

                //Console.WriteLine($"{neededQuantity.Value} of {neededQuantity.Key}. Run the reaction {reactionTimes} times.");

                foreach ((string chemical, int quantity) reactif in correspondingReaction.reactifs)
                {
                    if (!neededQuantities.ContainsKey(reactif.chemical))
                    {
                        neededQuantities[reactif.chemical] = 0;
                    }

                    neededQuantities[reactif.chemical] += reactif.quantity * reactionTimes;


                    //Console.WriteLine($"    need {reactif.quantity * reactionTimes} of {reactif.chemical}");
                }

                //neededQuantities.Remove(neededQuantity.Key);
            }

            return neededQuantities["ORE"];
        }
    }
}
