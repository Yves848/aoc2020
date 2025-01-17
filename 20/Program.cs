using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

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
  var grid = tiles.ToList();
  PrintMatrix(grid[0].Value.tile);
  Console.WriteLine($"");
  PrintMatrix(FlipVertically(grid[0].Value.tile));
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
