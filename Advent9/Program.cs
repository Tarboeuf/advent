using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = LoadInputs().ToArray();
            GetPart2(inputs);
        }

        private static void GetPart2(long[] inputs)
        {
            long error = 2089807806;
            //long error = 127;
            for (int i = 0; i < inputs.Length; i++)
            {
                long sum = 0;
                for (int k = i; k < inputs.Length; k++)
                {
                    sum += inputs[k];
                    if (sum == error)
                    {
                        var usedValues = inputs.Skip(i).Take(k - i).ToList();
                        System.Console.WriteLine(usedValues.Min() + usedValues.Max());
                        return;
                    }
                    if (sum > error)
                    {
                        break;
                    }
                }
            }
        }

        static void GetError(long[] inputs)
        {
            int preambleSize = 25;
            var preamble = new Queue<long>(inputs.Take(preambleSize));
            foreach (var item in inputs.Skip(preambleSize))
            {
                if (!IsValid(preamble, item))
                {
                    System.Console.WriteLine(item);
                    return;
                }
                preamble.Dequeue();
                preamble.Enqueue(item);
            }
        }

        static bool IsValid(IEnumerable<long> preamble, long value)
        {
            foreach (var item in preamble)
            {
                if(preamble.Contains(value - item))
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<long> LoadInputs()
        {
            using StreamReader reader = new StreamReader(@"inputs.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return long.Parse(line);
            }
        }
    }
}
