// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*=============================================================================
**
** Class: Stack
**
** Purpose: Represents a simple last-in-first-out (LIFO)
**          non-generic collection of objects.
**
**
=============================================================================*/

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace System.Collections
{
    // A simple stack of objects.  Internally it is implemented as an array,
    // so Push can be O(n).  Pop is O(1).
    /// <summary>Represents a simple last-in-first-out (LIFO) non-generic collection of objects.</summary>
    /// <filterpriority>1</filterpriority>
    [DebuggerTypeProxy(typeof(System.Collections.Stack.StackDebugView))]
    [DebuggerDisplay("Count = {Count}")]
    public class Stack : ICollection
    {
        private Object[] _array;     // Storage for stack elements
        [ContractPublicPropertyName("Count")]
        private int _size;           // Number of items in the stack.
        private int _version;        // Used to keep enumerator in sync w/ collection.
        private Object _syncRoot;

        private const int _defaultCapacity = 10;

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the default initial capacity.</summary>
        public Stack()
        {
            _array = new Object[_defaultCapacity];
            _size = 0;
            _version = 0;
        }

        // Create a stack with a specific initial capacity.  The initial capacity
        // must be a non-negative number.
        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
        /// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.Stack" /> can contain. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="initialCapacity" /> is less than zero. </exception>
        public Stack(int initialCapacity)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity), SR.ArgumentOutOfRange_NeedNonNegNum);
            Contract.EndContractBlock();
            if (initialCapacity < _defaultCapacity)
                initialCapacity = _defaultCapacity;  // Simplify doubling logic in Push.
            _array = new Object[initialCapacity];
            _size = 0;
            _version = 0;
        }

        // Fills a Stack with the contents of a particular collection.  The items are
        // pushed onto the stack in the same order they are read by the enumerator.
        //
        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that contains elements copied from the specified collection and has the same initial capacity as the number of elements copied.</summary>
        /// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="col" /> is null. </exception>
        public Stack(ICollection col) : this((col == null ? 32 : col.Count))
        {
            if (col == null)
                throw new ArgumentNullException(nameof(col));
            Contract.EndContractBlock();
            IEnumerator en = col.GetEnumerator();
            while (en.MoveNext())
                Push(en.Current);
        }

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Stack" />.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Stack" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return _size;
            }
        }

        /// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe).</summary>
        /// <returns>true, if access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</summary>
        /// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual Object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange<Object>(ref _syncRoot, new Object(), null);
                }
                return _syncRoot;
            }
        }

        // Removes all Objects from the Stack.
        /// <summary>Removes all objects from the <see cref="T:System.Collections.Stack" />.</summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Clear()
        {
            Array.Clear(_array, 0, _size); // Don't need to doc this but we clear the elements so that the gc can reclaim the references.
            _size = 0;
            _version++;
        }

        /// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Stack" />.</summary>
        /// <returns>A shallow copy of the <see cref="T:System.Collections.Stack" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual Object Clone()
        {
            Contract.Ensures(Contract.Result<Object>() != null);

            Stack s = new Stack(_size);
            s._size = _size;
            Array.Copy(_array, 0, s._array, 0, _size);
            s._version = _version;
            return s;
        }

        /// <summary>Determines whether an element is in the <see cref="T:System.Collections.Stack" />.</summary>
        /// <returns>true, if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Stack" />; otherwise, false.</returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Stack" />. The value can be null. </param>
        /// <filterpriority>2</filterpriority>
        public virtual bool Contains(Object obj)
        {
            int count = _size;

            while (count-- > 0)
            {
                if (obj == null)
                {
                    if (_array[count] == null)
                        return true;
                }
                else if (_array[count] != null && _array[count].Equals(obj))
                {
                    return true;
                }
            }
            return false;
        }

        // Copies the stack into an array.
        /// <summary>Copies the <see cref="T:System.Collections.Stack" /> to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Stack" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Stack" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
        /// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Stack" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
        /// <filterpriority>2</filterpriority>
        public virtual void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (array.Rank != 1)
                throw new ArgumentException(SR.Arg_RankMultiDimNotSupported);
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), SR.ArgumentOutOfRange_NeedNonNegNum);
            if (array.Length - index < _size)
                throw new ArgumentException(SR.Argument_InvalidOffLen);
            Contract.EndContractBlock();

            int i = 0;
            object[] objArray = array as object[];
            if (objArray != null)
            {
                while (i < _size)
                {
                    objArray[i + index] = _array[_size - i - 1];
                    i++;
                }
            }
            else
            {
                while (i < _size)
                {
                    array.SetValue(_array[_size - i - 1], i + index);
                    i++;
                }
            }
        }

        // Returns an IEnumerator for this Stack.
        /// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual IEnumerator GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator>() != null);
            return new StackEnumerator(this);
        }

        // Returns the top object on the stack without removing it.  If the stack
        // is empty, Peek throws an InvalidOperationException.
        /// <summary>Returns the object at the top of the <see cref="T:System.Collections.Stack" /> without removing it.</summary>
        /// <returns>The <see cref="T:System.Object" /> at the top of the <see cref="T:System.Collections.Stack" />.</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty. </exception>
        /// <filterpriority>2</filterpriority>
        public virtual Object Peek()
        {
            if (_size == 0)
                throw new InvalidOperationException(SR.InvalidOperation_EmptyStack);
            Contract.EndContractBlock();
            return _array[_size - 1];
        }

        // Pops an item from the top of the stack.  If the stack is empty, Pop
        // throws an InvalidOperationException.
        /// <summary>Removes and returns the object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
        /// <returns>The <see cref="T:System.Object" /> removed from the top of the <see cref="T:System.Collections.Stack" />.</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty. </exception>
        /// <filterpriority>2</filterpriority>
        public virtual Object Pop()
        {
            if (_size == 0)
                throw new InvalidOperationException(SR.InvalidOperation_EmptyStack);
            //Contract.Ensures(Count == Contract.OldValue(Count) - 1);
            Contract.EndContractBlock();
            _version++;
            Object obj = _array[--_size];
            _array[_size] = null;     // Free memory quicker.
            return obj;
        }

        // Pushes an item to the top of the stack.
        // 
        /// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
        /// <param name="obj">The <see cref="T:System.Object" /> to push onto the <see cref="T:System.Collections.Stack" />. The value can be null. </param>
        /// <filterpriority>2</filterpriority>
        public virtual void Push(Object obj)
        {
            //Contract.Ensures(Count == Contract.OldValue(Count) + 1);
            if (_size == _array.Length)
            {
                Object[] newArray = new Object[2 * _array.Length];
                Array.Copy(_array, 0, newArray, 0, _size);
                _array = newArray;
            }
            _array[_size++] = obj;
            _version++;
        }

        // Returns a synchronized Stack.
        //
        /// <summary>Returns a synchronized (thread safe) wrapper for the <see cref="T:System.Collections.Stack" />.</summary>
        /// <returns>A synchronized wrapper around the <see cref="T:System.Collections.Stack" />.</returns>
        /// <param name="stack">The <see cref="T:System.Collections.Stack" /> to synchronize. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="stack" /> is null. </exception>
        /// <filterpriority>2</filterpriority>
        public static Stack Synchronized(Stack stack)
        {
            if (stack == null)
                throw new ArgumentNullException(nameof(stack));
            Contract.Ensures(Contract.Result<Stack>() != null);
            Contract.EndContractBlock();
            return new SyncStack(stack);
        }


        // Copies the Stack to an array, in the same order Pop would return the items.
        /// <summary>Copies the <see cref="T:System.Collections.Stack" /> to a new array.</summary>
        /// <returns>A new array containing copies of the elements of the <see cref="T:System.Collections.Stack" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual Object[] ToArray()
        {
            Contract.Ensures(Contract.Result<Object[]>() != null);

            if (_size == 0)
                return Array.Empty<Object>();

            Object[] objArray = new Object[_size];
            int i = 0;
            while (i < _size)
            {
                objArray[i] = _array[_size - i - 1];
                i++;
            }
            return objArray;
        }

        private class SyncStack : Stack
        {
            private Stack _s;
            private Object _root;

            internal SyncStack(Stack stack)
            {
                _s = stack;
                _root = stack.SyncRoot;
            }

            public override bool IsSynchronized
            {
                get { return true; }
            }

            public override Object SyncRoot
            {
                get
                {
                    return _root;
                }
            }

            public override int Count
            {
                get
                {
                    lock (_root)
                    {
                        return _s.Count;
                    }
                }
            }

            public override bool Contains(Object obj)
            {
                lock (_root)
                {
                    return _s.Contains(obj);
                }
            }

            public override Object Clone()
            {
                lock (_root)
                {
                    return new SyncStack((Stack)_s.Clone());
                }
            }

            public override void Clear()
            {
                lock (_root)
                {
                    _s.Clear();
                }
            }

            public override void CopyTo(Array array, int arrayIndex)
            {
                lock (_root)
                {
                    _s.CopyTo(array, arrayIndex);
                }
            }

            public override void Push(Object value)
            {
                lock (_root)
                {
                    _s.Push(value);
                }
            }

            [SuppressMessage("Microsoft.Contracts", "CC1055")]  // Thread safety problems with precondition - can't express the precondition as of Dev10.
            public override Object Pop()
            {
                lock (_root)
                {
                    return _s.Pop();
                }
            }

            public override IEnumerator GetEnumerator()
            {
                lock (_root)
                {
                    return _s.GetEnumerator();
                }
            }

            [SuppressMessage("Microsoft.Contracts", "CC1055")]  // Thread safety problems with precondition - can't express the precondition
            public override Object Peek()
            {
                lock (_root)
                {
                    return _s.Peek();
                }
            }

            public override Object[] ToArray()
            {
                lock (_root)
                {
                    return _s.ToArray();
                }
            }
        }


        private class StackEnumerator : IEnumerator
        {
            private Stack _stack;
            private int _index;
            private int _version;
            private Object _currentElement;

            internal StackEnumerator(Stack stack)
            {
                _stack = stack;
                _version = _stack._version;
                _index = -2;
                _currentElement = null;
            }

            public virtual bool MoveNext()
            {
                bool retval;
                if (_version != _stack._version) throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
                if (_index == -2)
                {  // First call to enumerator.
                    _index = _stack._size - 1;
                    retval = (_index >= 0);
                    if (retval)
                        _currentElement = _stack._array[_index];
                    return retval;
                }
                if (_index == -1)
                {  // End of enumeration.
                    return false;
                }

                retval = (--_index >= 0);
                if (retval)
                    _currentElement = _stack._array[_index];
                else
                    _currentElement = null;
                return retval;
            }

            public virtual Object Current
            {
                get
                {
                    if (_index == -2) throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
                    if (_index == -1) throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
                    return _currentElement;
                }
            }

            public virtual void Reset()
            {
                if (_version != _stack._version) throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
                _index = -2;
                _currentElement = null;
            }
        }

        internal class StackDebugView
        {
            private Stack _stack;

            public StackDebugView(Stack stack)
            {
                if (stack == null)
                    throw new ArgumentNullException(nameof(stack));
                Contract.EndContractBlock();

                _stack = stack;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public Object[] Items
            {
                get
                {
                    return _stack.ToArray();
                }
            }
        }
    }
}
