using System;
using System.Collections.Generic;
using System.Text;

namespace RingBuffer
{
    public class CircularBuffer<T>
    {
        private Queue<T> buffer;

        public int Capacity { get; private set; }
        public int Count {
            get
            {
                return buffer.Count;
            } 
        }

        public CircularBuffer(int _capacity)
        {
            buffer = new Queue<T>(_capacity);
            Capacity = _capacity;
        }

        public void Add(T obj)
        {
            if (buffer.Count == Capacity)
            {
                buffer.Dequeue();
                buffer.Enqueue(obj);
            }
            else
                buffer.Enqueue(obj);
        }
        public T Read()
        {
            return buffer.Dequeue();
        }

        public T Peek()
        {
            return buffer.Peek();
        }
    }

}
