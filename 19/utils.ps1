class Stack : System.Collections.Generic.List[object] {
  Stack() {
    [ordered]@{}
  }
  
  [object] pop() {
   $value = $this[$this.Count-1]
   $this.RemoveAt($this.Count-1)
   return $value  
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