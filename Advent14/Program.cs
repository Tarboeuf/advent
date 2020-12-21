    using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test();
            // return;

            var inputs = LoadInputs(@"inputs.txt");
            Part2(inputs);
        }
        private static void Part1(IEnumerable<Data> inputs)
        {
            long sum = 0;
            HashSet<long> done = new HashSet<long>();
            foreach(var input in inputs.Reverse())
            {
                input.Mems.Reverse();
                foreach (var item in input.Mems)
                {
                    if(done.Contains(item.ID))
                    {
                        continue;
                    }
                    done.Add(item.ID);
                    long value =  (item.Value | input.Mask1) & input.Mask0;
                    System.Console.WriteLine(value);
                    sum += value;
                }
            }
            System.Console.WriteLine($"sum {sum}");
        }

        private static void Part2(IEnumerable<Data> inputs)
        {
            long sum = 0;
            HashSet<long> done = new HashSet<long>();
            foreach(var input in inputs.Reverse())
            {
                input.Mems.Reverse();
                foreach (var item in input.Mems)
                {
                    //200 + 8
                    if(done.Contains(item.ID))
                    {
                        continue;
                    }
                    done.Add(item.ID);
                    long value =  (item.Value | input.Mask1) & input.Mask0;
                    System.Console.WriteLine(value);
                    sum += value;
                }
            }
            System.Console.WriteLine($"sum {sum}");
        }

        static void Test()
        {
            var values = LoadInputs(@"inputsTests.txt");

        }
        public static IEnumerable<Data> LoadInputs(string file)
        {
            using StreamReader reader = new StreamReader(file);
            
            string line;
            Data currentData = null;

            System.Console.WriteLine();
            while((line = reader.ReadLine()) != null)
            {
                if(line.StartsWith("mask"))
                {
                    if(currentData != null)
                    {
                        yield return currentData;
                    }
                    currentData = new Data();
                    
                    currentData.Mask1 = Convert.ToInt64(line.Substring(7).Replace('X', '0'), 2);
                    currentData.Mask0 = Convert.ToInt64(line.Substring(7).Replace('X', '1'), 2);
                }
                else
                {
                    Match match = regex.Match(line);
                    currentData.Mems.Add(new Mem
                    {
                        ID = long.Parse(match.Groups[1].Value),
                        Value = long.Parse(match.Groups[2].Value),
                    });
                }
            }
            yield return currentData;
        }

        static Regex regex = new Regex(@"mem\[(\d+)\] = (\d+)");
        public class Data
        {
            public long Mask1 { get; set; }

            public long Mask0 { get; set; }

            public List<Mem> Mems { get; set; } = new List<Mem>();
        }

        public class Mem
        {
            public long ID { get; set; }
            public long Value { get; set; }
        }
    }
}
