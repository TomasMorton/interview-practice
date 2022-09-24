// unit of work is each word
// word[0] through word[0..] must be checked

public string ReplaceSuccessors(HashSet<string> roots, string sentence)
{
    var newWords =
        sentence
            .Split(" ")
            .Select(word => ReplaceSuccessor(roots, word));
        
    return string.Join(" ", newWords);
}

private string ReplaceSuccessor(HashSet<string> roots, string word)
{
    for (var length = 1; length <= word.Length; length++)
    {
        var potentialRoot = word[0..length];
        if (roots.Contains(potentialRoot))
            return potentialRoot;
    }
        
    return word;
}