using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Pipelines
{
    class SequentialStringCompression
    {
        private readonly int _nStrings;
        private readonly int _stringLength;
        private readonly string _charsInString;
        
        double _avgCompressionRatio = 0;

        public SequentialStringCompression(string charsInString, int nStrings, int stringLength)
        {
            _charsInString = charsInString;
            _nStrings = nStrings;
            _stringLength = stringLength;
        }


        public double Run()
        {
            for (var i = 0; i < _nStrings; i++)
            {
                // Generate string
                var str = Generate(_stringLength);

                // Compress string
                var compressedStr = Compress(str);

                // Update compression stats
                UpdateCompressionStats(i, str, compressedStr);
            }

            return _avgCompressionRatio;
        }


        public double RunPipeline()
        {
            var strColl = new BlockingCollection<string>();
            var comprStrColl = new BlockingCollection<string[]>();

            Task generateTask = new Task(() =>
            {
                DoGenerate(_nStrings, _stringLength, strColl);
            });

            Task compressTask = new Task(() =>
            {
                DoCompress(strColl, comprStrColl);
            });

            Task UpdateCompressionStatsTask = new Task(() =>
            {
                DoUpdateCompressionStats(comprStrColl);
            });


            generateTask.Start();
            compressTask.Start();
            UpdateCompressionStatsTask.Start();

            Task.WaitAll(generateTask, compressTask, UpdateCompressionStatsTask);

            return _avgCompressionRatio;
        }


        public double RunPipelineBalance()
        {
            var strColl = new BlockingCollection<string>();
            var comprStrColl = new BlockingCollection<string[]>();

            Task generateTask = new Task(() =>
            {
                DoGenerate(_nStrings, _stringLength, strColl);
            });
            strColl.
            Task compressTask = new Task(() =>
            {
                DoCompress(strColl, comprStrColl);
            });

            Task UpdateCompressionStatsTask = new Task(() =>
            {
                DoUpdateCompressionStats(comprStrColl);
            });


            generateTask.Start();
            compressTask.Start();
            UpdateCompressionStatsTask.Start();

            Task.WaitAll(generateTask, compressTask, UpdateCompressionStatsTask);

            return _avgCompressionRatio;
        }

        private void DoGenerate(int nStrings, int stringLength,BlockingCollection<string> output)
        {
            try
            {
                for(int i = 0; i < nStrings; i++)
                {
                    output.Add(Generate(stringLength));
                }
            }
            finally
            {
                output.CompleteAdding();
            }
        }

        private void DoCompress(BlockingCollection<string> input, BlockingCollection<string[]> output)
        {
            try
            {
                foreach (var item in input.GetConsumingEnumerable())
                {
                    output.Add(new string[]{Compress(item),item});
                }
            }
            finally
            {
                output.CompleteAdding();
            }
            
        }

        private void DoUpdateCompressionStats(BlockingCollection<string[]> input)
        {
            try
            {
                int i = 0;
                foreach (var item in input.GetConsumingEnumerable())
                {
                    UpdateCompressionStats(i++, item[1], item[0]);
                }
            }
            finally
            {

            }
        }

        private void UpdateCompressionStats(int i, string str, string compressedStr)
        {
            _avgCompressionRatio = ((i * _avgCompressionRatio) + ((double)(compressedStr.Length) / str.Length)) / (i + 1);
            
        }


        private string Compress(string str)
        {
            var result = "";
            for (var i = 0; i < str.Length; i++)
            {
                var j = i;
                result += str[i];
                while ((j < str.Length) && (str[i] == str[j]))
                    j++;

                if (j > i + 1)
                {
                    result += (j - i);
                    i = j-1;
                }
            }
            return result;
        }

        private string Generate(int stringLength)
        {
            var random = new Random();
            var result = new string(Enumerable.Repeat(_charsInString, stringLength).Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
    }
}
