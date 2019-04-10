namespace ArrayExtension.Filter
{
    /// <summary>
    /// Predicate interface.
    /// </summary>
    public interface IPredicate<T>
    {
        /// <summary>
        /// Tells if value matches some condition. 
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>True if value matches condition otherwise - false.</returns>
        bool IsMatch(T value);
    }
}