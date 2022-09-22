Iteration
    if (input.Length == 0) return;    
    Recurse(input[1..])

Selection
    if (condition) return
    if (input.Length == 0) return
    Recurse(withCurrent)
    Recurse(withoutCurrent)

Ordering
    if (input.Length == 0) return
    foreach (var item in input)
        Recurse(solution+item, input-item)

Depth-First
    if (end) return
    foreach (var option in options)
        Recurse()

Subproblems
    if (n == 0) return a solution
    var next = Recurse(n-1)
    return next + logic-for-current-solution

Divide and Conquer
    if (input.Length == 0) return
    var dividedA = Recurse(input[0..mid])
    var dividedB = Recurse(input[mid..])
    return merge(dividedA, dividedB)