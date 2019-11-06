# C# Ring Buffer
This is a simple implementation of a ring buffer / circular buffer in CSharp .NET Core. 
Its working like a queue where items can be enqueued and dequeued from a array with a fixed length.


## Benchmarks
Processing of 1000 items with a normal CSharp .NET Queue and a Ring Buffer with different max sizes:

Assign:

| Ring Buffer size:        | Duration with 1000 items:       |
| ------------- |:------------------------------------------:|
| 50			| 0,9196 ms								     |
| 100			| 0,9913 ms									 |
| 200			| 0,9255 ms									 |

.NET Queue Duration with 1000 item: **0,0512 ms**
