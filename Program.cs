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
            int benchmarkDuration = 1;

            Queue<int> queue = new Queue<int>(bufferCapacity);
            RingBuffer<int> ringBuffer = new RingBuffer<int>(bufferCapacity);
            CircularBuffer<int> circularBuffer = new CircularBuffer<int>(bufferCapacity);

            Benchmark.Time("C# Queue", () =>
            {
                for (int a = 0; a < benchmarkDuration; a++)
                {
                    for (int i = 0; i < itemSize; i++)
                    {
                        queue.Enqueue(i);
                    }
                    for (int i = 0; i < bufferCapacity; i++)
                    {
                        queue.Dequeue();
                    }
                }
            });

            Benchmark.Time("C# Queue Ring Buffer", () =>
            {
                for (int a = 0; a < benchmarkDuration; a++)
                {
                    for (int i = 0; i < itemSize; i++)
                    {
                        circularBuffer.Add(i);
                    }
                    for (int i = 0; i < bufferCapacity; i++)
                    {
                        circularBuffer.Read();
                    }
                }
            });

            Benchmark.Time("Own Ring Buffer", () =>
            {
                for (int a = 0; a < benchmarkDuration; a++)
                {
                    for (int i = 0; i < itemSize; i++)
                    {
                        ringBuffer.Enqueue(i);
                    }

                    for (int i = 0; i < bufferCapacity; i++)
                    {
                        ringBuffer.Dequeue();
                    }
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
        public static void Time(string name, Action f)
        {
            f(); // warmup: let the CLR genererate code for generics, get caches hot, etc.
            GC.GetTotalMemory(true);
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < 10; i++)
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
