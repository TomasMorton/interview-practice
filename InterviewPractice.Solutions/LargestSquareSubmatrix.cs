public int LargestSquareSubmatrix(bool[][] matrix)
{  
    var biggestSquarePossible = Math.Min(matrix.Length, matrix[0].Length);
    var results = new List<int>();
    
    for (var row = 0; row <= matrix.Length - biggestSquarePossible; row++)
    {
        for (var col = 0; col <= matrix[0].Length - biggestSquarePossible; col++)
        {
            var submatrix = CreateSubmatrix(matrix, row, col, biggestSquarePossible);
            results.Add(Search(submatrix, biggestSquarePossible));
        }
    }
    
    return results.Max();
}

private int Search(bool[][] matrix, int squareSize)
{
    if (matrix.Length == 1 && matrix[0].Length == 1)
        return matrix[0][0] ? 1 : 0;
        
        
    int biggest = int.MinValue;
    int smallest = int.MaxValue;
	var subsquareSize = squareSize - 1;
    for (var row = 0; row <= matrix.Length - subsquareSize; row++)
    {
        for (var col = 0; col <= matrix[0].Length - subsquareSize; col++)
        {
            var submatrix = CreateSubmatrix(matrix, row, col, subsquareSize);
            var result = Search(submatrix, subsquareSize);
            biggest = Math.Max(biggest, result);
            smallest = Math.Min(smallest, result);
        }
    }
    
    //All submatrixes are valid
    if (smallest == subsquareSize)
        return squareSize;
        
    return biggest;
}

private bool[][] CreateSubmatrix(bool[][] matrix, int fromRow, int fromCol, int size)
{
	var result = new bool[size][];
	for (var rowOffset = 0; rowOffset < size; rowOffset++)
	{
		var colResult = matrix[fromRow + rowOffset][fromCol..(fromCol+size)];
		result[rowOffset] = colResult;
	}
    return result;
}


//
public int LargestSquareSubmatrix2(bool[,] matrix)
{  
    int max = 0;
    
    for (var row = 0; row < matrix.GetLength(0); row++)
    {
        for (var col = 0; col < matrix.GetLength(1); col++)
        {
			if (!matrix[row, col])
				continue;
			
            var result = Search(matrix, row, col);
            max = Math.Max(max, result);
        }
    }
    
    return max;
}

public int Search(bool[,] matrix, int fromRow, int fromCol)
{
    if (fromRow == matrix.GetLength(0) || fromCol == matrix.GetLength(1))
        return 0;

	if (!matrix[fromRow, fromCol])
		return 0;
	
    var right = Search(matrix, fromRow, fromCol + 1);
    var bottom = Search(matrix, fromRow + 1, fromCol);
    var bottomRight = Search(matrix, fromRow + 1, fromCol + 1);
    
    var smallestSubsquare = Math.Min(right, Math.Min(bottom, bottomRight));
    return 1 + smallestSubsquare;
}


//
public int LargestSquareSubmatrixCached(bool[,] matrix)
{  
    int max = 0;
    
    var cache = new int?[matrix.GetLength(0), matrix.GetLength(1)];
    
    for (var row = 0; row < matrix.GetLength(0); row++)
    {
        for (var col = 0; col < matrix.GetLength(1); col++)
        {
			if (!matrix[row, col])
				continue;
			
            var result = Search(matrix, row, col, cache);
            max = Math.Max(max, result);
        }
    }
    
    return max;
}

public int Search(bool[,] matrix, int fromRow, int fromCol, int?[,] cache)
{
    if (fromRow == matrix.GetLength(0) || fromCol == matrix.GetLength(1))
        return 0;

	if (!matrix[fromRow, fromCol])
		return 0;
	
    if (cache[fromRow, fromCol] != null)
        return cache[fromRow, fromCol].Value;
    
    var right = Search(matrix, fromRow, fromCol + 1, cache);
    var bottom = Search(matrix, fromRow + 1, fromCol, cache);
    var bottomRight = Search(matrix, fromRow + 1, fromCol + 1, cache);
    
    var smallestSubsquare = Math.Min(right, Math.Min(bottom, bottomRight));
    cache[fromRow, fromCol] = 1 + smallestSubsquare;
    return cache[fromRow, fromCol].Value;
}


//
public int LargestSquareSubmatrixIterative(bool[,] matrix)
{ 
    var max = 0;    
    var cache = new int[matrix.GetLength(0), matrix.GetLength(1)];    
    
    for (var row = 0; row < matrix.GetLength(0); row++)
    {
        for (var col = 0; col < matrix.GetLength(1); col++)
        {
        
            if (row == 0 || col == 0)
            {
                cache[row, col] = matrix[row, col] ? 1 : 0;
                continue;
            }
            
            if (!matrix[row, col])
                continue;
            
            var left = cache[row, col - 1];                
            var top = cache[row - 1, col];                
            var topLeft = cache[row - 1, col - 1];
                                
            var smallestSubsquare = Math.Min(left, Math.Min(top, topLeft));
            cache[row, col] = 1 + smallestSubsquare;            
            
            max = Math.Max(max, cache[row, col]);
        }        
    }
    
    return max;
}