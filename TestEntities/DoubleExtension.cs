using System.Text;
using TestEntities.Transform;

namespace TestEntities
{
    /// <summary>
    /// Provides methods to work with double numbers.
    /// </summary>
    public static class DoubleExtension
    {
        /// <summary>
        /// Amount of bits for double representation.
        /// </summary>
        private const int AmountOfBitsForDouble = 64;

        /// <summary>
        /// Transforms number to binary string.
        /// </summary>
        /// <param name="number">Given number.</param>
        /// <returns>String representation of double number.</returns>
        public static string TransformToIEEE754(this double number)
        {
            var float64 = new Float64(number);
            ulong ulongValue = float64.UlongValue;
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < AmountOfBitsForDouble; i++)
            {
                if ((ulongValue & 1) == 1)
                {
                    stringBuilder.Insert(0, "1");
                }
                else
                {
                    stringBuilder.Insert(0, "0");
                }

                ulongValue >>= 1;
            }

            return stringBuilder.ToString();
        }
    }
}
