using System;
using System.Collections.Generic;
using System.Text;

namespace RingBuffer
{
    public class TempBuffer<T>
    {
        private T[] buffer;

        public TempBuffer(int _length)
        {
            buffer = new T[_length > 0 ? _length : 1];
            Tail = 0;
        }

        public T this[int _index]
        {
            get
            {
                return buffer[_index];
            }
        }

        public void Add(T _element)
        {
            // push array values to the right (if array is full last element will be killed)
            for (int i = buffer.Length - 1; i > 0; i--)
            {
                buffer[i] = buffer[i - 1];
            }

            // set new element on first index
            buffer[0] = _element;

            // set tail
            Tail = Math.Min(Tail + 1, buffer.Length);
        }

        public T Newest
        {
            get { return buffer[0]; }
            set { buffer[0] = value; }
        }

        public int Capacity
        {
            get { return buffer.Length; }
        }

        public int Tail
        {
            get; private set;
        }
    }
}
