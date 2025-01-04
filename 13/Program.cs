using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2020\\13\\test.txt").ToList();
int early = int.Parse(file[0]);
List<(long, int)> ids = [];
List<int> incs = [];
int inc = 1;
// string buses = "17,x,13,19";
string buses = file[1];
// string buses = "1789,37,47,1889";

buses.Split(",").ToList().ForEach(p =>
{
  long i = 0;
  if (long.TryParse(p, out i))
  {
    ids.Add((i, inc));
    if (ids.Count() > 1) incs.Add(inc);
    inc = 0;
  }
  inc++;
});
incs.Add(0);
void part1()
{
  long ans = 0;
  long start = long.MaxValue;
  ids.ToList().ForEach(id =>
  {
    long rem = early / id.Item1;
    if ((early - rem) < start)
    {
      start = (early - rem);
    }
    // Console.WriteLine(id);
  });
  Console.WriteLine($"start {start}");
  // determine lower ts to start.
  long t = 0;
  long ts = 0;
  long bus = 0;
  while (t < int.MaxValue)
  {
    string output = $"{start + t}\t";
    for (int i = 0; i < ids.Count(); i++)
    {
      if ((start + t) % ids[i].Item1 == 0)
      {
        output += "D\t";

      }
      else
      {
        output += ".\t";
      }
    }
    if (ts > 0) { t = long.MaxValue - 1; }
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
  long ans = 0;
  long start = long.MaxValue;
  Console.WriteLine(file[1]);
  ids.ToList().ForEach(id =>
  {
    long rem = early / id.Item1;
    if ((early - rem) < start)
    {
      start = (early - rem);
    }
    Console.WriteLine(id);
  });
  //      100000000000000
  // start = 100046300000000;
  // start = 1068780;
  start = 100000000000000;
  Console.WriteLine($"start {start}");
  // determine lower ts to start.
  long t = start;
  long ts = 0;
  while (t < long.MaxValue)
  {
    string output = $"{t}\t";
    bool serie = true;
    int i = 0;
    long t2 = t;
    while (i < ids.Count)
    {
      // if (serie) serie = (start+t2) % ids[i].Item1 == 0;
      if (t2 % ids[i].Item1 != 0)
      // {
      //   output += "D\t";
      // }
      // else
      {
        // output += ".\t";
        serie = false;
      }
      t2 += incs[i];
      i++;
    }
    if (serie)
    {
      ts = t;
      t = long.MaxValue - 1;
    }
    if (t % 100000000 == 0 || serie)
    {
      Console.WriteLine(output);
      File.WriteAllText($"{home}\\git\\aoc2020\\13\\result.txt", output.ToString());
    }
    t++;
  }
  ans = ts;
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

// part1();

part2();
