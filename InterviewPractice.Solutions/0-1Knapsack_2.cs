public int OptimiseKnapsack(Item[] items, int maxWeight)
{
    return Optimise(items, maxWeight, new List<Item>());
}

private int Optimise(Item[] items, int weight, List<Item> selections)
{
    if (weight < 0)
        return 0;

    if (items.Length == 0)
        return selections.Sum(x => x.Value);
        
    var currentItem = items[0];
    var remainingItems = items[1..];
    
    var withoutCurrentItem = Optimise(remainingItems, weight, selections);
    
    var selectionsWithCurrent = new List<Item>(selections);
    selectionsWithCurrent.Add(currentItem);
    var withCurrentItem = Optimise(remainingItems, weight - currentItem.Weight, selectionsWithCurrent);
    
    return Math.Max(withoutCurrentItem, withCurrentItem);
}

private record Item(int Weight, int Value);


//
public int OptimiseKnapsack2(Item[] items, int maxWeight)
{
    return Optimise(items, maxWeight, 0);
}

private int Optimise(Item[] items, int remainingWeight, int totalValue)
{
    if (remainingWeight < 0)
        return 0;

    if (items.Length == 0)
        return totalValue;
        
    var currentItem = items[0];
    var remainingItems = items[1..];
    
    var withoutCurrentItem = Optimise(remainingItems, remainingWeight, totalValue);
    
    var withCurrentItem = Optimise(remainingItems, remainingWeight - currentItem.Weight, totalValue +currentItem.Value);
    
    return Math.Max(withoutCurrentItem, withCurrentItem);
}

private record Item(int Weight, int Value);


//
public int OptimiseKnapsackCached(Item[] items, int maxWeight)
{
    var cache = new int?[items.Length + 1,maxWeight + 1];
    return Optimise(items, maxWeight, 0, cache);
}

private int Optimise(Item[] items, int remainingWeight, int totalValue, int?[,] cache)
{
    if (remainingWeight < 0)
        return 0;

    if (items.Length == 0)
        return totalValue;
        
    if (cache[items.Length, remainingWeight] != null)
        return cache[items.Length, remainingWeight].Value;
        
    var currentItem = items[0];
    var remainingItems = items[1..];
    
    var withoutCurrentItem = Optimise(remainingItems, remainingWeight, totalValue, cache);
    
    var withCurrentItem = Optimise(remainingItems, remainingWeight - currentItem.Weight, totalValue +currentItem.Value, cache);
    
    cache[items.Length, remainingWeight] = Math.Max(withoutCurrentItem, withCurrentItem);
    return cache[items.Length, remainingWeight].Value;
}

private record Item(int Weight, int Value);



//
public int OptimiseKnapsackIterative(Item[] items, int maxWeight)
{
    var cache = new int[items.Length + 1,maxWeight + 1];
    for (var itemCount = 1; itemCount <= items.Length; itemCount++)
    {
        for (var weight = 1; weight <= maxWeight; weight++)
        {
			var item = items[itemCount-1];
			
			var valueWithoutItem = cache[itemCount-1, weight];
			var valueWithItem = weight - item.Weight >= 0 ?
				cache[itemCount - 1, weight - item.Weight] + item.Value
				: 0;
			cache[itemCount, weight] = Math.Max(valueWithoutItem, valueWithItem);
			  
        }
    }
    
    return cache[items.Length, maxWeight];
}

private record Item(int Weight, int Value);


//
public int OptimiseKnapsackIterative2(Item[] items, int maxWeight)
{
    var cache = new int[maxWeight + 1];
    for (var itemCount = 1; itemCount <= items.Length; itemCount++)
    {
        var newCache = new int[maxWeight + 1];
        for (var weight = 1; weight <= maxWeight; weight++)
        {
			var item = items[itemCount-1];            
			
			var valueWithoutItem = cache[weight];
			var valueWithItem = weight - item.Weight >= 0
                ? cache[weight - item.Weight] + item.Value
				: 0;
			newCache[weight] = Math.Max(valueWithoutItem, valueWithItem);
			  
        }
        cache = newCache;
    }
    
    return cache[maxWeight];
}

private record Item(int Weight, int Value);