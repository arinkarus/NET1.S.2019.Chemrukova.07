using System.Collections.Generic;

namespace TestEntities.JaggedArraySort
{
    /// <summary>
    /// Comparer for sorting jagged array depending on lines.
    /// </summary>
    public class LinesDescBySumComparer : IComparer<int[]>
    {
        /// <summary>
        /// Returns 1 if sum of elements in first array is less that in second.
        /// </summary>
        /// <param name="x">First array.</param>
        /// <param name="y">Second array.</param>
        /// <returns>1 - if sum of elements in x array is less than 
        /// sum in y array. 0 - if summaries are equal. Otherwise it returns -1.
        /// </returns>
        public int Compare(int[] x, int[] y)
        {
            if (x == null)
            {
                return 1;
            }

            if (y == null)
            {
                return -1;
            }

            if (x == y)
            {
                return 0;
            }

            int sumOfFirstArray = x.Sum();
            int sumOfSecondArray = y.Sum();
            if (sumOfFirstArray < sumOfSecondArray)
            {
                return 1;
            }

            if (sumOfFirstArray == sumOfSecondArray)
            {
                return 0;
            }

            return -1;
        }
    }
}
