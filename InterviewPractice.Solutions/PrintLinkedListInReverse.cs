namespace InterviewPractice.Solutions;

public class PrintLinkedListInReverse<T>
{
    public static void Print(LinkedList<T>? list)
    {
        if (list == null)
            return;

        Console.Write("[");
        Print(list.First);
        Console.WriteLine("]");
    }

    private static void Print(LinkedListNode<T>? head)
    {
        if (head == null)
            return;

        Print(head.Next);
        Console.Write($" {head.Value};");
    }
}