using System.ComponentModel;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;



string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}/git/aoc2020/19/test.txt");
var blocs = file.Split("\r\n\r\n");
Dictionary<int, (string,string)> rules = [];
Dictionary<int, string> letter = [];
Regex reLine = new(@"\d+");
Regex reLetter = new(@"""(a|b)""");

List<(int,string)> list = [];
void part1()
{
  int ans = 0;
  // blocs[0].Split("\r\n").ToList().ForEach(l =>
  // {
  //   List<string> p = [.. l.Split(":")];
  //   int num = int.Parse(p[0]);
  //   if (!reLine.IsMatch(p[1]))
  //   {
  //     items.Add(num, new Letter(p[1]));
  //   }
  //   else
  //   {
  //     items.Add(num, new Rule(p[1]));
  //   }
  // });
  blocs[0].Split("\r\n").ToList().ForEach(l =>
  {
    List<string> p = [.. @l.Split(":")];
    int num = int.Parse(p[0]);
    string right = @p[1].Trim();//.Replace("\"","");
    if (reLetter.IsMatch(right))
    {
      letter.Add(num,reLetter.Match(l).Groups[1].Value);
    }
    else
    {
      list.Add((num,p[1]));
    }
  });

  int i = 0;
  
  

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


public class Item
{

  public int t { get; set; }
}

public class Letter : Item
{
  public string l { get; set; }

  public Letter(string letter)
  {
    t = 0;
    l = letter.Replace("\"", "");
  }
}

public class Rule : Item
{
  public List<List<int>> rules { get; set; } = [];
  public Rule(string rule)
  {
    t = 1;
    makeRules(rule);
  }

  private void makeRules(string rule)
  {
    var parts = rule.Split(" | ");
    parts.ToList().ForEach(r =>
    {
      rules.Add(r.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList().Select(p => int.Parse(p)).ToList());
    });
  }
}