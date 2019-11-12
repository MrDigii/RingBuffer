using System;
using System.Collections.Generic;
using System.Text;

namespace RingBuffer
{
    public class CircularQueue<T>
    {
        private Queue<T> buffer;

        public int Capacity { get; private set; }
        public int Count {
            get
            {
                return buffer.Count;
            } 
        }

        public CircularQueue(int _capacity)
        {
            buffer = new Queue<T>(_capacity);
            Capacity = _capacity;
        }

        public void Enqueue(T obj)
        {
            if (buffer.Count == Capacity)
            {
                buffer.Dequeue();
                buffer.Enqueue(obj);
            }
            else
                buffer.Enqueue(obj);
        }
        public T Dequeue()
        {
            return buffer.Dequeue();
        }

        public T Peek()
        {
            return buffer.Peek();
        }
    }

}
