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
            var inputs = LoadInputs().OrderBy(i => i).ToArray();

            Check(GetDisctinctArrangements(1,2,3,4), 7);
            Check(GetDisctinctArrangements(3,4,5), 2);
            Check(GetDisctinctArrangements(3,4,5,6,9), 4);
            Check(GetDisctinctArrangements(1,2,3), 4);
            Check(GetDisctinctArrangements(1,2,4), 3);
            Check(GetDisctinctArrangements(1,4,6,7,8) , 3);

            long nbArrangements = GetDisctinctArrangements(inputs);
            
            System.Console.WriteLine(nbArrangements);
        }

        static void Check(long value, long expected)
        {
            if(value != expected)
            {
                throw new Exception(value.ToString());
            }
        }

        static IEnumerable<int?> GetValues(int nbValues)
        {
            for (int i = 0; i < nbValues; i++)
            {
                yield return null;
            }
        }
        public static long GetDisctinctArrangements(params int[] inputs)
        {
            var inputsEnum = Enumerable.Repeat(0, 1).Concat(inputs);
            var diffs = inputsEnum.Zip(inputsEnum.Skip(1))
                .Select(i => (i.Second - i.First)).ToArray();
            
            List<Diff> groups = new List<Diff>();
            
            for (int i = 0; i < diffs.Length; i++)
            {
                int current = diffs[i];
                int occurence = 1;
                int surroundByTwo = 0;
                if(i != 0 && diffs[i-1] == 2)
                {
                    surroundByTwo++;
                }
                for (int j = i+1; j < diffs.Length; j++)
                {
                    if(diffs[j] != current)
                    {
                        break;
                    }
                    occurence++;
                    i = j;
                }
                if(i < diffs.Length - 1 && diffs[i+1] == 2)
                {
                    surroundByTwo++;
                }
                groups.Add(new Diff{Occurence = occurence, Value = current, SurroundByTwo = surroundByTwo});
            }

            return groups.Select(g => g.Compute()).Aggregate((d1, d2) => d1 * d2);
        }

        public class Diff
        {
            public int Occurence { get; set; }
            public int Value { get; set; }
            public int SurroundByTwo { get; set; }

            public long Compute()
            {
                if(Value != 1)
                {
                    return 1;
                }
                if(Occurence == 1)
                {
                    return 1 + SurroundByTwo; 
                }
                if(Occurence == 2)
                {
                    return 1 + 1 + SurroundByTwo;
                }
                return 1 + SurroundByTwo + 3 * (long)Math.Pow(2, Occurence - 3);
            }
        }


        public static int GetDiff(IEnumerable<int> inputs)
        {
            int sum1 = 0;
            int sum2 = 0;
            int sum3 = 1;
            switch (inputs.First())
                {
                    case 1: 
                        sum1++;
                        break;
                    case 2: 
                        sum2++;
                        break;
                    case 3: 
                        sum3++;
                        break;
                    default:
                        throw new Exception("poiul");
                }
            foreach (var item in inputs.Skip(1).Zip(inputs))
            {
                int diff = item.First - item.Second;
                switch (diff)
                {
                    case 1: 
                        sum1++;
                        break;
                    case 2: 
                        sum2++;
                        break;
                    case 3: 
                        sum3++;
                        break;
                    default:
                        throw new Exception(diff.ToString());
                }
            }
            return sum1 * sum3;
        }

        public static IEnumerable<int> LoadInputs()
        {
            using StreamReader reader = new StreamReader(@"inputs.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return int.Parse(line);
            }
        }
    }
}
