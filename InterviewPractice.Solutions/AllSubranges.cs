namespace InterviewPractice.Solutions;

public class AllSubranges
{
    public static List<string> Find(string s)
    {
        if (s.Length == 0)
            return new List<string>();

        var result = Find(s[1..]);
        var nextCharacter = s[0];
        var withNextCharacter = result.Select(x => nextCharacter + x);
        return new[] { nextCharacter.ToString() }.Concat(withNextCharacter).Concat(result).ToList();
    }
}