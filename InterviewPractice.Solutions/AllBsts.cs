using static InterviewPractice.Solutions.Tree;

namespace InterviewPractice.Solutions;

public class AllBsts
{
    public List<Tree> Generate(int maxValue)
    {
        var values = Enumerable.Range(1, maxValue).Reverse().ToArray();
        return Generate(values);
    }

    private List<Tree> Generate(int[] values)
    {
        if (values.Length == 1)
            return new List<Tree> { new(values[0]) };

        var subtrees = Generate(values[1..]);
        return subtrees.SelectMany(x => Merge(x, values[0])).ToList();
    }

    private List<Tree> Merge(Tree tree, int value)
    {
        var results = new List<Tree>();

        results.AddRange(Merge(tree, 0, value));

        var clone = tree.Clone();
        var newRoot = new Node(value);
        newRoot.Left = clone.Root; //Always move to left since newer root is bigger

        clone.Root = newRoot;
        results.Add(clone);

        return results;
    }

    private List<Tree> Merge(Tree tree, int depth, int value)
    {
        var clone = tree.Clone();
        var node = clone.Root;
        for (var i = 0; i < depth; i++)
            node = value > node!.Value ? node.Right : node.Left;

        if (node == null)
            return new List<Tree>();

        var results = Merge(tree, depth + 1, value);

        var newNode = new Node(value);
        newNode.Left = node.Right; //Always move remaining tree to left as we insert in order
        node.Right = newNode;

        results.Add(clone);
        return results;
    }
}

public class Tree
{
    public Node Root;

    public Tree(int value)
    {
        Root = new Node(value);
    }

    private Tree(Node root)
    {
        Root = root;
    }

    public Tree Clone()
    {
        var newRoot = Root.Clone();

        return new Tree(newRoot);
    }

    public override string ToString() => Root.ToString();

    public class Node
    {
        public Node? Left;
        public Node? Right;
        public int Value;

        public Node(int value)
        {
            Value = value;
        }

        public Node Clone()
        {
            var cloned = new Node(Value);
            cloned.Left = Left?.Clone();
            cloned.Right = Right?.Clone();

            return cloned;
        }

        public override string ToString() => $"{Value} [{Left?.ToString()} | {Right?.ToString()}]";
    }
}