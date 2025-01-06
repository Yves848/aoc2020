using System.Security.Cryptography;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2020\\16\\test2.txt");
var blocs = file.Split("\r\n\r\n");
var nearBy = blocs[2].Split("\r\n").ToList();
nearBy.RemoveAt(0);
Console.WriteLine(nearBy.Count);
Regex reRanges = new Regex(@"([\s|\w]+)\: (\d+)-(\d+) or (\d+)-(\d+)");
List<(string, (int, int))> ranges = [];
blocs[0].Split("\r\n").ToList().ForEach(r =>
{
  MatchCollection m = reRanges.Matches(r);
  ranges.Add((m[0].Groups[1].Value, (int.Parse(m[0].Groups[2].Value), int.Parse(m[0].Groups[3].Value))));
  ranges.Add((m[0].Groups[1].Value, (int.Parse(m[0].Groups[4].Value), int.Parse(m[0].Groups[5].Value))));
});
void part1()
{
  int rate = 0;
  int row = 0;
  while (row < nearBy.Count)
  {
    var r = nearBy[row];
    bool validRow = true;
    r.Split(',').ToList().ForEach(n =>
    {
      int val = int.Parse(n);
      bool valid = false;
      ranges.ToList().ForEach(range =>
      {
        var (name, (i1, i2)) = range;
        valid = valid || (val >= i1 && val <= i2);
      });
      rate += valid ? 0 : val;
      validRow = validRow && valid;
    });
    if (validRow)
    {
      row++;
    }
    else
    {
      nearBy.RemoveAt(row);
    }
  };
  Console.WriteLine($"Part 1 - Answer :");
  Console.WriteLine(rate);
  Console.WriteLine(nearBy.Count);
}


void part2()
{
  List<List<int>> na = [];
  nearBy.ToList().ForEach(n =>
  {
    na.Add(n.Split(',').ToList().Select(p => int.Parse(p)).ToList());
  });
  int c = 0;
  while (c < na.Count)
  {
    int r = 0;
    int?[] map = new int?[20];
    HashSet<String> fields = [];
    HashSet<String> fields0 = [];
    while (r < na.Count)
    {
      int range = 0;
      while(range < ranges.Count -1)
      {
        var (name, (i1, i2)) =  ranges[range];  
        var (_, (ii1, ii2)) =  ranges[range+1];  
        int value = na[r][c]; 
        if ((i1 <= value && i2 >= value ) || (ii1 <= value && ii2 >= value)) {
          if (!fields0.Contains(name)) fields.Add(name);
        } else {
          fields0.Add(name);
          // if (fields.Contains(name)) fields.Remove(name);
        }
        range +=2;
      };
      r++;
    }
    c++;
  }
  Console.WriteLine($"Part 2 - Answer : ");
}

part1();

part2();
