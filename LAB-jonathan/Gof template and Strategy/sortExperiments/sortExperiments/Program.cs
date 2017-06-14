using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IGenerator> generators = new List<IGenerator>();
            List<ISortAlgorithms> algorithms = new List<ISortAlgorithms>();

            generators.Add(new RandomGenerator((int)DateTime.Now.Ticks));
            generators.Add(new nearlySortedGenerator(95, (int)DateTime.Now.Ticks));
            generators.Add(new ReverseOrderGenerator());
            generators.Add(new FewUniqueRandomOrderGenerator((int)DateTime.Now.Ticks, 5));
            algorithms.Add(new BubbleSort());
            algorithms.Add(new InsertionSort());
            algorithms.Add(new ShellSort());
            algorithms.Add(new QuickSort());

            Experiment allTest = new Experiment(generators, algorithms, 10000, "The Test");

            allTest.start();

            Console.ReadLine();

        }
    }
}
