using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayExtension.Transform
{
    /// <summary>
    /// Interface that has method to transform some double number to string representation.
    /// </summary>
    public interface ITransformer<TSource, TDest>
    {
        /// <summary>
        /// Transforms double to string.
        /// </summary>
        /// <param name="number">Given number.</param>
        /// <returns>String representation of double.</returns>
        TDest Transform(TSource number);
    }
}
