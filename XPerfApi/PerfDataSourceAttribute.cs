using System;

namespace XPerf.Api
{
    /// <summary>
    /// Provides an attribute to designate performance data sources
    /// </summary>
    public class PerfDataSourceAttribute : Attribute
    {
        /// <summary>
        /// The name of the source
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The description of the source
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The minimum polling interval of the source, in milliseconds
        /// </summary>
        public int MinPollingInterval { get; }

        /// <summary>
        /// The minimum value the source will provide
        /// </summary>
        public float? Min { get; }

        /// <summary>
        /// The maximum value the source will provide
        /// </summary>
        public float? Max { get; }

        /// <summary>
        /// Describes a performance data source
        /// </summary>
        /// <param name="name">The name of the source</param>
        /// <param name="description">The description of the source</param>
        /// <param name="minPollingInterval">The minimum polling interval of the source, in milliseconds</param>
        public PerfDataSourceAttribute(string name, string description, int minPollingInterval)
        {
            Name = name;
            Description = description;
            MinPollingInterval = minPollingInterval;
            Min = null;
            Max = null;
        }

        /// <summary>
        /// Describes a performance data source
        /// </summary>
        /// <param name="name">The name of the source</param>
        /// <param name="description">The description of the source</param>
        /// <param name="minPollingInterval">The minimum polling interval of the source, in milliseconds</param>
        /// <param name="min">The minimum value the source will provide</param>
        public PerfDataSourceAttribute(string name, string description, int minPollingInterval, float min)
        {
            Name = name;
            Description = description;
            MinPollingInterval = minPollingInterval;
            Min = min;
            Max = null;
        }

        /// <summary>
        /// Describes a performance data source
        /// </summary>
        /// <param name="name">The name of the source</param>
        /// <param name="description">The description of the source</param>
        /// <param name="minPollingInterval">The minimum polling interval of the source, in milliseconds</param>
        /// <param name="min">The minimum value the source will provide</param>
        /// <param name="max">The maximum value the source will provide</param>
        public PerfDataSourceAttribute(string name, string description, int minPollingInterval, float min, float max)
        {
            Name = name;
            Description = description;
            MinPollingInterval = minPollingInterval;
            Min = min;
            Max = max;
        }
    }
}
