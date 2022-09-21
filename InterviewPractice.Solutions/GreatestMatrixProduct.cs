namespace InterviewPractice.Solutions;

public class GreatestMatrixProductNonNegative
{
    public int Find(int[][] matrix) => Search(matrix, new Cell(0, 0));

    private int Search(int[][] matrix, Cell current)
    {
        //Final square
        if (current.row == matrix.Length - 1 && current.col == matrix[0].Length - 1)
            return matrix[current.row][current.col];

        //Out of bounds
        if (current.row >= matrix.Length || current.col >= matrix[0].Length)
            return -1;

        var down = Search(matrix, current with { row = current.row + 1 });
        var right = Search(matrix, current with { col = current.col + 1 });

        var best = Math.Max(down, right);
        return best * matrix[current.row][current.col];
    }

    private record Cell(int row, int col);
}

public class GreatestMatrixProductNegative
{
    public int Find(int[][] matrix)
    {
        var results = Search(matrix, new Cell(0, 0));
        return results.Max();
    }

    private List<int> Search(int[][] matrix, Cell current)
    {
        //Final square
        if (current.row == matrix.Length - 1 && current.col == matrix[0].Length - 1)
            return new List<int> { matrix[current.row][current.col] };

        //Out of bounds
        if (current.row >= matrix.Length || current.col >= matrix[0].Length)
            return new List<int>();

        var down = Search(matrix, current with { row = current.row + 1 });
        var right = Search(matrix, current with { col = current.col + 1 });

        return down.Concat(right).Select(x => x * matrix[current.row][current.col]).ToList();
    }

    private record Cell(int row, int col);
}

public class GreatestMatrixProductCached
{
    public int Find(int[][] matrix)
    {
        var cache = new Dictionary<Cell, List<int>>();
        var results = Search(matrix, new Cell(0, 0), cache);
        return results.Max();
    }

    private List<int> Search(int[][] matrix, Cell current, Dictionary<Cell, List<int>> cache)
    {
        //Final square
        if (current.row == matrix.Length - 1 && current.col == matrix[0].Length - 1)
            return new List<int> { matrix[current.row][current.col] };

        //Out of bounds
        if (current.row >= matrix.Length || current.col >= matrix[0].Length)
            return new List<int>();

        if (cache.TryGetValue(current, out var cached))
            return cached;

        var down = Search(matrix, current with { row = current.row + 1 }, cache);
        var right = Search(matrix, current with { col = current.col + 1 }, cache);

        var products = down.Concat(right).Select(x => x * matrix[current.row][current.col]).ToList();
        cache[current] = products;
        return products;
    }

    private record Cell(int row, int col);
}

public class GreatestMatrixProductIterative
{
    public int Find(int[][] matrix)
    {
        var cache = new Dictionary<(int, int), List<int>>();

        for (var row = 0; row < matrix.Length; row++)
        {
            for (var col = 0; col < matrix[0].Length; col++)
            {
                if (row == 0 && col == 0)
                {
                    cache[(0, 0)] = new List<int> { matrix[0][0] };
                    continue;
                }

                var up = row > 0 ? cache[(row - 1, col)] : new List<int>();
                var left = col > 0 ? cache[(row, col - 1)] : new List<int>();
                var products = up.Concat(left).Select(x => x * matrix[row][col]).ToList();
                cache[(row, col)] = products;
            }
        }

        return cache[(matrix.Length - 1, matrix[0].Length - 1)].Max();
    }
}

public class GreatestMatrixProductIterative2
{
    public int Find(int[][] matrix)
    {
        var minCache = new Dictionary<(int, int), int>();
        var maxCache = new Dictionary<(int, int), int>();

        for (var row = 0; row < matrix.Length; row++)
        {
            for (var col = 0; col < matrix[0].Length; col++)
            {
                if (row == 0 && col == 0)
                {
                    minCache[(0, 0)] = matrix[0][0];
                    maxCache[(0, 0)] = matrix[0][0];
                    continue;
                }

                var previousMin = ComparePrevious(row, col, minCache, Math.Min);
                var previousMax = ComparePrevious(row, col, maxCache, Math.Max);
                
                var previousMinWithCurrent = previousMin * matrix[row][col];
                var previousMaxWithCurrent = previousMax * matrix[row][col];

                minCache[(row, col)] = Math.Min(previousMinWithCurrent, previousMaxWithCurrent);
                maxCache[(row, col)] = Math.Max(previousMinWithCurrent, previousMaxWithCurrent);
            }
        }

        var finalCell = (matrix.Length - 1, matrix[0].Length - 1);
        return Math.Max(minCache[finalCell], maxCache[finalCell]);
    }

    private int ComparePrevious(int row, int col, Dictionary<(int, int), int> cache, Func<int, int, int> compare)
    {
        var up = row > 0 ? cache[(row - 1, col)] : (int?)null;
        var left = col > 0 ? cache[(row, col - 1)] : (int?)null;

        if (up == null)
            return left!.Value;

        if (left == null)
            return up.Value;

        return compare(up.Value, left.Value);
    }
}