namespace InterviewPractice.Solutions;

public class StackAsQueue<T>
{
    private readonly Stack<T> _stack;

    public StackAsQueue()
    {
        _stack = new Stack<T>();
    }

    public T Dequeue() => _stack.Pop();

    public T Peek() => _stack.Peek();

    public void Print()
    {
        if (_stack.Count == 0)
        {
            Console.WriteLine();
            return;
        }
        
        var top = _stack.Pop();
        Console.Write($"{top};");
        Print();
        _stack.Push(top);
    }

    public void Enqueue(T value)
    {
        if (_stack.Count == 0)
        {
            _stack.Push(value);
            return;
        }

        var existingValue = _stack.Pop();
        Enqueue(value);
        _stack.Push(existingValue);
    }
}