namespace ArrayExtension.Filter
{
    /// <summary>
    /// Predicate interface.
    /// </summary>
    public interface IPredicate
    {
        /// <summary>
        /// Returns true if number much some condition.
        /// </summary>
        /// <param name="number">Number that will be checked.</param>
        /// <returns></returns>
        bool IsMatch(int number);
    }
}