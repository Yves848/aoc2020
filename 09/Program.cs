using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines($"{home}\\git\\aoc2020\\09\\test.txt");
List<long> nums = file.ToList().Select(p => long.Parse(p)).ToList();
var preamble = 25;
long ans1 = 0;
void part1()
{
  long ans = 0;
  int i = preamble;
  while (i < nums.Count)
  {
    List<long> slice = nums.GetRange(i - preamble, preamble);
    int i2 = 0;
    bool eq = false;
    while (i2 < slice.Count)
    {
      // long diff = nums[i] - slice[i2];
      long target = nums[i];
      long first = slice[i2];
      int i3 = 0;
      while (i3 < slice.Count)
      {
        long next = slice[i3];
        if (first != next)
        {
          if (first + next == target)
          {
            eq = true;
            i2 = slice.Count;
            break;
          }
        }
        i3++;
      }
      i2++;
    }
    if (!eq)
    {
      ans = nums[i];
      break;
    }
    i++;
  }
  ans1 = ans;
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
  long ans = 0;
  int i = 0;
  List<long> set = [];
  while (i < nums.Count)
  {
    int i2 = i;
    set.Clear();
    ans = 0;
    while (i2 < nums.Count)
    {
      set.Add(nums[i2]);
      ans += nums[i2];
      if (ans >= ans1)
      {
        if (ans == ans1)
        {
          i = nums.Count;
        }
        break;
      }
      i2++;
    }
    i++;
  }
  ans = set.ToList().Min() + set.ToList().Max();
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
