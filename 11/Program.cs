using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2020\\11\\test.txt").ToList();
int h = file.Count();
int w = file[0].Length;

List<(int, int)> dir = [
  (0,-1), // L
  (0,1), // R
  (-1,0), // U
  (1,0), // D
  (-1,-1), // LU
  (-1,1), // RU
  (1,-1), // LD
  (1,1) // RD
];

void part1()
{
  int ans = 0;
  int x = 0;
  int y = 0;
  bool changed = true;
  List<string> temp = [];
  while (changed)
  {
    temp.Clear();
    y = 0;
    while (y < h)
    {
      x = 0;
      string line = file[y];
      while (x < w)
      {
        if (file[y][x] != '.')
        {
          if (file[y][x] == 'L')
          {
            int oqp = 0;
            for (int i = 0; i < dir.Count(); i++)
            {
              var (yy, xx) = dir[i];
              int x2 = x + xx;
              int y2 = y + yy;
              if (y2 >= 0 && y2 < h && x2 >= 0 && x2 < w)
              {
                oqp += file[y2][x2] == '#' ? 1 : 0;
              }
            }
            if (oqp == 0)
            {
              line = line.Remove(x, 1).Insert(x, "#");
            }
          }
          else
          {
            int oqp = 0;
            for (int i = 0; i < dir.Count(); i++)
            {
              var (yy, xx) = dir[i];
              int x2 = x + xx;
              int y2 = y + yy;
              if (y2 >= 0 && y2 < h && x2 >= 0 && x2 < w)
              {
                oqp += file[y2][x2] == '#' ? 1 : 0;
              }
            }
            if (oqp >= 4)
            {
              line = line.Remove(x, 1).Insert(x, "L");
            }
          }
        }
        x++;
      }
      temp.Add(line);
      y++;
    }
    y = 0;
    changed = false;
    while (y < temp.Count && !changed)
    {
      changed = temp[y] != file[y];
      y++;
    }
    file.Clear();
    temp.ToList().ForEach(l => file.Add(l));
  }
  file.ToList().ForEach(l =>
  {
    Console.WriteLine(l);
    l.ToList().ForEach(c => ans += c == '#' ? 1 : 0);
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
  int x = 0;
  int y = 0;
  bool changed = true;
  file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2020\\11\\test.txt").ToList();
  file.ToList().ForEach(l =>
  {
    Console.WriteLine(l);

  });
  List<string> temp = [];
  int i2 = 0;
  while (changed)
  {

    temp.Clear();
    y = 0;
    while (y < h)
    {
      x = 0;
      string line = file[y];
      while (x < w)
      {
        if (file[y][x] != '.')
        {
          if (file[y][x] == 'L')
          {
            int oqp = 0;
            for (int i = 0; i < dir.Count(); i++)
            {
              var (yy, xx) = dir[i];
              int x2 = x + xx;
              int y2 = y + yy;
              // if (y2 >= 0 && y2 < h && x2 >= 0 && x2 < w)
              while (y2 >= 0 && y2 < h && x2 >= 0 && x2 < w)
              {
                if (file[y2][x2] == '#' || file[y2][x2] == 'L')
                {
                  if (file[y2][x2] == '#')
                  oqp++;
                  break;
                }
                else
                {
                  x2 += xx;
                  y2 += yy;
                }
              }
            }
            if (oqp == 0)
            {
              line = line.Remove(x, 1).Insert(x, "#");
            }
          }
          else
          {
            int oqp = 0;
            for (int i = 0; i < dir.Count(); i++)
            {
              var (yy, xx) = dir[i];
              int x2 = x + xx;
              int y2 = y + yy;
              while (y2 >= 0 && y2 < h && x2 >= 0 && x2 < w)
              {
                if (file[y2][x2] == '#' || file[y2][x2] == 'L')
                {
                  if (file[y2][x2] == '#')
                  oqp++;
                  break;
                }
                else
                {
                  x2 += xx;
                  y2 += yy;
                }
              }
            }
            if (oqp >= 5)
            {
              line = line.Remove(x, 1).Insert(x, "L");
            }
          }
        }
        x++;
      }
      temp.Add(line);
      y++;
    }
    i2++;
    Console.WriteLine($"---{i2}---");
    temp.ToList().ForEach(l =>
  {
    Console.WriteLine(l);

  });
    y = 0;
    changed = false;
    while (y < temp.Count && !changed)
    {
      changed = temp[y] != file[y];
      y++;
    }
    file.Clear();
    temp.ToList().ForEach(l => file.Add(l));
  }
  Console.WriteLine("#####");
  file.ToList().ForEach(l =>
  {
    Console.WriteLine(l);
    l.ToList().ForEach(c => ans += c == '#' ? 1 : 0);
  });
  Console.WriteLine($"Part  - Answer : {ans}");
}

part1();

part2();
