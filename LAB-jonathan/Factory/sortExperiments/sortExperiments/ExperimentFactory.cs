using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortExperiments
{


    abstract class ExperimentCreator
    {
        public abstract IExperiment FactoryMethode();
    }

    class CompleteExperimentCreator : ExperimentCreator
    {
        private int _n;
        private string _name;
        List<IGenerator> generators = new List<IGenerator>();
        List<ISortAlgorithms> algorithms = new List<ISortAlgorithms>();
        public CompleteExperimentCreator(int n = 10000, string name = "Complete Experiment")
        {
            _n = n;
            _name = name;
            generators.Add(new RandomGenerator((int)DateTime.Now.Ticks));
            generators.Add(new nearlySortedGenerator(95, (int)DateTime.Now.Ticks));
            generators.Add(new ReverseOrderGenerator());
            generators.Add(new FewUniqueRandomOrderGenerator((int)DateTime.Now.Ticks, 5));
            algorithms.Add(new BubbleSort());
            algorithms.Add(new InsertionSort());
            algorithms.Add(new ShellSort());
            algorithms.Add(new QuickSort());
        }
        public override IExperiment FactoryMethode()
        {
            return new Experiment(generators, algorithms, _n, _name);
        }
    }

    class BubbleExperimentCreator : ExperimentCreator
    {
        private int _n;
        private string _name;
        List<IGenerator> generators = new List<IGenerator>();
        List<ISortAlgorithms> algorithms = new List<ISortAlgorithms>();
        public BubbleExperimentCreator(int n = 10000, string name = "Bubble Experiment")
        {
            _n = n;
            _name = name;
            generators.Add(new RandomGenerator((int)DateTime.Now.Ticks));
            generators.Add(new nearlySortedGenerator(95, (int)DateTime.Now.Ticks));
            generators.Add(new ReverseOrderGenerator());
            generators.Add(new FewUniqueRandomOrderGenerator((int)DateTime.Now.Ticks, 5));
            algorithms.Add(new BubbleSort());
        }
        public override IExperiment FactoryMethode()
        {
            return new Experiment(generators, algorithms, _n, _name);
        }
    }

}
