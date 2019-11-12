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
        public static long maxQueueTicks = 0;
        public static long minQueueTicks = 0;
        public static long accQueueTicks = 0;

        public static long maxQueueRingTicks = 0;
        public static long minQueueRingTicks = 0;
        public static long accQueueRingTicks = 0;

        public static long maxRingTicks = 0;
        public static long minRingTicks = 0;
        public static long accRingTicks = 0;

        static void Main(string[] args)
        {
            int itemSize = 2048;
            int bufferCapacity = 1024;
            int benchmarkDuration = 20;

            Queue<PlayerSnapshot> queue = new Queue<PlayerSnapshot>(bufferCapacity);
            RingBuffer<PlayerSnapshot> ringBuffer = new RingBuffer<PlayerSnapshot>(bufferCapacity);
            CircularQueue<PlayerSnapshot> circularQueue = new CircularQueue<PlayerSnapshot>(bufferCapacity);

            Benchmark.Time("C# Queue", benchmarkDuration, () =>
            {

                for (int i = 0; i < itemSize; i++)
                {
                    queue.Enqueue(new PlayerSnapshot()
                    {
                        Id = i,
                        Name = "Player_" + i,
                    });
                }
                for (int i = 0; i < bufferCapacity; i++)
                {
                    queue.Dequeue();
                }

            }, OnStopwatchQueue);

            Benchmark.Time("C# Queue Ring Buffer", benchmarkDuration, () =>
            {

                for (int i = 0; i < itemSize; i++)
                {
                    circularQueue.Enqueue(new PlayerSnapshot()
                    {
                        Id = i,
                        Name = "Player_" + i,
                    });
                }
                for (int i = 0; i < bufferCapacity; i++)
                {
                    circularQueue.Dequeue();
                }
            }, OnStopwatchQueueRing);

            Benchmark.Time("Own Ring Buffer", benchmarkDuration, () =>
            {
                for (int i = 0; i < itemSize; i++)
                {
                    ringBuffer.Enqueue(new PlayerSnapshot()
                    {
                        Id = i,
                        Name = "Player_" + i,
                    });                    
                }
                
                /*
                Console.WriteLine("Newest: " + ringBuffer.Newest.Id);
                Console.WriteLine("Oldest: " + ringBuffer.Oldest.Id);
                */

                for (int i = 0; i < bufferCapacity; i++)
                {
                    ringBuffer.Dequeue();
                }
            }, OnStopwatchRingBuffer);

            Console.WriteLine("Queue Min: {0} Max: {1} Avg: {2} ticks", minQueueTicks, maxQueueTicks, accQueueTicks / benchmarkDuration);
            Console.WriteLine("Queue Ring Min: {0}  Max: {1} Avg: {2} ticks", minQueueRingTicks, maxQueueRingTicks, accQueueRingTicks / benchmarkDuration);
            Console.WriteLine("Ring Buffer Min: {0} Max: {1} Avg: {2} ticks", minRingTicks, maxRingTicks, accRingTicks / benchmarkDuration);

            Console.WriteLine($"Capacity: {ringBuffer.Capacity} Count: {ringBuffer.Count} IsFull: {ringBuffer.Count == ringBuffer.Capacity} IsEmpty: {ringBuffer.Count == 0}");
            foreach (PlayerSnapshot snapshot in ringBuffer)
            {
                Console.WriteLine($"Id: {snapshot.Id} Name: {snapshot.Name}");
            }

        }

        public static void OnStopwatchQueue(long _elapsedTicks)
        {
            if (_elapsedTicks > maxQueueTicks) maxQueueTicks = _elapsedTicks;
            if (_elapsedTicks < minQueueTicks || minQueueTicks == 0) minQueueTicks = _elapsedTicks;
            accQueueTicks += _elapsedTicks;
        }

        public static void OnStopwatchQueueRing(long _elapsedTicks)
        {
            if (_elapsedTicks > maxQueueRingTicks) maxQueueRingTicks = _elapsedTicks;
            if (_elapsedTicks < minQueueRingTicks || minQueueRingTicks == 0) minQueueRingTicks = _elapsedTicks;
            accQueueRingTicks += _elapsedTicks;
        }

        public static void OnStopwatchRingBuffer(long _elapsedTicks)
        {
            if (_elapsedTicks > maxRingTicks) maxRingTicks = _elapsedTicks;
            if (_elapsedTicks < minRingTicks || minRingTicks == 0) minRingTicks = _elapsedTicks;
            accRingTicks += _elapsedTicks;
        }
    }

    public class Benchmark
    {
        public delegate void Callback(long _ticks);

        public static void Time(string name, int _benchmarkDuration, Action _f, Callback _callback)
        {
            _f(); // warmup: let the CLR genererate code for generics, get caches hot, etc.
            
            for (int i = 0; i < _benchmarkDuration; i++)
            {
                Stopwatch watch = Stopwatch.StartNew();
                _f();
                watch.Stop();
                // Console.WriteLine("{0}: {1} ticks", name, watch.ElapsedTicks);
                _callback(watch.ElapsedTicks);
            }            
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
