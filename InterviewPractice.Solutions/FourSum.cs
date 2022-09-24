public class Solution {
    public IList<IList<int>> FourSum(int[] nums, int target) {
        
        var results = new List<IList<int>>();
        Search(nums, 0, target, new List<int>(), results);
        
        return results
            .Select(x => x.OrderBy(y=>y))
            .GroupBy(x => string.Join(",", x))
            .Select(x => (IList<int>)x.First().ToList())
            .ToList();
    }
    
    private void Search(int[] nums, int n, int target, IList<int> path, List<IList<int>> results)
    {
        if (path.Count == 4)
        {
            if (target == 0)
                results.Add(path);
            return;
        }
        
        if (n == nums.Length)
            return;
        
        //Without current
        Search(nums, n + 1, target, path, results);
        
        //With current
        var current = nums[n];
        var pathWithCurrent = new List<int>(path);
        pathWithCurrent.Add(current);
        Search(nums, n + 1, target - current, pathWithCurrent, results);
    }
}

//TODO: Cache and iterate