namespace InterviewPractice.Solutions;

public class StairSteps
{
    public int GetUniquePaths(int numberOfSteps)
    {
        if (numberOfSteps <= 2)
            return numberOfSteps;

        return GetUniquePaths(numberOfSteps - 1) + GetUniquePaths(numberOfSteps - 2);
    }

    public int GetUniquePathsIterative(int numberOfSteps)
    {
        if (numberOfSteps <= 2)
            return numberOfSteps;

        var last = 2;
        var secondLast = 1;

        for (var step = 3; step <= numberOfSteps; step++)
        {
            var currentTotal = last + secondLast;
            secondLast = last;
            last = currentTotal;
        }

        return last;
    }
}