var file = (args.Length > 0 ? File.ReadAllLines(args[0]) : File.ReadAllLines(@"C:\Users\yvesg\git\aoc2020\01\test.txt"));

List<int> numbers = file.ToList().Select(p => int.Parse(p)).ToList();

void part1()
{
  int i = 0;
  int ans = 0;
  while (i < numbers.Count - 1)
  {
    int j = i + 1;
    while (j < numbers.Count)
    {
      if (numbers[i] + numbers[j] == 2020)
      {
        ans = numbers[i] * numbers[j];
        i = numbers.Count;
        break;
      }
      j++;
    }
    i++;
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int i = 0;
  int ans = 0;
  while (i < numbers.Count - 2)
  {
    int j = i + 1;
    while (j < numbers.Count - 1)
    {
      int k = j + 1;
      while (k < numbers.Count)
      {

        if (numbers[i] + numbers[j] + numbers[k] == 2020)
        {
          ans = numbers[i] * numbers[j] * numbers[k];
          i = numbers.Count;
          j = numbers.Count;
          break;
        }
        k++;
      }

      j++;
    }
    i++;
  }
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();