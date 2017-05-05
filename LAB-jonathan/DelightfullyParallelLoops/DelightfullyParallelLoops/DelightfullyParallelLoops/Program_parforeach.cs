using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelightfullyParallelLoops
{
    class Program
    {

        public struct dataSampleABC
        {
            public double A;
            public double B;
            public double C;
        };

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            const long N = 9000000;
            dataSampleABC[] ABC = new dataSampleABC[N];

            
            
            
            Console.WriteLine("Starts parallel loop (foreach) for now.");
            
            
            stopwatch.Start();

            Parallel.ForEach<dataSampleABC>(ABC, ds =>
            {
                ds.C = ds.A * ds.B;

            }
            );


            /*for (int i = 0; i < N; i++)
            {
                C[i] = A[i] * B[i];
            }*/
            stopwatch.Stop();
            Console.WriteLine("Sequential loop time in milliseconds: {0}", stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        public static void MyParallelFor(
   int inclusiveLowerBound, int exclusiveUpperBound, Action<int> body)
        {
            // Determine size of each partition of work (size/nCores) – static partitioning
            int size = exclusiveUpperBound - inclusiveLowerBound;
            int numProcs = Environment.ProcessorCount;
            int range = size / numProcs;

            // Initialize threads to do work
            var threads = new List<Thread>(numProcs);
            for (int p = 0; p < numProcs; p++)
            {
                int start = p * range + inclusiveLowerBound;
                int end = (p == numProcs - 1) ? exclusiveUpperBound : start + range;
                threads.Add(new Thread(() =>
                {
                    for (int i = start; i < end; i++) body(i);
                }));
            }

            // Start and await threads
            foreach (var thread in threads) thread.Start();  // Start them all
            foreach (var thread in threads) thread.Join();   // wait on all
        }



    }
}
