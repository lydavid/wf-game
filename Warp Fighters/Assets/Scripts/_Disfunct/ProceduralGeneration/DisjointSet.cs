// DisjointSet from: http://www.mathblog.dk/disjoint-set-data-structure/
internal class DisjointSet
{

    // The number of elements in the collection.
    public int Count { get; private set; }

    // The parent of each element in the collection.
    private int[] Parent;

    // The rank of each element in the collection.
    private int[] Rank;

    // Initializes a new Disjoint-Set data structure, with the specified amount of elements in the collection.
    public DisjointSet(int count)
    {

        this.Count = count;
        this.Parent = new int[this.Count];
        this.Rank = new int[this.Count];

        // Initially, all elements are in their own set.
        for (int i = 0; i < this.Count; i++)
        {
            this.Parent[i] = i;
            this.Rank[i] = 0;
        }
    }

    // Finds the representative of the set that i is an element of
    public int Find(int i)
    {

        // If i is the parent of itself
        if (Parent[i] == i)
        {

            // Then i is the representative of his set
            return i;
        }
        else
        { // Else if i is not the parent of itself

            // Then i is not the representative of his set,
            // so we recursively call Find on it's parent, and save it in our result variable
            int result = Find(Parent[i]);

            // We then cache the result by moving i's node directly under the representative of his set
            Parent[i] = result;

            // And then we return the result
            return result;
        }
    }

    // Unites the set that includes i and the set that includes j
    public void Union(int i, int j)
    {

        // Find the representatives (or the root nodes) for the set that includes i
        int irep = this.Find(i),
        // And do the same for the set that includes j
        jrep = this.Find(j),
        // Get the rank of i's tree
        irank = Rank[irep],
        // Get the rank of j's tree
        jrank = Rank[jrep];

        // Elements are in the same set, no need to unite anything.
        if (irep == jrep)
            return;

        // If i's rank is less than j's rank
        if (irank < jrank)
        {

            // Then move i under j
            this.Parent[irep] = jrep;

        } // Else if j's rank is less than i's rank
        else if (jrank < irank)
        {

            // Then move j under i
            this.Parent[jrep] = irep;

        } // Else if their ranks are the same
        else
        {

            // Then move i under j (doesn't matter which one goes where)
            this.Parent[irep] = jrep;

            // And increment the the result tree's rank by 1
            Rank[jrep]++;
        }
    }

    // Determine whether i and j are in the same set (ie under the same rep)
    public bool SameSet(int i, int j)
    {
        return Find(i) == Find(j);
    }

}
