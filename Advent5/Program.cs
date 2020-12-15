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
            var inputs = LoadInputs().OrderBy(i => i.Id).ToList();
            
            var missingItem = inputs.Skip(1).Zip(inputs)
                .First(z => z.First.Id != z.Second.Id + 1).Second;

            System.Console.WriteLine(missingItem.Id + 1);
            System.Console.WriteLine(inputs.Max(s => s.Id));
        }

        public static IEnumerable<Seat> LoadInputs()
        {
            using StreamReader reader = new StreamReader(@"inputs.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int row = line.Substring(0, 7).Select((c, i) => new { tr = c == 'B', i })
                    .Sum((b) => b.tr ? (int)Math.Pow(2, 7 - b.i - 1) : 0);

                int column = line.Substring(7, 3).Select((c, i) => new { tr = c == 'R', i })
                    .Sum((b) => b.tr ? (int)Math.Pow(2, 3 - b.i - 1) : 0);

                yield return new Seat { Column = column, Row = row };
            }
        }
    }

    public class Seat
    {
        public int Column { get; set; }
        public int Row { get; set; }

        public int Id => Column + Row * 8;
    }
}
