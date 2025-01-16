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