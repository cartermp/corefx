// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*============================================================
**
** Class:  Comparer
**
** Purpose: Compares two objects for equivalence,
**          where string comparisons are case-sensitive.
**
===========================================================*/

using System.Globalization;
using System.Diagnostics.Contracts;

namespace System.Collections
{
    /// <summary>Compares two objects for equivalence, where string comparisons are case-sensitive.</summary>
    /// <filterpriority>2</filterpriority>
    public sealed class Comparer : IComparer
    {
        private CompareInfo _compareInfo;
        /// <summary>Represents an instance of <see cref="T:System.Collections.Comparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread. This field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        public static readonly Comparer Default = new Comparer(CultureInfo.CurrentCulture);
        /// <summary>Represents an instance of <see cref="T:System.Collections.Comparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />. This field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        public static readonly Comparer DefaultInvariant = new Comparer(CultureInfo.InvariantCulture);

        private const string CompareInfoName = "CompareInfo";

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Comparer" /> class using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use for the new <see cref="T:System.Collections.Comparer" />. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="culture" /> is null. </exception>
        public Comparer(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }
            Contract.EndContractBlock();
            _compareInfo = culture.CompareInfo;
        }

        // Compares two Objects by calling CompareTo.
        // If a == b, 0 is returned.
        // If a implements IComparable, a.CompareTo(b) is returned.
        // If a doesn't implement IComparable and b does, -(b.CompareTo(a)) is returned.
        // Otherwise an exception is thrown.
        // 
        /// <summary>Performs a case-sensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
        /// <returns>A signed integer that indicates the relative values of <paramref name="a" /> and <paramref name="b" />, as shown in the following table.Value Meaning Less than zero <paramref name="a" /> is less than <paramref name="b" />. Zero <paramref name="a" /> equals <paramref name="b" />. Greater than zero <paramref name="a" /> is greater than <paramref name="b" />. </returns>
        /// <param name="a">The first object to compare. </param>
        /// <param name="b">The second object to compare. </param>
        /// <exception cref="T:System.ArgumentException">Neither <paramref name="a" /> nor <paramref name="b" /> implements the <see cref="T:System.IComparable" /> interface.-or- <paramref name="a" /> and <paramref name="b" /> are of different types and neither one can handle comparisons with the other. </exception>
        /// <filterpriority>2</filterpriority>
        public int Compare(Object a, Object b)
        {
            if (a == b) return 0;
            if (a == null) return -1;
            if (b == null) return 1;

            string sa = a as string;
            string sb = b as string;
            if (sa != null && sb != null)
                return _compareInfo.Compare(sa, sb);

            IComparable ia = a as IComparable;
            if (ia != null)
                return ia.CompareTo(b);

            IComparable ib = b as IComparable;
            if (ib != null)
                return -ib.CompareTo(a);

            throw new ArgumentException(SR.Argument_ImplementIComparable);
        }
    }
}
