public class Solution {
    public int[] FindRedundantConnection(int[][] edges) {
     
        var visited = new HashSet<int>();
        return Traverse(edges, 0, visited);
    }
    
    private int[] Traverse(int[][] graph, int current, HashSet<int> visited)
    {
        var next = graph[current];
        var from = next[0];
        var to = next[1];        
        
        if (visited.Contains(to))
            return next;
        
        visited.Add(from);
        visited.Add(to);
        return Traverse(graph, current + 1, visited);
    }
}