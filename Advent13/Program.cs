    using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test();
            // return;

            var inputs = LoadInputs(@"inputs.txt");
            //Part1(inputs);
            long result = GetTimeStamp(inputs.Buses);
            System.Console.WriteLine("Resultat");
            System.Console.WriteLine(result);
            System.Console.Read();
        }

        static long GetTimeStamp(List<long?> values)
        {
            
            var tuples = values.Select((v,i) => Tuple.Create(v, (long)i+1))
                .Where(v => v.Item1.HasValue).OrderByDescending(v => v.Item1.Value).ToList();

            while(tuples.Count != 1)
            {
                System.Console.WriteLine(tuples.Count);
                tuples = tuples.Skip(2).Concat(Enumerable.Repeat(Compute(tuples[1], tuples[0]), 1))
                    .OrderByDescending(v => v.Item1.Value).ToList();
            }

            return tuples[0].Item1.Value - tuples[0].Item2 + 1;
        }

        static Tuple<long?, long> Compute(Tuple<long?, long> min, Tuple<long?, long> max)
        {
            long maxRange = min.Item1.Value * max.Item1.Value;
            System.Console.WriteLine($"long {maxRange} = {min.Item1.Value} * {max.Item1.Value};");
            long  commonValue = GetDepartureTime(min, max);
            return Tuple.Create((long?)maxRange, maxRange - commonValue);
        }

        static long GetDepartureTime(Tuple<long?, long> min, Tuple<long?, long> max)
        {
            long idMax = max.Item1.Value;
            long maxRange = idMax * min.Item1.Value;

            long percentInc = maxRange/20;
            long percent = maxRange/20;
            System.Console.Write("".PadRight(20, '_'));
            System.Console.CursorLeft -= 20;
            for (long i = idMax; i < maxRange; i+=idMax)
            {
                if(i > percent)
                {
                    System.Console.Write('#');
                    percent+=percentInc;
                }
                if(i - max.Item2 < 0)
                {
                    continue;
                }
                long value = (i - max.Item2 + min.Item2) / min.Item1.Value;
                decimal dvalue = (i - max.Item2 + min.Item2) / (decimal)min.Item1.Value;
                if(value == dvalue)
                {
                    System.Console.WriteLine();
                    return i - max.Item2;
                }
            }

            System.Console.WriteLine("Error");
            return 0;
        }

        private static void Part1(Data inputs)
        {
            var tuple = inputs.Buses.Select(b => new { b, value = (inputs.Timestamp / b) * b + b }).OrderBy(b => b.value).First();
            var value = (tuple.value - inputs.Timestamp) * tuple.b;
            System.Console.WriteLine(value); ;
        }

        static void Test()
        {
            var values = LoadInputs(@"inputsSmall.txt");

        }
        public static Data LoadInputs(string file)
        {
            using StreamReader reader = new StreamReader(file);
            List<string> inputs = new List<string>();
            string line1 = reader.ReadLine();
            string line2 = reader.ReadLine();
            return new Data(int.Parse(line1), line2.Split(',').Select(v => v == "x" ? (long?)null : int.Parse(v)).ToList());
        }

        public class Data
        {
            public Data(int timestamp, List<long?> buses)
            {
                Timestamp = timestamp;
                Buses = buses;
            }
            public int Timestamp{get;}
            public List<long?> Buses{get;}
        }

    }
}
