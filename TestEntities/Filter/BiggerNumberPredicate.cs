using ArrayExtension.Filter;

namespace TestEntities.Filter
{
    /// <summary>
    /// Predicate that tells if number is bigger that another number. 
    /// </summary>
    public class BiggerNumberPredicate : IPredicate<int>
    {
        /// <summary>
        /// Number that will be used for comparison.
        /// </summary>
        private readonly int numberToCompare;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiggerNumberPredicate"/> class.
        /// </summary>
        /// <param name="numberToCompare">Number to compare.</param>
        public BiggerNumberPredicate(int numberToCompare)
        {
            this.numberToCompare = numberToCompare;
        }

        /// <summary>
        /// Returns true if number is bigger that number to compare.
        /// </summary>
        /// <param name="number">Number that is checked.</param>
        /// <returns></returns>
        public bool IsMatch(int number) => number > this.numberToCompare;
    }
}
