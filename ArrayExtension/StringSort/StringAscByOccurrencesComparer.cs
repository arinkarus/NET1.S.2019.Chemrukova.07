using System.Collections.Generic;

namespace ArrayExtension.StringSort
{
    /// <summary>
    /// Comparer for strings that depends on the occurrences of certain symbol in strings.
    /// </summary>
    public class StringAscByOccurrencesComparer : IComparer<string>
    {
        /// <summary>
        /// Symbol for counting occurrences.
        /// </summary>
        private readonly char symbol;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringAscByOccurrencesComparer"/> class.
        /// </summary>
        /// <param name="symbol">Symbol for counting occurrences.</param>
        public StringAscByOccurrencesComparer(char symbol)
        {
            this.symbol = symbol;
        }

        /// <summary>
        /// Compares two strings depending on the occurrences of given symbols in strings.
        /// </summary>
        /// <param name="x">First string.</param>
        /// <param name="y">Second string.</param>
        /// <returns> Returns an integer that indicates their relative position in the sort order.</returns>
        public int Compare(string x, string y)
        {
            int occurrencesForX = x.GetCountOfSymbol(this.symbol);
            int occurrencesForY = y.GetCountOfSymbol(this.symbol);
            if (occurrencesForX > occurrencesForY)
            {
                return 1;
            }

            if (occurrencesForX == occurrencesForY)
            {
                return 0;
            }

            return -1;
        }
    }
}
