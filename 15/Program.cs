using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2020\\15\\test.txt");
var numbers = file.Split(',').ToList().Select(p => int.Parse(p)).ToList();

void part1()
{
  int ans = 0;
  Dictionary<int, List<int>> turns = [];
  HashSet<int> spoken = [];
  int turn = 1;
  int oldNumber = 0;
  int count = 1;
  while (turn <= 2020)
  {
    ans = oldNumber;
    if (turn <= numbers.Count)
    {
      turns.Add(numbers[turn - 1], [turn]);
      oldNumber = numbers[turn - 1];
    }
    else
    {
      if (spoken.Contains(oldNumber))
      {
        var tu = turns[oldNumber];
        if (count == 2)
        {
          oldNumber = 1;
        }
        else
        {
          if (tu.Count == 1)
          {
            if (turn == tu[0] + 1)
            {
              oldNumber = 0;
            }
            else
              oldNumber = turn - tu[0];
          }
          else
          {
            oldNumber = tu[1] - tu[0];
          }
        }
        if (turns.ContainsKey(oldNumber))
        {
          tu = turns[oldNumber];
          if (tu.Count == 2) tu.RemoveAt(0);
          tu.Add(turn);
        }
        else
        {
          turns.Add(oldNumber, [turn]);
        }
      }
      else
      {
        if (!turns.ContainsKey(oldNumber))
          turns.Add(oldNumber, [turn]);
        oldNumber = 0;
        if (!turns.ContainsKey(oldNumber))
          turns.Add(oldNumber, [turn]);
        var tu = turns[oldNumber];
        if (tu.Count == 2) tu.RemoveAt(0);
        tu.Add(turn);
      }
      spoken.Add(oldNumber);
    }
    turn++;
    // Console.WriteLine(oldNumber);
    if (ans == oldNumber) count++; else count = 1;
  }
  Console.WriteLine($"Part 1 - Answer : {oldNumber}");
}


void part2()
{
  Dictionary<int,int> last_index = [];
  foreach(var (i,n) in numbers.Select((i,n) => (n,i)))
  {
    last_index.Add(n,i);
  }
  int next_= 0;
  while (numbers.Count < 30000000) {
    int prev = numbers.Last();
    int prev_prev = -1;
    if (last_index.ContainsKey(prev)) prev_prev = last_index[prev];
    last_index[prev] = numbers.Count -1;
    if (prev_prev == -1) {
      next_ = 0;
    }
    else {
      next_ = numbers.Count - 1 - prev_prev;
    }
    numbers.Add(next_);
  }
  Console.WriteLine(numbers.Last());
}

part1();

part2();
