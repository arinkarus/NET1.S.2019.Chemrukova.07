using System.Collections.Generic;
using System.Linq;

namespace TestEntities.JaggedArraySort
{
    /// <summary>
    /// Comparer for sorting jagged array depending on lines.
    /// </summary>
    public class LinesAscBySumComparer : IComparer<int[]>
    {
        /// <summary>
        /// Returns 1 if sum of elements in first array is greater that in second.
        /// </summary>
        /// <param name="x">First array.</param>
        /// <param name="y">Second array.</param>
        /// <returns>1 - if sum of elements in x array is greater than 
        /// sum in y array. 0 - if summaries are equal. Otherwise it returns -1.
        /// </returns>
        public int Compare(int[] x, int[] y)
        {
            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            if (x == y)
            {
                return 0;
            }

            return x.Sum() - y.Sum();
        }
    }
}
