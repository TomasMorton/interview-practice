public class TraverseTree<T>
{
    public static void Test()
    {    
        var root = new Node<int>(25,
            Left: new(15,
                Left: new(10,
                    Left: new(4),
                    Right: new(12)),
                Right: new(22,
                    Left: new(18),
                    Right: new(24))),
            Right: new(50,
                Left: new(35,
                    Left: new(31),
                    Right: new(44)),
                Right: new(70,
                    Left: new(66),
                    Right: new(90)))
        );

        var solver = new TraverseTree<int>();
        
        Console.Write("Pre-order: ");
        solver.PreOrderTraverse(root);
        Console.WriteLine();
        
        Console.Write("In-order: ");
        solver.InOrderTraverse(root);
        Console.WriteLine();
        
        Console.Write("Post-order: ");
        solver.PostOrderTraverse(root);
        Console.WriteLine();
        
        Console.Write("Level-order: ");
        solver.LevelOrderTraverse(root);
        Console.WriteLine();
    }


    public void PreOrderTraverse(Node<T>? n)
    {
        if (n == null)
            return;
            
        Console.Write($"{n.Value}, ");
        PreOrderTraverse(n.Left);
        PreOrderTraverse(n.Right);
    }

    public void InOrderTraverse(Node<T>? n)
    {
        if (n == null)
            return;
            
        InOrderTraverse(n.Left);
        Console.Write($"{n.Value}, ");
        InOrderTraverse(n.Right);
    }

    public void PostOrderTraverse(Node<T>? n)
    {
        if (n == null)
            return;
            
        PostOrderTraverse(n.Left);
        PostOrderTraverse(n.Right);
        Console.Write($"{n.Value}, ");
    }

    public void LevelOrderTraverse(Node<T>? n)
    {        
        var queue = new Queue<Node<T>?>();
        queue.Enqueue(n);
        
        while (queue.Any())
        {
            var next = queue.Dequeue();
            
            if (next == null)
                continue;
            
            Console.Write($"{next.Value}, ");
            queue.Enqueue(next.Left);
            queue.Enqueue(next.Right);            
        } 
    }


    public record Node<T>(T Value, Node<T>? Left = null, Node<T>? Right = null);
}