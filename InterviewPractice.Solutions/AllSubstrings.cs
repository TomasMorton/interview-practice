namespace InterviewPractice.Solutions;

public class AllSubstrings2
{
    public static List<string> Find(string s)
    {
        if (s.Length == 0)
            return new List<string>();

        var result = Find(s[1..]);
        for (var to = 1; to <= s.Length; to++)
        {
            var substring = s[..to];
            result.Add(substring);
        }

        return result;
    }
}

public class AllSubstrings
{
    public static List<string> Find(string s)
    {
        return Find(s, 0);
    }
    
    private static List<string> Find(string s, int current)
    {
        if (current >= s.Length)
            return new List<string>();

        var result = Find(s, current + 1);
        for (var to = current + 1; to <= s.Length; to++)
        {
            var substring = s[current..to];
            result.Add(substring);
        }

        return result;
    }
}

