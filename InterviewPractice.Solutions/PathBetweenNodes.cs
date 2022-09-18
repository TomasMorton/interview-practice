namespace InterviewPractice.Solutions;

public class PathBetweenNodes
{
    public static void Test()
    {
        var node1 = new Node(1);
        var node2 = new Node(2);
        var node3 = new Node(3);
        var node4 = new Node(4);
        var node5 = new Node(5);
        var node6 = new Node(6);

        node1.Neighbours = new List<Node> { node2, node3, node4, node5, node6 };
        node2.Neighbours = new List<Node> { node5 }; //through-1 link
        node3.Neighbours = new List<Node> { node4 }; //through-multiple link
        node4.Neighbours = new List<Node> { node5 }; //through-multiple link
        node5.Neighbours = new List<Node> { node1, node2, node3, node4 };
        node6.Neighbours = new List<Node> { node1, node1, node6 }; //Self linked, back-linked and repeat-linked edge cases

        var paths = new PathBetweenNodes().FindPaths(node1, node5);

        foreach (var path in paths)
        {
            Console.WriteLine(string.Join(", ", path));
        }
    }
    
    public List<List<Node>> FindPaths(Node from, Node to)
    {
        var results = new List<List<Node>>();
        Traverse(from, to, new HashSet<Node>(), results);

        return results;
    }

    private void Traverse(Node from, Node to, HashSet<Node> path, List<List<Node>> results)
    {
        if (from == to)
        {
            path.Add(to);
            results.Add(path.ToList());
            return;
        }

        if (path.Contains(from))
            return;

        foreach (var neighbour in from.Neighbours)
        {
            var newPath = Clone(path);
            newPath.Add(from);
            Traverse(neighbour, to, newPath, results);
        }
    }

    private HashSet<T> Clone<T>(HashSet<T> original) => original.ToHashSet();

    public class Node
    {
        public int Value;
        public List<Node> Neighbours;

        public Node(int value)
        {
            Value = value;
            Neighbours = new List<Node>();
        }

        public override string ToString() => Value.ToString();
    }
}