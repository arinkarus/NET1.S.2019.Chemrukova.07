using System.Collections.Generic;

namespace TestEntities.Transform
{
    /// <summary>
    /// Transforms double number to russian words.
    /// </summary>
    public class ToRussianWordsTransformer : ToWordsTransformer
    {
        /// <summary>
        /// Returns special values for double written in russian words.
        /// </summary>
        /// <returns>Special values for double.</returns>
        protected override Dictionary<double, string> GetSpecialValues() =>
            new Dictionary<double, string>
            {
                [double.NaN] = "Не число",
                [double.PositiveInfinity] = "Плюс бесконечность",
                [double.NegativeInfinity] = "Минус бесконечность"
            };

        /// <summary>
        /// Returns words for digits.
        /// </summary>
        /// <returns>Words representation in russian for digits.</returns>
        protected override Dictionary<char, string> GetWords() =>
            new Dictionary<char, string>
            {
                ['0'] = "ноль",
                ['1'] = "один",
                ['2'] = "два",
                ['3'] = "три",
                ['4'] = "четыре",
                ['5'] = "пять",
                ['6'] = "шесть",
                ['7'] = "семь",
                ['8'] = "восемь",
                ['9'] = "девять",
                ['.'] = "точка",
                ['E'] = "экспонента",
                ['-'] = "минус",
                ['+'] = "плюс",
            };    
    }
}
