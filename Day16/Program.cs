using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
    class Program
    {
        static List<int> _factorList;

        public static string Line;

        static void Main(string[] args)
        {
            StringBuilder builder = new StringBuilder();

            string input = "59767332893712499303507927392492799842280949032647447943708128134759829623432979665638627748828769901459920331809324277257783559980682773005090812015194705678044494427656694450683470894204458322512685463108677297931475224644120088044241514984501801055776621459006306355191173838028818541852472766531691447716699929369254367590657434009446852446382913299030985023252085192396763168288943696868044543275244584834495762182333696287306000879305760028716584659188511036134905935090284404044065551054821920696749822628998776535580685208350672371545812292776910208462128008216282210434666822690603370151291219895209312686939242854295497457769408869210686246";
            input = "03036732577212944063491565474664";

            for (int i = 0; i < 1000; i++)
            {
                builder.Append(input);
            }

            Line = builder.ToString();

            _factorList = new List<int>() {0, 1, 0, -1};

            for (int p = 0; p < 100; p++)
            {
                string result = "";
                for (int i = 0; i < Line.Length; i++)
                {
                    result += Math.Abs(ComputeDigits(i) % 10);
                }

                Line = result;
            }
            Console.WriteLine(Line.Substring(Int32.Parse(Line.Substring(0, 7)), 8));
        }

        public static int ComputeDigits(int digit) // should be 0 at start
        {
            int result = 0;
            IEnumerator<int> multiplicators = GetMultiplicators(digit + 1).GetEnumerator();
            multiplicators.MoveNext();
            multiplicators.MoveNext();

            foreach (char c in Line)
            {
                // Console.WriteLine($"{char.GetNumericValue(c)} * {multiplicators.Current}");
                result += (int) char.GetNumericValue(c) * multiplicators.Current;
                multiplicators.MoveNext();
            }

            multiplicators.Dispose();
            return result;
        }

        public static IEnumerable<int> GetMultiplicators(int element)
        {

            int f = 0;
            while(true)
            {
                int factor = _factorList[f % _factorList.Count];

                for (int i = 0; i < element; i++)
                {
                    yield return factor;
                }

                f++;
            }
        }
    }
}
