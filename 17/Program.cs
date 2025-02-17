﻿using System.Data;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2020/17/test.txt").ToList();
Dictionary<(int, int, int,int), bool> grid = [];
List<(int, int, int,int)> directions = [];
int h = file.Count;
int w = file[0].Length;

void fillDirections()
{
  List<int> d = [-1, 0, 1];
  d.ForEach(y =>
  {
    d.ForEach(x =>
    {
      d.ForEach(z =>
      {
          d.ForEach(w => {
            if (!(x == 0 && y == 0 && z == 0 && w == 0))
              directions.Add((x, y, z,w));
          });
      });
    });
  });
}

void fillGrid()
{
  int r = 0;
  while (r < h)
  {
    int c = 0;
    while (c < w)
    {
      grid.Add((r, c, 0,0), file[r].Substring(c, 1) == "#");
      c++;
    }
    r++;
  }
}

int actives((int, int, int,int) pos, Dictionary<(int, int, int,int), bool> g)
{
  int nb = 0;
  var (y, x, z, w) = pos;
  directions.ForEach(d =>
        {
          var (dy, dx, dz,dw) = d;
          int y2 = y + dy;
          int x2 = x + dx;
          int z2 = z + dz;
          int w2 = w + dw;
          if (g.ContainsKey((y2, x2, z2,w2)))
          {
            if (g[(y2, x2, z2,w2)])
            nb += 1;
          }
        });
  return nb;
}

void part1()
{
  int ans = 0;
  fillDirections();
  fillGrid();
  for (int i = 0; i < 6; i++)
  {
    Dictionary<(int, int, int,int), bool> temp = [];
    grid.ToList().ForEach(cell =>
    {
      var (y, x, z, w) = cell.Key;
      var active = cell.Value;
      directions.ForEach(d =>
      {
        var (dy, dx, dz, dw) = d;
        int y2 = y + dy;
        int x2 = x + dx;
        int z2 = z + dz;
        int w2 = w + dw;
        if (!grid.ContainsKey((y2, x2, z2,w2)))
          if (!temp.ContainsKey((y2, x2, z2,w2)))
            temp.Add((y2, x2, z2,w2), false);
      });
      temp.Add((y, x, z,w), grid[(y, x, z,w)]);
    });
    int j = 0;
    grid.Clear();
    while (j < temp.Count)
    {
      var g = temp.ToList()[j];
      var (y, x, z,w) = g.Key;
      int nb = actives((y, x, z,w), temp);
      if (g.Value)
      {
        grid.Add((y, x, z,w),(nb >= 2 && nb <= 3));
      }
      else
      {
        grid.Add((y, x, z,w),(nb == 3));
      }
      j++;
    };
  }
  grid.ToList().ForEach(g =>
  {
    ans += g.Value ? 1 : 0;
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
