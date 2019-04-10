using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArrayExtension.Transform;

namespace TestEntities.Transform
{
    public class ToDecimalFromStringTransformer : ITransformer<string, int>
    {
        /// <summary>
        /// Number system.
        /// </summary>
        private int @base;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDecimalFromStringTransformer"/> class.
        /// </summary>
        /// <param name="base">Given base.</param>
        public ToDecimalFromStringTransformer(int @base)
        {
            CheckBase(@base);
            this.@base = @base;
        }

        /// <summary>
        /// Returns number in decimal notation.
        /// </summary>
        /// <param name="value">Selected value.</param>
        /// <returns>Number in decimal notation.</returns>
        public int Transform(string value)
        {
            int number = 0;
            foreach(char symbol in value)
            {
                int current = GetCurrentNumber(symbol);
                CheckCurrentDigit(current);
                number = number * this.@base + current; 
            }
            return number;
        }

        private int GetCurrentNumber(char symbol)
        {
            if (symbol >= '0' && symbol <= '9')
            {
               return symbol - '0';
            }
            symbol = Char.ToUpper(symbol);
            if (symbol >= 'A' && symbol <= 'F')
            {
               return symbol - 'A' + 10;
            }
            throw new ArgumentException("Invalid symbol in string");
        }

        private void CheckBase(int @base)
        {
            if (@base < 2 || @base > 16)
            {
                throw new ArgumentOutOfRangeException($"{nameof(@base)} has to be between 2 and 16!");
            }
        }

        private void CheckCurrentDigit(int value)
        {
            if (value >=  @base)
            {
                throw new ArgumentException("Invalid digit for selected base!");
            }  
        }
    }
}
