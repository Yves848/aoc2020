using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2020\\13\\test.txt").ToList();
int early = int.Parse(file[0]);
List<long> ids = [];
file[1].Split(",").ToList().ForEach(p =>
{
  long i = 0;
  if (long.TryParse(p, out i))
  {
    ids.Add(i);
  }
});

void part1()
{
  long ans = 0;
  long start = long.MaxValue;
  ids.ToList().ForEach(id =>
  {
    long rem = early / id;
    if ((early-rem) < start) { 
      start = (early-rem);
    }
    Console.WriteLine(id);
  });
  Console.WriteLine($"start {start}");
  // determine lower ts to start.
  long t = 0;
  long ts = 0;
  long bus = 0;
  while (t < int.MaxValue) {
    string output = $"{start+t}\t";
    for (int i = 0; i < ids.Count();i++) {
      if ((start+t) % ids[i] == 0) {
        output += "D\t";
        if (start+t >= early) {
          ts = start + t;
          bus = ids[i];
        }
      } else {
        output += ".\t";
      }
    }
    if (ts > 0) {t = long.MaxValue-1;}
    Console.WriteLine(output);
    t++;
  }
  ans = (ts - early) * bus;
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

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
