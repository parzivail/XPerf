using System;

namespace XPerf.Api
{
    /// <summary>
    /// Provides an attribute to designate performance data sources
    /// </summary>
    public class XPerfPluginAttribute : Attribute
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
        /// Describes a performance data source
        /// </summary>
        /// <param name="name">The name of the source</param>
        /// <param name="description">The description of the source</param>
        public XPerfPluginAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
