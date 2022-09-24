public boolean evenlySpaced(int a, int b, int c) {
  var min = Math.Min(Math.Min(a, b), c);
  var max = Math.Max(Math.Max(a, b), c);
  
  var total = min + max;
  if (total % 2 == 1)
    return false;
    
  var mid = total / 2;
  
  return mid == a || mid == b || mid == c;
}
