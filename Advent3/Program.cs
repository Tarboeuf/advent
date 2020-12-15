using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent3
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = LoadInputs();
            
            System.Console.WriteLine(
                ComputeSlope(inputs, 1, 1) *
                ComputeSlope(inputs, 3, 1) *
                ComputeSlope(inputs, 5, 1) *
                ComputeSlope(inputs, 7, 1) *
                ComputeSlope(inputs, 1, 2)
                );
        }

        static int ComputeSlope(List<List<bool>> inputs, int x_, int y_)
        {
            int x = x_;
            int width = inputs[0].Count;
            int nbTrees = 0;
            for (int y = y_; y < inputs.Count; y+=y_)
            {
                if(inputs[y][x])
                {
                    nbTrees++;
                }
                x += x_;
                x %= width;
            }
            return nbTrees;
        }

        public static Regex _regex = new Regex(@"(\d+)\-(\d+) ([a-zA-Z]): ([a-zA-Z]*)");

        public static List<List<bool>> LoadInputs()
        {
            List<List<bool>> result = new List<List<bool>>();
            using StreamReader reader = new StreamReader(@"inputs.txt");
            string line;
            while((line = reader.ReadLine()) != null)
            {
               List<bool> bools = line.Select(c => c == '#').ToList();
               result.Add(bools);
            }
            return result;
        }
    }

}
