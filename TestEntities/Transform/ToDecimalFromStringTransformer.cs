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
            this.@base = @base;
        }

        /// <summary>
        /// Returns number in decimal notation.
        /// </summary>
        /// <param name="value">Selected value.</param>
        /// <returns>Number in decimal notation.</returns>
        public int Transform(string value)
        {
            var numberSystem = new NumberSystem(this.@base);
            return numberSystem.Transform(value);
        }

        /// <summary>
        /// Represents the number system.
        /// </summary>
        internal class NumberSystem
        {
            /// <summary>
            /// Base that is from 2 to 16.
            /// </summary>
            private readonly int @base;

            /// <summary>
            /// Alphabet for selected number system.
            /// </summary>
            private readonly Dictionary<char, int> alphabet;
           
            /// <summary>
            /// Initializes a new instance of the <see cref="NumberSystem"/> class.
            /// </summary>
            /// <param name="base">Given base.</param>
            public NumberSystem(int @base)
            {
                CheckBase(@base);
                this.@base = @base;
                alphabet = GetAphabet(@base);
            }

            /// <summary>
            /// Transforms from string to number. 
            /// </summary>
            /// <param name="input">Given string.</param>
            /// <returns>Number in decimal.</returns>
            public int Transform(string input)
            {
                int number = 0;
                foreach (char symbol in input)
                {
                    var symbolToCheck = char.ToUpper(symbol);
                    if (!this.alphabet.ContainsKey(symbolToCheck))
                    {
                        throw new ArgumentException($"{nameof(symbol)} is invalid for transforming!");
                    }

                    int current = alphabet[symbolToCheck];
                    number = checked(number * this.@base + current);
                }
                var some = new NumberSystem(@base);
                return number;
            }

            private void CheckBase(int @base)
            {
                if (@base < 2 || @base > 16)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(@base)} has to be between 2 and 16!");
                }

            }

            private Dictionary<char, int> GetAphabet(int @base)
            {
                var alphabet = new Dictionary<char, int>();
                for (int i = 0; i < @base; i++)
                {
                    if (i < 10)
                    {
                        alphabet[(char)(i + '0')] = i;
                    }
                    else
                    {
                        alphabet[(char)(i + 'A' - 10)] = i;
                    }
                }
                return alphabet;
            }

        }
    }
}
