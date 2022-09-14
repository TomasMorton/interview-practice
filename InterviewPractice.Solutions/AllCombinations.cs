namespace InterviewPractice.Solutions;

public class AllCombinations
{
    public List<List<T>> Find<T>(T[] options, int combinationSize)
    {
        var results = new List<List<T>>();

        Find(options, combinationSize, 0, results, new List<T>());
        return results;
    }

    private static void Find<T>(IReadOnlyList<T> options, int combinationSize, int current, List<List<T>> results, List<T> path)
    {
        if (path.Count == combinationSize)
        {
            results.Add(path);
            return;
        }

        if (current == options.Count)
            return;

        var pathWithCurrent = path.ToList();
        pathWithCurrent.Add(options[current]);

        Find(options, combinationSize, current + 1, results, pathWithCurrent);

        Find(options, combinationSize, current + 1, results, path);
    }
}