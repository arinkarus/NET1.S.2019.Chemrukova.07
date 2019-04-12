using System.Collections.Generic;

namespace ArrayExtension.Tests
{
    public struct TestEntityForBinarySearch<T>
    {
        public T itemToSearch;
        public IComparer<T> comparer;
        public T[] array;

        public TestEntityForBinarySearch(T[] array, T itemToSearch, IComparer<T> comparer)
        {
            this.array = array;
            this.comparer = comparer;
            this.itemToSearch = itemToSearch;
        }
    }
}
