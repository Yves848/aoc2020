var file = (args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines(@"C:\Users\yvesg\git\aoc2020\03\test.txt"));

void part1()
{
  int ans = 0;
  int r = 1;
  int c = 3;
  while (r < file.Length)
  {
    string line = file[r];

    while (c > line.Length)
    {
      line += line;
    }

    if (line[c] == '#') ans++;
    c += 3;
    r++;
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

int slope(int rr, int d)
{
  int ans = 0;
  int r = d;
  int c = rr;
  while (r < file.Length)
  {
    string line = file[r];

    while (c >= line.Length)
    {
      line += line;
    }

    if (line[c] == '#') ans++;
    c += rr;
    r+=d;
  }
  return ans;
}

void part2()
{
  int ans = 1;
  ans *= slope(1,1);
  ans *= slope(3,1);
  ans *= slope(5,1);
  ans *= slope(7,1);
  ans *= slope(1,2);



  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();

//123456789111111111122222222223333333333
//         012345678901234567890123456789
//..##.........##.........##.........##..