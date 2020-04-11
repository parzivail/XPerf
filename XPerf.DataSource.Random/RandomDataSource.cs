using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPerf.Api;

namespace XPerf.DataSource.Random
{
    [XPerfPlugin("Random", "Provides random data for debugging")]
    public class RandomDataSource : XPerfDataProvider
    {
        private readonly System.Random _random;

        public RandomDataSource()
        {
            _random = new System.Random();

            UnitHeader = "Float [0,1)";
            GraphHeader = "Random Floats";
            GraphDetailHeader = "Random 32-bit float values for debugging";
        }

        public override float CollectData()
        {
            return (float)_random.NextDouble();
        }
    }
}
