namespace InterviewPractice.Solutions;

public class DigitSumGenerator
{
    public void GenerateNumbersWithSum(int digits, int sum)
    {
        var results = new List<List<int>>();
        Generate(sum, digits, new List<int>(), results);

        foreach (var result in results)
            Console.WriteLine(string.Join("", result));
    }

    private void Generate(int sum, int remainingDigits, List<int> current, List<List<int>> results)
    {
        var total = current.Sum();
        if (remainingDigits == 0)
        {
            if (total == sum)
                results.Add(current);
            return;
        }

        //Number is too large
        if (total > sum)
            return;

        //Number is too small
        if (total + remainingDigits * 9 < sum)
            return;

        for (var i = 0; i < 10; i++)
        {
            if (i == 0 && current.Count == 0)
                continue; //no leading 0s

            var next = current.ToList();
            next.Add(i);

            Generate(sum, remainingDigits - 1, next, results);
        }
    }
}

public class DigitSumGenerator2
{
    public void GenerateNumbersWithSum(int digits, int sum)
    {
        var results = new List<List<int>>();
        Generate(sum, digits, new List<int>(), results);

        foreach (var result in results)
            Console.WriteLine(string.Join("", result));
    }

    private void Generate(int sum, int remainingDigits, List<int> current, List<List<int>> results)
    {
        if (remainingDigits == 0)
        {
            if (sum == 0)
                results.Add(current);
            return;
        }

        //too small
        if (sum < 0)
            return;

        //too large
        if (sum - remainingDigits * 9 > 0)
            return;

        for (var i = 0; i < 10; i++)
        {
            if (i == 0 && current.Count == 0)
                continue; //no leading 0s

            var next = current.ToList();
            next.Add(i);

            Generate(sum - i, remainingDigits - 1, next, results);
        }
    }
}

public class DigitSumCounter
{
    public void CountNumbersWithSum(int digits, int sum)
    {
        var total = 0;
        for (var i = 1; i < 10; i++) //no leading 0s
            total += Count(sum - i, digits - 1);
        Console.WriteLine(total);
    }

    private int Count(int sum, int digits)
    {
        if (digits == 0)
            return sum == 0 ? 1 : 0;

        //Can just add 0s
        if (sum == 0)
            return 1;

        //too small
        if (sum < 0)
            return 0;

        //too large
        if (sum - digits * 9 > 0)
            return 0;

        var total = 0;
        for (var i = 0; i < 10; i++)
            total += Count(sum - i, digits - 1);

        return total;
    }
}

public class DigitSumCounterCached
{
    public void CountNumbersWithSum(int digits, int sum)
    {
        var cache = new Dictionary<(int, int), int>();

        var total = 0;
        for (var i = 1; i < 10; i++) //no leading 0s
            total += Count(sum - i, digits - 1, cache);

        Console.WriteLine(total);
    }

    private int Count(int sum, int digits, Dictionary<(int, int), int> cache)
    {
        if (digits == 0)
            return sum == 0 ? 1 : 0;

        if (cache.ContainsKey((sum, digits)))
            return cache[(sum, digits)];

        //Can just add 0s
        if (sum == 0)
            return 1;

        //too small
        if (sum < 0)
            return 0;

        // //too large
        if (sum - digits * 9 > 0)
            return 0;

        var total = 0;
        for (var i = 0; i < 10; i++)
            total += Count(sum - i, digits - 1, cache);

        cache[(sum, digits)] = total;
        return total;
    }
}