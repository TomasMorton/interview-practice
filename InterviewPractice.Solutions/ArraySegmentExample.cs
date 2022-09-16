namespace InterviewPractice.Solutions;

public class ArraySegmentExample
{
    public static void Test()
    {
        var all = new[] { 1, 2, 3, 4, 5 };
        var first = new ArraySegment<int>(all, 0, 1);
        var set = new ArraySegment<int>(all, 1, 3);

        PrintIndexAndValues(first);
        PrintIndexAndValues(set);

        Console.WriteLine(string.Join(", ", all));
        Console.WriteLine(string.Join(", ", set));

        set[0] = 10;

        Console.WriteLine(string.Join(", ", set));
        Console.WriteLine(string.Join(", ", all));

        void PrintIndexAndValues<T>(ArraySegment<T> arrSeg)
        {
            for (var i = arrSeg.Offset; i < arrSeg.Offset + arrSeg.Count; i++)
                Console.WriteLine("   [{0}] : {1}", i, arrSeg.Array![i]);

            Console.WriteLine();
        }
    }
}