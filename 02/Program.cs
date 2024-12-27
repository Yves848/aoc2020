using System.ComponentModel;
using System.Text.RegularExpressions;

var file = (args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines(@"C:\Users\yvesg\git\aoc2020\02\test.txt"));


void part1()
{
  int ans = 0;
  file.ToList().ForEach(l =>
  {
    var parts = l.Split(':');
    var criteres = parts[0];
    string psw = parts[1].ToString().Trim();
    string nums = @"\d+";
    Regex bornes = new Regex(nums);
    MatchCollection b = bornes.Matches(criteres);
    int b1 = int.Parse(b[0].Value);
    int b2 = int.Parse(b[1].Value);
    char car = criteres[criteres.Length - 1];
    int nb = psw.Count(c => c == car);
    if (nb >= b1 && nb <= b2) ans++;
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  file.ToList().ForEach(l =>
    {
      var parts = l.Split(':');
      var criteres = parts[0];
      string psw = parts[1].ToString().Trim();
      string nums = @"\d+";
      Regex bornes = new Regex(nums);
      MatchCollection b = bornes.Matches(criteres);
      int b1 = int.Parse(b[0].Value);
      int b2 = int.Parse(b[1].Value);
      char car = criteres[criteres.Length - 1];
      if (psw[b1-1] == car ^ psw[b2-1] == car) ans++;
    });
  Console.WriteLine($"Part 2- Answer : {ans}");
}

part1();
part2();