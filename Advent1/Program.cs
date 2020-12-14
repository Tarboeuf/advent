using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Advent1
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = LoadInputs().OrderBy(i => i).ToList();
            var inputsReverse = LoadInputs().OrderByDescending(i => i).ToList();
            
            foreach(var input in inputs)
            {
                foreach(var end in inputs)
                {
                    foreach(var third in inputs)
                    {
                        int sum = input + end + third;
                        if(sum == 2020)
                        {
                            Console.WriteLine(input * end * third);
                            return;
                        }
                        if(sum > 2020)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public static IEnumerable<int> LoadInputs()
        {
            using (StreamReader reader = new StreamReader(@"F:\Source\Advent1\inputs.txt"))
            {

                string line;
                while((line = reader.ReadLine()) != null)
                {
                    yield return int.Parse(line);
                }
            }
        }
    }
}
