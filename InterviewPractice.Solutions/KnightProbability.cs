namespace InterviewPractice.Solutions;

public class KnightProbability
{
    public double Calculate(int boardSize, Cell start, int numberOfMoves) => Move(boardSize, start, numberOfMoves);

    private double Move(int boardSize, Cell from, int movesLeft)
    {
        if (!IsOnBoard(boardSize, from))
            return 0d;

        if (movesLeft == 0)
            return 1d;

        var probabilities = new[]
        {
            Move(boardSize, from.NNE(), movesLeft - 1),
            Move(boardSize, from.NNW(), movesLeft - 1),
            Move(boardSize, from.ENE(), movesLeft - 1),
            Move(boardSize, from.ESE(), movesLeft - 1),
            Move(boardSize, from.SSE(), movesLeft - 1),
            Move(boardSize, from.SSW(), movesLeft - 1),
            Move(boardSize, from.WSW(), movesLeft - 1),
            Move(boardSize, from.WNW(), movesLeft - 1),
        };

        return probabilities.Sum() / 8;
    }

    private bool IsOnBoard(int boardSize, Cell cell)
        => 0 <= cell.row && cell.row < boardSize &&
           0 <= cell.col && cell.col < boardSize;

    public record Cell(int row, int col)
    {
        public Cell NNE() => new(row - 2, col + 1);
        public Cell NNW() => new(row - 2, col - 1);

        public Cell ENE() => new(row - 1, col + 2);
        public Cell ESE() => new(row + 1, col + 2);

        public Cell SSE() => new(row + 2, col + 1);
        public Cell SSW() => new(row + 2, col - 1);

        public Cell WSW() => new(row + 1, col - 2);
        public Cell WNW() => new(row - 1, col - 2);
    }
}

public class KnightProbabilityCached
{
    public double Calculate(int boardSize, Cell start, int numberOfMoves) => Move(boardSize, start, numberOfMoves, new Dictionary<(Cell, int), double>());

    private double Move(int boardSize, Cell from, int movesLeft, Dictionary<(Cell, int), double> cache)
    {
        if (!IsOnBoard(boardSize, from))
            return 0d;

        if (movesLeft == 0)
            return 1d;

        if (cache.TryGetValue((from, movesLeft), out var probability))
            return probability;

        var probabilities = new[]
        {
            Move(boardSize, from.NNE(), movesLeft - 1, cache),
            Move(boardSize, from.NNW(), movesLeft - 1, cache),
            Move(boardSize, from.ENE(), movesLeft - 1, cache),
            Move(boardSize, from.ESE(), movesLeft - 1, cache),
            Move(boardSize, from.SSE(), movesLeft - 1, cache),
            Move(boardSize, from.SSW(), movesLeft - 1, cache),
            Move(boardSize, from.WSW(), movesLeft - 1, cache),
            Move(boardSize, from.WNW(), movesLeft - 1, cache),
        };

        var result = probabilities.Sum() / 8;
        cache[(from, movesLeft)] = result;

        return result;
    }

    private bool IsOnBoard(int boardSize, Cell cell)
        => 0 <= cell.row && cell.row < boardSize &&
           0 <= cell.col && cell.col < boardSize;

    public record Cell(int row, int col)
    {
        public Cell NNE() => new(row - 2, col + 1);
        public Cell NNW() => new(row - 2, col - 1);

        public Cell ENE() => new(row - 1, col + 2);
        public Cell ESE() => new(row + 1, col + 2);

        public Cell SSE() => new(row + 2, col + 1);
        public Cell SSW() => new(row + 2, col - 1);

        public Cell WSW() => new(row + 1, col - 2);
        public Cell WNW() => new(row - 1, col - 2);
    }
}

public class KnightProbabilityIterative
{
    public double Calculate(int boardSize, Cell start, int numberOfMoves)
    {
        var cache = new Dictionary<(Cell, int), double>();

        for (var move = 1; move <= numberOfMoves; move++)
        {
            for (var row = 0; row < boardSize; row++)
            {
                for (var col = 0; col < boardSize; col++)
                {
                    var from = new Cell(row, col);
                    var probabilities = new[]
                    {
                        CalculateProbabilityOf(boardSize, from.NNE(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.NNW(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.ENE(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.ESE(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.SSE(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.SSW(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.WSW(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.WNW(), move - 1, cache),
                    };
                    var probability = probabilities.Sum() / 8;
                    cache[(from, move)] = probability;
                }
            }
        }

        return cache[(start, numberOfMoves)];
    }

    private double CalculateProbabilityOf(int boardSize, Cell cell, int move, Dictionary<(Cell, int), double> cache)
    {
        if (!IsOnBoard(boardSize, cell))
            return 0d;

        if (move == 0)
            return 1d;

        return cache[(cell, move)];
    }

    private bool IsOnBoard(int boardSize, Cell cell)
        => 0 <= cell.row && cell.row < boardSize &&
           0 <= cell.col && cell.col < boardSize;

    public record Cell(int row, int col)
    {
        public Cell NNE() => new(row - 2, col + 1);
        public Cell NNW() => new(row - 2, col - 1);

        public Cell ENE() => new(row - 1, col + 2);
        public Cell ESE() => new(row + 1, col + 2);

        public Cell SSE() => new(row + 2, col + 1);
        public Cell SSW() => new(row + 2, col - 1);

        public Cell WSW() => new(row + 1, col - 2);
        public Cell WNW() => new(row - 1, col - 2);
    }
}

public class KnightProbabilityIterative2
{
    public double Calculate(int boardSize, Cell start, int numberOfMoves)
    {
        var cache = new Dictionary<Cell, double>();

        if (numberOfMoves == 0)
            return CalculateProbabilityOf(boardSize, start, 0, cache);

        for (var move = 1; move <= numberOfMoves; move++)
        {
            var nextCache = new Dictionary<Cell, double>();
            for (var row = 0; row < boardSize; row++)
            {
                for (var col = 0; col < boardSize; col++)
                {
                    var from = new Cell(row, col);
                    var probabilities = new[]
                    {
                        CalculateProbabilityOf(boardSize, from.NNE(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.NNW(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.ENE(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.ESE(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.SSE(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.SSW(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.WSW(), move - 1, cache),
                        CalculateProbabilityOf(boardSize, from.WNW(), move - 1, cache),
                    };
                    var probability = probabilities.Sum() / 8;
                    nextCache[from] = probability;
                }
            }

            cache = nextCache;
        }

        return cache[start];
    }

    private double CalculateProbabilityOf(int boardSize, Cell cell, int move, Dictionary<Cell, double> cache)
    {
        if (!IsOnBoard(boardSize, cell))
            return 0d;

        if (move == 0)
            return 1d;

        return cache[cell];
    }

    private bool IsOnBoard(int boardSize, Cell cell)
        => 0 <= cell.row && cell.row < boardSize &&
           0 <= cell.col && cell.col < boardSize;

    public record Cell(int row, int col)
    {
        public Cell NNE() => new(row - 2, col + 1);
        public Cell NNW() => new(row - 2, col - 1);

        public Cell ENE() => new(row - 1, col + 2);
        public Cell ESE() => new(row + 1, col + 2);

        public Cell SSE() => new(row + 2, col + 1);
        public Cell SSW() => new(row + 2, col - 1);

        public Cell WSW() => new(row + 1, col - 2);
        public Cell WNW() => new(row - 1, col - 2);
    }
}