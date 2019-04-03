namespace ArrayExtension.Filter
{
    /// <summary>
    /// Predicate that tells if number is palindrome.
    /// </summary>
    public class IsPalindromePredicate : IPredicate
    {
        /// <summary>
        /// Returns true if number is palindrome.
        /// </summary>
        /// <param name="number">Number to check.</param>
        /// <returns>True if number is palindrome.</returns>
        public bool IsMatch(int number)
        {
            if (number < 0)
            {
                return false;
            }

            int reversed = this.GetReversedNumber(number);
            return number == reversed;
        }

        private int GetReversedNumber(int number)
        {
             int reversed = 0;
             for (; number != 0; number /= 10)
             {
                 reversed = (reversed * 10) + (number % 10);
             }

            return reversed;
        }
    }
}
