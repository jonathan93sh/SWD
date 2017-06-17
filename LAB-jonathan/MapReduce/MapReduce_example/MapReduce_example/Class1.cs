using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
{
    internal static class MapReduceDemo
    {

        // See https://msdn.microsoft.com/en-us/library/bb397687.aspx - Lambdas with the standard query operators
        // The return value is always specified in the last type parameter. 
        // E.g. Func<int, string, bool> defines a delegate with two input parameters, int and string, and a return type of bool
        public static ParallelQuery<TResult> MapReduce<TSource, TMapped, TKey, TResult>(
            this ParallelQuery<TSource> source,
            Func<TSource, IEnumerable<TMapped>> map,
            Func<TMapped, TKey> keySelector,
            Func<IGrouping<TKey, TMapped>, IEnumerable<TResult>> reduce)
        {
            // ==============================================================
            // COMMENT IN TO GET INSTRUMENTED VERSION OF MapReduce()
            // ==============================================================
            //Console.WriteLine("TSource: {0}", typeof(TSource));
            //Console.WriteLine("TMapped: {0}", typeof(TMapped));
            //Console.WriteLine("TKey: {0}", typeof(TKey));
            //Console.WriteLine("TResult: {0}", typeof(TResult));

            // var selectManyFromSourceResult = source.SelectMany(map);
            // foreach (var mapped in selectManyFromSourceResult) Console.WriteLine("mapped: {0}", mapped);

            // var grouped = selectManyFromSourceResult.GroupBy(keySelector);
            //foreach (var grouping in grouped)
            //{
            //    Console.Write("grouped: {0}: ", grouping.Key);
            //    foreach (var groupItem in grouping)
            //    {
            //        Console.Write("{0}, ", groupItem);
            //    }
            //    Console.WriteLine();
            //}

            // var reduced = grouped.SelectMany(reduce);
            //foreach (var r in reduced) Console.WriteLine("reduced: {0}", r);
            // return reduced;

            return source
                .SelectMany(map)
                .GroupBy(keySelector)
                .SelectMany(reduce);
        }

        //*********************************************************
        //*********************************************************
        // WORD-COUNT-BY-LENGTH EXAMPLE
        //*********************************************************
        //*********************************************************
        private static void Main(string[] args)
        {
            var files =
                Directory.EnumerateFiles(@"C:\git\SWD\LAB-jonathan\MapReduce\Books", "*.txt")
                    .AsParallel();

            var wordCount = files.MapReduce(
                path => Source(path),
                map => Map(map),
                group => Reduce(group));

            var wc = wordCount.ToList();
            //wc.Sort(AscendingComparison);

            foreach (var pair in wc)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
            Console.ReadLine();
        }

        // Source() provides the source data on which the MapReduce query shall run. 
        private static IEnumerable<string> Source(string path)
        {
            return File.ReadLines(path) // Read all lines in the path
                .SelectMany(line => line.ToLower().Split(new char[] {' ', ',', '.', '-', '!', '?', ';','"','\'','_',':',';'}));
                // Project the words into a single enumerable
        }


        // Map() returns the key which the word fits
        private static string Map(string word)
        {
            return word;
        }


        // Reduce() returns a list of Key/value pairs representing the results
        // An IGrouping<> represents a set of values that have the same key
        private static IEnumerable<KeyValuePair<string, int>> Reduce(IGrouping<string, string> group)
        {
            return new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>(group.Key, group.Count())
            };
        }


        private static int AscendingComparison(KeyValuePair<int, int> a, KeyValuePair<int, int> b)
        {
            return a.Key - b.Key;
        }
    }
}
