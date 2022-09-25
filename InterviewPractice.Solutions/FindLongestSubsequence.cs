/*
    generate all possible words and check if in dictionary
        good if word is small and dictionary is long
    
    go through each word in dictionary and match the characters in order
        prefix tree can eliminate multiple words at once
        good if diciontary is small and word is long

*/

public string? LongestSubsequence(string s, HashSet<string> dictionary)
{
    return FindWord(s, dictionary);
}

private string? FindWord(string s, HashSet<string> dictionary)
{
    if (s == "")
        return null;
        
    if (dictionary.Contains(s))
        return s;
        
    string? longestMatch = null;
    for (var i = 0; i < s.Length; i++)
    {
        var withoutLetter = FindWord(s[0..i] + s[(i + 1)..], dictionary);
        if (withoutLetter == null)
            continue;
            
        if (longestMatch == null || longestMatch.Length < withoutLetter.Length)
            longestMatch = withoutLetter;
    }
    
    return longestMatch;    
}


//
public string? LongestSubsequenceCached(string s, HashSet<string> dictionary)
{
    var cache = new Dictionary<string, string?>();
    return FindWord(s, dictionary, cache);
}

private string? FindWord(string s, HashSet<string> dictionary, Dictionary<string, string?> cache)
{
    if (s == "")
        return null;
        
    if (dictionary.Contains(s))
        return s;
        
    if (cache.TryGetValue(s, out var cached))
        return cached;
        
    string? longestMatch = null;
    for (var i = 0; i < s.Length; i++)
    {
        var withoutLetter = FindWord(s[0..i] + s[(i + 1)..], dictionary, cache);
        if (withoutLetter == null)
            continue;
            
        if (longestMatch == null || longestMatch.Length < withoutLetter.Length)
            longestMatch = withoutLetter;
    }
    
    cache[s] = longestMatch;
    return longestMatch;    
}


//
public string? LongestSubsequenceeIterative(string s, HashSet<string> dictionary)
{
    var cache = new HashSet<string>[s.Length + 1];
    
    string? longestWord = null;
    
    for (var char = 0; char < s.Length; char++)
    {
        cache[numChars] = new HashSet<string>();        
    
        if (numChars == 1)
        {
            foreach (var
            var word = s[numChars];            
            cache[numChars] = dictionary.ContainsKey(word) ? 1 : 0;
            continue;
        } 
        foreach (var word in cache[numChars-1])
        {
        }
    }
      
        
        
    }
 
    
}