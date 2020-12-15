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
            
            System.Console.WriteLine(inputs.Count(IsPassordValidPart2));
        }

        static bool IsPassordValid(Data data)
        {
            return data.BirthYear != null &&
                data.IssueYear != null &&
                data.ExpirationYear != null &&
                data.Height != null &&
                data.HairColor != null &&
                data.EyeColor != null &&
                data.PassportId != null;
        }
        
        static bool IsPassordValidPart2(Data data)
        {
            if(!IsPassordValid(data))
            {
                return false;
            }
            return data.BirthYear >= 1920 && data.BirthYear <= 2002 &&
                data.IssueYear >= 2010 && data.IssueYear <= 2020 &&
                data.ExpirationYear >= 2020 && data.ExpirationYear <= 2030 &&
                IsHeightValid(data.Height) &&
                IsColorValid(data.HairColor) &&
                IsEyeColorValid(data.EyeColor) &&
                data.PassportId.Length == 9;
        }

        static bool IsEyeColorValid(string color)
        {
            return availableEyeColors.Contains(color);
        }
        
        static HashSet<string> availableEyeColors = new HashSet<string>
            {"amb", "blu" ,"brn" ,"gry" ,"grn" ,"hzl" ,"oth" };
        static HashSet<char> availableChars = new HashSet<char>
            {'1', '2', '3', '4', '5', '6', '7', '8',
             '9', 'a', 'b', 'c', 'd', 'e', 'f', '0', };
        static bool IsColorValid(string color)
        {
            if(color.Length != 7)
            {
                return false;
            }
            
            return color[0] == '#' && color.Skip(1).All(availableChars.Contains);
        }
        static bool IsHeightValid(string height)
        {
            if(height.EndsWith("in"))
            {
                if(!int.TryParse(height.Substring(0, height.Length - 2), out int tall))
                {
                    return false;
                }
                return tall >= 59 && tall <= 76;
            }
            if(height.EndsWith("cm"))
            {
                if(!int.TryParse(height.Substring(0, height.Length - 2), out int tall))
                {
                    return false;
                }
                return tall >= 150 && tall <= 193;
            }
            return false;
        }

        //public static Regex _regex = new Regex(@"#[]");

        public static IEnumerable<Data> LoadInputs()
        {
            using StreamReader reader = new StreamReader(@"inputs.txt");
            string line;
            Data data = new Data();
            while((line = reader.ReadLine()) != null)
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    yield return data;
                    data = new Data();
                }
                var strs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach(var str in strs)
                {
                    var keyValue = str.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    switch(keyValue[0])
                    {
                        case "byr":
                            data.BirthYear = int.Parse(keyValue[1]);
                            break;
                        case "iyr":
                            data.IssueYear = int.Parse(keyValue[1]);
                            break;
                        case "eyr":
                            data.ExpirationYear = int.Parse(keyValue[1]);
                            break;
                        case "hgt":
                            data.Height = keyValue[1];
                            break;
                        case "hcl":
                            data.HairColor = keyValue[1];
                            break;
                        case "ecl":
                            data.EyeColor = keyValue[1];
                            break;
                        case "pid":
                            data.PassportId = keyValue[1];
                            break;
                        case "cid":
                            data.CountryId = int.Parse(keyValue[1]);
                            break;
                    }
                }
            }
            yield return data;
        }
    }

    public class Data
    {
        public int? BirthYear { get; set; }
        public int? IssueYear{get;set;}
        public int? ExpirationYear{get;set;}
        public string Height{get;set;}
        public string HairColor{get;set;}
        public string EyeColor{get;set;}
        public string PassportId{get;set;}
        public int? CountryId{get;set;}
    }
}
