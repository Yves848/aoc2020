. ./utils.ps1


$file = [System.IO.File]::ReadAllText("$PSScriptRoot/test.txt").split("$lf$lf");
Write-SpectreRule -Title " Part 1" -Alignment Center
$rules = @{}
$letters = @{}
foreach($line in $file[0].split("$lf")) {
  $m = [regex]::Matches($line,"(\d+)")
  $num = $m[0].Value
  if ([regex]::IsMatch($line,"""(.)""")) {
    $letters.Add($num,[regex]::Match($line,"""(.)""").Value)
  }
  else {
    $r = [Stack]::new()
    $line.Split(":")[1].Split("|") | ForEach-Object {
      $n = $_.Split(" ", [System.StringSplitOptions]::RemoveEmptyEntries) | ForEach-Object {[int]$_}
      $r.Add($n)
    }
    $rules.Add([int]$num,$r)
  }
}
Write-SpectreRule -Title " Letters " -Alignment Center -Color Red
$letters
Write-SpectreRule -Title " Rules " -Alignment Center -Color Blue
$rules.GetEnumerator() | Sort-Object Name
$zero = $rules[0]
$Q = [Stack]::new()
$zero

$Q.push(@{"ababa" = (12,34); "coucou" = 23})
# $Q.push("one")
# $Q.pop()
$Q