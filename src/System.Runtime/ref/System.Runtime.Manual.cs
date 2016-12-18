// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// ------------------------------------------------------------------------------
// Changes to this file must follow the http://aka.ms/api-review process.
// ------------------------------------------------------------------------------


using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

// This is required for ProjectN to extend reflection. Once we make extensibility via contracts work on desktop, this can be removed.
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("System.Private.Reflection.Extensibility, PublicKey=002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293")]

namespace System
{
    /// <summary>
    /// Represents a single-precision floating-point number.
    /// </summary>
    public partial struct Single
    {
        /// <summary>
        /// Represents the smallest possible value of <see cref="Single" />. This field is constant.
        /// </summary>
        public const float MinValue = (float)-3.40282346638528859e+38;
        /// <summary>
        /// Represents the smallest positive <see cref="Single" /> value that is greater than
        /// zero. This field is constant.
        /// </summary>
        public const float Epsilon = (float)1.4e-45;
        /// <summary>
        /// Represents the largest possible value of <see cref="Single" />. This field is constant.
        /// </summary>
        public const float MaxValue = (float)3.40282346638528859e+38;
        /// <summary>
        /// Represents positive infinity. This field is constant.
        /// </summary>
        public const float PositiveInfinity = (float)1.0 / (float)0.0;
        /// <summary>
        /// Represents negative infinity. This field is constant.
        /// </summary>
        public const float NegativeInfinity = (float)-1.0 / (float)0.0;
        /// <summary>
        /// Represents not a number (NaN). This field is constant.
        /// </summary>
        public const float NaN = (float)0.0 / (float)0.0;
    }
    /// <summary>
    /// Represents a double-precision floating-point number.
    /// </summary>
    public partial struct Double
    {
        /// <summary>
        /// Represents the smallest possible value of a <see cref="Double" />. This field is
        /// constant.
        /// </summary>
        public const double MinValue = -1.7976931348623157E+308;
        /// <summary>
        /// Represents the largest possible value of a <see cref="Double" />. This field is constant.
        /// </summary>
        public const double MaxValue = 1.7976931348623157E+308;
        /// <summary>
        /// Represents the smallest positive <see cref="Double" /> value that is greater than
        /// zero. This field is constant.
        /// </summary>
        /// <remarks>The hex representation is 0x1 on little endian machines.</remarks>
        public const double Epsilon = 4.9406564584124654E-324;
        /// <summary>
        /// Represents negative infinity. This field is constant.
        /// </summary>
        public const double NegativeInfinity = (double)-1.0 / (double)(0.0);
        /// <summary>
        /// Represents positive infinity. This field is constant.
        /// </summary>
        public const double PositiveInfinity = (double)1.0 / (double)(0.0);
        /// <summary>
        /// Represents a value that is not a number (NaN). This field is constant.
        /// </summary>
        public const double NaN = (double)0.0 / (double)0.0;
    }
    /// <summary>
    /// Represents type declarations: class types, interface types, array types, value types, enumeration
    /// types, type parameters, generic type definitions, and open or closed constructed generic types.To
    /// browse the .NET Framework source code for this type, see the Reference Source.
    /// </summary>
    public partial class Type
    {
        /// <summary>
        /// Gets the type that declares the current nested type or generic type parameter.
        /// </summary>
        /// <returns>
        /// A <see cref="Type" /> object representing the enclosing type, if the current type
        /// is a nested type; or the generic type definition, if the current type is a type parameter
        /// of a generic type; or the type that declares the generic method, if the current type is a
        /// type parameter of a generic method; otherwise, null.
        /// </returns>
        public abstract Type DeclaringType { get; }
        public abstract string Name { get; }
    }
}
