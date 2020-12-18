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

            int hDist = 0;
            int vDist = 0;

            int hw = 10;
            int vw = 1;

            foreach (var item in inputs)
            {
                char currentdir = item[0];
                switch (currentdir)
                {
                    case 'N':
                        vw += int.Parse(item.Substring(1));
                        break;
                    case 'S':
                        vw -= int.Parse(item.Substring(1));
                        break;
                    case 'W':
                        hw -= int.Parse(item.Substring(1));
                        break;
                    case 'E':
                        hw += int.Parse(item.Substring(1));
                        break;
                    case 'R':
                        Point pointR = Rotate(new Point(hw, vw),int.Parse(item.Substring(1)));
                        hw = pointR.X;
                        vw = pointR.Y;
                        break;
                    case 'L':
                        Point pointL = Rotate(new Point(hw, vw), -int.Parse(item.Substring(1)));
                        hw = pointL.X;
                        vw = pointL.Y;
                        break;
                    case 'F':
                        hDist += hw * int.Parse(item.Substring(1));
                        vDist += vw * int.Parse(item.Substring(1));
                        break;
                    default:
                        throw new Exception();
                }
                System.Console.WriteLine($"{hDist}, {vDist} - {hw}, {vw}");
            }

            System.Console.WriteLine(Math.Abs(hDist) + Math.Abs(vDist));
        }

        class Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X {get;}
            public int Y {get;}
        }

        static Point Rotate(Point point, int r)
        {
            int toto = r/90;
            if(toto < 0)
            {
                toto = 4 + toto;
            }
            for (int i = 0; i < toto; i++)
            {
                point = Rotate(point);
            }
            return point;
        }
        
        static Point Rotate(Point point)
        {
            return new Point(point.Y, -point.X);
        }

        static char GetNextDir(int r, char dir)
        {
            int toto = r/90;
            if(toto < 0)
            {
                toto = 4 + toto;
            }
            for (int i = 0; i < toto; i++)
            {
                dir = GetNextDir(dir);
            }
            return dir;
        }
        
        static char GetNextDir(char dir)
        {
            return dir switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => ' ',
            };
        }

        static void Test()
        {
            var values = LoadInputs(@"inputsSmall.txt");

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
