using ArrayExtension;
using System;

namespace TestEntities.JaggedArraySort
{
    /// <summary>
    /// Contains helper methods to work with array's elements.
    /// </summary>
    public static class CalculationHelper
    {
        /// <summary>
        /// Gets sum of elements for array.
        /// </summary>
        /// <param name="array">Given array.</param>
        /// <returns>Sum of elements.</returns>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        public static int Sum(this int[] array)
        {
            ArrayExtension.ArrayExtension.ValidateArray(array);
            int sum = 0;
            foreach (var item in array)
            {
                sum += item;
            }

            return sum;
        }

        /// <summary>
        /// Gets max element from given array.
        /// </summary>
        /// <param name="array">Given array.</param>
        /// <returns>Max element.</returns>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        public static int GetMax(this int[] array)
        {
            ArrayExtension.ArrayExtension.ValidateArray(array);
            int max = array[0];
            for (int i = 0;  i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }

            return max;
        }


        /// <summary>
        /// Gets min element from given array.
        /// </summary>
        /// <param name="array">Given array.</param>
        /// <returns>Min element.</returns>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        public static int GetMin(this int[] array)
        {
            ArrayExtension.ArrayExtension.ValidateArray(array);
            int min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                }
            }

            return min;
        }
    }
}
