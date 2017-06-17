using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playingCards
{
    internal static class playing_cards_mapReduce_demo
    {
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

        public static void Main(string[] args)
        {

            var files = Directory.EnumerateFiles(@"C:\git\SWD\LAB-jonathan\MapReduce\cards", "*.txt")
                        .AsParallel();

            Stopwatch s1 = new Stopwatch();
            {
                s1.Start();
                

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
                s1.Stop();
                Console.WriteLine("Time spent: {0} ms", s1.ElapsedMilliseconds);
            }

            {
                s1.Start();

                List<card> source = new List<card>(files.Count());

                foreach(string path in files.ToList())
                {
                    foreach(string p in File.ReadAllLines(path))
                    {
                        card c = new card();
                        var cardstr = p.Split(splitChar);

                        switch(cardstr[0])
                        {
                            case "SPADE":
                                c.type = Ctype.SPADE;
                                break;
                            case "CLUB":
                                c.type = Ctype.CLUB;
                                break;
                            case "HEART":
                                c.type = Ctype.HEART;
                                break;
                            case "DIAMOND":
                                c.type = Ctype.DIAMOND;
                                break;
                            default:
                                break;
                        }

                        c.value = int.Parse(cardstr[1]);
                        source.Add(c);
                    }
                }

                long N_SPADE = 0;
                long N_CLUB = 0;
                long N_HEART = 0;
                long N_DIAMOND = 0;

                foreach(card c in source)
                {
                    switch(c.type)
                    {
                        case Ctype.SPADE:
                            N_SPADE += c.value;
                            break;
                        case Ctype.CLUB:
                            N_CLUB += c.value;
                            break;
                        case Ctype.HEART:
                            N_HEART += c.value;
                            break;
                        case Ctype.DIAMOND:
                            N_DIAMOND += c.value;
                            break;
                    }
                }

                Console.WriteLine("SPADE : {0} \r\nCLUB : {1}\r\nHEART : {2}\r\nDIAMOND : {3}", N_SPADE, N_CLUB, N_HEART, N_DIAMOND);

                s1.Stop();
                Console.WriteLine("Time spent: {0} ms", s1.ElapsedMilliseconds);
                
            }
            Console.ReadLine();

        }
        public enum Ctype {SPADE, CLUB, HEART, DIAMOND};
        public struct card
        {
            public Ctype type;
            public int value;
        }

        public static IEnumerable<card> Source(string path)
        {
            IEnumerable<card> cards = File.ReadLines(path).Select(p =>
                {
                    card c = new card();
                    var cardstr = p.Split(splitChar);

                    switch(cardstr[0])
                    {
                        case "SPADE":
                            c.type = Ctype.SPADE;
                            break;
                        case "CLUB":
                            c.type = Ctype.CLUB;
                            break;
                        case "HEART":
                            c.type = Ctype.HEART;
                            break;
                        case "DIAMOND":
                            c.type = Ctype.DIAMOND;
                            break;
                        default:
                            break;
                    }

                    c.value = int.Parse(cardstr[1]);
                    return c;
                }

                );
            
            return cards;
        }

        // Map() returns the key which the word fits
        public static char[] splitChar = new char[] { ',' };
        public static Ctype Map(card c)
        {
            return c.type;
        }


        // Reduce() returns a list of Key/value pairs representing the results
        // An IGrouping<> represents a set of values that have the same key
        public static IEnumerable<KeyValuePair<string, long>> Reduce(IGrouping<Ctype, card> group)
        {
            return new KeyValuePair<string, long>[]
            {
                new KeyValuePair<string, long>(group.Key.ToString(), 
                    group.Sum(g => 
                    {
                        return (long) g.value;
                    })
                )
                
            };
        }

    }
}
