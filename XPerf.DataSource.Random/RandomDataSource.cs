using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPerf.Api;

namespace XPerf.DataSource.Random
{
    public class RandomDataSource : XPerfDataProvider
    {
        private readonly float _min;
        private readonly float _max;

        private readonly System.Random _random;

        public RandomDataSource(string name, float min, float max)
        {
            _min = min;
            _max = max;
            _random = new System.Random();

            GraphHeader = name;
            UnitHeader = $"Float [{min},{max})";
            GraphDetailHeader = "Random 32-bit float values for debugging";
        }

        public override float CollectData()
        {
            return (float)(_random.NextDouble() * (_max - _min) + _min);
        }

        /// <inheritdoc />
        public override float? GetMin()
        {
            return _min;
        }

        /// <inheritdoc />
        public override float? GetMax()
        {
            return _max;
        }
    }
}
