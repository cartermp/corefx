// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*=============================================================================
**
** Class: CollectionBase
**
** Purpose: Provides the abstract base class for a strongly typed collection.
**
=============================================================================*/

using System.Diagnostics.Contracts;

namespace System.Collections
{
    // Useful base class for typed read/write collections where items derive from object
    /// <summary>Provides the abstract base class for a strongly typed collection.</summary>
    /// <filterpriority>2</filterpriority>
    public abstract class CollectionBase : IList
    {
        private ArrayList _list;

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.CollectionBase" /> class with the default initial capacity.</summary>
        protected CollectionBase()
        {
            _list = new ArrayList();
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.CollectionBase" /> class with the specified capacity.</summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        protected CollectionBase(int capacity)
        {
            _list = new ArrayList(capacity);
        }


        /// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> containing the list of elements in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        /// <returns>An <see cref="T:System.Collections.ArrayList" /> representing the <see cref="T:System.Collections.CollectionBase" /> instance itself.Retrieving the value of this property is an O(1) operation.</returns>
        protected ArrayList InnerList
        {
            get
            {
                return _list;
            }
        }

        /// <summary>Gets an <see cref="T:System.Collections.IList" /> containing the list of elements in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        /// <returns>An <see cref="T:System.Collections.IList" /> representing the <see cref="T:System.Collections.CollectionBase" /> instance itself.</returns>
        protected IList List
        {
            get { return (IList)this; }
        }

        /// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.CollectionBase" /> can contain.</summary>
        /// <returns>The number of elements that the <see cref="T:System.Collections.CollectionBase" /> can contain.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><see cref="P:System.Collections.CollectionBase.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
        /// <filterpriority>2</filterpriority>
        public int Capacity
        {
            get
            {
                return InnerList.Capacity;
            }
            set
            {
                InnerList.Capacity = value;
            }
        }


        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.CollectionBase" /> instance. This property cannot be overridden.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.CollectionBase" /> instance.Retrieving the value of this property is an O(1) operation.</returns>
        /// <filterpriority>2</filterpriority>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>Removes all objects from the <see cref="T:System.Collections.CollectionBase" /> instance. This method cannot be overridden.</summary>
        /// <filterpriority>2</filterpriority>
        public void Clear()
        {
            OnClear();
            InnerList.Clear();
            OnClearComplete();
        }

        /// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.CollectionBase" /> instance. This method is not overridable.</summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
        /// <filterpriority>2</filterpriority>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index), SR.ArgumentOutOfRange_Index);
            Contract.EndContractBlock();
            Object temp = InnerList[index];
            OnValidate(temp);
            OnRemove(index, temp);
            InnerList.RemoveAt(index);
            try
            {
                OnRemoveComplete(index, temp);
            }
            catch
            {
                InnerList.Insert(index, temp);
                throw;
            }
        }

        bool IList.IsReadOnly
        {
            get { return InnerList.IsReadOnly; }
        }

        bool IList.IsFixedSize
        {
            get { return InnerList.IsFixedSize; }
        }

        bool ICollection.IsSynchronized
        {
            get { return InnerList.IsSynchronized; }
        }

        Object ICollection.SyncRoot
        {
            get { return InnerList.SyncRoot; }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            InnerList.CopyTo(array, index);
        }

        Object IList.this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index), SR.ArgumentOutOfRange_Index);
                Contract.EndContractBlock();
                return InnerList[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index), SR.ArgumentOutOfRange_Index);
                Contract.EndContractBlock();
                OnValidate(value);
                Object temp = InnerList[index];
                OnSet(index, temp, value);
                InnerList[index] = value;
                try
                {
                    OnSetComplete(index, temp, value);
                }
                catch
                {
                    InnerList[index] = temp;
                    throw;
                }
            }
        }

        bool IList.Contains(Object value)
        {
            return InnerList.Contains(value);
        }

        int IList.Add(Object value)
        {
            OnValidate(value);
            OnInsert(InnerList.Count, value);
            int index = InnerList.Add(value);
            try
            {
                OnInsertComplete(index, value);
            }
            catch
            {
                InnerList.RemoveAt(index);
                throw;
            }
            return index;
        }


        void IList.Remove(Object value)
        {
            OnValidate(value);
            int index = InnerList.IndexOf(value);
            if (index < 0) throw new ArgumentException(SR.Arg_RemoveArgNotFound);
            OnRemove(index, value);
            InnerList.RemoveAt(index);
            try
            {
                OnRemoveComplete(index, value);
            }
            catch
            {
                InnerList.Insert(index, value);
                throw;
            }
        }

        int IList.IndexOf(Object value)
        {
            return InnerList.IndexOf(value);
        }

        void IList.Insert(int index, Object value)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), SR.ArgumentOutOfRange_Index);
            Contract.EndContractBlock();
            OnValidate(value);
            OnInsert(index, value);
            InnerList.Insert(index, value);
            try
            {
                OnInsertComplete(index, value);
            }
            catch
            {
                InnerList.RemoveAt(index);
                throw;
            }
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.CollectionBase" /> instance.</returns>
        /// <filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        /// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        /// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
        /// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
        /// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
        protected virtual void OnSet(int index, Object oldValue, Object newValue)
        {
        }

        /// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        /// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
        /// <param name="value">The new value of the element at <paramref name="index" />.</param>
        protected virtual void OnInsert(int index, Object value)
        {
        }

        /// <summary>Performs additional custom processes when clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        protected virtual void OnClear()
        {
        }

        /// <summary>Performs additional custom processes when removing an element from the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        /// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
        /// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
        protected virtual void OnRemove(int index, Object value)
        {
        }

        /// <summary>Performs additional custom processes when validating a value.</summary>
        /// <param name="value">The object to validate.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="value" /> is null.</exception>
        protected virtual void OnValidate(Object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Contract.EndContractBlock();
        }

        /// <summary>Performs additional custom processes after setting a value in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        /// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
        /// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
        /// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
        protected virtual void OnSetComplete(int index, Object oldValue, Object newValue)
        {
        }

        /// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        /// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
        /// <param name="value">The new value of the element at <paramref name="index" />.</param>
        protected virtual void OnInsertComplete(int index, Object value)
        {
        }

        /// <summary>Performs additional custom processes after clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        protected virtual void OnClearComplete()
        {
        }

        /// <summary>Performs additional custom processes after removing an element from the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
        /// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
        /// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
        protected virtual void OnRemoveComplete(int index, Object value)
        {
        }
    }
}
