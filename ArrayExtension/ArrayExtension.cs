using System;
using System.Collections;
using System.Collections.Generic;
using ArrayExtension.Filter;
using ArrayExtension.Transform;
using ArrayExtension.Exceptions;
using System.Collections.ObjectModel;

namespace ArrayExtension
{
    /// <summary>
    /// Provides methods that helps to work with arrays.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Filters values by some predicate.
        /// </summary>
        /// <typeparam name="T">Given type.</typeparam>
        /// <param name="source">Given source.</param>
        /// <param name="predicate">Given predicate.</param>
        /// <returns>Filtered array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when predicate is null
        /// or souce is null.
        /// </exception>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, IPredicate<T> predicate)
        {
            CheckOnNull(source);
            CheckOnNull(predicate);    
            IEnumerable<T> GetFilteredItems(IEnumerable<T> items, IPredicate<T> givenPredicate)
            {
                foreach (var item in source)
                {
                    if (predicate.IsMatch(item))
                    {
                        yield return item;
                    }
                }
            }

            return GetFilteredItems(source, predicate);
        }

        /// <summary>
        /// Transforms values to other depending on given transformer.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDest">Destination type.</typeparam>
        /// <param name="source">Given source.</param>
        /// <param name="transformer">Given transformer.</param>
        /// <returns>Transformed values.</returns>
        /// /// <exception cref="ArgumentNullException">Thrown when transformer is null
        /// or source is null.
        /// </exception>
        public static IEnumerable<TDest> Transform<TSource, TDest>(this IEnumerable<TSource> source, ITransformer<TSource, TDest> transformer)
        {
            CheckOnNull(source);
            CheckOnNull(transformer);
            IEnumerable<TDest> GetItems()
            {
                foreach (var item in source)
                {
                    TDest value = transformer.Transform(item);
                    yield return value;
                }
            }         
            
            return GetItems();
        }

        /// <summary>
        /// Return sorted values depending on passed criteria (comparer).
        /// </summary>
        /// <typeparam name="T">Given type.</typeparam>
        /// <param name="source">Given source.</param>
        /// <param name="comparer">Given comparer.</param>
        /// <returns>Sorted values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when comparer is null
        /// or source is null.
        /// </exception>
        public static IEnumerable<T> SortBy<T>(this IEnumerable<T> source, IComparer<T> comparer)
        {
            CheckOnNull(comparer);
            List<T> list = new List<T>(source);
            return GetSorted();
            IEnumerable<T> GetSorted()
            {
                list.Sort(comparer);
                return list;
            }
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
                if (!(itemToSearch is IComparable<T> || itemToSearch is IComparable))
                {
                    throw new ComparisonIsNotFound($"Can't find comparison.");
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
