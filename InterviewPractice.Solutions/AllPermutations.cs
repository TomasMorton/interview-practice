namespace InterviewPractice.Solutions;

public class AllPermutations
{
    public static void Test()
    {
        var solver = new AllPermutations();
        var result = solver.Generate("abc");
        Console.WriteLine(string.Join("; ", result));
    }

    public List<string> Generate(string s)
    {
        var results = new List<string>();
        Generate(results, s, string.Empty);
        return results;
    }

    private void Generate(List<string> results, string remaining, string path)
    {
        if (remaining.Length == 0)
        {
            results.Add(path);
            return;
        }

        for (var i = 0; i < remaining.Length; i++)
        {
            var pathWithCurrent = path + remaining[i];
            Generate(results, remaining[..i] + remaining[(i + 1)..], pathWithCurrent);
        }
    }
}

public class AllPermutations2
{
    public static void Test()
    {
        var solver = new AllPermutations2();
        var result = solver.Generate("abc");
        Console.WriteLine(string.Join("; ", result));
    }

    private List<string> Generate(string remaining)
    {
        if (remaining.Length == 0)
            return new List<string> { "" };

        var results = Generate(remaining[1..]);
        var finalResults = new List<string>();
        foreach (var result in results)
        {
            for (var i = 0; i <= result.Length; i++)
            {
                var inserted = result[..i] + remaining[0] + result[i..];
                finalResults.Add(inserted);
            }
        }

        return finalResults;
    }
}