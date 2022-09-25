bool IsBinarySearchTree(Node n)
{
    var result = Search(n);
    return result != null;
}

private (int min, int max)? Search(Node n)
{
    if (n == null)
        return (int.MinValue, int.MaxValue);
        
    var left = Search(n.Left);
    if (left == null)
        return null;
    
    if (left.min > n.Value)
        return null;        
    
    var right = Search(n.Right);
    if (right == null)
        return null;
        
    if (right.max < n.Value)
        return null;
        
    return (Math.Min(left.min, right.min), Math.Max(left.max, right.max));
}