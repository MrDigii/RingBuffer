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
            RingBuffer<PlayerSnapshot> ringBuffer = new RingBuffer<PlayerSnapshot>(20, true);
            Queue<PlayerSnapshot> queue = new Queue<PlayerSnapshot>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i <= 1000; i++)
            {
                ringBuffer.Enqueue(new PlayerSnapshot
                {
                    Id = i,
                    Name = "Lukas_" + i,
                });
            }

            sw.Stop();
            Console.WriteLine("RingBuffer={0} ns", sw.Elapsed.TotalMilliseconds * 1000000);
            sw.Reset();
            sw.Start();

            for (int i = 0; i <= 100; i++)
            {
                queue.Enqueue(new PlayerSnapshot
                {
                    Id = i,
                    Name = "Lukas_" + i,
                });
            }

            sw.Stop();
            Console.WriteLine("Queue={0} ns", sw.Elapsed.TotalMilliseconds * 1000000);

            Console.WriteLine($"Capacity: {ringBuffer.Capacity} Count: {ringBuffer.Count} IsFull: {ringBuffer.IsFull} IsEmpty: {ringBuffer.IsEmpty}");
            foreach (PlayerSnapshot snapshot in ringBuffer)
            {
                Console.WriteLine($"Id: {snapshot.Id} Name: {snapshot.Name}");
            }
        }
    }
}
