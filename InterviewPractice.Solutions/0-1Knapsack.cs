using System.Text.Json;

namespace InterviewPractice.Solutions;

public class KnapsackComplete
{
    public static void Test()
    {
        var solver = new KnapsackComplete();

        var items = new Item[] { new(1, 6), new(2, 10), new(3, 12) };
        var maxValue = solver.FindMaxValue(items, 5);
        Console.WriteLine($"Maximum value: {maxValue}");
    }

    public double FindMaxValue(Item[] items, double maxWeight)
    {
        var allValues = new List<List<Item>>();
        FindAllValues(items, maxWeight, allValues, new List<Item>());

        Console.WriteLine("All options: " + JsonSerializer.Serialize(allValues));

        var bestValues = allValues.MaxBy(x => x.Sum(i => i.Value))!;
        Console.WriteLine($"Best option: {string.Join(", ", bestValues)}");

        return bestValues.Sum(i => i.Value);
    }

    private void FindAllValues(Item[] items, double maxWeight, List<List<Item>> results, List<Item> currentSet)
    {
        if (items.Length == 0)
        {
            results.Add(currentSet);
            return;
        }

        var nextItem = items[0];
        var remainingItems = items[1..];

        FindAllValues(remainingItems, maxWeight, results, currentSet);

        var currentWeight = currentSet.Sum(i => i.Weight);
        if (currentWeight + nextItem.Weight > maxWeight)
        {
            results.Add(currentSet);
            return;
        }

        var newSet = currentSet.ToList();
        newSet.Add(nextItem);
        FindAllValues(remainingItems, maxWeight, results, newSet);
    }

    public record Item(double Weight, double Value);
}

public class KnapsackMinimalClean
{
    public static void Test()
    {
        var solver = new KnapsackMinimalClean();

        var items = new Item[] { new(1, 6), new(2, 10), new(3, 12) };
        var maxValue = solver.FindMaxValue(items, 5);
        Console.WriteLine($"Maximum value: {maxValue}");
    }

    public double FindMaxValue(Item[] items, double remainingWeight)
    {
        if (items.Length == 0)
            return 0;

        var currentItem = items[0];
        var remainingItems = items[1..];

        var withoutCurrentItem = FindMaxValue(remainingItems, remainingWeight);

        if (remainingWeight - currentItem.Weight < 0)
            return withoutCurrentItem;

        var withCurrentItem = FindMaxValue(remainingItems, remainingWeight - currentItem.Weight) + currentItem.Value;

        return Math.Max(withCurrentItem, withoutCurrentItem);
    }

    public record Item(double Weight, double Value);
}

public class KnapsackMinimal
{
    public static void Test()
    {
        var solver = new KnapsackMinimal();

        var items = new Item[] { new(1, 6), new(2, 10), new(3, 12) };
        var maxValue = solver.FindMaxValue(items, 5);
        Console.WriteLine($"Maximum value: {maxValue}");
    }

    public double FindMaxValue(Item[] items, double remainingWeight)
    {
        if (items.Length == 0)
            return 0;

        var currentItem = items[0];
        var remainingItems = items[1..];

        if (remainingWeight - currentItem.Weight < 0)
            return FindMaxValue(remainingItems, remainingWeight);

        return Math.Max(
            FindMaxValue(remainingItems, remainingWeight),
            FindMaxValue(remainingItems, remainingWeight - currentItem.Weight) + currentItem.Value);
    }

    public record Item(double Weight, double Value);
}

public class KnapsackMinimalCached
{
    public static void Test()
    {
        var solver = new KnapsackMinimalCached();

        var items = new Item[] { new(1, 6), new(2, 10), new(3, 12) };
        var maxValue = solver.FindMaxValue(items, 5);
        Console.WriteLine($"Maximum value: {maxValue}");
    }

    public double FindMaxValue(Item[] items, double maxWeight)
        => FindMaxValue(items, maxWeight, new Dictionary<int, Dictionary<double, double>>());

    //Cache: currentItemCount => (remainingWeight => maxValueForWeight)
    private double FindMaxValue(Item[] items, double remainingWeight, Dictionary<int, Dictionary<double, double>> cache)
    {
        if (items.Length == 0)
            return 0;

        if (!cache.ContainsKey(items.Length))
            cache.Add(items.Length, new Dictionary<double, double>());
        if (cache[items.Length].TryGetValue(remainingWeight, out var cachedValue))
            return cachedValue;

        var currentItem = items[0];
        var remainingItems = items[1..];

        if (remainingWeight - currentItem.Weight < 0)
            return FindMaxValue(remainingItems, remainingWeight, cache);

        var maxValueForWeight = Math.Max(
            FindMaxValue(remainingItems, remainingWeight, cache),
            FindMaxValue(remainingItems, remainingWeight - currentItem.Weight, cache) + currentItem.Value);

        cache[items.Length][remainingWeight] = maxValueForWeight;
        return maxValueForWeight;
    }

    public record Item(double Weight, double Value);
}

public class KnapsackIterative
{
    public static void Test()
    {
        var solver = new KnapsackIterative();

        var items = new Item[] { new(1, 6), new(2, 10), new(3, 12) };
        var maxValue = solver.FindMaxValue(items, 5);
        Console.WriteLine($"Maximum value: {maxValue}");
    }

    public int FindMaxValue(Item[] items, int maxWeight)
    {
        var cache = new int[items.Length + 1, maxWeight + 1];

        for (var numItems = 1; numItems <= items.Length; numItems++)
        {
            var currentItem = items[numItems - 1];
            for (var weight = 0; weight <= maxWeight; weight++)
            {
                if (currentItem.Weight > weight)
                {
                    cache[numItems, weight] = cache[numItems - 1, weight];
                }
                else
                {
                    var withoutCurrent = cache[numItems - 1, weight];
                    var withCurrent = cache[numItems - 1, weight - currentItem.Weight] + currentItem.Value;
                    cache[numItems, weight] = Math.Max(withoutCurrent, withCurrent);
                }
            }
        }

        return cache[items.Length, maxWeight];
    }

    public record Item(int Weight, int Value);
}