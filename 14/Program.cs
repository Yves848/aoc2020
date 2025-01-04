using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2020\\14\\test.txt").ToList();
Regex reMask = new Regex(@"[01X]{36}");
Regex reMem = new Regex(@"\[(\d+)\] = (\d+)");

void part1()
{
  Dictionary<int,long> mem = [];
  long ans = 0;
  string mask = "";
  file.ToList().ForEach(l => {
    if (reMask.IsMatch(l)) {
      mask = reMask.Match(l).Value;
    } else {
      MatchCollection parts = reMem.Matches(l);
      int m = int.Parse(parts[0].Groups[1].Value);
      string value = Convert.ToString(long.Parse(parts[0].Groups[2].Value),2).PadLeft(36,'0');
      for (int i = 0; i < value.Length; i++) {
        if (mask[i] != 'X') {
          value = value.Remove(i,1).Insert(i,mask[i].ToString());
        }
      }
      if (mem.ContainsKey(m)) {
        mem[m] = Convert.ToInt64(value,2);
      } else {
        mem.Add(m,Convert.ToInt64(value,2));
      }
      Console.WriteLine($"{mem} {value}"); 
    }
  });
  mem.ToList().ForEach(p => ans += p.Value);
  Console.WriteLine($"Part 1 - Answer : {ans}");
}


void part2()
{
  int ans = 0;

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
