# C# Ring Buffer
This is a simple implementation of a ring buffer / circular buffer in CSharp .NET Core. 
Its working like a queue where items can be enqueued and dequeued from a array with a fixed length.

## Benchmarking

- **Items**: 2048
- **Buffer Capacity**: 1024
- **Field type**:

```cs
class PlayerSnapshot
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

The buffers are tested by filling them with 2048 items and removing each item from the buffer afterwards.
By doing this 20 times we can measure the min max values of all elapsed ticks for each buffer type.

| Buffer type				| Simulations		| Min Ticks		| Max Ticks		| Average Ticks	|
| -------------				| ----------------- | ------------- | ------------- | ------------- |
| C# .NET Queue				| 20				| 2441			| 25862			| 4620			|
| C# .NET Queue Ring Buffer | 20				| 1910			| 103792		| 8041			|
| Own Ring Buffer			| 20				| 2458			| 10206			| 3379			|