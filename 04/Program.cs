using System.Text.RegularExpressions;

var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText(@"C:\Users\yvesg\git\aoc2020\04\test.txt");
List<string> fields = new List<string> {"byr:","iyr:","eyr:","hgt:","hcl:","ecl:","pid:","cid:"};
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


void part1()
{
  int ans = 0;
  string pattern = @"(\w+):([#|\w]+)";
  Regex re = new Regex(pattern);
  var passports = file.Split("\r\n\r\n");
  passports.ToList().ForEach(p => {
    MatchCollection check = re.Matches(p);
    if (check.Count == fields.Count) {
      ans++;
    } else {
      if (!p.Contains("cid:") && check.Count == fields.Count-1) ans++;
    }
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  string pattern = @"(\w+):([#|\w]+)";
  Regex re = new Regex(pattern);
  var passports = file.Split("\r\n\r\n");
  passports.ToList().ForEach(p => {
    p = p.Replace("\r\n"," ");
    MatchCollection check = re.Matches(p);
    bool valid = (check.Count == fields.Count) || (!p.Contains("cid:") && check.Count == fields.Count-1);
    if (valid) {
      int i = 0;
      while (valid && i < check.Count) {

        i++;
      }
    }
  });

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
