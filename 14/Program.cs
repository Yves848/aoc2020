using System.Collections.Specialized;
using System.IO.Pipelines;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2020\\14\\test.txt").ToList();
Regex reMask = new Regex(@"[01X]{36}");
Regex reMem = new Regex(@"\[(\d+)\] = (\d+)");
HashSet<long> adr = [];
void part1()
{
  Dictionary<int, long> mem = [];
  long ans = 0;
  string mask = "";
  file.ToList().ForEach(l =>
  {
    if (reMask.IsMatch(l))
    {
      mask = reMask.Match(l).Value;
    }
    else
    {
      MatchCollection parts = reMem.Matches(l);
      int m = int.Parse(parts[0].Groups[1].Value);
      string value = Convert.ToString(long.Parse(parts[0].Groups[2].Value), 2).PadLeft(36, '0');
      for (int i = 0; i < value.Length; i++)
      {
        if (mask[i] != 'X')
        {
          value = value.Remove(i, 1).Insert(i, mask[i].ToString());
        }
      }
      if (mem.ContainsKey(m))
      {
        mem[m] = Convert.ToInt64(value, 2);
      }
      else
      {
        mem.Add(m, Convert.ToInt64(value, 2));
      }
      Console.WriteLine($"{mem} {value}");
    }
  });
  mem.ToList().ForEach(p => ans += p.Value);
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

string flip(string bits, string mask, int index)
{
  // if (index == -1) return bits;
  HashSet<string> result = [];
  List<string> bit = ["0", "1"];
  bit.ToList().ForEach(b =>
  {
    bits = bits.Remove(index, 1).Insert(index, b);
    if (mask.IndexOf('X', index + 1) > -1)
      flip(bits, mask, mask.IndexOf('X', index + 1));
    if (!bits.Contains('X')) {
      // Console.WriteLine(bits);
      adr.Add(Convert.ToInt64(bits,2));
    }
  });
  return bits;
}

void part2()
{
  // int ans = 0;
  // string test = "00000000000000000000000000000001X0XX";
  // string mask = "00000000000000000000000000000001X0XX";
  // List<long> mem = [];
  // string result = flip(test, mask, mask.IndexOf('X'));
  // mem.ToList().ForEach(Console.WriteLine);
  Dictionary<long, long> mem = [];
  long ans = 0;
  string mask = "";
  file.ToList().ForEach(l =>
  {
    if (reMask.IsMatch(l))
    {
      mask = reMask.Match(l).Value;
    }
    else
    {
      MatchCollection parts = reMem.Matches(l);
      int value = int.Parse(parts[0].Groups[2].Value);
      string address = Convert.ToString(long.Parse(parts[0].Groups[1].Value), 2).PadLeft(36, '0');
      for (int i = 0; i < mask.Length; i++) {
        if ("X1".Contains(mask[i].ToString())) {
          address = address.Remove(i,1).Insert(i,mask[i].ToString());
        }
      }
      adr.Clear();
      address = flip(address,address,address.IndexOf('X'));
      adr.ToList().ForEach(m =>
      {
        if (mem.ContainsKey(m)) {
          mem[m] = value;
        } else {
          mem.Add(m,value);
        }
      });
      // Console.WriteLine($"{mem} {value}");
    }
  });
  mem.ToList().ForEach(p => ans += p.Value);
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
