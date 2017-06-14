using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortExperiments
{
    interface IGenerator
    {
        int[] generate(int n);
        string getName();
    }

    class RandomGenerator : IGenerator
    {
        private int _seed;

        public RandomGenerator(int seed)
        {
            _seed = seed;
        }

        public int[] generate(int n)
        {
            Random ran = new Random(_seed);

            int[] array = new int[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = ran.Next(n);
            }
            return array;
        }


        public string getName()
        {
            return "Random generator";
        }
    }

    class nearlySortedGenerator : IGenerator
    {
        private int _seed;
        private int _procent;

        public nearlySortedGenerator(int procent, int seed)
        {
            _seed = seed;
            _procent = (procent > 100 ? 100 : procent);
        }

        public int[] generate(int n)
        {
            Random ran = new Random(_seed);

            int[] array = new int[n];

            for (int i = 0; i < n; i++)
            {
                if (ran.Next(100) < _procent)
                {
                    array[i] = i;
                }
                else
                {
                    array[i] = ran.Next(n);
                }

                
            }
            return array;
        }


        public string getName()
        {
            return "Nearly Sorted Generator";
        }
    }

    class ReverseOrderGenerator : IGenerator
    {

        public ReverseOrderGenerator()
        {
        }

        public int[] generate(int n)
        {

            int[] array = new int[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = n-i;
            }
            return array;
        }


        public string getName()
        {
            return "Reverse Order generator";
        }
    }
    class FewUniqueRandomOrderGenerator : IGenerator
    {
        private int _seed;
        private int _max;
        public FewUniqueRandomOrderGenerator(int seed, int max)
        {
            _seed = seed;
            _max = max;
        }

        public int[] generate(int n)
        {
            Random ran = new Random(_seed);

            int[] array = new int[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = ran.Next(_max);
            }
            return array;
        }


        public string getName()
        {
            return "Few Unique Random Order generator";
        }
    }
}
