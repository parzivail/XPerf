using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPerf.Api
{
    public abstract class XPerfSourceProvider
    {
        public abstract XPerfDataProvider CreateDataProvider();
    }
}
