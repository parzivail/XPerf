using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPerf.Api;
using XPerf.Controls;
using XPerf.Plugins;

namespace XPerf
{
    public partial class FormGraph : Form
    {
        private readonly Random _random = new Random();

        public FormGraph()
        {
            InitializeComponent();

            CreateGraphs();
        }

        private void CreateGraphs()
        {
            splitContainer.Panel1.Controls.Add(graphPreviewPanel);

            var sources = PluginProvider.LoadPlugins<IPollableDataSource, PerfDataSourceAttribute>("DataSources");

            foreach (var source in sources)
            {
                var c = new LineGraphListItem
                {
                    Text = source.Metadata.Name, 
                    Detail = source.Metadata.Description, 
                    Tag = source
                };

                if (source.Metadata.Min.HasValue)
                    c.MinValue = source.Metadata.Min.Value;

                if (source.Metadata.Max.HasValue)
                    c.MaxValue = source.Metadata.Max.Value;

                c.Selected += (sender, args) => Console.WriteLine(((LineGraphListItem) sender).Text);

                graphPreviewPanel.Controls.Add(c);
            }
        }

        private void CollectData(object sender, EventArgs e)
        {
            foreach (Control control in graphPreviewPanel.Controls)
            {
                if (!(control is LineGraphListItem li))
                    continue;
                
                Poll(li);
            }
        }

        private void Poll(LineGraphListItem graph)
        {
            if (!(graph.Tag is PluginInstance<IPollableDataSource, PerfDataSourceAttribute> instance))
                return;

            graph.AddDataPoint(instance.Plugin.Poll());
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
