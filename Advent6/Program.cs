using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = LoadInputs();
            
            int nbTotal = 0;
            
            foreach(var input in inputs)
            {
                nbTotal += input.SelectMany(i => i)
                    .GroupBy(i => i).Where(g => g.Count() == input.Count)
                    .Count();
            }

            System.Console.WriteLine(nbTotal);
        }

        public static IEnumerable<List<string>> LoadInputs()
        {
            using StreamReader reader = new StreamReader(@"inputs.txt");
            string line;
            List<string> list = new List<string>();
            while ((line = reader.ReadLine()) != null)
            {
                if(string.IsNullOrEmpty(line))
                {
                    yield return list;
                    list = new List<string>();
                    continue;
                }
                list.Add(line);
            }
            if(list.Any())
            {
                yield return list;
            }
        }
    }
}
