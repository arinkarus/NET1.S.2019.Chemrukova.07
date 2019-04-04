using System.Collections.Generic;

namespace TestEntities.Transform
{
    /// <summary>
    /// Transforms double number to english words.
    /// </summary>
    public class ToEnglishWordsTransformer : ToWordsTransformer
    {
        /// <summary>
        /// Returns special values for double written in english words.
        /// </summary>
        /// <returns>Special values for double.</returns>
        protected override Dictionary<double, string> GetSpecialValues() =>
            new Dictionary<double, string>
            {
                [double.NaN] = "Not a number",
                [double.PositiveInfinity] = "Positive Infinity",
                [double.NegativeInfinity] = "Negative Infinity"
            };

        /// <summary>
        /// Returns words for digits.
        /// </summary>
        /// <returns>Words representation in english for digits.</returns>
        protected override Dictionary<char, string> GetWords() =>
            new Dictionary<char, string>
            {
                ['0'] = "zero",
                ['1'] = "one",
                ['2'] = "two",
                ['3'] = "three",
                ['4'] = "four",
                ['5'] = "five",
                ['6'] = "six",
                ['7'] = "seven",
                ['8'] = "eight",
                ['9'] = "nine",
                ['.'] = "point",
                ['E'] = "exponenta",
                ['-'] = "minus",
                ['+'] = "plus",
            };
    }
}
