using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortExperiments
{

    interface ISuperSorter
    {
        double sort(int[] array);
    }

    class SuperSorter : ISuperSorter
    {
        ISortAlgorithms _algorithms; // GoF Strategy

        public SuperSorter()
        {

        }

        public void setAlgorithms(ISortAlgorithms alg)
        {
            _algorithms = alg;
        }
        public double sort(int[] array)
        {
            if (_algorithms == null)
                return -1.0;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            _algorithms.sort(array);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }


    interface ISortAlgorithms
    {
        void sort(int[] unsorted);
        string getName();
    }

    class BubbleSort : ISortAlgorithms
    {
        public void sort(int[] unsorted)
        {
            SortAlgorithmsFunktions.IntArrayBubbleSort(unsorted);
        }


        public string getName()
        {
            return "Bubble";
        }
    }

    class InsertionSort : ISortAlgorithms
    {
        public void sort(int[] unsorted)
        {
            SortAlgorithmsFunktions.IntArrayInsertionSort(unsorted);
        }
        public string getName()
        {
            return "Insertion";
        }
    }

    class ShellSort : ISortAlgorithms
    {
        public void sort(int[] unsorted)
        {
            SortAlgorithmsFunktions.IntArrayShellSortBetter(unsorted);
        }
        public string getName()
        {
            return "ShellSort";
        }
    }

    class QuickSort : ISortAlgorithms
    {
        public void sort(int[] unsorted)
        {
            SortAlgorithmsFunktions.IntArrayQuickSort(unsorted);
        }
        public string getName()
        {
            return "QuickSort";
        }
    }


    class SortAlgorithmsFunktions
    {
        public static void exchange(int[] data, int m, int n)
        {
            int temporary;

            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }

        public static void IntArrayBubbleSort(int[] data)
        {
            int i, j;
            int N = data.Length;

            for (j = N - 1; j > 0; j--)
            {
                for (i = 0; i < j; i++)
                {
                    if (data[i] > data[i + 1])
                        exchange(data, i, i + 1);
                }
            }
        }
        public static int IntArrayMin(int[] data, int start)
        {
            int minPos = start;
            for (int pos = start + 1; pos < data.Length; pos++)
                if (data[pos] < data[minPos])
                    minPos = pos;
            return minPos;
        }

        public static void IntArraySelectionSort(int[] data)
        {
            int i;
            int N = data.Length;

            for (i = 0; i < N - 1; i++)
            {
                int k = IntArrayMin(data, i);
                if (i != k)
                    exchange(data, i, k);
            }
        }

        public static void IntArrayInsertionSort(int[] data)
        {
            int i, j;
            int N = data.Length;

            for (j = 1; j < N; j++)
            {
                for (i = j; i > 0 && data[i] < data[i - 1]; i--)
                {
                    exchange(data, i, i - 1);
                }
            }
        }

        public static void IntArrayShellSort(int[] data, int[] intervals)
        {
            int i, j, k, m;
            int N = data.Length;

            // The intervals for the shell sort must be sorted, ascending

            for (k = intervals.Length - 1; k >= 0; k--)
            {
                int interval = intervals[k];
                for (m = 0; m < interval; m++)
                {
                    for (j = m + interval; j < N; j += interval)
                    {
                        for (i = j; i >= interval && data[i] < data[i - interval]; i -= interval)
                        {
                            exchange(data, i, i - interval);
                        }
                    }
                }
            }
        }

        public static void IntArrayShellSortNaive(int[] data)
        {
            int[] intervals = { 1, 2, 4, 8 };
            IntArrayShellSort(data, intervals);
        }


        static int[] GenerateIntervals(int n)
        {
            if (n < 2)
            {  // no sorting will be needed
                return new int[0];
            }
            int t = Math.Max(1, (int)Math.Log(n, 3) - 1);
            int[] intervals = new int[t];
            intervals[0] = 1;
            for (int i = 1; i < t; i++)
                intervals[i] = 3 * intervals[i - 1] + 1;
            return intervals;
        }

        public static void IntArrayShellSortBetter(int[] data)
        {
            int[] intervals = GenerateIntervals(data.Length);
            IntArrayShellSort(data, intervals);
        }

        public static void IntArrayQuickSort(int[] data, int l, int r)
        {
            int i, j;
            int x;

            i = l;
            j = r;

            x = data[(l + r) / 2]; /* find pivot item */
            while (true)
            {
                while (data[i] < x)
                    i++;
                while (x < data[j])
                    j--;
                if (i <= j)
                {
                    exchange(data, i, j);
                    i++;
                    j--;
                }
                if (i > j)
                    break;
            }
            if (l < j)
                IntArrayQuickSort(data, l, j);
            if (i < r)
                IntArrayQuickSort(data, i, r);
        }

        public static void IntArrayQuickSort(int[] data)
        {
            IntArrayQuickSort(data, 0, data.Length - 1);
        }
    }


}
