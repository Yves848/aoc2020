string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines($"{home}\\git\\aoc2020\\10\\test.txt");

List<int> jolts = file.ToList().Select(p => int.Parse(p)).ToList();
Dictionary<int, int> diffs = [];
List<int> combinations = [];
void part1()
{
  int ans = 0;
  while (jolts.Count > 0)
  {
    List<int> adapters = jolts.ToList().Where(p => p - ans >= 1 && p - ans <= 3).ToList();
    adapters.Sort();
    int adapter = adapters[0];
    int diff = adapter - ans;
    if (!diffs.ContainsKey(diff)) {
      diffs.Add(diff,1);
    } else {
      diffs[diff] = diffs[diff]+1;
    }
    jolts.Remove(adapter);
    ans = adapter;
  }
  ans = diffs[1] * (diffs[3]+1);
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

int findAdapter(int i) {
  if (i == jolts.Count-1) return 1;
  if (diffs.ContainsKey(i)) return diffs[i];
  int ans = 0;
  int j = i + 1;
  while (j < jolts.Count) {
    if (jolts[j]-jolts[i] <=3) ans+= findAdapter(j);
    j++;
  }
  if (diffs.ContainsKey(i)) {
    diffs[i] = ans;
  } else 
    diffs.Add(i,ans);
  return ans;
}

void part2()
{
  jolts = file.ToList().Select(p => int.Parse(p)).ToList();
  jolts.Insert(0,0);
  jolts.Sort();
  jolts.Add(jolts[jolts.Count-1]+3);
  int result =  findAdapter(0);
  Console.WriteLine($"Part 2 - Answer : {result}");
}

part1();

part2();
