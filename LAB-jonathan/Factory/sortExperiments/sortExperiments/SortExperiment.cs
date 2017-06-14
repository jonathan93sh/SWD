using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortExperiments
{

    abstract class sortExperiment
    {
        protected List<ISortAlgorithms> _Algoritmes;
        protected List<IGenerator> _Generators;
        public void start()
        {
            Console.WriteLine("Experiment: " + getTitle());
            
            foreach(IGenerator generator in _Generators)
            {
                setGenerator(generator);
                Console.WriteLine("current generator: " + generator.getName());
                int[] array = generate();

                foreach(ISortAlgorithms alg in _Algoritmes)
                {
                    setAlgorithm(alg);
                    Console.WriteLine("current algorithm: " + alg.getName());
                    
                    int[] tmpArray = new int[array.Length];
                    for (int i = 0; i < array.Length; i++)
                        tmpArray[i] = array[i];

                    Console.WriteLine("unsorted numbers(first 10): ");
                    for (int i = 0; i < (array.Length < 10 ? array.Length: 10) ; i++)
                    {
                        Console.Write(tmpArray[i] + " ");
                    }
                    Console.WriteLine();
                    double time = sort(tmpArray);

                    Console.WriteLine("sorted numbers(first 10), sort time(" + time + " ms) : ");
                    for (int i = 0; i < (array.Length < 10 ? array.Length : 10); i++)
                    {
                        Console.Write(tmpArray[i] + " ");
                    }
                    Console.WriteLine();
                }
            }
            
        }

        protected abstract void setGenerator(IGenerator gen);
        protected abstract void setAlgorithm(ISortAlgorithms alg);
        protected abstract string getTitle();
        protected abstract int[] generate();
        protected abstract double sort(int[] array);
    }

    class Experiment : sortExperiment
    {
        private IGenerator _CurrGenerator = null;
        private SuperSorter _superSorter = new SuperSorter();
        private int _n;
        private string _name;
        public Experiment(List<IGenerator> generator, List<ISortAlgorithms> alg, int n, string name)
        {
            _Generators = generator;
            _Algoritmes = alg;
            
            _n = n;
            _name = name;
        }

        protected override void setGenerator(IGenerator gen)
        {
            if(gen != null)
            {
                _CurrGenerator = gen; 
            }
        }

        protected override void setAlgorithm(ISortAlgorithms alg)
        {
            if(alg != null)
            {
                _superSorter.setAlgorithms(alg);
            }
        }

        protected override string getTitle()
        {
            return _name;
        }

        protected override int[] generate()
        {
            return _CurrGenerator.generate(_n);
        }

        protected override double sort(int[] array)
        {
            return _superSorter.sort(array);
        }
    }


}
