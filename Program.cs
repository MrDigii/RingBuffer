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
            RingBuffer<PlayerSnapshot> ringBuffer = new RingBuffer<PlayerSnapshot>(3);
            ringBuffer.Enqueue(new PlayerSnapshot
            {
                Id = 1,
                Name = "Lukas",
            });

            ringBuffer.Enqueue(new PlayerSnapshot
            {
                Id = 2,
                Name = "Alexander",
            });
            ringBuffer.Enqueue(new PlayerSnapshot
            {
                Id = 3,
                Name = "Johann",
            });

            ringBuffer.Dequeue();
            ringBuffer.Dequeue();

            ringBuffer.Enqueue(new PlayerSnapshot
            {
                Id = 4,
                Name = "Paul",
            });


            Console.WriteLine("Capacity: " + ringBuffer.Capacity);
            Console.WriteLine("Count: " + ringBuffer.Count);
            Console.WriteLine("IsFull: " + ringBuffer.IsFull);
            Console.WriteLine("IsEmpty: " + ringBuffer.IsEmpty);

            foreach (PlayerSnapshot snapshot in ringBuffer)
            {
                Console.WriteLine($"Id: {snapshot.Id} Name: {snapshot.Name}");
            }

            Console.WriteLine("Capacity: " + ringBuffer.Capacity);
            Console.WriteLine("Count: " + ringBuffer.Count);
            Console.WriteLine("IsFull: " + ringBuffer.IsFull);
            Console.WriteLine("IsEmpty: " + ringBuffer.IsEmpty);
        }
    }
}
