using System.Collections.Immutable;
using System.IO.Pipelines;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines($"{home}\\git\\aoc2020\\07\\test.txt");
HashSet<string> valids = [];
Dictionary<string, List<(string, int)>> colors = [];
string myBag = "shiny gold";

List<(string, int)> bagContent(string content)
{
  List<(string, int)> result = [];
  content = content.Replace(" bag, ", ",");
  content = content.Replace(" bags, ", ",");
  content = content.Replace(" bags.", "");
  content = content.Replace(" bag.", "");
  Regex re = new Regex(@"(\d+)\s([\w\s]+)");
  re.Matches(content).ToList().ForEach(bag =>
  {
    result.Add((bag.Groups[2].Value, int.Parse(bag.Groups[1].Value)));
  });
  return result;
}

bool isValidBag(string color)
{
  bool result = false;
  if (valids.Contains(color)) return true;
  var content = colors[color].ToList();
  int i = 0;
  while (i < content.Count)
  {
    if (content[i].Item1 == myBag)
    {
      result = true;
    }
    else result = isValidBag(content[i].Item1);
    if (result) break;
    i++;
  }
  return result;
}

int bagsInMyBag(string color)
{
  int result = 0;
  List<(string,int)>
  return result;
}

void part1()
{
  int ans = 0;
  file.ToList().ForEach(line =>
  {
    var split = line.Split(" bags contain ");
    colors.Add(split[0], bagContent(split[1]));
  });
  colors.ToList().ForEach(color =>
  {
    if (color.Key != myBag)
    {
      if (isValidBag(color.Key))
      {
        valids.Add(color.Key);
      }
    }
  });
  ans = valids.Count;
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
