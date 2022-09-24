public bool IsValidParentheses(string s)
{
    var expected = new Stack<char>();
    foreach (var chr in s)
    {
        if (chr == '(')
            expected.Push(')');
        else if (chr == '{')
            expected.Push('}');
        else if (chr == '[')
            expected.Push(']');
        else
        {
            if (expected.Count == 0) return false;
            if (expected.Pop() != chr) return false;
        }        
    }
    
    return expected.Count == 0;
}
