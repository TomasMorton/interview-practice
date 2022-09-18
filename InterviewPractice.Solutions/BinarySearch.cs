namespace InterviewPractice.Solutions;

public class BinarySearch
{
    public static void Test()
    {
        var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var result = new BinarySearch().Find(arr, 1);
        Console.WriteLine(result);
    }
    
    public int? Find(int[] data, int target)
    {
        return Find(new ArraySegment<int>(data), target);
    }

    private int? Find(ArraySegment<int> data, int target)
    {
        if (data.Count == 0)
            return null;

        var midPoint = data.Count / 2;
        if (data[midPoint] == target)
            return midPoint + data.Offset;
        
        if (data[midPoint] > target)
            return Find(data[..(midPoint)], target);

        return Find(data[(midPoint + 1)..], target);
    }
}