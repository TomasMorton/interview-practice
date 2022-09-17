//Build all the words that can be made by interleaving a and b

//optimisations:
//the length of c must equal a + b
//if the "next" char doesn't match it's not a valid solution

namespace InterviewPractice.Solutions;

public class IsInterleavedString
{
    public static void Test()
    {
        var solver = new IsInterleavedString();
        var a = "aaaa";
        var b = "bbbb";
        var expected = "aabbabab";
        var result = solver.IsInterleaved(a, b, expected);

        Console.WriteLine($"Is {expected} an interleave of {a} and {b}? {result}");
    }

    public bool IsInterleaved(string a, string b, string expected)
    {
        if (a.Length + b.Length != expected.Length)
            return false;

        return IsInterleaved(expected, a, b, string.Empty);
    }

    private bool IsInterleaved(string expected, string a, string b, string current)
    {
        if (a.Length == 0 && b.Length == 0)
            return current == expected;

        if (!expected.StartsWith(current))
            return false;

        var withA = a.Length > 0 ? IsInterleaved(expected, a[1..], b, current + a[0]) : false;
        var withB = b.Length > 0 ? IsInterleaved(expected, a, b[1..], current + b[0]) : false;

        return withA || withB;
    }
}

public class IsInterleavedStringCached
{
    public static void Test()
    {
        var solver = new IsInterleavedStringCached();
        var a = "aaaa";
        var b = "bbbb";
        var expected = "aabbabab";
        var result = solver.IsInterleaved(a, b, expected);

        Console.WriteLine($"Is {expected} an interleave of {a} and {b}? {result}");
    }

    public bool IsInterleaved(string a, string b, string expected)
    {
        if (a.Length + b.Length != expected.Length)
            return false;

        var cache = new Dictionary<string, bool>();

        return IsInterleaved(expected, cache, a, b, string.Empty);
    }

    private bool IsInterleaved(string expected, Dictionary<string, bool> cache, string a, string b, string current)
    {
        if (a.Length == 0 && b.Length == 0)
            return current == expected;

        if (cache.ContainsKey(current))
            return cache[current];

        if (!expected.StartsWith(current))
            return false;

        var withA = a.Length > 0 ? IsInterleaved(expected, cache, a[1..], b, current + a[0]) : false;
        var withB = b.Length > 0 ? IsInterleaved(expected, cache, a, b[1..], current + b[0]) : false;

        var couldBeInterleaved = withA || withB;

        cache[current] = couldBeInterleaved;
        return couldBeInterleaved;
    }
}

public class IsInterleavedString2
{
    public static void Test()
    {
        var solver = new IsInterleavedString2();
        var a = "aaaa";
        var b = "bbbb";
        var expected = "aabbabab";
        var result = solver.IsInterleaved(a, b, expected);

        Console.WriteLine($"Is {expected} an interleave of {a} and {b}? {result}");
    }

    public bool IsInterleaved(string a, string b, string expected)
    {
        if (a.Length + b.Length != expected.Length)
            return false;

        return IsInterleavedImpl(a, b, expected);
    }

    private bool IsInterleavedImpl(string a, string b, string expected)
    {
        if (expected.Length == 0)
            return a.Length == 0 && b.Length == 0;

        var aMatch = a.Length > 0 && a[0] == expected[0];
        var bMatch = b.Length > 0 && b[0] == expected[0];

        var withA = aMatch ? IsInterleavedImpl(a[1..], b, expected[1..]) : false;
        var withB = bMatch ? IsInterleavedImpl(a, b[1..], expected[1..]) : false;

        return withA || withB;
    }
}

public class IsInterleavedString2Cached
{
    public static void Test()
    {
        var solver = new IsInterleavedString2Cached();
        var a = "aaaa";
        var b = "bbbb";
        var expected = "aabbabab";
        var result = solver.IsInterleaved(a, b, expected);

        Console.WriteLine($"Is {expected} an interleave of {a} and {b}? {result}");
    }

    public bool IsInterleaved(string a, string b, string expected)
    {
        if (a.Length + b.Length != expected.Length)
            return false;

        var cache = new Dictionary<string, bool>();
        
        return IsInterleaved(cache, a, b, expected);
    }

    private bool IsInterleaved(Dictionary<string, bool> cache, string a, string b, string expected)
    {
        if (expected.Length == 0)
            return a.Length == 0 && b.Length == 0;

        if (cache.TryGetValue(expected, out var result))
            return result;
            
        
        var aMatch = a.Length > 0 && a[0] == expected[0];
        var bMatch = b.Length > 0 && b[0] == expected[0];

        var withA = aMatch ? IsInterleaved(cache, a[1..], b, expected[1..]) : false;
        var withB = bMatch ? IsInterleaved(cache, a, b[1..], expected[1..]) : false;

        var isCandidate = withA || withB;
        cache[expected] = isCandidate;
        return isCandidate;
    }
}