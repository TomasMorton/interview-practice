/*
	Each input is useable once
	~~A result may be smaller than the input length, but could be extended to another result~~
	Edit: A result must contain all of the input values - moving to another file
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
		var results = new List<List<int>>();
		CountPermutations(target, 0, new (), results);
		
		var equations = results.Select(list => string.Join(" ", list)).Distinct();
		foreach (var equation in equations)
			Console.WriteLine(equation);
		
		return equations.Count();
	}
	
	private void CountPermutations(int target, int current, List<int> path, List<List<int>> results)
	{
		if (target == 0)
			results.Add(path);
		
		if (current == Numbers.Length)
		{
			if (target == 0)
				results.Add(path);
		
			return;
		}
		
		//Without current
		CountPermutations(target, current + 1, path, results);
		
		var currentValue = Numbers[current];
		//With + current
		var pathWithPositive = new List<int>(path);
		pathWithPositive.Add(currentValue);
		CountPermutations(target - currentValue, current + 1, pathWithPositive, results);
		
		//With - current
		var pathWithNegative = new List<int>(path);
		pathWithNegative.Add(-currentValue);
		CountPermutations(target + currentValue, current + 1, pathWithNegative, results);
	}
}

