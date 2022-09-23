//
public List<int> MakeChange(int[] denomintations, int total)
{
	var combinations = new List<List<int>>();
    FindCombinations(denomintations, total, new List<int>(), combinations);
    return combinations.MinBy(x => x.Count);
}

private void FindCombinations(int[] denominations, int total, List<int> path, List<List<int>> results)
{
    if (total == 0)
    {
        results.Add(path);
        return;
    }
    
    if (total < 0)
        return;
    
    foreach (var denomination in denominations)
    {
        var pathWithCurrent = new List<int>(path);
        pathWithCurrent.Add(denomination);
        FindCombinations(denominations, total - denomination, pathWithCurrent, results);
    }
}


//
public List<int> MakeChange2(int[] denomintations, int total)
{
    var options = FindCombinations(denomintations, total, new List<int>());
    return options.MinBy(x => x.Count);
}

private List<List<int>> FindCombinations(int[] denominations, int total, List<int> path)
{
    if (total == 0)
        return new List<List<int>> { path };
    
    if (total < 0)
        return new List<List<int>>();
    
    var results = new List<List<int>>();
    foreach (var denomination in denominations)
    {
        var pathWithCurrent = new List<int>(path);
        pathWithCurrent.Add(denomination);
        var newCombinations = FindCombinations(denominations, total - denomination, pathWithCurrent);
        results.AddRange(newCombinations);
    }
    
    return results;
}


//
public List<int> MakeChange3(int[] denomintations, int total)
{
    var options = FindCombinations(denomintations, total, new List<int>());
    return options.MinBy(x => x.Count);
}

private List<List<int>> FindCombinations(int[] denominations, int total, List<int> path)
{
    if (total == 0)
        return new List<List<int>> { path };
    
    if (total < 0)
        return new List<List<int>>();
    
    return denominations
        .SelectMany(SelectCoin)
        .ToList()
    
  
    List<List<int>> SelectCoin(int value)
    {    
        var pathWithCurrent = new List<int>(path);
        pathWithCurrent.Add(denomination);
        return FindCombinations(denominations, total - value, pathWithCurrent);
    }
}


//
public List<int> MakeChangeCached(int[] denomintations, int total)
{
    var cache = new List<List<int>>[total+1];
    var options = FindCombinations(denomintations, total, cache);
    return options.MinBy(x => x.Count);
}

private List<List<int>> FindCombinations(int[] denominations, int total, List<List<int>>[] cache)
{
    if (total == 0)
        return new List<List<int>> { new List<int>() };
    
    if (total < 0)
        return new List<List<int>>();
    
    if (cache[total] != null)
        return cache[total];
    
    var results = denominations
        .SelectMany(SelectCoin)
        .ToList();
        
    cache[total] = results;
    return results;
    
  
    List<List<int>> SelectCoin(int value)
    {    
		
    	if (cache[total] != null)
        	return cache[total];
		
        var combinations = FindCombinations(denominations, total - value, cache);
        return combinations.Select(combination =>
		{
			var withCurrent = combination.ToList();
			withCurrent.Add(value);
			return withCurrent;
		}).ToList();
    }
}


//
public int? MinimumCoins(int[] denominations, int total)
{
    if (total == 0)
        return 0;
    
    if (total < 0)
        return null;
    
    int? minimumCoins = null;
    foreach (var value in denominations)
    {
        var result = MinimumCoins(denominations, total - value);
        if (result == null)
            continue;
        
        minimumCoins = Math.Min(result.Value, minimumCoins ?? int.MaxValue);        
    }
    
    return minimumCoins + 1;
}

private int? MinimumCoinsCached(int[] denominations, int total)
{
    if (total == 0)
        return 0;
    
    if (total < 0)
        return null;
    
    int? minimumCoins = null;
    foreach (var value in denominations)
    {
        var result = MinimumCoins(denominations, total - value);
        if (result == null)
            continue;
        
        minimumCoins = Math.Min(result.Value, minimumCoins ?? int.MaxValue);        
    }
    
    return minimumCoins + 1;
}


//
public int? MinimumCoinsCached(int[] denominations, int total)
{
    return Count(denominations, total, new int[total+1]);
}

private int? Count(int[] denominations, int total, int[] cache)
{
    if (total == 0)
        return 0;
    
    if (total < 0)
        return null;
    
    if (cache[total] != null)
        return cache[total];
    
    int? minimumCoins = null;
    foreach (var value in denominations)
    {
        var result = Count(denominations, total - value, cache);
        if (result == null)
            continue;
        
        minimumCoins = Math.Min(result.Value, minimumCoins ?? int.MaxValue);        
    }
    
    var coinsForTotal = minimumCoins + 1;
    cache[total] = coinsForTotal;
    return coinsForTotal;
}


//
public int? MinimumCoinsIterative(int[] denominations, int total)
{
    var cache = new int?[total + 1];
    
    for (var value = 1; value <= total; value++)
    {
        int? minCoins = null;
        
        foreach (var denomination in denominations)
        {
            var remaining = value - denomination;
            if (remaining < 0)
                continue;
            
            if (remaining == 0)
            {
                minCoins = 0;
                break;
            }
            
            var minForRemaining = cache[remaining];
            if (minForRemaining != null)
                minCoins = Math.Min(minForRemaining.Value, minCoins ?? int.MaxValue);
        }
        
        if (minCoins != null)
            cache[value] = minCoins + 1;
    }
    
    return cache[total];
}