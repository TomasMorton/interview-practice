public bool SameTree(Node a, Node b)
{
    if (a == null && b == null)
        return true;
        
    if (a == null || b == null)
        return false;
        
    if (a.Value != b.Value)
        return false;
        
    return SameTree(a.Left, b.Left) && SameTree(a.Right, b.Right);
}