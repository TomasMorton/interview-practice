public class InterleavingFlattener{

    private readonly Iterator<T>[] iterators;
    private int nextIterator;
    
    
    public IF(Iterator<T>[] iterlist){
        iterators = iterlist;
        nextIterator = 0;
    }
  
    public T next(){
        for (var i = 0; i < iterators.Length; i++)
        {
            var next = i + nextIterator % iterators.Length;
            nextIterator++;
            
            var iterator = iterators[next];
            if (iterator.hasNext()
            {
                return iterator.next();
            }
        }
        
        return null;
    }
  
    public boolean hasNext(){
        for (var i = 0; i < iterators.Length; i++)
        {
            var next = i + nextIterator % iterators.Length;
            nextIterator++;
            
            var iterator = iterators[next];
            if (iterator.hasNext()
            {
                return true;
            }
        }
        
        return false;
    }
}
    