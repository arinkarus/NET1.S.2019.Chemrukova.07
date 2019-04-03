using System;

namespace ArrayExtension.Filter
{
    /// <summary>
    /// Predicate that tells if number contains certain digit.
    /// </summary>
    public class ContainsDigitPredicate : IPredicate
    {
        /// <summary>
        /// Digit that will be used for filtering.
        /// </summary>
        private readonly int digit;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainsDigitPredicate"/> class.
        /// </summary>
        /// <param name="digit">Given digit.</param>
        public ContainsDigitPredicate(int digit)
        {
            this.CheckIfDigit(digit);
            this.digit = digit;
        }

        /// <summary>
        /// Returns true if number contains certain digit.
        /// </summary>
        /// <param name="number">Given number.</param>
        /// <returns>True - if number contains digit else - false.</returns>
        /// <exception cref="ArgumentException">Thrown when digit parameter isn't a digit.</exception>
        public bool IsMatch(int number)
        {
            for (; number != 0; number /= 10)
            {
                if (Math.Abs(number % 10) == this.digit)
                {
                    return true;
                }
            }

            return false;
        }

        private void CheckIfDigit(int digit)
        {
            if (this.digit < 0 || this.digit > 9)
            {
                throw new ArgumentException($"{nameof(digit)} isn't a digit.");
            }
        }
    }
}
