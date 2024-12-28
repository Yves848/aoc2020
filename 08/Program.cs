using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines($"{home}\\git\\aoc2020\\08\\test.txt");

List<(string,int)> operations = [];
Regex reOp = new Regex(@"\w{3}");
Regex reVal = new Regex(@"[\+|\-\d]+");
file.ToList().ForEach(line => {
  operations.Add((reOp.Match(line).Value,int.Parse(reVal.Match(line).Value)));
});

void part1()
{
  int acc = 0;
  int i = 0;
  HashSet<int> op = [];
  while(true) {
    if (op.Contains(i)) break;
    string operation = operations[i].Item1;
    int value = operations[i].Item2;
    op.Add(i);
    switch (operation){
      case "acc": {
        acc += value;
        i++;
        break;
      }
      case "nop": {
        i++;
        break;
      }
      case "jmp": {
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
  int ans = 0;

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
