namespace InterviewPractice.Solutions;

public class AllDistinctPermutations
{
    public static void Test()
    {
        var solver = new AllDistinctPermutations();
        var result = solver.Generate("aabaa");
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

        var cache = new bool[26];

        for (var i = 0; i < remaining.Length; i++)
        {
            var cacheIndex = (int) remaining[i] - 'a';
            if (cache[cacheIndex])
                continue;

            cache[cacheIndex] = true;
            var pathWithCurrent = path + remaining[i];
            Generate(results, remaining[..i] + remaining[(i + 1)..], pathWithCurrent);
        }
    }
}