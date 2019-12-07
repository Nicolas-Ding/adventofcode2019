using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day5
{
    public class IntCodeReader
    {
        private int _parameterMode;
        private int[] _program;
        private int _compteur;

        public IntCodeReader(string line, int parameterMode = -1)
        {
            _parameterMode = parameterMode;
            _program = line.Split((',')).Select(Int32.Parse).ToArray();
            _compteur = 0;

        }


        public int RunIntCode(int[] inputs)
        {
            int inputCursor = 0;

            while (true)
            {
                int code = _program[_compteur];

                switch (code % 100)
                {
                    case 1:
                        _program[_program[_compteur + 3]] = GetInput(_program, _compteur, 1) + GetInput(_program, _compteur, 2);
                        _compteur += 4;
                        break;
                    case 2:
                        _program[_program[_compteur + 3]] = GetInput(_program, _compteur, 1) * GetInput(_program, _compteur, 2);
                        _compteur += 4;
                        break;
                    case 3:
                        int input;
                        if (_parameterMode >= 0)
                        {
                            input = _parameterMode;
                            _parameterMode = -2;
                        }
                        else
                        {
                            input = inputs[inputCursor];
                            inputCursor++;
                        }
                        _program[_program[_compteur + 1]] = input;
                        _compteur += 2;
                        break;
                    case 4:
                        // We return here, but we should still update compteur for next execution. 
                        int returnValue = GetInput(_program, _compteur, 1);
                        _compteur += 2;
                        return returnValue;
                    case 5:
                        if (GetInput(_program, _compteur, 1) != 0)
                        {
                            _compteur = GetInput(_program, _compteur, 2);
                        }
                        else
                        {
                            _compteur += 3;
                        }

                        break;
                    case 6:
                        if (GetInput(_program, _compteur, 1) == 0)
                        {
                            _compteur = GetInput(_program, _compteur, 2);
                        }
                        else
                        {
                            _compteur += 3;
                        }

                        break;
                    case 7:
                        _program[_program[_compteur + 3]] =
                            GetInput(_program, _compteur, 1) < GetInput(_program, _compteur, 2) ? 1 : 0;
                        _compteur += 4;
                        break;
                    case 8:
                        _program[_program[_compteur + 3]] =
                            GetInput(_program, _compteur, 1) == GetInput(_program, _compteur, 2) ? 1 : 0;
                        _compteur += 4;
                        break;
                    case 99:
                        //Console.WriteLine(inputs[0]);
                        //Console.WriteLine("HALT");
                        return Int32.MinValue;
                    default:
                        return -1;
                }
            }
        }

        public int GetInput(int[] program, int compteur, int i)
        {
            int code = program[compteur];
            int d = 10;
            for (int j = 0; j < i; j++)
            {
                d *= 10;
            }

            return (code / d) % 10 == 1 ? program[compteur + i] : program[program[compteur + i]];
        }
    }
}
