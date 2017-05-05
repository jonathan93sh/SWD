using System;
using System.Linq;

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
