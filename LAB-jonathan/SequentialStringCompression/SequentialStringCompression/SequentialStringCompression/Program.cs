using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequentialStringCompression
{
    class Program
    {
        static void Main(string[] args)
        {
            var testStr = "ABC";

            Pipelines.SequentialStringCompression ssc = new Pipelines.SequentialStringCompression(testStr, 100000, 250);

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var cmpressRate = ssc.Run();

            stopwatch.Stop();
            
            var serialRuntime = stopwatch.ElapsedMilliseconds;
            System.Console.WriteLine("Serial calculation runtime: {0} ms, avg compress rate {1}", serialRuntime, cmpressRate);


            stopwatch = new Stopwatch();
            ssc = new Pipelines.SequentialStringCompression(testStr, 100000, 250);
            stopwatch.Start();

            cmpressRate = ssc.Run();

            stopwatch.Stop();

            serialRuntime = stopwatch.ElapsedMilliseconds;
            System.Console.WriteLine("pipeline calculation runtime: {0} ms, avg compress rate {1}", serialRuntime, cmpressRate);

            Console.ReadLine();

        }
    }
}
