using ArrayExtension.Filter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestEntities.Filter;
using TestEntities.StringSort;
using TestEntities.Transform;
using TestEntities.JaggedArraySort;

namespace ArrayExtension.Tests
{
    public class ArrayExtensionTests
    {
        #region Filter tests

        private static IEnumerable<TestCaseData> FilterTestCases
        {
            get
            {
                yield return new TestCaseData(arg1: new int[] { 1 }, arg2: new ContainsDigitPredicate(2), arg3: new int[] { });
                yield return new TestCaseData(arg1: new int[] { 24, 42, -4444, -4, 4, -4, 22, -788 },
                    arg2: new ContainsDigitPredicate(4), arg3: new int[] { 24, 42, -4444, -4, 4, -4 });
                yield return new TestCaseData(arg1: new int[] { 20, 10, 145, 20 },
                    arg2: new ContainsDigitPredicate(2), arg3: new int[] { 20, 20 });
                yield return new TestCaseData(arg1: new int[] { 1, 2, -5, 10, 15, 6, 7 }, arg2: new BiggerNumberPredicate(-5),
                    arg3: new int[] { 1, 2, 10, 15, 6, 7 });
                yield return new TestCaseData(arg1: new int[] { 10, 50, 51, 545, 55 },
                    arg2: new BiggerNumberPredicate(0),
                    arg3: new int[] { 10, 50, 51, 545, 55 });
                yield return new TestCaseData(arg1: new int[] { 0, 0, 0 }, arg2: new NumberPalindromePredicate(),
                    arg3: new int[] { 0, 0, 0 });
                yield return new TestCaseData(arg1: new int[] { 123, 3003, 12, 13, 14, 44144, 10101 }, arg2: new NumberPalindromePredicate(),
                    arg3: new int[] { 3003, 44144, 10101 });
                yield return new TestCaseData(arg1: new int[] { 12, 7, 10, 0, 3, -8, -9 }, arg2: new EvenNumberPredicate(),
                    arg3: new int[] { 12, 10, 0, -8 });
            }
        }

        [Test]
        public void Filter_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => ArrayExtension.Filter<int>(null, new NumberPalindromePredicate()));

        [Test]
        public void Filter_ArrayIsEmpty_ThrowArgumentException() =>
          Assert.Throws<ArgumentException>(() => new int[] { }.Filter(new NumberPalindromePredicate()));

        [Test]
        public void Filter_PredicateIsNull__ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new int[] { 2, 5 }.Filter(null));

        [Test, TestCaseSource(nameof(FilterTestCases))]
        public void Filter_ConreteArrayAndPredicate_ReturnFilteredArray(int[] array, IPredicate<int> predicate, int[] expected)
        {
            CollectionAssert.AreEqual(array.Filter(predicate), expected);
        }

        [TestCase(-1)]
        [TestCase(10)]
        public void Filter_ContainsDigitPredicate_NotADigitPassed_ThrowArgumentException(int digit) =>
            Assert.Throws<ArgumentException>(() => new int[] { 1 }.Filter(new ContainsDigitPredicate(digit)));

        #endregion

        #region Transform tests

        [Test]
        public void Transform_TransformerIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new double[] { 1.1 }.Transform<double, string>(null));

        [Test]
        public void Transform_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => ArrayExtension.Transform<double, string>(null, new ToBinaryRepresentationTransformer()));

        [Test]
        public void Transform_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => new double[] { }.Transform(new ToBinaryRepresentationTransformer()));

        [TestCase(new double[] { -255.255, 255.255 }, ExpectedResult = new string[]
            { "1100000001101111111010000010100011110101110000101000111101011100",
             "0100000001101111111010000010100011110101110000101000111101011100" })]
        [TestCase(new double[] { 4294967295.0, double.MinValue,
             double.Epsilon, double.NaN, -0.0 }, ExpectedResult = new string[] {
            "0100000111101111111111111111111111111111111000000000000000000000",
            "1111111111101111111111111111111111111111111111111111111111111111",
            "0000000000000000000000000000000000000000000000000000000000000001",
            "1111111111111000000000000000000000000000000000000000000000000000",
            "1000000000000000000000000000000000000000000000000000000000000000"  })]
        public string[] Transform_ArrayAndToBinaryTransformer_ReturnArrayOfStrings(double[] array)
        {
            return array.Transform(new ToBinaryRepresentationTransformer());
        }

        [TestCase(new double[] { 1.21, 1E-10, 2 }, ExpectedResult = new string[]
            { "один точка два один", "один экспонента минус один ноль", "два" })]
        [TestCase(new double[] { -5.05, 9.99, double.NaN }, ExpectedResult = new string[]
            { "минус пять точка ноль пять", "девять точка девять девять", "Не число" })]
        [TestCase(new double[] { double.PositiveInfinity, double.NegativeInfinity },
            ExpectedResult = new string[] { "Плюс бесконечность", "Минус бесконечность" })]
        public string[] Transform_ArrayToRussianWords_ReturnArrayOfStrings(double[] array)
        {
            return array.Transform(new ToRussianWordsTransformer());
        }

        [TestCase(new double[] { 1E-10, 3.2 }, ExpectedResult = new string[]
            { "one exponenta minus one zero", "three point two" })]
        [TestCase(new double[] { 22.02, double.NaN, 0 }, ExpectedResult = new string[]
            { "two two point zero two", "Not a number", "zero" })]
        [TestCase(new double[] { 330, double.PositiveInfinity, double.NegativeInfinity }, ExpectedResult =
            new string[] { "three three zero", "Positive Infinity", "Negative Infinity" })]
        public string[] Transform_ArrayToEnglishWords_ReturnArrayOfStrings(double[] array)
        {
            return array.Transform(new ToEnglishWordsTransformer());
        }

        [Test]
        public void Transform_ToDecFromStringBaseOutOfRange_ThrowArgumentOutOfRangeException() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new string[] { "010" }.Transform
            (new ToDecimalFromStringTransformer(17)));

        [TestCase("02", 2)]
        [TestCase("0121", 2)]
        [TestCase("0332", 3)]
        [TestCase("-----", 2)]
        [TestCase("AABAB", 11)]
        public void Transform_ToDecFromStringInvalidElementsGiven_ThrowArgumentException(string invalidString, int @base) =>
            Assert.Throws<ArgumentException>(() => new string[] { invalidString }.Transform(new ToDecimalFromStringTransformer(@base)));

        [TestCase(new string[] { "A", "FFF" }, 16, ExpectedResult = new int[] { 10, 4095 })]
        [TestCase(new string[] { "202", "222" }, 3, ExpectedResult = new int[] { 20, 26 })]
        [TestCase(new string[] { "10011011", "11111111", "0", "011" }, 2, ExpectedResult = new int[] { 155, 255, 0, 3 })]
        public int[] Transform_ToDecFromStringConcreteArray_ReturnNumbers(string[] values, int @base)
        {
            return values.Transform(new ToDecimalFromStringTransformer(@base));
        }

        #endregion

        #region Sort tests

        [Test]
        public void Sort_ComparerIsNull_ThrowArgumentNullException() =>
           Assert.Throws<ArgumentNullException>(() => ArrayExtension.SortBy(new string[] { "hello" }, null));

        [Test]
        public void Sort_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => ArrayExtension.SortBy(null, new StringAscByLengthComparer()));

        [Test]
        public void Sort_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => ArrayExtension.SortBy(new string[] { }, new StringAscByLengthComparer()));

        [TestCase(new string[] { "sort", "22", null, "1" }, new string[] { null, "1", "22", "sort" })]
        [TestCase(new string[] { "test", "test1010", "1", "{2}" }, new string[] { "1", "{2}", "test", "test1010" })]
        [TestCase(new string[] { "one", "", "hello", "e" }, new string[] { "", "e", "one", "hello" })]
        public void Sort_ArrayAndAscByLengthComparer_ReturnSortedArrayOfStrings(string[] array, string[] expected)
        {
            string[] sorted = ArrayExtension.SortBy(array, new StringAscByLengthComparer());
            CollectionAssert.AreEqual(expected, sorted);
        }

        [TestCase(new string[] { "1", "111", null, "4444", "hello", null }, new string[] { "hello", "4444", "111", "1", null, null })]
        [TestCase(new string[] { "let", "0", "1", "testtest" }, new string[] { "testtest", "let", "0", "1" })]
        [TestCase(new string[] { "someString", "level", "two", "some some some.", "let" },
            new string[] { "some some some.", "someString", "level", "two", "let" })]
        public void Sort_ArrayAndDescByLengthComparer_ReturnSortedArrayOfStrings(string[] array, string[] expected)
        {
            string[] sorted = ArrayExtension.SortBy(array, new StringDescByLengthComparer());
            CollectionAssert.AreEqual(expected, sorted);
        }

        [TestCase(new string[] { "one", "ooo", null }, 'o', new string[] { null, "one", "ooo" })]
        [TestCase(new string[] { " ", "something", "test by test", "hello" }, 't',
            new string[] { " ", "hello", "something", "test by test" })]
        [TestCase(new string[] { "absbsba", "", "hhaaahahh", "halo" }, 'a', new string[] { "", "halo", "absbsba", "hhaaahahh" })]
        public void Sort_ArrayAndAscBySymbolOccurrences_ReturnSortedArrayOfStrings(string[] array, char symbol, string[] expected)
        {
            string[] sorted = ArrayExtension.SortBy(array, new StringAscByOccurrencesComparer(symbol));
            CollectionAssert.AreEqual(expected, sorted);
        }

        [TestCase(new string[] { null, "one", "ooo", null }, 'o', new string[] { "ooo", "one", null, null })]
        [TestCase(new string[] { "temp", "test", "eeee", "e", "123" }, 'e', new string[] { "eeee", "temp", "test", "e", "123" })]
        [TestCase(new string[] { "xxx", "abc", "abcde", "xyzzyxxyzz", "yes" }, 'x',
            new string[] { "xxx", "xyzzyxxyzz", "abc", "abcde", "yes" })]
        public void Sort_ArrayAndDescBySymbolOccurences_ReturnSortedArrayOfStrings(string[] array, char symbol, string[] expected)
        {
            string[] sorted = ArrayExtension.SortBy(array, new StringDescByOccurrencesComparer(symbol));
            CollectionAssert.AreEqual(expected, sorted);
        }

        #endregion

        #region Sort jagged array

        [Test]
        public void SortJaggedArray_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => ArrayExtension.SortBy(null, new LinesDescBySumComparer()));

        [Test]
        public void SortJaggedArray_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => ArrayExtension.SortBy(new int[][] { }, new LinesDescBySumComparer()));

        [Test]
        public void SortJaggedArray_ComparerIsNull__ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => ArrayExtension.SortBy(new int[][] { new int[] { 1, 2 } }, null));

        [Test]
        public void SortJaggedArray_LinesDescBySumComparer()
        {
            int[][] array = new int[][]
            {
                new int[] {100},
                new int[] { 1050, 10, -1050 },
                null,
                new int[] { 1, 2, 3 },
                null
            };
            int[][] expectedArray = new int[][]
            {
                new int[] { 100 },
                new int[] { 1050, 10, -1050 },
                new int[] { 1, 2, 3 },
                null,
                null
            };
            int[][] sorted = array.SortBy(new LinesDescBySumComparer());
            CollectionAssert.AreEqual(expectedArray, sorted);
        }

        [Test]
        public void SortJaggedArray_LinesAscBySumComparer()
        {
            int[][] array = new int[][]
            {
                new int[] { 255, 20, 555 },
                new int[] { -200, -400, 0 },
                null,
                new int[] { 1, 2, 3, 4, 4 },
                null,
                new int[] { 0, 0, 0 },
            };
            int[][] expectedArray = new int[][]
            {
                null,
                null,
                new int[] { -200, -400, 0 },
                new int[] { 0, 0, 0 },
                new int[] { 1, 2, 3, 4, 4 },
                new int[] { 255, 20, 555 },
            };
            int[][] sortedArray = array.SortBy(new LinesAscBySumComparer());
            CollectionAssert.AreEqual(expectedArray, sortedArray);
        }

        [Test]
        public void SortJaggedArray_LinesAscByMaxElementComparer()
        {
            int[][] array = new int[][]
            {
                new int[] { 255, 20, 555 },
                new int[] { -200, -400, 0 },
                null,
                new int[] { 1, 2, 3, 4, 4 },
                null,
                new int[] { 0, 0, 0 },
            };
            int[][] expectedArray = new int[][]
            {
                null,
                null,
                new int[] { -200, -400, 0 },
                new int[] { 0, 0, 0 },
                new int[] { 1, 2, 3, 4, 4 },
                new int[] { 255, 20, 555 }
            };
            int[][] sortedArray = array.SortBy(new LinesAscByMaxElementComparer());
            CollectionAssert.AreEqual(expectedArray, sortedArray);
        }

        [Test]
        public void SortJaggedArray_LinesDescByMaxElementComparer()
        {
            int[][] array = new int[][]
            {
                new int[] { 255, 20, 555 },
                new int[] { -200, -400, 0 },
                null,
                new int[] { 1, 2, 3, 4, 4 },
                null,
                new int[] { 0, 0, 0 },
            };
            int[][] expectedArray = new int[][]
            {
                new int[] { 255, 20, 555 },
                new int[] { 1, 2, 3, 4, 4 },
                new int[] { -200, -400, 0 },
                new int[] { 0, 0, 0 },
                null,
                null
            };
            int[][] sortedArray = array.SortBy(new LinesDescByMaxElementComparer());
            CollectionAssert.AreEqual(expectedArray, sortedArray);
        }

        [Test]
        public void SortJaggedArray_LinesAscByMinElementComparer()
        {
            int[][] array = new int[][]
            {
                null,
                new int[] { 200, 80, 1 },
                new int[] { 250, 10, 10, -8 },
                new int[] { 1, 2, 3, 4, 5 },
                new int[] { 0, 0, 1, 12 },
                null
            };
            int[][] expectedArray = new int[][]
            {
                null,
                null,
                new int[] { 250, 10, 10, -8 },
                new int[] { 0, 0, 1, 12 },
                new int[] { 200, 80, 1 },
                new int[] { 1, 2, 3, 4, 5 },
            };
            CollectionAssert.AreEqual(expectedArray, array.SortBy(new LinesAscByMinElementComparer()));
        }

        [Test]
        public void SortJaggedArray_LinesDescByMinElementComparer()
        {
            int[][] array = new int[][]
            {
                null,
                new int[] { -5, -55, 0 },
                new int[] { 333, 22, 100000 },
                new int[] { 0, 1, 0 },
                null
            };
            int[][] expectedArray = new int[][]
            {
                new int[] { 333, 22, 100000 },
                new int[] { 0, 1, 0 },
                new int[] { -5, -55, 0 },
                null,
                null
            };
            int[][] sortedArray = array.SortBy(new LinesDescByMinElementComparer());
            CollectionAssert.AreEqual(expectedArray, sortedArray);
        }

        #endregion

        #region BinarySearchTestCases

        private static IEnumerable<TestCaseData> BinarySearchTestCases
        {
            get
            {
                yield return new TestCaseData(arg1: new TestEntityForBinarySearch<string>(new string[] { "the greatest length string", "some some some some", "length string",
                    "less", null }, null, new StringDescByLengthComparer()), arg2: 4);

                yield return new TestCaseData(arg1: new TestEntityForBinarySearch<string>(
                    new string[] { "some string" }, "some string", new StringAscByLengthComparer()), arg2: 0);

                yield return new TestCaseData(arg1: new TestEntityForBinarySearch<string>(new string[] { "the greatest length string", "some some some some", "length string",
                    "less", "hi" }, "less", new StringDescByLengthComparer()), arg2: 3);

                yield return new TestCaseData(arg1: new TestEntityForBinarySearch<string>(new string[] { "the greatest length string", "some some some some", "length string",
                   "less", "hi" }, "less", null), arg2: 3);

                yield return new TestCaseData(arg1: new TestEntityForBinarySearch<int>(new int[] { 1, 2, 3, 4 }, 4, Comparer<int>.Default), arg2: 3);

                yield return new TestCaseData(arg1: new TestEntityForBinarySearch<int>(new int[] { 1, 5, 7, 9, 1 }, 10, Comparer<int>.Default), arg2: -1);

                yield return new TestCaseData(arg1: new TestEntityForBinarySearch<int[]>(new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } },
                    new int[] { 7, 8, 9 }, new LinesAscBySumComparer()), arg2: 2);
            }
        }

        [Test, TestCaseSource(nameof(BinarySearchTestCases))]
        public void BinarySearch_ConcreteArray_ReturnIndex<T>(TestEntityForBinarySearch<T> data, int expectedIndex)
        {
            int actualIndex = data.array.BinarySearch(data.itemToSearch, data.comparer);
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [Test]
        public void BinarySearch_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => ArrayExtension.BinarySearch<string>(null, "str", new StringAscByLengthComparer()));

        [Test]
        public void BinarySearch_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => new string[] { }.BinarySearch("str", new StringAscByLengthComparer()));

        [Test]
        public void BinarySearch_NoDefaultComparer_ReturnIndex() =>
            Assert.Throws<ArgumentException>(() =>
            new EntityWithoutComparer[] { new EntityWithoutComparer { Value = 5 },
                new EntityWithoutComparer { Value = 6 } }.
           BinarySearch<EntityWithoutComparer>(new EntityWithoutComparer { }));

            
        #endregion
    }
}
