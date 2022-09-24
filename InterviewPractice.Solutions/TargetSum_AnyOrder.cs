/*
	Each input is useable once
	A result must contain all of the input values
	~~This is an Ordering problem~~
	Edit: Order cannot change. Moving to another file.
	Solutions must be unique, but the +/- matters
	
	Input:
		int[] for potential numbers
		a sum to make
	Output: Number of unique equations equaling the sum
*/

public class TargetSum()
{
	public int CountPermutations(int[] numbers, int target)
	{
		var results = new List<List<int>>();
		CountPermutations(numbers, target, new (), results);
		
		var equations = results.Select(list => string.Join(" ", list)).Distinct();
		foreach (var equation in equations)
			Console.WriteLine(equation);
		
		return equations.Count();
	}
	
	private void CountPermutations(int[] numbers, int target, List<int> path, List<List<int>> results)
	{
		if (numbers.Length == 0)
		{
			if (target == 0)
				results.Add(path);
			
			return;
		}
		
		for (var i = 0; i < numbers.Length; i++)
		{
			var number = numbers[i];
			var remaining = numbers[0..i].Concat(numbers[(i+1)..]).ToArray();
			
			var pathWithPositive = new List<int>(path);
			pathWithPositive.Add(number);
			CountPermutations(remaining, target - number, pathWithPositive, results);
			
			var pathWithNegative = new List<int>(path);
			pathWithNegative.Add(-number);
			CountPermutations(remaining, target + number, pathWithNegative, results);
		}
	}
}

public class TargetSumCached()
{
	public int CountPermutations(int[] numbers, int target)
	{
		var results = new List<List<int>>();
		CountPermutations(numbers, target, new (), results);
		
		var equations = results.Select(list => string.Join(" ", list)).Distinct();
		foreach (var equation in equations)
			Console.WriteLine(equation);
		
		return equations.Count();
	}
	
	private void CountPermutations(int[] numbers, int target, List<int> path, List<List<int>> results)
	{
		if (numbers.Length == 0)
		{
			if (target == 0)
				results.Add(path);
			
			return;
		}
		
		for (var i = 0; i < numbers.Length; i++)
		{
			var number = numbers[i];
			var remaining = numbers[0..i].Concat(numbers[(i+1)..]).ToArray();
			
			var pathWithPositive = new List<int>(path);
			pathWithPositive.Add(number);
			CountPermutations(remaining, target - number, pathWithPositive, results);
			
			var pathWithNegative = new List<int>(path);
			pathWithNegative.Add(-number);
			CountPermutations(remaining, target + number, pathWithNegative, results);
		}
	}
}

