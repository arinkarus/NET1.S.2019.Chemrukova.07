using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayExtension.StringSort
{
    /// <summary>
    /// Provides useful helper methods for working with strings.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Returns number of occurrences of given symbol in some string.
        /// </summary>
        /// <param name="text">Given string.</param>
        /// <param name="givenSymbol">Given symbol.</param>
        /// <returns>Count of symbol in string.</returns>
        public static int GetCountOfSymbol(this string text, char givenSymbol)
        {
            return text.Split(givenSymbol).Length - 1;
        }    
    }
}
