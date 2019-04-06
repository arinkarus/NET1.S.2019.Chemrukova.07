using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEntities.JaggedArraySort
{
    /// <summary>
    /// Comparer for sorting jagged array depending on lines.
    /// </summary>
    public class LinesAscByMinElementComparer : IComparer<int[]>
    {
        /// <summary>
        /// Returns 1 if min element in x is greater than min element in y.
        /// </summary>
        /// <param name="x">First sz array.</param>
        /// <param name="y">Second sz array.</param>
        /// <returns>1 - if x min element is greater than y min element. 0 - if equals.
        /// Otherwise it returns -1.
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

            int minElementOfFirst = x.GetMin();
            int minElementOfSecond = y.GetMin();
            if (minElementOfFirst > minElementOfSecond)
            {
                return 1;
            }

            if (minElementOfFirst == minElementOfSecond)
            {
                return 0;
            }

            return -1;
        }
    }
}
