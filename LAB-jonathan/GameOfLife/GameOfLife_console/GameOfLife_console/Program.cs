using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife;
using System.Diagnostics;

namespace GameOfLife_console
{
    class Program
    {
        static void Main(string[] args)
        {

            GameOfLife.GameOfLife gol = new GameOfLife.GameOfLife(1000, 50);

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            
            gol.Run();

            stopwatch.Stop();

            var serialRuntime = stopwatch.ElapsedMilliseconds;
            System.Console.WriteLine("Serial calculation runtime: {0} ms", serialRuntime);

            //Console.ReadLine();

            gol = new GameOfLife.GameOfLife(1000, 50);

            stopwatch = new Stopwatch();

            stopwatch.Start();

            gol.RunParLoop();

            stopwatch.Stop();

            serialRuntime = stopwatch.ElapsedMilliseconds;
            System.Console.WriteLine("parloop calculation runtime: {0} ms", serialRuntime);


            gol = new GameOfLife.GameOfLife(1000, 50);

            stopwatch = new Stopwatch();

            stopwatch.Start();

            gol.RunParWithBarrier();

            stopwatch.Stop();

            serialRuntime = stopwatch.ElapsedMilliseconds;
            System.Console.WriteLine("par with barrier calculation runtime: {0} ms", serialRuntime);

            Console.ReadLine();


        }
    }
}
