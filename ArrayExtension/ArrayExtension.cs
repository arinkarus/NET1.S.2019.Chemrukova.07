using System;
using System.Collections.Generic;
using ArrayExtension.Filter;
using ArrayExtension.Transform;

namespace ArrayExtension
{
    /// <summary>
    /// Provides methods that helps to work with arrays.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Filters array by some predicate.
        /// </summary>
        /// <param name="array">Given array.</param>
        /// <param name="predicate">Given predicate.</param>
        /// <returns>Filtered array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when predicate is null
        /// or array is null.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        public static T[] Filter<T>(this T[] array, IPredicate<T> predicate)
        {
            ValidateArray(array);
            CheckOnNull(predicate);
            var filtered = new List<T>();
            for (int i = 0;  i < array.Length; i++)
            {
                if (predicate.IsMatch(array[i]))
                {
                    filtered.Add(array[i]);
                }
            }

            return filtered.ToArray();
        }

        /// <summary>
        /// Transforms array of double numbers to array of strings.
        /// </summary>
        /// <param name="array">Given array.</param>
        /// <param name="transformer">Given transformer.</param>
        /// <returns>Array of strings.</returns>
        /// /// <exception cref="ArgumentNullException">Thrown when transformer is null
        /// or array is null.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        public static TDest[] Transform<TSource, TDest>(this TSource[] array, ITransformer<TSource, TDest> transformer)
        {
            ValidateArray(array);
            CheckOnNull(transformer);
            var stringRepresentations = new TDest[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                stringRepresentations[i] = transformer.Transform(array[i]);
            }

            return stringRepresentations;
        }

        /// <summary>
        /// Sort array depending on passed criteria (comparer).
        /// </summary>
        /// <param name="array">Given array.</param>
        /// <param name="comparer">Given comparer.</param>
        /// <exception cref="ArgumentNullException">Thrown when comparer is null
        /// or array is null.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        public static T[] SortBy<T>(this T[] array, IComparer<T> comparer)
        {
            ValidateArray(array);
            CheckOnNull(comparer);
            var arrayToSort = new T[array.Length];
            array.CopyTo(arrayToSort, 0);
            Array.Sort(arrayToSort, comparer);
            return arrayToSort;
        }

        /// <summary>
        /// Checks array for null or emptiness. 
        /// </summary>
        /// <typeparam name="T">T type for array.</typeparam>
        /// <param name="array">Given array.</param>
        /// <exception cref="ArgumentNullException">Thrown if array is null.</exception>
        /// <exception cref="ArgumentException">Thrown if array is empty.</exception>
        public static void ValidateArray<T>(T[] array)
        {
            CheckOnNull(array);
            if (array.Length == 0)
            {
                throw new ArgumentException($"{nameof(array)} can't be empty.");
            }
        }

        private static void CheckOnNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(obj)} can't be null.");
            }
        }

        private static void SwapArrays(ref int[] a, ref int[] b)
        {
            int[] temp = a;
            a = b;
            b = temp;
        }
    }
}
