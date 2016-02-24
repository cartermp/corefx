// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*=============================================================================
**
** Class: ReadOnlyCollectionBase
**
** Purpose: Provides the abstract base class for a
**          strongly typed non-generic read-only collection.
**
=============================================================================*/


namespace System.Collections
{
    // Useful base class for typed readonly collections where items derive from object
    /// <summary>Provides the abstract base class for a strongly typed non-generic read-only collection.</summary>
    /// <filterpriority>2</filterpriority>
    public abstract class ReadOnlyCollectionBase : ICollection
    {
        private ArrayList _list;

        /// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
        /// <returns>An <see cref="T:System.Collections.ArrayList" /> representing the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance itself.</returns>
        protected ArrayList InnerList
        {
            get
            {
                if (_list == null)
                    _list = new ArrayList();
                return _list;
            }
        }

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.Retrieving the value of this property is an O(1) operation.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual int Count
        {
            get { return InnerList.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return InnerList.IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return InnerList.SyncRoot; }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            InnerList.CopyTo(array, index);
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual IEnumerator GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }
    }
}
