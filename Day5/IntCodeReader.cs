using System;
using System.Linq;

namespace Day5
{
    public class IntCodeReader
    {
        private int _parameterMode;
        private DefaultToZeroDictionary _program;
        private long _compteur;
        private long _relativeBase;

        public IntCodeReader(string line, int parameterMode = -1)
        {
            _parameterMode = parameterMode;
            long i = 0;
            _program = new DefaultToZeroDictionary(line.Split((',')).Select(Int64.Parse).ToDictionary(v => i++, v => v));
            _compteur = 0;
            _relativeBase = 0;
        }


        public long RunIntCode(int[] inputs)
        {
            int inputCursor = 0;

            while (true)
            {
                long code = _program[_compteur];

                switch (code % 100)
                {
                    case 1:
                        SaveInput(_program, _compteur, 3, GetInput(_program, _compteur, 1) + GetInput(_program, _compteur, 2));
                        _compteur += 4;
                        break;
                    case 2:
                        SaveInput(_program, _compteur, 3, GetInput(_program, _compteur, 1) * GetInput(_program, _compteur, 2));
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

                        SaveInput(_program, _compteur, 1, input);
                        
                        _compteur += 2;
                        break;
                    case 4:
                        // We return here, but we should still update compteur for next execution. 
                        long returnValue = GetInput(_program, _compteur, 1);
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
                        SaveInput(_program, _compteur, 3,
                            GetInput(_program, _compteur, 1) < GetInput(_program, _compteur, 2) ? 1 : 0);
                        _compteur += 4;
                        break;
                    case 8:
                        SaveInput(_program, _compteur, 3,
                            GetInput(_program, _compteur, 1) == GetInput(_program, _compteur, 2) ? 1 : 0);
                        _compteur += 4;
                        break;
                    case 9:
                        _relativeBase += GetInput(_program, _compteur, 1);
                        _compteur += 2;
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

        public long GetParameterMode(DefaultToZeroDictionary program, long compteur, int i)
        {
            long code = program[compteur];
            int d = 10;
            for (int j = 0; j < i; j++)
            {
                d *= 10;
            }

            return (code / d) % 10;
        }

        public long GetInput(DefaultToZeroDictionary program, long compteur, int i)
        {
            long parameterMode = GetParameterMode(program, compteur, i);

            // switch on parameter mode
            switch (parameterMode)
            {
                case 0:
                    return program[program[compteur + i]];
                case 1:
                    return program[compteur + i];
                case 2:
                    return program[_relativeBase + program[compteur + i]];
                default:
                    throw new Exception(
                        $"This parameter mode {parameterMode} should never happen");
            }
        }

        public void SaveInput(DefaultToZeroDictionary program, long compteur, int i, long value)
        {
            long parameterMode = GetParameterMode(program, compteur, i);

            switch (parameterMode)
            {
                case 0:
                    _program[_program[_compteur + i]] = value;
                    break;
                case 2:
                    _program[_relativeBase + _program[_compteur + i]] = value;
                    break;
                default:
                    throw new Exception($"parameter mode for OpCode should never be {GetParameterMode(_program, _compteur, i)}");
            }
        }
    }
}
