using System;
using System.Linq;

namespace IntCodeUtils
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

        public long RunIntCode(Func<long> GetInputs)
        {
            int inputCursor = 0;

            while (true)
            {
                long code = _program[_compteur];

                switch (code % 100)
                {
                    case 1:
                        SaveInput(3, GetCurrentParameter(1) + GetCurrentParameter(2));
                        _compteur += 4;
                        break;
                    case 2:
                        SaveInput(3, GetCurrentParameter(1) * GetCurrentParameter(2));
                        _compteur += 4;
                        break;
                    case 3:
                        long input;
                        if (_parameterMode >= 0)
                        {
                            input = _parameterMode;
                            _parameterMode = -2;
                        }
                        else
                        {
                            input = GetInputs();
                            inputCursor++;
                        }

                        SaveInput(1, input);
                        
                        _compteur += 2;
                        break;
                    case 4:
                        // We return here, but we should still update compteur for next execution. 
                        long returnValue = GetCurrentParameter(1);
                        _compteur += 2;
                        return returnValue;
                    case 5:
                        if (GetCurrentParameter(1) != 0)
                        {
                            _compteur = GetCurrentParameter(2);
                        }
                        else
                        {
                            _compteur += 3;
                        }

                        break;
                    case 6:
                        if (GetCurrentParameter(1) == 0)
                        {
                            _compteur = GetCurrentParameter(2);
                        }
                        else
                        {
                            _compteur += 3;
                        }

                        break;
                    case 7:
                        SaveInput(3, GetCurrentParameter(1) < GetCurrentParameter(2) ? 1 : 0);
                        _compteur += 4;
                        break;
                    case 8:
                        SaveInput(3, GetCurrentParameter(1) == GetCurrentParameter(2) ? 1 : 0);
                        _compteur += 4;
                        break;
                    case 9:
                        _relativeBase += GetCurrentParameter(1);
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

        public long GetCurrentParameterMode(int i)
        {
            long code = _program[_compteur];
            int d = 10;
            for (int j = 0; j < i; j++)
            {
                d *= 10;
            }

            return (code / d) % 10;
        }
        
        public long GetCurrentParameter(int i)
        {
            long parameterMode = GetCurrentParameterMode(i);

            // switch on parameter mode
            switch (parameterMode)
            {
                case 0:
                    return _program[_program[_compteur + i]];
                case 1:
                    return _program[_compteur + i];
                case 2:
                    return _program[_relativeBase + _program[_compteur + i]];
                default:
                    throw new Exception(
                        $"This parameter mode {parameterMode} should never happen");
            }
        }

        public void SaveInput(int i, long value)
        {
            long parameterMode = GetCurrentParameterMode(i);

            switch (parameterMode)
            {
                case 0:
                    _program[_program[_compteur + i]] = value;
                    break;
                case 2:
                    _program[_relativeBase + _program[_compteur + i]] = value;
                    break;
                default:
                    throw new Exception($"parameter mode for OpCode should never be {GetCurrentParameterMode(i)}");
            }
        }
    }
}
