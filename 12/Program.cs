/*
  N
W   E
  S

*/
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines($"{home}\\git\\aoc2020\\12\\data.txt");
List<string> directions = [.. file];
Dictionary<string,(int,int)> actions = new Dictionary<string, (int,int)>();
actions.Add("N",(-1,0));
actions.Add("E",(0,1));
actions.Add("S",(1,0));
actions.Add("W",(0,-1));
Regex action = new Regex(@"\w");
Regex nb = new Regex(@"\d+");
void part1()
{
  int ans = 0;
  (int,int) start = (0,0);
  var (r,c) = start;
  string facing = "E";
  directions.ToList().ForEach(dir => {
    var a = action.Match(dir).Value;
    var n = int.Parse(nb.Match(dir).Value);
    var (rr,cc) = (0,0);
    switch (a) {
      case "F": {
        (rr,cc) = actions[facing];
        rr *= n;
        cc *= n;
        break;
      }
      case "R": {
        int deg = n / 90;
        int i = actions.Keys.ToList().IndexOf(facing);
        i += deg;
        if (i >= actions.Count) {
          i %= actions.Count;
        }
        facing = actions.Keys.ToList()[i];
        break;
      }
      case "L": {
        int deg = n / 90;
        int i = actions.Keys.ToList().IndexOf(facing);
        i -= deg;
        if (i < 0) {
          i = actions.Count - Math.Abs(i % actions.Count);
        }
        facing = actions.Keys.ToList()[i];
        break;
      }
      case "N":
      case "S":
      case "E":
      case "W": {
        (rr,cc) = actions[a];
        rr *= n;
        cc *= n;
        break;
      }
    }
    r += rr;
    c += cc;
    Console.WriteLine($"action {r} nb {c}");
  });
  ans = Math.Abs(r)+Math.Abs(c);
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
