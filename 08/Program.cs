using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines($"{home}\\git\\aoc2020\\08\\test.txt");

List<(string, int)> operations = [];
Regex reOp = new Regex(@"\w{3}");
Regex reVal = new Regex(@"[\+|\-\d]+");
file.ToList().ForEach(line =>
{
  operations.Add((reOp.Match(line).Value, int.Parse(reVal.Match(line).Value)));
});

void part1()
{
  int acc = 0;
  int i = 0;
  HashSet<int> op = [];
  while (true)
  {
    if (op.Contains(i)) break;
    string operation = operations[i].Item1;
    int value = operations[i].Item2;
    op.Add(i);
    switch (operation)
    {
      case "acc":
        {
          acc += value;
          i++;
          break;
        }
      case "nop":
        {
          i++;
          break;
        }
      case "jmp":
        {
          i += value;
          break;
        }
    }
  }

  Console.WriteLine($"Part 1 - Answer : {acc}");
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
  int acc = 0;
  int i = 0;
  int i2 = 0;


  while (true)
  {
    List<(string, int)> pmod = [];
    file.ToList().ForEach(line =>
    {
      pmod.Add((reOp.Match(line).Value, int.Parse(reVal.Match(line).Value)));
    });
    while (i < pmod.Count)
    {
      if (pmod[i].Item1 == "jmp")
      {
        pmod[i] = ("nop", pmod[i].Item2);
        i++;
        break;
      }
      else
      {
        if (pmod[i].Item1 == "nop")
        {
          pmod[i] = ("jmp", pmod[i].Item2);
          i++;
          break;
        }
      }
      i++;
    }

    int t = 0;
    i2 = 0;
    acc = 0;
    while (i2 < pmod.Count && t < 1000)
    {
      string operation = pmod[i2].Item1;
      int value = pmod[i2].Item2;

      switch (operation)
      {
        case "acc":
          {

            acc += value;
            i2++;
            break;
          }
        case "nop":
          {

            i2++;
            break;
          }
        case "jmp":
          {
            i2 += value;
            break;
          }
      }
      
      t++;
    }
    if (i2 == pmod.Count) break;
  }

  Console.WriteLine($"Part 2 - Answer : {acc}");
}

part1();

part2();
