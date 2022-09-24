/*
	Each input is useable once
	Order must be maintained
	A result must contain all of the input values
	This is a Selection problem
	Solutions must be unique, but the +/- matters
	
	Input:
		int[] for potential numbers
		a sum to make
	Output: Number of unique equations equaling the sum
*/

public record TargetSum(int[] Numbers)
{
	public int CountPermutations(int target)
	{
		return CountPermutations(target, 0);
	}
	
	private int CountPermutations(int target, int current)
	{
		if (current == Numbers.Length)
            return target == 0 ? 1 : 0;
		
        var number = Numbers[current];
        var withPositive = CountPermutations(target - number, current + 1);
        var withNegative = CountPermutations(target + number, current + 1);
        return withPositive + withNegative;
	}
}


//
public record TargetSumCached(int[] Numbers)
{
	public int CountPermutations(int target)
	{
        var cache = new Dictionary<(int, int), int>();
		return CountPermutations(target, 0, cache);
	}
	
	private int CountPermutations(int target, int current, Dictionary<(int, int), int> cache)
	{
		if (current == Numbers.Length)
            return target == 0 ? 1 : 0;
		
        if (cache.TryGetValue((current, target), out var cached))
            return cached;
        
        var number = Numbers[current];
        var withPositive = CountPermutations(target - number, current + 1, cache);
        var withNegative = CountPermutations(target + number, current + 1, cache);
        cache[(current, target)] = withPositive + withNegative;
        return cache[(current, target)];
	}
}


//
public record TargetSumIterative(int[] Numbers)
{
	public int CountPermutations(int target)
	{
        var maxSum = Numbers.Select(x => Math.Abs(x)).Sum();
        var cache = new int[Numbers.Length + 1, maxSum * 2+1];        
		var zeroIndex = maxSum;
        
        for (var i = 1; i <= Numbers.Length; i++)
        {
            for (var sum = -maxSum; sum <= maxSum; sum++)
            {
                var number = Numbers[i-1];
                var sumIndex = sum + zeroIndex;
				
				if (i == 1)
				{
					cache[1, sumIndex] = number == Math.Abs(sum) ? 1 : 0;
					continue;
				}					
                
                var indexPositive = sumIndex + number;
                var indexNegative = sumIndex - number;
                
                int canMakePositive = 0;
                if (indexPositive > 0 && indexPositive < cache.GetLength(1))
                    canMakePositive = cache[i-1, indexPositive];
                                
                int canMakeNegative = 0;
                if (indexNegative > 0 && indexNegative < cache.GetLength(1))
                    canMakeNegative = cache[i-1, indexNegative];
                
                cache[i, sumIndex] = canMakePositive + canMakeNegative;
            }
        }
        
        return cache[Numbers.Length, target + zeroIndex];
	}
}
