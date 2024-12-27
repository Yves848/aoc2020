using System.IO.Pipes;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2020\\06\\test.txt");

void part1()
{
  int ans = 0;
  Regex groups = new Regex(@"(\w)+");
  file.Split("\r\n\r\n").ToList().ForEach(g => {
    HashSet<string> group = [];
    var gr = groups.Matches(g);
    gr.ToList().ForEach(c => {
      c.Value.ToList().ForEach(s =>group.Add(s.ToString()));
    });
    ans += group.Count;
  });
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
  int ans = 0;
  Regex regroups = new Regex(@"(\w)+");
  file.Split("\r\n\r\n").ToList().ForEach(groups => {
    Dictionary<string,int> group = [];
    List<string> answers = groups.Split("\r\n").ToList();
    answers.ToList().ForEach(gr => {
      gr.ToList().ForEach(g => {
        if (!group.ContainsKey(g.ToString())) {
          group.Add(g.ToString(),1);
        } else {
          group[g.ToString()] = group[g.ToString()]+1; 
        }
      });
    });
    group.ToList().ForEach(g => {
      if (g.Value == answers.Count) ans++;
    });
  });
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
