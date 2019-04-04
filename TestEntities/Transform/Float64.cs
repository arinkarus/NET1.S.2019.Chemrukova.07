using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestEntities.Transform
{
    /// <summary>
    /// Instance of this struct holds double value and long 
    /// value in one piece of memory.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Float64
    {
        /// <summary>
        /// Double value.
        /// </summary>
        [FieldOffset(0)]
        private readonly double doubleValue;

        /// <summary>
        /// Unsigned long value.
        /// </summary>
        [FieldOffset(0)]
        private readonly ulong ulongValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Float64"/> struct.
        /// </summary>
        /// <param name="value">Double number.</param>
        public Float64(double value)
        {
            this.ulongValue = 0;
            this.doubleValue = value;
        }

        /// <summary>
        /// Gets unsigned value 
        /// </summary>
        public ulong UlongValue
        {
            get
            {
                return this.ulongValue;
            }
        }
    }
}
