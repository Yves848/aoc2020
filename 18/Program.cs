using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2020\\18\\test.txt").ToList();

string removeP(string l) {
  int p = l.IndexOf('(');
  if (p > 0) l = removeP(l.Substring(p+1));
  
  return "";
}

long resolve(string l) {
  int p = l.IndexOf('(');
  while (p > -1) {
    removeP(l.Substring(p+1));
    p = l.IndexOf('(');
  }
  return 0;
}

void part1()
{
  long ans = 0;
  file.ForEach(l => {
    ans += resolve(l);
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}


void part2()
{
  int ans = 0;

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
