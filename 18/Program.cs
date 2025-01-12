using System.ComponentModel;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2020/18/test.txt").ToList();

long compute(string line, char[] rule)
{
  var l = Regex.Replace(line, @"\s+", " ").Trim().Split(' ').ToList();
  long ans = 0;
  rule.ToList().ForEach(r =>
  {
    int i = 0;
    while (i < l.Count-1)
    {
      if (l[i] == r.ToString())
      {
        long op1 = long.Parse(l[i - 1]);
        long op2 = long.Parse(l[i + 1]);
        if (r == '+')
        { 
          l[i] =  (op1 + op2).ToString();
          l.RemoveAt(i+1);
          l.RemoveAt(i-1);
          i = 0;
        } else {
          l[i] =  (op1 * op2).ToString();
          l.RemoveAt(i+1);
          l.RemoveAt(i-1);
          i = 0;
        }
      }
      i++;
    }
  });
  ans = long.Parse(l[0]);
  return ans;
}


long compute2(string line, char[] rule)
{
  var l = Regex.Replace(line, @"\s+", " ").Trim().Split(' ').ToList();
  long ans = int.Parse(l[0]);
  int i = 1;
  while (i < l.Count - 1)
  {
    if (l[i] == "+")
    {
      ans += long.Parse(l[i + 1]);
    }
    else
    {
      ans *= long.Parse(l[i + 1]);
    }
    i += 2;
  }
  return ans;
}

long resolve(string l, char[] rule)
{
  Regex p = new Regex(@"[\(\|\)]");
  Stack<int> Q = [];
  MatchCollection m = p.Matches(l);
  if (m.Count > 0)
  {
    int i = 0;
    Q.Push(m[i].Index);
    i++;
    while (i < m.Count)
    {
      if (m[i].Value == "(")
      {
        Q.Push(m[i].Index);
      }
      else
      {
        int i1 = Q.Pop();
        int nb = m[i].Index - i1;
        string temp = l.Substring(i1 + 1, nb - 1);
        long ans = compute(temp, rule);
        l = l.Remove(i1, nb + 1).Insert(i1, ans.ToString().PadRight(nb + 1));
        // Console.WriteLine($"  temp : {temp}");
        // Console.WriteLine($"l : {l}");

      }
      i++;
    }
  }
  return compute(l, rule);
}

void part1()
{
  long ans = 0;
  file.ForEach(l =>
  {
    // Console.ForegroundColor = ConsoleColor.DarkRed;
    // Console.WriteLine($"T : {l}");
    // Console.ForegroundColor = ConsoleColor.White;
    long r = resolve(l, ['*', '+']);
    // Console.ForegroundColor = ConsoleColor.Green;
    // Console.WriteLine($"Total : {r}");
    // Console.ForegroundColor = ConsoleColor.White;
    ans += r;
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}


void part2()
{
  long ans = 0;
  file.ForEach(l =>
  {
    // Console.ForegroundColor = ConsoleColor.DarkRed;
    // Console.WriteLine($"T : {l}");
    // Console.ForegroundColor = ConsoleColor.White;
    long r = resolve(l, ['+', '*']);
    // Console.ForegroundColor = ConsoleColor.Green;
    // Console.WriteLine($"Total : {r}");
    // Console.ForegroundColor = ConsoleColor.White;
    ans += r;
  });
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
