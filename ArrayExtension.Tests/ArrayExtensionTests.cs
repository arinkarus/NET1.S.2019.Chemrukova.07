using ArrayExtension.Filter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestEntities.Filter;
using TestEntities.StringSort;
using TestEntities.Transform;

namespace ArrayExtension.Tests
{
    public class ArrayExtensionTests
    {
        #region Filter tests

        private static IEnumerable<TestCaseData> FilterTestCases
        {
            get
            {
                yield return new TestCaseData(arg1: new int[] { 1 } , arg2: new ContainsDigitPredicate(2), arg3: new int[] { });
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
            Assert.Throws<ArgumentNullException>(() => ArrayExtension.Filter(null, new NumberPalindromePredicate()));
        
        [Test]
        public void Filter_ArrayIsEmpty_ThrowArgumentException() =>
          Assert.Throws<ArgumentException>(() => new int[] { }.Filter(new NumberPalindromePredicate()));

        [Test]
        public void Filter_PredicateIsNull__ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new int[] { 2, 5 }.Filter(null));

        [Test, TestCaseSource(nameof(FilterTestCases))]
        public void Filter_ConreteArrayAndPredicate_ReturnFilteredArray(int[] array, IPredicate predicate, int[] expected)
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
            Assert.Throws<ArgumentNullException>(() => new double[] { 1.1 }.Transform(null));

        [Test]
        public void Transform_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => ArrayExtension.Transform(null, new ToBinaryRepresentationTransformer()));

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
        [TestCase(new double[] { 330, double.PositiveInfinity, double.NegativeInfinity}, ExpectedResult =
            new string[] {"three three zero", "Positive Infinity", "Negative Infinity"})]
        public string[] Transform_ArrayToEnglishWords_ReturnArrayOfStrings(double[] array)
        {
            return array.Transform(new ToEnglishWordsTransformer());
        }

        #endregion

        #region Sort tests

        [Test]
        public void Sort_ComparerIsNull_ThrowArgumentNullException() =>
           Assert.Throws<ArgumentNullException>(() => ArrayExtension.Sort(new string[] { "hello" }, null));

        [Test]
        public void Sort_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => ArrayExtension.Sort(null, new StringAscByLengthComparer()));

        [Test]
        public void Sort_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => ArrayExtension.Sort(new string[] { }, new StringAscByLengthComparer()));
        
        [TestCase(new string[] { "sort", "22", null, "1" }, new string[] { null, "1", "22", "sort" })]
        [TestCase(new string[] { "test", "test1010", "1", "{2}" }, new string[] { "1", "{2}", "test", "test1010" })]
        [TestCase(new string[] { "one", "", "hello", "e" }, new string[] { "", "e", "one", "hello" })]
        public void Sort_ArrayAndAscByLengthComparer_ReturnSortedArrayOfStrings(string[] array, string[] expected)
        {
            ArrayExtension.Sort(array, new StringAscByLengthComparer());
            CollectionAssert.AreEqual(array, expected);
        }

        [TestCase(new string[] { "1", "111", null, "4444", "hello", null }, new string[] { "hello", "4444", "111", "1", null, null })]
        [TestCase(new string[] { "let", "0", "1", "testtest" }, new string[] { "testtest", "let", "0", "1" })]
        [TestCase(new string[] { "someString", "level", "two", "some some some.", "let"},
            new string[] { "some some some.", "someString", "level", "two", "let" })]
        public void Sort_ArrayAndDescByLengthComparer_ReturnSortedArrayOfStrings(string[] array, string[] expected)
        {
            ArrayExtension.Sort(array, new StringDescByLengthComparer());
            CollectionAssert.AreEqual(array, expected);
        }

        [TestCase(new string[] { "one", "ooo", null }, 'o', new string[] { null, "one", "ooo" })]
        [TestCase(new string[] { " ", "something", "test by test", "hello" }, 't',
            new string[] {" ", "hello", "something", "test by test" })]
        [TestCase(new string[] { "absbsba", "", "hhaaahahh", "halo"}, 'a', new string[] { "", "halo", "absbsba", "hhaaahahh" })]
        public void Sort_ArrayAndAscBySymbolOccurrences_ReturnSortedArrayOfStrings(string[] array, char symbol, string[] expected)
        {
            ArrayExtension.Sort(array, new StringAscByOccurrencesComparer(symbol));
            CollectionAssert.AreEqual(array, expected);
        }

        [TestCase(new string[] { null, "one", "ooo", null }, 'o', new string[] { "ooo", "one", null, null })]
        [TestCase(new string[] { "temp", "test", "eeee", "e", "123"}, 'e', new string[] { "eeee", "temp", "test", "e", "123" })]
        [TestCase(new string[] { "xxx", "abc", "abcde", "xyzzyxxyzz", "yes"}, 'x', 
            new string[] { "xxx", "xyzzyxxyzz", "abc", "abcde", "yes"})]
        public void Sort_ArrayAndDescBySymbolOccurences_ReturnSortedArrayOfStrings(string[] array, char symbol, string[] expected)
        {
            ArrayExtension.Sort(array, new StringDescByOccurrencesComparer(symbol));
            CollectionAssert.AreEqual(array, expected);
        }

        #endregion
    }
}
