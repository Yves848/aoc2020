using System.Text.RegularExpressions;
using System.Xml.Schema;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
Console.WriteLine(home);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2020\\04\\test.txt");
List<string> fields = ["byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:", "cid:"];
List<string> eyecolors = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"];



void part1()
{
  int ans = 0;
  string pattern = @"(\w+):([#|\w]+)";
  Regex re = new Regex(pattern);
  var passports = file.Split("\r\n\r\n");
  passports.ToList().ForEach(p =>
  {
    MatchCollection check = re.Matches(p);
    if (check.Count == fields.Count)
    {
      ans++;
    }
    else
    {
      if (!p.Contains("cid:") && check.Count == fields.Count - 1) ans++;
    }
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void print(string str, bool valid)
{
  if (valid)
  {
    Console.ForegroundColor = ConsoleColor.Green;
  }
  else
  {
    Console.ForegroundColor = ConsoleColor.Red;
  }
  Console.Write($"{str} ");
}

void part2()
{
  int ans = 0;
  string pattern = @"(\w+):([#|\w]+)";
  string haircolor = @"\#[\da-f]{6}";
  Regex hairs = new Regex(haircolor);
  Regex re = new Regex(pattern);
  Regex reHeight = new Regex(@"\d+");
  Regex rePid = new Regex(@"\d{9}");
  var passports = file.Split("\r\n\r\n");
  passports.ToList().ForEach(p =>
  {
    p = p.Replace("\r\n", " ");
    MatchCollection check = re.Matches(p);
    bool valid = (check.Count == fields.Count) || (!p.Contains("cid:") && check.Count == fields.Count - 1);
    if (valid)
    {
      int i = 0;
      while (valid && i < check.Count)
      {
        string key = check[i].Groups[1].Value;
        string value = check[i].Groups[2].Value;
        switch (key)
        {
          case "byr":
            {
              valid = value.Length == 4 && int.Parse(value) >= 1920 && int.Parse(value) <= 2002;
              break;
            }
          case "iyr":
            {
              valid = value.Length == 4 && int.Parse(value) >= 2010 && int.Parse(value) <= 2020;
              break;
            }
          case "eyr":
            {
              valid = value.Length == 4 && int.Parse(value) >= 2020 && int.Parse(value) <= 2030;
              break;
            }
          case "hgt":
            {
              int height = int.Parse(reHeight.Match(value).Value);
              valid = false;
              if (value.Contains("in"))
              {
                valid = height >= 59 && height <= 76;
              }
              if (value.Contains("cm"))
              {
                valid = height >= 150 && height <= 193;
              }
              break;
            }
          case "hcl":
            {
              valid = hairs.IsMatch(value);
              break;
            }
          case "ecl":
            {
              valid = eyecolors.IndexOf(value) > -1;
              break;
            }
          case "pid":
            {
              valid = rePid.IsMatch(value) && value.Length == 9;
              break;
            }
        }
        print(check[i].Value, valid);
        i++;
      }
      Console.WriteLine();
    }
    else
    {
      print(p, valid);
      Console.WriteLine();
    }
    if (valid) ans++;
  });

  Console.WriteLine($"Part 2 - Answer : {ans}");
}
/*
byr (Birth Year)
iyr (Issue Year)
eyr (Expiration Year)
hgt (Height)
hcl (Hair Color)
ecl (Eye Color)
pid (Passport ID)
cid 
*/

part1();

part2();
