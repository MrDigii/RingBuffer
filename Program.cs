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
            int itemSize = 2048;
            int bufferCapacity = 128;
            int benchmarkDuration = 10;

            Queue<int> queue = new Queue<int>(bufferCapacity);
            RingBuffer<int> ringBuffer = new RingBuffer<int>(bufferCapacity);
            CircularQueue<int> circularQueue = new CircularQueue<int>(bufferCapacity);

            Benchmark.Time("C# Queue", benchmarkDuration, () =>
            {

                for (int i = 0; i < itemSize; i++)
                {
                    queue.Enqueue(i);
                }
                for (int i = 0; i < bufferCapacity; i++)
                {
                    queue.Dequeue();
                }

            });

            Benchmark.Time("C# Queue Ring Buffer", benchmarkDuration, () =>
            {

                for (int i = 0; i < itemSize; i++)
                {
                    circularQueue.Enqueue(i);
                }
                for (int i = 0; i < bufferCapacity; i++)
                {
                    circularQueue.Dequeue();
                }
            });

            Benchmark.Time("Own Ring Buffer", benchmarkDuration, () =>
            {
                for (int i = 0; i < itemSize; i++)
                {
                    ringBuffer.Enqueue(i);
                }

                for (int i = 0; i < bufferCapacity; i++)
                {
                    ringBuffer.Dequeue();
                }
            });

            Console.WriteLine($"Capacity: {ringBuffer.Capacity} Count: {ringBuffer.Count} IsFull: {ringBuffer.Count == ringBuffer.Capacity} IsEmpty: {ringBuffer.Count == 0}");
            foreach (int snapshot in ringBuffer)
            {
                Console.WriteLine($"Id: {snapshot}");
            }

        }
    }

    public class Benchmark
    {
        public static void Time(string name, int _benchmarkDuration, Action f)
        {
            f(); // warmup: let the CLR genererate code for generics, get caches hot, etc.
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < _benchmarkDuration; i++)
            {
                f();
            }
            watch.Stop();
            Console.WriteLine("{0}: {1} ticks", name, watch.ElapsedTicks);
        }

        public static void Memory(string name, Action f)
        {
            var initial = GC.GetTotalMemory(true);
            f();
            var final = GC.GetTotalMemory(true);
            Console.WriteLine("{0}: {1} bytes", name, final - initial);
        }
    }
}
