using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using XPerf.Api;

namespace XPerf.DataSource.PerfCounter
{
    [XPerfPlugin("CPU Usage", "Tracks processor utilization")]
    public class PerfCounterDataSource : XPerfDataProvider
    {
        private readonly PerformanceCounter _perfCounter;

        public PerfCounterDataSource()
        {
            _perfCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            using (var win32Proc = new ManagementObjectSearcher("select * from Win32_Processor"))
                foreach (var o in win32Proc.Get())
                    GraphDetailHeader = o["Name"].ToString();

            GraphHeader = "CPU";
            UnitHeader = "% Utilization";
        }

        public override float CollectData()
        {
            return _perfCounter.NextValue();
        }

        public override string Format(float value)
        {
            return $"{value}%";
        }
    }
}
