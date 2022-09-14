namespace InterviewPractice.Solutions;

public class IsPalindrome
{
    public bool CheckString(string s)
    {
        if (s.Length <= 1)
            return true;

        if (s[0] != s[^1])
            return false;

        return CheckString(s[1..^1]);
    }
}