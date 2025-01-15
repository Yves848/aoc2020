class Stack : System.Collections.Generic.List[object] {
  Stack() {
    [ordered]@{}
  }
  
  pop() {
   $value = $this[$this.Count-1]
   $this.RemoveAt($this.Count-1)
   $value  
  }

  push($value) {
    $this.Add($value)
  }

}

if ($IsWindows) {
  $lf = "`r`n"
} else {
  $lf = "`n"
}


$file = [System.IO.File]::ReadAllText("$PSScriptRoot\test.txt").split("$lf$lf");
Write-SpectreRule -Title " Part 1" -Alignment Center

foreach($line in $file[0].split("$lf")) {
  $m = [regex]::Matches($line,"(\d+)")
  $num = $m[0].Value
  if ([regex]::IsMatch($line,"""")) {
    Write-Host($line)
  }
  $num
}

# $Q = [Stack]::new()
# $Q.push(@{"two" = 2; "coucou" = 23})
# $Q.push("one")
# $Q.pop()
# $Q