. ./utils.ps1


function FindRule {
  param(
    [string]$temp,
    [int]$r
  )
  if ($letters.ContainsKey([int]$r)) {
    Write-SpectreRule -Title " letter $r" -Alignment Center -Color Red
    $temp += $letters[$r]
  } else {
    if ($rules.ContainsKey($r)) {
      $rule = $rules[$r]
      $i = 0;
      while ($i -lt $rule.Count) {
        $j = 0;
        $w = $rule[$i]
        Write-SpectreRule -Title " rule $w" -Alignment Center
        while ($j -lt $w.count) {
          $temp += FindRule -temp $temp -r $w[$j]
          $j++
        }
        $i++
      }
      
    }
  }
  return $temp
}

$file = [System.IO.File]::ReadAllText("$PSScriptRoot/test.txt").split("$lf$lf");
Write-SpectreRule -Title " Part 1" -Alignment Center
$rules = @{}
$letters = @{}
foreach($line in $file[0].split("$lf")) {
  $m = [regex]::Matches($line,"(\d+)")
  $num = [int]$m[0].Value
  if ([regex]::IsMatch($line,"""(.)""")) {
    $letters.Add($num,[regex]::Match($line,"""(.)""").Groups[1].Value)
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
# $Q = [Stack]::new()
# $zero

# $Q.push(@{"ababa" = (12,34); "coucou" = 23})
# $Q.push("one")
# $x = $Q.pop()
# $x
# $Q
$nn = $zero.pop()
$nn | ForEach-Object {$_
  $s += FindRule -temp "" -r ([int32]$_)
}
$s


