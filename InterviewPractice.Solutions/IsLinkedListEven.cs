bool IsEven(Node n)
{
    if (n == null)
        return true; //0 = even
        
    return !EvenOrOdd(n.Next);
}