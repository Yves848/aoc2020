using System.Text.RegularExpressions;

var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText(@"C:\Users\yvesg\git\aoc2020\04\test.txt");
List<string> fields = {""};

void part1()
{
  int ans = 0;
  string pattern = @"(\w+):([#|\w]+)";
  Regex re = new Regex(pattern);
  var passports = file.Split("\r\n\r\n");

  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
