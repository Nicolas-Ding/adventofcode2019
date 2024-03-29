﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Day16_2
{
    class Program
    {
        public static Dictionary<int, int> Line;

        static void Main(string[] args)
        {
            StringBuilder builder = new StringBuilder();

            string input = "59767332893712499303507927392492799842280949032647447943708128134759829623432979665638627748828769901459920331809324277257783559980682773005090812015194705678044494427656694450683470894204458322512685463108677297931475224644120088044241514984501801055776621459006306355191173838028818541852472766531691447716699929369254367590657434009446852446382913299030985023252085192396763168288943696868044543275244584834495762182333696287306000879305760028716584659188511036134905935090284404044065551054821920696749822628998776535580685208350672371545812292776910208462128008216282210434666822690603370151291219895209312686939242854295497457769408869210686246";
            int lecture = Int32.Parse(input.Substring(0, 7));

            for (int i = 0; i < 10000; i++)
            {
                builder.Append(input);
            }

            int k = 1;
            Line = builder.ToString().ToDictionary(c => k++, c => (int) char.GetNumericValue(c));

            for (int p = 0; p < 100; p++)
            {
                Console.WriteLine(p);
                Dictionary<int, int> next = new Dictionary<int, int>(Line.Count);
                next[input.Length * 10000 - 1] = Line[input.Length * 10000 - 1];
                for (int i = input.Length * 10000 - 2; i >= lecture; i--)
                {
                    next[i] = (next[i+1] + Line[i]) % 10;
                }

                Line = next;
            }

            for (int i = lecture + 1; i < lecture + 9; i++)
            {
                Console.WriteLine(Line[i]);
            }
        }
    }
}
