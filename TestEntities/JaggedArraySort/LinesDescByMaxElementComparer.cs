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
    public class LinesDescByMaxElementComparer : IComparer<int[]>
    {
        /// <summary>
        /// Returns 1 if max element in x is less than max element in y.
        /// </summary>
        /// <param name="x">First sz array.</param>
        /// <param name="y">Second sz array.</param>
        /// <returns>1 - if x max element is less than y max element. 0 - if equals.
        /// Otherwise it returns -1.
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

            int maxElementOfFirst = x.GetMax();
            int maxElementOfSecond = y.GetMax();
            if (maxElementOfFirst < maxElementOfSecond)
            {
                return 1;
            }

            if (maxElementOfFirst == maxElementOfSecond)
            {
                return 0;
            }

            return -1;
        }
    }
}
