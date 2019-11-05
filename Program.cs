using System;

namespace RingBuffer
{
    class PlayerSnapshot
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RingBuffer<PlayerSnapshot> ringBuffer = new RingBuffer<PlayerSnapshot>(20);
            Console.WriteLine(ringBuffer.Capacity);
        }
    }
}
