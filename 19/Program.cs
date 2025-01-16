using System.ComponentModel;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;



string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}/git/aoc2020/19/test.txt");
var blocs = file.Split("\r\n\r\n");
Dictionary<int, (string, Rule)> rules = [];
Dictionary<int, Item> letter = [];
Regex reLine = new(@"\d+");
Regex reLetter = new(@"""(a|b)""");

Dictionary<int, Rule> list = [];
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
      var l2 = new Letter(reLetter.Match(l).Groups[1].Value);
      letter.Add(num, l2);
    }
    else
    {

      list.Add(num, new Rule(p[1]));
    }
  });

  var zero = list[0];
  string temp = "";
  zero.rules[0].ForEach(z => {
    temp += findRule("",z);
  });
  

  Console.WriteLine($"temp : {temp}");


  Console.WriteLine($"Part 1 - Answer : {ans}");
}

string findRule(string temp, int i) {
  if (letter.ContainsKey(i)) {
    temp += ((Letter)letter[i]).l;
  }
   else {
    var rule = ((Rule)list[i]).rules;
    rule.ForEach(r => {
      r.ForEach(ru => {
        temp += findRule(temp,ru);
      });
    });
   }
  return temp;
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
    var r = rule.Split("|", StringSplitOptions.RemoveEmptyEntries);
    r.ToList().ForEach(ru =>
    {
      rules.Add([.. ru.Split(" ",StringSplitOptions.RemoveEmptyEntries).ToList().Select(ru => int.Parse(ru))]);
    });
  }
}