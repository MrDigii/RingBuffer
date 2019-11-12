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
            if (_capacity <= 0) throw new ArgumentException("Invalid capacity!", "_capacity");
            Capacity = _capacity;
            buffer = new T[_capacity];
            tail = 0;
            Count = 0;
        }

        #region Properties
        public int Capacity { get; private set; }
        public int Count { get; private set; }
        #endregion

        private T[] buffer;
        private int tail;

        #region Methods
        public void Enqueue(T _item)
        {
            // avoid arithmetic overflow
            if (tail == int.MaxValue)
                tail = 0;

            // set new item
            buffer[tail % Capacity] = _item;

            // increment tail
            tail = (tail + 1) % Capacity;
            if (Count < Capacity) Count++;
        }

        public T Dequeue()
        {
            if (Count == 0) throw new ArgumentException("Buffer is empty!");

            // get head of buffer
            int head = (tail - (Count - 1)) % Capacity;
            if (head < 0) head += Capacity;

            // decrement count to length without deleted item
            if (Count > 0) Count--;

            // get removed item
            return buffer[head];
        }

        public T this[int _index]
        {
            get
            {
                if (_index < 0 || _index >= Count) throw new IndexOutOfRangeException();
                // get relative index
                int relativeIndex = (tail - Count + _index) % Capacity; // attention % != Mod see: https://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain
                if (relativeIndex < 0) relativeIndex = relativeIndex + Capacity;
                return buffer[relativeIndex];
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
                buffer[i] = default;
            tail = 0;
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
