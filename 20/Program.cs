using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Runtime.Serialization;
using System.Data.Common;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}/git/aoc2020/20/test.txt");

var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";

var blocs = file.Split(lf + lf).ToList();
Regex reTileNum = new Regex(@"\d+");
Dictionary<int, (string[,] tile, (string L, string U, string D, string R) borders)> tiles = [];
blocs.ForEach(bloc =>
{
  int tileNum = 0;
  string[,] tile = new string[10, 10];
  int r = 0;
  bloc.Split(lf).ToList().ForEach(line =>
  {

    int h = bloc.Length;
    if (reTileNum.IsMatch(line))
    {
      tileNum = int.Parse(reTileNum.Match(line).Value);
      tile = new string[10, 10];
      r = 0;
    }
    else
    {
      int w = line.Length;
      int c = 0;
      while (c < w)
      {
        tile[r, c] = line.Substring(c, 1);
        c++;
      }
      r++;
    }
  });
  tiles.Add(tileNum, (tile, borders(tile)));
});

(string U, string L, string R, string D) borders(string[,] tile)
{
  (string U, string L, string R, string D) b = (U: "", L: "", R: "", D: ""); // Left, Up, Down, Right
  int r = 0;
  while (r < tile.GetLength(0))
  {
    b.U += tile[r, 0];
    b.D += tile[r, tile.GetLength(1) - 1];
    r++;
  }
  int c = 0;
  while (c < tile.GetLength(1))
  {
    b.L += tile[0, c];
    b.R += tile[tile.GetLength(0) - 1, c];
    c++;
  }
  return b;
}

string[,] FlipHorizontally(string[,] matrix)
{
  int rows = matrix.GetLength(0);
  int cols = matrix.GetLength(1);
  string[,] result = new string[rows, cols];

  for (int i = 0; i < rows; i++)
    for (int j = 0; j < cols; j++)
      result[i, cols - j - 1] = matrix[i, j];

  return result;
}

string[,] FlipVertically(string[,] matrix)
{
  int rows = matrix.GetLength(0);
  int cols = matrix.GetLength(1);
  string[,] result = new string[rows, cols];

  for (int i = 0; i < rows; i++)
    for (int j = 0; j < cols; j++)
      result[rows - i - 1, j] = matrix[i, j];

  return result;
}

string[,] RotateClockwise(string[,] matrix)
{
  int rows = matrix.GetLength(0);
  int cols = matrix.GetLength(1);
  string[,] result = new string[cols, rows];

  for (int i = 0; i < rows; i++)
    for (int j = 0; j < cols; j++)
      result[j, rows - i - 1] = matrix[i, j];

  return result;
}

string[,] transform(string[,] matrix, int t)
{
  int rows = matrix.GetLength(0);
  int cols = matrix.GetLength(1);
  string[,] result = new string[cols, rows];
  switch (t)
  {
    case 1:
      result = FlipVertically(matrix);
      break;
    case 2:
      result = FlipHorizontally(matrix);
      break;
    case 3:
      result = RotateClockwise(matrix);
      break;
    case 4:
      result = RotateClockwise(matrix); result = RotateClockwise(result);
      break;
    case 5:
      result = RotateClockwise(matrix); result = RotateClockwise(result); result = RotateClockwise(result);
      break;

  }
  return result;
}

void PrintMatrix(string[,] matrix)
{
  int rows = matrix.GetLength(0);
  int cols = matrix.GetLength(1);

  for (int i = 0; i < rows; i++)
  {
    for (int j = 0; j < cols; j++)
      Console.Write(matrix[i, j]);
    Console.WriteLine();
  }
}

void part1()
{
  int ans = 0;
  var list = tiles.ToList();
  int side = (int)Math.Sqrt((double)list.Count);
  Dictionary<int, ((int, int), List<string>)> grid = [];
  int i = 0;
  // recherche des coins
  while (i < list.Count)
  {
    var (num, piece) = list[i];
    // PrintMatrix(piece.tile);
    var g = piece.tile;
    var b = borders(g);
    int t = 0;
    while (t < 6 && !grid.ContainsKey(num))
    {
    (int U, int L, int R, int D) cont = (U: 0, L: 0, R: 0, D: 0);
      int j = 0;
      while (j < list.Count)
      {
        if (j != i)
        {
          var (num2, piece2) = list[j];
          var g2 = piece2.tile;
          int t2 = 0;
          while (t2 < 6)
          {
            var b2 = borders(g2);
            if (b.L == b2.R) cont.L++;
            if (b.U == b2.D) cont.U++;
            if (b.D == b2.U) cont.D++;
            if (b.R == b2.L) cont.R++;
            t2++;
            g2 = transform(piece2.tile,t2);
          }
        }
        j++;
      }
      t++;
      g = transform(piece.tile,t);
      b = borders(g);
    }
    i++;
  }

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

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
