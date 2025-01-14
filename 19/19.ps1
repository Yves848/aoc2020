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


$file = [System.IO.File]::ReadAllText("$PSScriptRoot\test.txt").split("`r`n`r`n");
Write-SpectreRule -Title " Part 1" -Alignment Center

foreach($line in $file[0].split("`r`n")) {
  $m = [regex]::Matches($line,"(\d+)")
  $m.Count
}

$Q = [Stack]::new()
$Q.push(@{"two" = 2; "coucou" = 23})
$Q.push("one")
$Q.pop()
$Q