using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = LoadInputs().ToList();
            // var inputs = new List<Rule>
            // {
            //     new Rule(1,3,'a', "abcde"),
            //     new Rule(1,3,'b', "cdefg"),
            //     new Rule(2,9,'c', "ccccccccc"),
            // };
            System.Console.WriteLine(inputs.Count(IsPasswordValidPart2));
        }

        public static bool IsPasswordValid(Rule rule)
        {
            int occurence = rule.Password.Count(c => c == rule.Letter);
            return occurence >= rule.MinOccurence && occurence <= rule.MaxOccurence;
        }
        
        public static bool IsPasswordValidPart2(Rule rule)
        {
            return rule.Password[rule.MinOccurence - 1] == rule.Letter ^
                   rule.Password[rule.MaxOccurence - 1] == rule.Letter;
        }

        public static Regex _regex = new Regex(@"(\d+)\-(\d+) ([a-zA-Z]): ([a-zA-Z]*)");

        public static IEnumerable<Rule> LoadInputs()
        {
            using StreamReader reader = new StreamReader(@"inputs.txt");
            string line;
            while((line = reader.ReadLine()) != null)
            {
                var match = _regex.Match(line);
                int firstOccurence = int.Parse(match.Groups[1].Value);
                int maxOcc = int.Parse(match.Groups[2].Value);
                char letter = match.Groups[3].Value.First();
                string password = match.Groups[4].Value;
                yield return new Rule(firstOccurence, maxOcc, letter, password);
            }
        }
    }

}
