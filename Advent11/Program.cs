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
            // Test();

            // return;

            var inputs = LoadInputs(@"inputs.txt");
            List<string> previous = new List<string>();
            Print(inputs);
            System.Console.Write("start :");
            System.Console.WriteLine(inputs.Sum(i => i.Count(c => c == '#')));
            
            int i = 0;
            while(!Equal(previous, inputs))
            {
                previous = inputs;
                inputs = GetNext(inputs);
                Print(inputs);
                i++;
            }
            System.Console.WriteLine($"{i} passages");
            System.Console.Write("end : ");
            System.Console.WriteLine(inputs.Sum(i => i.Count(c => c == '#')));
        }

        static void Test()
        {
            var values = LoadInputs(@"inputsSmall.txt");
            int width = values[0].Length;
            int height = values.Count;
            
            char[,] tabInput = new char[width,height];
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tabInput[x,y] = values[y][x];
                }
            }

            System.Console.WriteLine(GetNbOccupiedSeats(tabInput, 1, 1));
        }
        static List<string> GetNext(List<string> values)
        {
            int width = values[0].Length;
            int height = values.Count;

            char[,] tabInput = new char[width,height];
            char[,] tabOutput = new char[width,height];


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tabInput[x,y] = values[y][x];
                    tabOutput[x,y] = values[y][x];
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    switch (tabInput[x,y])
                    {
                        case 'L':
                            if(GetNbOccupiedSeats(tabInput, x, y) == 0)
                            {
                                tabOutput[x,y] = '#';
                            }
                            break;
                        case '#':
                            if(GetNbOccupiedSeats(tabInput, x, y) >= 5)
                            {
                                tabOutput[x,y] = 'L';
                            }
                            break;
                        default:
                            break;
                    }
                }
            }


            List<string> next = new List<string>();

            for (int y = 0; y < height; y++)
            {
                next.Add(string.Concat(Enumerable.Range(0, width).Select(x => tabOutput[x, y])));
            }

            return next;
        }
        static int GetNbOccupiedSeats(char[,] tab, int x, int y)
        {
            int result = 0;

            if(IsOccupied(tab, x, y, x => x + 1, y => y + 1)) result++;
            if(IsOccupied(tab, x, y, x => x + 1, y => y )) result++;
            if(IsOccupied(tab, x, y, x => x + 1, y => y - 1)) result++;
            if(IsOccupied(tab, x, y, x => x, y => y + 1)) result++;
            if(IsOccupied(tab, x, y, x => x, y => y - 1)) result++;
            if(IsOccupied(tab, x, y, x => x - 1, y => y + 1)) result++;
            if(IsOccupied(tab, x, y, x => x - 1, y => y )) result++;
            if(IsOccupied(tab, x, y, x => x - 1, y => y - 1)) result++;

            return result;
        }
        static bool IsOccupied(char[,] tab, int startx, int starty, Func<int, int> xShift, Func<int, int> yShift)
        {
            int x = xShift(startx);
            int y = yShift(starty);
            while(true)
            {
                if(x < 0) return false;
                if(y < 0) return false;

                if(x >= tab.GetLength(0)) return false;
                if(y >= tab.GetLength(1)) return false;

                if(tab[x,y] == '#')
                {
                    return true;
                }
                if(tab[x,y] == 'L')
                {
                    return false;
                }
                x = xShift(x);
                y = yShift(y);
            }
        }

        static int GetNbOccupiedSeatsPart1(char[,] tab, int x, int y)
        {
            int result = 0;

            if(IsOccupiedPart1(tab, x-1, y-1)) result++;
            if(IsOccupiedPart1(tab, x, y-1)) result++;
            if(IsOccupiedPart1(tab, x+1, y-1)) result++;
            if(IsOccupiedPart1(tab, x-1, y)) result++;
            if(IsOccupiedPart1(tab, x+1, y)) result++;
            if(IsOccupiedPart1(tab, x-1, y+1)) result++;
            if(IsOccupiedPart1(tab, x, y+1)) result++;
            if(IsOccupiedPart1(tab, x+1, y+1)) result++;

            return result;
        }
        static bool IsOccupiedPart1(char[,] tab, int x, int y)
        {
            if(x < 0) return false;
            if(y < 0) return false;

            if(x >= tab.GetLength(0)) return false;
            if(y >= tab.GetLength(1)) return false;

            return tab[x,y] == '#';
        }
        static bool Equal(List<string> values1, List<string> values2)
        {
            if(values1.Count != values2.Count)
            {
                return false;
            }
            for (int i = 0; i < values1.Count; i++)
            {
                if(values1[i] != values2[i])
                {
                    return false;
                }
            }
            return true;
        }

        static void Print(List<string> values)
        {
            System.Console.WriteLine();
            foreach (var item in values)
            {
                System.Console.WriteLine(item);
            }
        }

        public static List<string> LoadInputs(string file)
        {
            using StreamReader reader = new StreamReader(file);
            List<string> inputs = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                inputs.Add(line);
            }
            return inputs;
        }
    }
}
