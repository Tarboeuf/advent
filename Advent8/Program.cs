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
            var inputs = LoadInputs().ToArray();
            int pointer = 0;
            int maxPointer = inputs.Length - 1;
            int nbModifications = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                string path = i + " - ";
                bool isModified = true;
                while(!inputs[pointer].IsExecuted)
                {
                    bool change = i == pointer;
                    if(change && inputs[pointer].Name == "acc")
                    {
                        isModified = false;
                    }
                    // if(change)
                    // {
                    //     System.Console.WriteLine($"change {pointer} - {inputs[pointer].Name}");
                    // }
                    path += " " + pointer;
                    pointer = GetNextPointer(inputs, pointer, maxPointer, change);
                    if(pointer >= maxPointer)
                    {
                        break;
                    }
                }
                if(isModified)
                {
                    nbModifications++;
                }
                if(isModified)
                {
                    System.Console.WriteLine(path);
                }
                if(pointer >= maxPointer)
                {
                    path += " " + pointer;
                    pointer = GetNextPointer(inputs, pointer, maxPointer, false);
                    System.Console.WriteLine($"accu {accu}");
                    return;
                }
                foreach (var item in inputs)
                {
                    item.IsExecuted = false;
                }
                accu = 0;
                pointer = 0;
            }
            System.Console.WriteLine($"modifications {nbModifications}");
        }

        static int accu = 0;
        
        static int GetNextPointer(Instruction[] instructions, int pointer, int maxPointer, bool change)
        {
            var instruction = instructions[pointer];
            instruction.IsExecuted = true;
            switch(instruction.Name)
            {
                case "nop": 
                    if(change)
                    {
                        return pointer + instruction.Value;
                    }
                    return pointer + 1;
                case "jmp": 
                    if(change)
                    {
                        return pointer + 1;
                    }
                    return pointer + instruction.Value;
                case "acc": 
                    accu += instruction.Value;
                    return pointer + 1;
            }
            throw new NotImplementedException("");
        }

        static Regex _regexBag = new Regex(@"");

        public static IEnumerable<Instruction> LoadInputs()
        {
            using StreamReader reader = new StreamReader(@"inputs.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var strs = line.Split(' ');
                yield return new Instruction
                {
                    Name = strs[0],
                    Value = int.Parse(strs[1]),
                };
            }
        }

        public class Instruction
        {
            public int Value { get; set; }
            public string Name { get; set; }
            public bool IsExecuted { get; set; }
        }
    }
}
