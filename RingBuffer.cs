using System;
using System.Collections.Generic;
using System.Text;

namespace RingBuffer
{
    public class RingBuffer<T>
    {
        public RingBuffer(int _capacity)
        {
            if (_capacity > 0)
            {
                Capacity = _capacity;
                buffer = new T[_capacity];
            }
            else throw new ArgumentException("Must be greater than zero!", "_capacity");
        }

        #region Properties
        public int Capacity { get; private set; }
        #endregion

        private T[] buffer;
    }
}
