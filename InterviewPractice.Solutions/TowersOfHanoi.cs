namespace InterviewPractice.Solutions;

public class TowersOfHanoi
{
    public void Run(int numberOfDisks)
    {
        var from = new Stack<int>(numberOfDisks);
        for (var i = numberOfDisks; i > 0 ; i--)
        {
            from.Push(i);
        }
        var to = new Stack<int>(numberOfDisks);
        var buffer = new Stack<int>(numberOfDisks);

        Run(numberOfDisks, from, to, buffer);
        Console.WriteLine(string.Join(", ", to));
    }

    private void Run(int n, Stack<int> from, Stack<int> to, Stack<int> buffer)
    {
        if (n == 0)
            return;

        Run(n - 1, from, buffer, to);
        to.Push(from.Pop());

        Run(n - 1, buffer, to, from);
    }
}