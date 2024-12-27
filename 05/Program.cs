using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2020\\05\\test.txt");
List<int> seats = [];
void part1()
{
  int ans = int.MinValue;
  file.Split("\r\n").ToList().ForEach(line =>
  {
    (int, int) rows = (0, 127);
    (int, int) cols = (0, 7);
    string row = line.Substring(0, 7);
    string col = line.Substring(7);
    row.ToList().ForEach(r =>
    {
      var t = (rows.Item2 - rows.Item1 + 1) / 2;
      switch (r)
      {
        case 'F':
          {
            rows.Item2 -= t;
            break;
          }
        case 'B':
          {
            rows.Item1 += t;
            break;
          }
      }
    });
    col.ToList().ForEach(c =>
    {
      var t = (cols.Item2 - cols.Item1 + 1) / 2;
      switch (c)
      {
        case 'L':
          {
            cols.Item2 -= t;
            break;
          }
        case 'R':
          {
            cols.Item1 += t;
            break;
          }
      }
    });
    int seat = rows.Item1 *8 + cols.Item1;
    seats.Add(seat);
    if (seat > ans) ans = seat;
    Console.WriteLine($"{rows.Item1} {cols.Item1}");
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
  seats.Sort();
}

#pragma warning disable CS8321 // Local function is declared but never used
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
#pragma warning restore CS8321 // Local function is declared but never used

void part2()
{
  int ans = 0;
  int i = 0;
  while (i < seats.Count-1) {
    if (seats[i] == seats[i+1]-2) {
      ans = seats[i]+1;
      break;
    }
    i++;
  }
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
