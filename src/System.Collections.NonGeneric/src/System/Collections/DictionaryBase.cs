// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*============================================================
**
** Class:  DictionaryBase
**
** Purpose: Provides the abstract base class for a
**          strongly typed collection of key/value pairs.
**
===========================================================*/


namespace System.Collections
{
    // Useful base class for typed read/write collections where items derive from object
    /// <summary>Provides the abstract base class for a strongly typed collection of key/value pairs.</summary>
    /// <filterpriority>2</filterpriority>
    public abstract class DictionaryBase : IDictionary
    {
        private Hashtable _hashtable;

        /// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <returns>A <see cref="T:System.Collections.Hashtable" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
        protected Hashtable InnerHashtable
        {
            get
            {
                if (_hashtable == null)
                    _hashtable = new Hashtable();
                return _hashtable;
            }
        }

        /// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <returns>An <see cref="T:System.Collections.IDictionary" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
        protected IDictionary Dictionary
        {
            get { return (IDictionary)this; }
        }

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
        /// <filterpriority>2</filterpriority>
        public int Count
        {
            // to avoid newing inner list if no items are ever added
            get { return _hashtable == null ? 0 : _hashtable.Count; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return InnerHashtable.IsReadOnly; }
        }

        bool IDictionary.IsFixedSize
        {
            get { return InnerHashtable.IsFixedSize; }
        }

        bool ICollection.IsSynchronized
        {
            get { return InnerHashtable.IsSynchronized; }
        }

        ICollection IDictionary.Keys
        {
            get { return InnerHashtable.Keys; }
        }

        Object ICollection.SyncRoot
        {
            get { return InnerHashtable.SyncRoot; }
        }

        ICollection IDictionary.Values
        {
            get { return InnerHashtable.Values; }
        }

        /// <summary>Copies the <see cref="T:System.Collections.DictionaryBase" /> elements to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from the <see cref="T:System.Collections.DictionaryBase" /> instance. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.DictionaryBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
        /// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.DictionaryBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
        /// <filterpriority>2</filterpriority>
        public void CopyTo(Array array, int index)
        {
            InnerHashtable.CopyTo(array, index);
        }

        object IDictionary.this[object key]
        {
            get
            {
                object currentValue = InnerHashtable[key];
                OnGet(key, currentValue);
                return currentValue;
            }
            set
            {
                OnValidate(key, value);
                bool keyExists = true;
                Object temp = InnerHashtable[key];
                if (temp == null)
                {
                    keyExists = InnerHashtable.Contains(key);
                }

                OnSet(key, temp, value);
                InnerHashtable[key] = value;
                try
                {
                    OnSetComplete(key, temp, value);
                }
                catch
                {
                    if (keyExists)
                    {
                        InnerHashtable[key] = temp;
                    }
                    else
                    {
                        InnerHashtable.Remove(key);
                    }
                    throw;
                }
            }
        }

        bool IDictionary.Contains(object key)
        {
            return InnerHashtable.Contains(key);
        }

        void IDictionary.Add(object key, object value)
        {
            OnValidate(key, value);
            OnInsert(key, value);
            InnerHashtable.Add(key, value);
            try
            {
                OnInsertComplete(key, value);
            }
            catch
            {
                InnerHashtable.Remove(key);
                throw;
            }
        }

        /// <summary>Clears the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <filterpriority>2</filterpriority>
        public void Clear()
        {
            OnClear();
            InnerHashtable.Clear();
            OnClearComplete();
        }

        void IDictionary.Remove(object key)
        {
            if (InnerHashtable.Contains(key))
            {
                Object temp = InnerHashtable[key];
                OnValidate(key, temp);
                OnRemove(key, temp);

                InnerHashtable.Remove(key);
                try
                {
                    OnRemoveComplete(key, temp);
                }
                catch
                {
                    InnerHashtable.Add(key, temp);
                    throw;
                }
            }
        }

        /// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
        /// <filterpriority>2</filterpriority>
        public IDictionaryEnumerator GetEnumerator()
        {
            return InnerHashtable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InnerHashtable.GetEnumerator();
        }

        /// <summary>Gets the element with the specified key and value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <returns>An <see cref="T:System.Object" /> containing the element with the specified key and value.</returns>
        /// <param name="key">The key of the element to get. </param>
        /// <param name="currentValue">The current value of the element associated with <paramref name="key" />. </param>
        protected virtual object OnGet(object key, object currentValue)
        {
            return currentValue;
        }

        /// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <param name="key">The key of the element to locate. </param>
        /// <param name="oldValue">The old value of the element associated with <paramref name="key" />. </param>
        /// <param name="newValue">The new value of the element associated with <paramref name="key" />. </param>
        protected virtual void OnSet(object key, object oldValue, object newValue)
        {
        }

        /// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <param name="key">The key of the element to insert. </param>
        /// <param name="value">The value of the element to insert. </param>
        protected virtual void OnInsert(object key, object value)
        {
        }

        /// <summary>Performs additional custom processes before clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        protected virtual void OnClear()
        {
        }

        /// <summary>Performs additional custom processes before removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <param name="key">The key of the element to remove. </param>
        /// <param name="value">The value of the element to remove. </param>
        protected virtual void OnRemove(object key, object value)
        {
        }

        /// <summary>Performs additional custom processes when validating the element with the specified key and value.</summary>
        /// <param name="key">The key of the element to validate. </param>
        /// <param name="value">The value of the element to validate. </param>
        protected virtual void OnValidate(object key, object value)
        {
        }

        /// <summary>Performs additional custom processes after setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <param name="key">The key of the element to locate. </param>
        /// <param name="oldValue">The old value of the element associated with <paramref name="key" />. </param>
        /// <param name="newValue">The new value of the element associated with <paramref name="key" />. </param>
        protected virtual void OnSetComplete(object key, object oldValue, object newValue)
        {
        }

        /// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <param name="key">The key of the element to insert. </param>
        /// <param name="value">The value of the element to insert. </param>
        protected virtual void OnInsertComplete(object key, object value)
        {
        }

        /// <summary>Performs additional custom processes after clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        protected virtual void OnClearComplete()
        {
        }

        /// <summary>Performs additional custom processes after removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
        /// <param name="key">The key of the element to remove. </param>
        /// <param name="value">The value of the element to remove. </param>
        protected virtual void OnRemoveComplete(object key, object value)
        {
        }
    }
}
