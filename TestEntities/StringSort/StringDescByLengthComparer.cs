using System.Collections.Generic;

namespace TestEntities.StringSort
{
    /// <summary>
    /// Comparer for strings that is based on string lengths.
    /// </summary>
    public class StringDescByLengthComparer : IComparer<string>
    {
        /// <summary>
        /// Returns 0 if strings have equal length, 1 - first string has smaller length than 
        /// second, otherwise - -1.
        /// </summary>
        /// <param name="x">First string.</param>
        /// <param name="y">Second string.</param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (y == null)
            {
                return -1;
            }

            if (x == null)
            {
                return 1;
            }

            if (x.Length < y.Length)
            {
                return 1;
            }

            if (x.Length == y.Length)
            {
                return 0;
            }

            return -1;
        }
    }  
}
