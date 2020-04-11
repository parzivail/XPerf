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

        private float _value;

        public RandomDataSource()
        {
            _random = new System.Random();
        }

        /// <inheritdoc />
        public void Poll()
        {
            _value = (float) _random.NextDouble();
        }

        /// <inheritdoc />
        public float GetValue()
        {
            return _value;
        }

        /// <inheritdoc />
        public string Format(float value)
        {
            return $"{value}";
        }

        /// <inheritdoc />
        public string GetUnitHeader()
        {
            return "Float [0,1)";
        }

        /// <inheritdoc />
        public string GetGraphHeader()
        {
            return "Random Floats";
        }

        /// <inheritdoc />
        public string GetGraphDetailHeader()
        {
            return "Random 32-bit float values for debugging";
        }
    }
}
