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
    [PerfDataSource("CPU Usage", "Tracks processor utilization", 0, 0, 100)]
    public class PerfCounterDataSource : IPollableDataSource
    {
        private readonly PerformanceCounter _perfCounter;
        private readonly string _cpuInfo;
        private float _value;

        public PerfCounterDataSource()
        {
            _perfCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            using (var win32Proc = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (var o in win32Proc.Get())
                {
                    var obj = (ManagementObject) o;
                    var procName = obj["Name"].ToString();

                   _cpuInfo = procName;
                }
            }
        }

        /// <inheritdoc />
        public void Poll()
        {
            _value = _perfCounter.NextValue();
        }

        /// <inheritdoc />
        public float GetValue()
        {
            return _value;
        }

        /// <inheritdoc />
        public string Format(float value)
        {
            return $"{value}%";
        }

        /// <inheritdoc />
        public string GetUnitHeader()
        {
            return "% Utilization";
        }

        /// <inheritdoc />
        public string GetGraphHeader()
        {
            return "CPU";
        }

        /// <inheritdoc />
        public string GetGraphDetailHeader()
        {
            return _cpuInfo;
        }
    }
}
