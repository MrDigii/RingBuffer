using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RingBuffer
{
    public class RingBuffer<T> : IEnumerable<T>, IEnumerable
    {
        public RingBuffer(int _capacity)
        {
            if (_capacity > 0)
            {
                Capacity = _capacity;
                buffer = new T[_capacity];
                head = -1;
                tail = head;
                Count = 0;
            }
            else throw new ArgumentException("Must be greater than zero!", "_capacity");
        }

        #region Properties
        public int Capacity { get; private set; }
        public int Count { get; private set; }

        public bool IsFull
        {
            get
            {
                return head == (tail + 1) % Capacity;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return head == -1 && tail == -1;
            }
        }
        #endregion

        private T[] buffer;
        private int head;
        private int tail;

        #region Methods
        public void Enqueue(T _item)
        {
            // avoid arithmetic overflow
            if (tail == int.MaxValue)
                tail %= Capacity;

            if (!IsFull)
            {
                if (IsEmpty) head = 0;
                tail = (tail + 1) % Capacity;
                buffer[tail] = _item;
                if (Count < Capacity) Count++;
            } else
            {
                throw new ArgumentException("Buffer is full!");
            }
        }

        public T Dequeue()
        {
            T item;
            if (!IsEmpty)
            {
                item = buffer[head];
                if (head == tail) Clear();
                else
                {
                    head = (head + 1) % Capacity;
                    if (Count > 0) Count--;
                }
            } else
            {
                throw new ArgumentException("Buffer is empty!");
            }
            return item;
        }

        public T this[int _index]
        {
            get
            {
                if (_index < 0 || _index >= Count) throw new IndexOutOfRangeException();
                // get relative index
                int relativeIndex = (tail - Count + _index + 1) % Capacity; // attention % != Mod see: https://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain
                if (relativeIndex < 0) relativeIndex = relativeIndex + Capacity;
                return buffer[relativeIndex];
            }
        }

        public void Clear()
        {
            tail = -1;
            head = tail;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
