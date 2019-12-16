using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Day16
{
    class Program
    {
        static List<int> _factorList;

        public static string Line;

        // public static Dictionary<char, int> CharToInt;

        static void Main(string[] args)
        {
            StringBuilder builder = new StringBuilder();

            string input = "59767332893712499303507927392492799842280949032647447943708128134759829623432979665638627748828769901459920331809324277257783559980682773005090812015194705678044494427656694450683470894204458322512685463108677297931475224644120088044241514984501801055776621459006306355191173838028818541852472766531691447716699929369254367590657434009446852446382913299030985023252085192396763168288943696868044543275244584834495762182333696287306000879305760028716584659188511036134905935090284404044065551054821920696749822628998776535580685208350672371545812292776910208462128008216282210434666822690603370151291219895209312686939242854295497457769408869210686246";
            input = "19617804207202209144916044189917";
            input = "03036732577212944063491565474664";
            input = "03036732577212944063491565474664";

            //CharToInt = new Dictionary<char, int>
            //{
            //    ['0'] = 0,
            //    ['1'] = 1,
            //    ['2'] = 2,
            //    ['3'] = 3,
            //    ['4'] = 4,
            //    ['5'] = 5,
            //    ['6'] = 6,
            //    ['7'] = 7,
            //    ['8'] = 8,
            //    ['9'] = 9
            //};
            // input = "12345678";

            for (int i = 0; i < 10000; i++)
            {
                builder.Append(input);
            }

            Line = builder.ToString();

            _factorList = new List<int> {0, 1, 0, -1};
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int p = 0; p < 100; p++)
            {
                TimeSpan start = stopWatch.Elapsed;
                StringBuilder result = new StringBuilder();
                int previousResult = 0;
                int previousI = 0;
                for (int i = 0; i < Line.Length; i++)
                {
                    previousResult = Math.Abs(ComputeDigits(i) % 10);
                    result.Append(previousResult);
                    if (i > Line.Length / 4 + 5)
                    {
                        previousI = i;
                        break;
                    }
                }
                for (int i = previousI + 1; i < Line.Length; i++)
                {
                    previousResult -= -10 + (int) char.GetNumericValue(Line[i - 1]);
                    if (2 * i - 1 < Line.Length)
                    {
                        previousResult += (int)char.GetNumericValue(Line[2 * i - 1]);
                    }
                    if (2 * i < Line.Length)
                    {
                        previousResult += (int)char.GetNumericValue(Line[2 * i]);
                    }
                    result.Append(previousResult % 10);
                }
                Console.WriteLine($"{p} finished, elapsed time : {stopWatch.Elapsed.Minutes} min {stopWatch.Elapsed.Seconds} sec");

                Line = result.ToString();
            }
            Console.WriteLine(Line);
            //Console.WriteLine(Line.Substring(Int32.Parse(Line.Substring(0, 7)), 8));
        }

        public static int ComputeDigits(int line) // should be 0 at start
        {
            int digit = line + 1;
            int result = 0;

            int c = 1;

            while (c * digit - 1 < Line.Length)
            {
                int stop = Math.Min(Line.Length, (c + 1) * digit - 1);
                for (int i = c * digit - 1; i < stop; i++)
                {
                    result += (int)char.GetNumericValue(Line[i]);
                }

                stop = Math.Min(Line.Length, (c + 3) * digit - 1);
                for (int i = (c + 2) * digit - 1; i < stop; i++)
                {
                    result -= (int)char.GetNumericValue(Line[i]);
                }

                c+=4;
            }

            return result;
        }
    }
}
