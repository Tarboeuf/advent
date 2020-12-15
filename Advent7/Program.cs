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

            System.Console.WriteLine(GetMaxBag(inputs["shiny gold"]) - 1);
            //System.Console.WriteLine(GetContainer(inputs["shiny gold"]).Distinct().Count());
        }

        public static IEnumerable<Bag> GetContainer(Bag bag)
        {
            foreach(var container in bag.ContainerBags)
            {
                yield return container;
                foreach(var subContainer in GetContainer(container))
                {
                    yield return subContainer;
                }
            }
        }
        
        public static int GetMaxBag(Bag bag)
        {
            int nb = 1;
            foreach(var container in bag.ContainedBags)
            {
                nb += GetMaxBag(container.Item1) * container.Item2;
            }
            return nb;
        }

        static Regex _regexBag = new Regex(@"(?<bag>[a-z ]+) bags? contain (?<firstGroup>(?<firstNumber>\d+) (?<firstValue>[a-z ]+) bags?(?<secondGroup>, (?<secondNumber>\d+) (?<secondValue>[a-z ]+) bags?)*)");

        public static Dictionary<string, Bag> LoadInputs()
        {
            using StreamReader reader = new StreamReader(@"inputs.txt");
            Dictionary<string, Bag> result = new Dictionary<string, Bag>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var match = _regexBag.Match(line);
                string color = match.Groups["bag"].Value;
                Bag parent = null;
                if(result.ContainsKey(color))
                {
                    parent = result[color];
                }
                else
                {
                    parent = new Bag(color);
                    result.Add(color, parent);
                }
                if(match.Groups["firstValue"].Success)
                {
                    string value = match.Groups["firstValue"].Value;
                    if(!result.ContainsKey(value))
                    {
                        result.Add(value, new Bag(value));
                    }
                    
                    parent.ContainedBags.Add(Tuple.Create(result[value], int.Parse(match.Groups["firstNumber"].Value)));
                    result[value].ContainerBags.Add(parent);
                }
                
                if(match.Groups["secondValue"].Success)
                {
                    int i = 0;
                    foreach(Capture capture in match.Groups["secondValue"].Captures)
                    {
                        string value = capture.Value;
                        if(!result.ContainsKey(value))
                        {
                            result.Add(value, new Bag(value));
                        }
                        parent.ContainedBags.Add(Tuple.Create(result[value], int.Parse(match.Groups["secondNumber"].Captures[i++].Value)));
                        result[value].ContainerBags.Add(parent);
                    }
                }
            }
            return result;
        }

        public class Bag
        {
            public Bag(string color)
            {
                Color = color;
            }

            public string Color { get; set; }

            public List<Tuple<Bag, int>> ContainedBags { get; } = new List<Tuple<Bag, int>>();
            
            public List<Bag> ContainerBags { get; } = new List<Bag>();
        }
    }
}
