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

            Queue<int> queue = new Queue<int>(bufferCapacity);
            RingBuffer<int> ringBuffer = new RingBuffer<int>(bufferCapacity);
            CircularBuffer<int> circularBuffer = new CircularBuffer<int>(bufferCapacity);

            Stopwatch sw = new Stopwatch();

            long accQueue = 0;
            for (int a = 0; a < 10; a++)
            {
                if (a == 0) Console.WriteLine("Queue: ----------------------");
                sw.Restart();
                for (int i = 0; i < itemSize; i++)
                {
                    queue.Enqueue(i);
                }
                for (int i = 0; i < bufferCapacity; i++)
                {
                    queue.Dequeue();
                }
                sw.Stop();

                TimeSpan elapsedTime2 = sw.Elapsed;
                Console.WriteLine("Queue={0} ticks", elapsedTime2.Ticks);
                accQueue += elapsedTime2.Ticks;
            }
            Console.WriteLine("Average Ticks: " + accQueue / 10);

            long accRingQueue = 0;
            for (int a = 0; a < 10; a++)
            {
                if (a == 0) Console.WriteLine("Ring Buffer Queue: ----------------------");
                sw.Restart();
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
                Console.WriteLine("RingBuffer Queue={0} ticks", elapsedTime3.Ticks);
                accRingQueue += elapsedTime3.Ticks;
            }
            Console.WriteLine("Average Ticks: " + accRingQueue / 10);

            long accRing = 0;
            for (int a = 0; a < 10; a++)
            {
                if (a == 0) Console.WriteLine("Ring Buffer: ----------------------");
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

                TimeSpan elapsedTime4 = sw.Elapsed;
                Console.WriteLine("RingBuffer={0} ticks", elapsedTime4.Ticks);
                accRing += elapsedTime4.Ticks;
            }
            Console.WriteLine("Average Ticks: " + accRing / 10);

            Console.WriteLine($"Capacity: {ringBuffer.Capacity} Count: {ringBuffer.Count} IsFull: {ringBuffer.Count == ringBuffer.Capacity} IsEmpty: {ringBuffer.Count == 0}");            
            foreach (int snapshot in ringBuffer)
            {
                Console.WriteLine($"Id: {snapshot}");
            }
            
        }
    }
}
