using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net.Sockets;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}/git/aoc2020/20/test.txt");

var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";

var blocs = file.Split(lf + lf).ToList();
Regex reTileNum = new Regex(@"\d+");
int w = 0;
int h = 0;
Dictionary<int, (List<string>,(string,string,string,string))> tiles = [];
blocs.ForEach(bloc =>
{
  int tileNum = 0;
  List<string> tile = [];
  bloc.Split(lf).ToList().ForEach(line =>
  {
    if (reTileNum.IsMatch(line))
    {
      tileNum = int.Parse(reTileNum.Match(line).Value);
    }
    else
    {
      tile.Add(line);
    }
  });
  tiles.Add(tileNum, (tile,borders(tile)));
});

(string,string,string,string) borders(List<string> tile) {
  (string,string,string,string) b = ("","","",""); // Left, Up, Down, Right
  tile.ForEach(line => {
    b.Item1 += line.Substring(0,1);
    b.Item4 += line.Substring(line.Length-1,1);
  });
  b.Item2 = tile[0];
  b.Item3 = tile[tile.Count-1];
  return b;
}

void part1()
{
  int ans = 0;
  w = (int)Math.Sqrt(Convert.ToDouble(tiles.Count));
  tiles.ToList().ForEach(tile => {
    Console.WriteLine($"Tile {tile.Key}");
    tile.Value.Item1.ForEach(line => Console.WriteLine(line));
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

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
