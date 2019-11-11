using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            int itemSize = 2000;
            int bufferCapacity = 128;
            RingBuffer<int> ringBuffer = new RingBuffer<int>(bufferCapacity);
            CircularBuffer<int> circularBuffer = new CircularBuffer<int>(bufferCapacity);

            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < itemSize; i++)
            {
                circularBuffer.Add(i);
            }
            for (int i = 0; i < bufferCapacity; i++)
            {
                circularBuffer.Read();
            }
            sw.Stop();

            TimeSpan elapsedTime3 = sw.Elapsed;
            Console.WriteLine("RingBuffer Queue={0} ns", elapsedTime3.TotalMilliseconds * 1000000);

            sw.Restart();
            for (int i = 0; i < itemSize; i++)
            {
                ringBuffer.Enqueue(i);
            }

            for (int i = 0; i < bufferCapacity; i++)
            {
                ringBuffer.Dequeue();
            }
            sw.Stop();

            TimeSpan elapsedTime2 = sw.Elapsed;
            Console.WriteLine("RingBuffer={0} ns", elapsedTime2.TotalMilliseconds * 1000000);

            Console.WriteLine($"Capacity: {ringBuffer.Capacity} Count: {ringBuffer.Count} IsFull: {ringBuffer.Count == ringBuffer.Capacity} IsEmpty: {ringBuffer.Count == 0}");            
            foreach (int snapshot in ringBuffer)
            {
                Console.WriteLine($"Id: {snapshot}");
            }
            
        }
    }
}
