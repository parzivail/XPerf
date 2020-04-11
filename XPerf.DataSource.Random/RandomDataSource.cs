using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPerf.Api;

namespace XPerf.DataSource.Random
{
    [PerfDataSource("Random", "Provides random data for debugging", 0, 0, 1)]
    public class RandomDataSource : IPollableDataSource
    {
        private readonly System.Random _random;

        public RandomDataSource()
        {
            _random = new System.Random();
        }

        /// <inheritdoc />
        public float Poll()
        {
            return (float) _random.NextDouble();
        }

        /// <inheritdoc />
        public string Format(float value)
        {
            return $"{value}";
        }
    }
}
