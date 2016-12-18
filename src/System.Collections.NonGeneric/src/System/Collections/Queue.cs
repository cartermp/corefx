// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*=============================================================================
**
** Class: Queue
**
** Purpose: Represents a first-in, first-out collection of objects.
**
=============================================================================*/

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace System.Collections
{
    // A simple Queue of objects.  Internally it is implemented as a circular
    // buffer, so Enqueue can be O(n).  Dequeue is O(1).
    /// <summary>Represents a first-in, first-out collection of objects.</summary>
    /// <filterpriority>1</filterpriority>
    [DebuggerTypeProxy(typeof(System.Collections.Queue.QueueDebugView))]
    [DebuggerDisplay("Count = {Count}")]
    public class Queue : ICollection
    {
        private Object[] _array;
        private int _head;       // First valid element in the queue
        private int _tail;       // Last valid element in the queue
        private int _size;       // Number of elements.
        private int _growFactor; // 100 == 1.0, 130 == 1.3, 200 == 2.0
        private int _version;
        private Object _syncRoot;

        private const int _MinimumGrow = 4;
        private const int _ShrinkThreshold = 32;

        // Creates a queue with room for capacity objects. The default initial
        // capacity and grow factor are used.
        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the default initial capacity, and uses the default growth factor.</summary>
        public Queue()
            : this(32, (float)2.0)
        {
        }

        // Creates a queue with room for capacity objects. The default grow factor
        // is used.
        //
        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the default growth factor.</summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity" /> is less than zero. </exception>
        public Queue(int capacity)
            : this(capacity, (float)2.0)
        {
        }

        // Creates a queue with room for capacity objects. When full, the new
        // capacity is set to the old capacity * growFactor.
        //
        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the specified growth factor.</summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
        /// <param name="growFactor">The factor by which the capacity of the <see cref="T:System.Collections.Queue" /> is expanded. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity" /> is less than zero.-or- <paramref name="growFactor" /> is less than 1.0 or greater than 10.0. </exception>
        public Queue(int capacity, float growFactor)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), SR.ArgumentOutOfRange_NeedNonNegNum);
            if (!(growFactor >= 1.0 && growFactor <= 10.0))
                throw new ArgumentOutOfRangeException(nameof(growFactor), SR.Format(SR.ArgumentOutOfRange_QueueGrowFactor, 1, 10));
            Contract.EndContractBlock();

            _array = new Object[capacity];
            _head = 0;
            _tail = 0;
            _size = 0;
            _growFactor = (int)(growFactor * 100);
        }

        // Fills a Queue with the elements of an ICollection.  Uses the enumerator
        // to get each of the elements.
        //
        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that contains elements copied from the specified collection, has the same initial capacity as the number of elements copied, and uses the default growth factor.</summary>
        /// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="col" /> is null. </exception>
        public Queue(ICollection col) : this((col == null ? 32 : col.Count))
        {
            if (col == null)
                throw new ArgumentNullException(nameof(col));
            Contract.EndContractBlock();
            IEnumerator en = col.GetEnumerator();
            while (en.MoveNext())
                Enqueue(en.Current);
        }


        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Queue" />.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Queue" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual int Count
        {
            get { return _size; }
        }

        /// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Queue" />.</summary>
        /// <returns>A shallow copy of the <see cref="T:System.Collections.Queue" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual Object Clone()
        {
            Queue q = new Queue(_size);
            q._size = _size;

            int numToCopy = _size;
            int firstPart = (_array.Length - _head < numToCopy) ? _array.Length - _head : numToCopy;
            Array.Copy(_array, _head, q._array, 0, firstPart);
            numToCopy -= firstPart;
            if (numToCopy > 0)
                Array.Copy(_array, 0, q._array, _array.Length - _head, numToCopy);

            q._version = _version;
            return q;
        }

        /// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe).</summary>
        /// <returns>true if access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</summary>
        /// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual Object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange(ref _syncRoot, new Object(), null);
                }
                return _syncRoot;
            }
        }

        // Removes all Objects from the queue.
        /// <summary>Removes all objects from the <see cref="T:System.Collections.Queue" />.</summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Clear()
        {
            if (_size != 0)
            {
                if (_head < _tail)
                    Array.Clear(_array, _head, _size);
                else
                {
                    Array.Clear(_array, _head, _array.Length - _head);
                    Array.Clear(_array, 0, _tail);
                }

                _size = 0;
            }

            _head = 0;
            _tail = 0;
            _version++;
        }

        // CopyTo copies a collection into an Array, starting at a particular
        // index into the array.
        // 
        /// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Queue" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Queue" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
        /// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Collections.Queue" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
        /// <filterpriority>2</filterpriority>
        public virtual void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (array.Rank != 1)
                throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, nameof(array));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), SR.ArgumentOutOfRange_Index);
            Contract.EndContractBlock();
            int arrayLen = array.Length;
            if (arrayLen - index < _size)
                throw new ArgumentException(SR.Argument_InvalidOffLen);

            int numToCopy = _size;
            if (numToCopy == 0)
                return;
            int firstPart = (_array.Length - _head < numToCopy) ? _array.Length - _head : numToCopy;
            Array.Copy(_array, _head, array, index, firstPart);
            numToCopy -= firstPart;
            if (numToCopy > 0)
                Array.Copy(_array, 0, array, index + _array.Length - _head, numToCopy);
        }

        // Adds obj to the tail of the queue.
        //
        /// <summary>Adds an object to the end of the <see cref="T:System.Collections.Queue" />.</summary>
        /// <param name="obj">The object to add to the <see cref="T:System.Collections.Queue" />. The value can be null. </param>
        /// <filterpriority>2</filterpriority>
        public virtual void Enqueue(Object obj)
        {
            if (_size == _array.Length)
            {
                int newcapacity = (int)((long)_array.Length * (long)_growFactor / 100);
                if (newcapacity < _array.Length + _MinimumGrow)
                {
                    newcapacity = _array.Length + _MinimumGrow;
                }
                SetCapacity(newcapacity);
            }

            _array[_tail] = obj;
            _tail = (_tail + 1) % _array.Length;
            _size++;
            _version++;
        }

        // GetEnumerator returns an IEnumerator over this Queue.  This
        // Enumerator will support removing.
        // 
        /// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Queue" />.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Queue" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual IEnumerator GetEnumerator()
        {
            return new QueueEnumerator(this);
        }

        // Removes the object at the head of the queue and returns it. If the queue
        // is empty, this method simply returns null.
        /// <summary>Removes and returns the object at the beginning of the <see cref="T:System.Collections.Queue" />.</summary>
        /// <returns>The object that is removed from the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
        /// <filterpriority>2</filterpriority>
        public virtual Object Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException(SR.InvalidOperation_EmptyQueue);
            Contract.EndContractBlock();

            Object removed = _array[_head];
            _array[_head] = null;
            _head = (_head + 1) % _array.Length;
            _size--;
            _version++;
            return removed;
        }

        // Returns the object at the head of the queue. The object remains in the
        // queue. If the queue is empty, this method throws an 
        // InvalidOperationException.
        /// <summary>Returns the object at the beginning of the <see cref="T:System.Collections.Queue" /> without removing it.</summary>
        /// <returns>The object at the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
        /// <filterpriority>2</filterpriority>
        public virtual Object Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException(SR.InvalidOperation_EmptyQueue);
            Contract.EndContractBlock();

            return _array[_head];
        }

        // Returns a synchronized Queue.  Returns a synchronized wrapper
        // class around the queue - the caller must not use references to the
        // original queue.
        // 
        /// <summary>Returns a new <see cref="T:System.Collections.Queue" /> that wraps the original queue, and is thread safe.</summary>
        /// <returns>A <see cref="T:System.Collections.Queue" /> wrapper that is synchronized (thread safe).</returns>
        /// <param name="queue">The <see cref="T:System.Collections.Queue" /> to synchronize. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="queue" /> is null. </exception>
        /// <filterpriority>2</filterpriority>
        public static Queue Synchronized(Queue queue)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));
            Contract.EndContractBlock();
            return new SynchronizedQueue(queue);
        }

        // Returns true if the queue contains at least one object equal to obj.
        // Equality is determined using obj.Equals().
        //
        // Exceptions: ArgumentNullException if obj == null.
        /// <summary>Determines whether an element is in the <see cref="T:System.Collections.Queue" />.</summary>
        /// <returns>true if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Queue" />; otherwise, false.</returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Queue" />. The value can be null. </param>
        /// <filterpriority>2</filterpriority>
        public virtual bool Contains(Object obj)
        {
            int index = _head;
            int count = _size;

            while (count-- > 0)
            {
                if (obj == null)
                {
                    if (_array[index] == null)
                        return true;
                }
                else if (_array[index] != null && _array[index].Equals(obj))
                {
                    return true;
                }
                index = (index + 1) % _array.Length;
            }

            return false;
        }

        internal Object GetElement(int i)
        {
            return _array[(_head + i) % _array.Length];
        }

        // Iterates over the objects in the queue, returning an array of the
        // objects in the Queue, or an empty array if the queue is empty.
        // The order of elements in the array is first in to last in, the same
        // order produced by successive calls to Dequeue.
        /// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to a new array.</summary>
        /// <returns>A new array containing elements copied from the <see cref="T:System.Collections.Queue" />.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual Object[] ToArray()
        {
            if (_size == 0)
                return Array.Empty<Object>();

            Object[] arr = new Object[_size];
            if (_head < _tail)
            {
                Array.Copy(_array, _head, arr, 0, _size);
            }
            else
            {
                Array.Copy(_array, _head, arr, 0, _array.Length - _head);
                Array.Copy(_array, 0, arr, _array.Length - _head, _tail);
            }

            return arr;
        }


        // PRIVATE Grows or shrinks the buffer to hold capacity objects. Capacity
        // must be >= _size.
        private void SetCapacity(int capacity)
        {
            Object[] newarray = new Object[capacity];
            if (_size > 0)
            {
                if (_head < _tail)
                {
                    Array.Copy(_array, _head, newarray, 0, _size);
                }
                else
                {
                    Array.Copy(_array, _head, newarray, 0, _array.Length - _head);
                    Array.Copy(_array, 0, newarray, _array.Length - _head, _tail);
                }
            }

            _array = newarray;
            _head = 0;
            _tail = (_size == capacity) ? 0 : _size;
            _version++;
        }

        /// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Queue" />.</summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Queue" /> is read-only.</exception>
        /// <filterpriority>2</filterpriority>
        public virtual void TrimToSize()
        {
            SetCapacity(_size);
        }


        // Implements a synchronization wrapper around a queue.
        private class SynchronizedQueue : Queue
        {
            private Queue _q;
            private Object _root;

            internal SynchronizedQueue(Queue q)
            {
                _q = q;
                _root = _q.SyncRoot;
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
                        return _q.Count;
                    }
                }
            }

            public override void Clear()
            {
                lock (_root)
                {
                    _q.Clear();
                }
            }

            public override Object Clone()
            {
                lock (_root)
                {
                    return new SynchronizedQueue((Queue)_q.Clone());
                }
            }

            public override bool Contains(Object obj)
            {
                lock (_root)
                {
                    return _q.Contains(obj);
                }
            }

            public override void CopyTo(Array array, int arrayIndex)
            {
                lock (_root)
                {
                    _q.CopyTo(array, arrayIndex);
                }
            }

            public override void Enqueue(Object value)
            {
                lock (_root)
                {
                    _q.Enqueue(value);
                }
            }

            [SuppressMessage("Microsoft.Contracts", "CC1055")]  // Thread safety problems with precondition - can't express the precondition as of Dev10.
            public override Object Dequeue()
            {
                lock (_root)
                {
                    return _q.Dequeue();
                }
            }

            public override IEnumerator GetEnumerator()
            {
                lock (_root)
                {
                    return _q.GetEnumerator();
                }
            }

            [SuppressMessage("Microsoft.Contracts", "CC1055")]  // Thread safety problems with precondition - can't express the precondition as of Dev10.
            public override Object Peek()
            {
                lock (_root)
                {
                    return _q.Peek();
                }
            }

            public override Object[] ToArray()
            {
                lock (_root)
                {
                    return _q.ToArray();
                }
            }

            public override void TrimToSize()
            {
                lock (_root)
                {
                    _q.TrimToSize();
                }
            }
        }


        // Implements an enumerator for a Queue.  The enumerator uses the
        // internal version number of the list to ensure that no modifications are
        // made to the list while an enumeration is in progress.
        private class QueueEnumerator : IEnumerator
        {
            private Queue _q;
            private int _index;
            private int _version;
            private Object _currentElement;

            internal QueueEnumerator(Queue q)
            {
                _q = q;
                _version = _q._version;
                _index = 0;
                _currentElement = _q._array;
                if (_q._size == 0)
                    _index = -1;
            }

            public virtual bool MoveNext()
            {
                if (_version != _q._version) throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);

                if (_index < 0)
                {
                    _currentElement = _q._array;
                    return false;
                }

                _currentElement = _q.GetElement(_index);
                _index++;

                if (_index == _q._size)
                    _index = -1;
                return true;
            }

            public virtual Object Current
            {
                get
                {
                    if (_currentElement == _q._array)
                    {
                        if (_index == 0)
                            throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
                        else
                            throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
                    }
                    return _currentElement;
                }
            }

            public virtual void Reset()
            {
                if (_version != _q._version) throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
                if (_q._size == 0)
                    _index = -1;
                else
                    _index = 0;
                _currentElement = _q._array;
            }
        }

        internal class QueueDebugView
        {
            private Queue _queue;

            public QueueDebugView(Queue queue)
            {
                if (queue == null)
                    throw new ArgumentNullException(nameof(queue));
                Contract.EndContractBlock();

                _queue = queue;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public Object[] Items
            {
                get
                {
                    return _queue.ToArray();
                }
            }
        }
    }
}
