using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace XPerf.CounterIO
{
    internal class PerformanceCounterMetadata
    {
        public string Category { get; set; }
        public PerformanceCounterCategoryType Type { get; set; }

        public PerformanceCounterMetadataInstance[] Instances;

        private static void ExportMetadata(string filename)
        {
            var categories = PerformanceCounterCategory.GetCategories();

            var objDump = new List<PerformanceCounterMetadata>();

            foreach (var category in categories)
            {
                try
                {
                    var meta = new PerformanceCounterMetadata
                    {
                        Category = category.CategoryName,
                        Type = category.CategoryType
                    };

                    if (meta.Category == "Thread")
                        continue;

                    if (category.CategoryType == PerformanceCounterCategoryType.MultiInstance)
                    {
                        var instances = category.GetInstanceNames();

                        var foundInstances = new List<PerformanceCounterMetadataInstance>();
                        foreach (var i in instances)
                        {
                            try
                            {
                                foundInstances.Add(new PerformanceCounterMetadataInstance
                                {
                                    Instance = i,
                                    Counters = category.GetCounters(i).Select(counter => counter.CounterName)
                                        .Distinct().ToArray()
                                });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Instance GetCounters failed: {category.CategoryName}: {e.Message}");
                            }
                        }

                        meta.Instances = foundInstances.ToArray();
                    }
                    else
                    {
                        meta.Instances = new PerformanceCounterMetadataInstance[1];
                        meta.Instances[0] = new PerformanceCounterMetadataInstance()
                        {
                            Instance = null,
                            Counters = category.GetCounters().Select(counter => counter.CounterName).Distinct()
                                .ToArray()
                        };
                    }

                    objDump.Add(meta);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Root Listing Failed: {category.CategoryName}: {e.Message}");
                }
            }

            File.WriteAllText(filename, JsonConvert.SerializeObject(objDump));
        }
    }
}