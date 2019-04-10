using ArrayExtension.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEntities.Transform
{
    /// <summary>
    /// Implements ITransformer interface.
    /// </summary>
    public abstract class ToWordsTransformer : ITransformer<double, string>
    {
        /// <summary>
        /// Transforms number to some language representation.
        /// </summary>
        /// <param name="number">Given number.</param>
        /// <returns>Returns words representation.</returns>
        public string Transform(double number)
        { 
            string numberValue = Convert.ToString(number, System.Globalization.CultureInfo.InvariantCulture);
            Dictionary<double, string> specialValues = this.GetSpecialValues();
            if (specialValues.ContainsKey(number))
            {
                return specialValues[number];
            }

            Dictionary<char, string> words = this.GetWords();
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < numberValue.Length - 1; i++)
            {
                stringBuilder.Append($"{words[numberValue[i]]} ");
            }

            stringBuilder.Append(words[numberValue[numberValue.Length - 1]]);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Method that returns words.
        /// </summary>
        /// <returns>Returns words for digits.</returns>
        protected abstract Dictionary<char, string> GetWords();

        /// <summary>
        /// Method that returns special values.
        /// </summary>
        /// <returns>Returns special values for double number.</returns>
        protected abstract Dictionary<double, string> GetSpecialValues();
    }
}
