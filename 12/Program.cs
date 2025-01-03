/*
  N
W   E
  S

*/
using System.ComponentModel;
using System.Data.Common;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines($"{home}\\git\\aoc2020\\12\\data.txt");
List<string> directions = [.. file];
Dictionary<string, (int, int)> actions = new Dictionary<string, (int, int)>
{
    { "N", (1, 0) },
    { "E", (0, 1) },
    { "S", (-1, 0) },
    { "W", (0, -1) }
};
Regex action = new Regex(@"\w");
Regex nb = new Regex(@"\d+");

void part1()
{
  int ans = 0;
  (int, int) start = (0, 0);
  var (r, c) = start;
  string facing = "E";
  directions.ToList().ForEach(dir =>
  {
    var a = action.Match(dir).Value;
    var n = int.Parse(nb.Match(dir).Value);
    var (rr, cc) = (0, 0);
    switch (a)
    {
      case "F":
        {
          (rr, cc) = actions[facing];
          rr *= n;
          cc *= n;
          break;
        }
      case "R":
        {
          int deg = n / 90;
          int i = actions.Keys.ToList().IndexOf(facing);
          i += deg;
          if (i >= actions.Count)
          {
            i %= actions.Count;
          }
          facing = actions.Keys.ToList()[i];
          break;
        }
      case "L":
        {
          int deg = n / 90;
          int i = actions.Keys.ToList().IndexOf(facing);
          i -= deg;
          if (i < 0)
          {
            i = actions.Count - Math.Abs(i % actions.Count);
          }
          facing = actions.Keys.ToList()[i];
          break;
        }
      case "N":
      case "S":
      case "E":
      case "W":
        {
          (rr, cc) = actions[a];
          rr *= n;
          cc *= n;
          break;
        }
    }
    r += rr;
    c += cc;
    // Console.WriteLine($"action {r} nb {c}");
  });
  ans = Math.Abs(r) + Math.Abs(c);
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

string pointToCoordinates((int, int) coord)
{
  string v = "";
  string h = "";
  var (y, x) = coord;
  v = y >= 0 ? "N" : "S";
  h = x >= 0 ? "E" : "W";
  return $"{y}{v} {x}{h}";
}

(string, string) pointToWP((int, int) coord)
{
  string v = "";
  string h = "";
  var (y, x) = coord;
  v = y >= 0 ? "N" : "S";
  h = x >= 0 ? "E" : "W";
  return (v, h);
}

string rotate(string p, int deg)
{
  int i = actions.Keys.ToList().IndexOf(p);
  if (deg > 0)
  {
    i += deg;
    if (i >= actions.Count)
    {
      i %= actions.Count;
    }
  }
  else
  {
    i -= (deg *-1);
    if (i < 0)
    {
      i = actions.Count - Math.Abs(i % actions.Count);
    }
  }
  return actions.Keys.ToList()[i];
}

void part2()
{
  int ans = 0;
  (int, int) ship = (0, 0);
  (int, int) wp = (1, 10); // 1 North, 10 East (relative to ship)
  Console.WriteLine($"ship {pointToCoordinates(ship)} waypoint {pointToCoordinates(wp)}");
  directions.ToList().ForEach(dir =>
  {
    var a = action.Match(dir).Value; // Action
    var n = int.Parse(nb.Match(dir).Value); // Number
    
    if (a == "N") {
      wp.Item1 += n;
    }
    if (a == "S") {
      wp.Item1 -= n;
    }
    if (a == "E") {
      wp.Item2 += n;
    }
    if (a == "W") {
      wp.Item2 -= n;
    }
    if (a == "L") {
      for (int i = 0; i < n / 90; i++) {
        var (wy,wx) = wp;
        wp.Item2 = wy*-1;
        wp.Item1 = wx;
      }
    }
    if (a == "R") {
      for (int i = 0; i < n / 90; i++) {
        var (wy,wx) = wp;
        wp.Item2 = wy;
        wp.Item1 = wx*-1;
      }
    }
    if (a == "F") {
      ship.Item1 += n*wp.Item1;
      ship.Item2 += n*wp.Item2;
    }
    Console.WriteLine($"ship {pointToCoordinates(ship)} waypoint {pointToCoordinates(wp)}");
  });
  ans = Math.Abs(ship.Item1) + Math.Abs(ship.Item2);

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
