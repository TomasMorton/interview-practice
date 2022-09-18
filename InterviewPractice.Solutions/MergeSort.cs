namespace InterviewPractice.Solutions;

public class MergeSort
{
    public void Sort(int[] data)
    {
        Sort(new ArraySegment<int>(data));
    }

    private List<int> Sort(ArraySegment<int> data)
    {
        if (data.Count == 0)
            return new List<int>();

        if (data.Count == 1)
            return new List<int>(data);

        var midPoint = data.Count / 2;

        var left = Sort(data[..midPoint]);
        var right = Sort(data[midPoint..]);

        return Merge(data, left, right);
    }

    private List<int> Merge(ArraySegment<int> data, List<int> left, List<int> right)
    {
        var leftIx = 0;
        var rightIx = 0;
        var resultIx = 0;

        while (leftIx < left.Count && rightIx < right.Count)
        {
            if (left[leftIx] <= right[rightIx])
                data[resultIx++] = left[leftIx++];
            else
                data[resultIx++] = right[rightIx++];
        }

        while (leftIx < left.Count)
            data[resultIx++] = left[leftIx++];

        while (rightIx < right.Count)
            data[resultIx++] = right[rightIx++];

        return data.ToList();
    }
}

public class MergeSort2
{
    public void Sort(int[] data)
    {
        Sort(new ArraySegment<int>(data));
    }

    private void Sort(ArraySegment<int> data)
    {
        if (data.Count <= 1)
            return;

        var midPoint = data.Count / 2;

        var left = data[..midPoint];
        Sort(left);
        var right = data[midPoint..];
        Sort(right);

        Merge(data, left, right);
    }

    private void Merge(ArraySegment<int> data, ArraySegment<int> l, ArraySegment<int> r)
    {
        var leftIx = 0;
        var rightIx = 0;
        var resultIx = 0;

        var left = l.ToArray();
        var right = r.ToArray();

        while (leftIx < left.Length && rightIx < right.Length)
        {
            if (left[leftIx] <= right[rightIx])
                data[resultIx++] = left[leftIx++];
            else
                data[resultIx++] = right[rightIx++];
        }

        while (leftIx < left.Length)
            data[resultIx++] = left[leftIx++];

        while (rightIx < right.Length)
            data[resultIx++] = right[rightIx++];
    }
}