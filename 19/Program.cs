using System.Text.RegularExpressions;



string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}/git/aoc2020/19/test.txt");
var blocs = file.Split("\n\n");
Dictionary<int,Item> items = [];


void part1()
{
  int ans = 0;
  Letter l = new Letter("a");
  items.Add(0,l);
  Console.WriteLine(blocs);
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


public class Item {
  
  protected int t {get; set;}
}

public class Letter : Item {
  protected string l{get; set;}

  public Letter(string letter){
    t = 0;
    l = letter;
  }
}

public class Rule : Item {
  
}