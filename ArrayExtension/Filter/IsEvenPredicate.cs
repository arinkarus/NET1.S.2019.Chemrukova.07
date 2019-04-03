using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayExtension.Filter
{
    /// <summary>
    /// Predicate that tells if number is even.
    /// </summary>
    public class IsEvenPredicate : IPredicate
    {
        /// <summary>
        /// Check if number is even.
        /// </summary>
        /// <param name="number">Number to check.</param>
        /// <returns>Returns true if number is even.</returns>
        public bool IsMatch(int number)
        {
            return number % 2 == 0;
        }
    }
}
