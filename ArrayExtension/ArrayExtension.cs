using System;
using System.Collections;
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
        /// <typeparam name="T">Given type.</typeparam>
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
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDest">Destination type.</typeparam>
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
        /// Return sorted array depending on passed criteria (comparer).
        /// </summary>
        /// <typeparam name="T">Given type.</typeparam>
        /// <param name="array">Given array.</param>
        /// <param name="comparer">Given comparer.</param>
        /// <returns>Sorted array.</returns>
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
        /// Returns index of found element.
        /// </summary>
        /// <typeparam name="T">Given type.</typeparam>
        /// <param name="array">Given array.</param>
        /// <param name="index">Start index.</param>
        /// <param name="length">Length from start index.</param>
        /// <param name="itemToSearch">Item to search.</param>
        /// <param name="comparer">Given comparer.</param>
        /// <returns>Found index.</returns>
        /// <exception cref="ArgumentException">Thrown when array is empty or default 
        /// comparer is not found.
        /// </exception>
        public static int BinarySearch<T>(this T[] array, int index, int length, T itemToSearch, IComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                if ((comparer is IComparer))
                {
                    throw new ArgumentException($"Default comparer not found: {nameof(comparer)}");
                }

                comparer = Comparer<T>.Default;
            }

            return GetResultOfBinarySearch(array, index, length - 1, itemToSearch, comparer);
        }

        /// <summary>
        /// Binary search algorithms implementation.
        /// </summary>
        /// <typeparam name="T">Given type.</typeparam>
        /// <param name="array">Array for searching.</param>
        /// <param name="itemToSearch">Item that has to be found.</param>
        /// <param name="comparer">Comparer that is used for searching.</param>
        /// <returns>Index of found element.</returns>
        /// <exception cref="ArgumentNullException">Thrown when comparer is null
        /// or array is null.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown when array is empty or default 
        /// comparer is not found.
        /// </exception>
        public static int BinarySearch<T>(this T[] array, T itemToSearch, IComparer<T> comparer = null)
        {
            ValidateArray(array);
            int leftIndex = array.GetLowerBound(0);
            return BinarySearch<T>(array, leftIndex, array.Length, itemToSearch, comparer); 
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

        private static int GetResultOfBinarySearch<T>(this T[] array, int leftIndex, int rightIndex, T itemToSearch, IComparer<T> comparer = null)
        {
            if (leftIndex == rightIndex)
            {
                return leftIndex;
            }

            while (true)
            {
                if (rightIndex - leftIndex == 1)
                {
                    if (comparer.Compare(array[leftIndex], itemToSearch) == 0)
                    {
                        return leftIndex;
                    }

                    if (comparer.Compare(array[rightIndex], itemToSearch) == 0)
                    {
                        return rightIndex;
                    }

                    return -1;
                }

                int middleIndex = leftIndex + ((rightIndex - leftIndex) / 2);
                int comparisonResult = comparer.Compare(array[middleIndex], itemToSearch);
                if (comparisonResult == 0)
                {
                    return middleIndex;
                }

                if (comparisonResult < 0)
                {
                    leftIndex = middleIndex;
                }

                if (comparisonResult > 0)
                {
                    rightIndex = middleIndex;
                }
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
