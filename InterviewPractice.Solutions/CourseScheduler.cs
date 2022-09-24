public class Solution {
    
    private int numCourses;
    private Dictionary<int, List<int>> prereqs;
    
    public int[] FindOrder(int numCourses, int[][] prerequisites) {        
        this.numCourses = numCourses;
        this.prereqs = CreatePrereqs(prerequisites, numCourses);
        
        try
        {
            var results = new List<int>();
            for (var course = 0; course < numCourses; course++)
            {
                var visited = new HashSet<int>();
                results.AddRange(Visit(course, visited));
            }
            
            return results.ToArray();

        } catch (InvalidOperationException)
        {
            return new int[0];
        }
    }
    
    private List<int> Visit(int current, HashSet<int> visited)
    {
        if (!prereqs.ContainsKey(current))
            return new List<int>(); //Already used
        
        if (visited.Contains(current))
            throw new InvalidOperationException("Cycle in graph");
        
        visited.Add(current);
        
        var result = new List<int>();
        foreach (var dep in prereqs[current])
        {
            result.AddRange(Visit(dep, visited));            
        }
        
        result.Add(current);
        prereqs.Remove(current);
        
        return result;
    }
    
    private static Dictionary<int, List<int>> CreatePrereqs(int[][] input, int numCourses)
    {
        var result = new Dictionary<int, List<int>>();
        for (var i = 0; i < numCourses; i++)
            result[i] = new List<int>();
            
        foreach (var req in input)
            result[req[0]].Add(req[1]);
        
        return result;
    }
}