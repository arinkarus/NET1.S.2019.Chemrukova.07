﻿using ArrayExtension.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEntities.Transform
{
    /// <summary>
    /// Implements ITransformer interface.
    /// </summary>
    public class ToBinaryRepresentationTransformer : ITransformer<double, string>
    {
        /// <summary>
        /// Transforms double to it's binary string representation.
        /// </summary>
        /// <param name="number">Given number.</param>
        /// <returns>Binary representation in IEEE754 format.</returns>
        public string Transform(double number)
        {
            return number.TransformToIEEE754();
        }
    }
}
