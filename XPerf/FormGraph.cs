using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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

            var sources = PluginProvider.LoadPlugins<XPerfDataProvider, XPerfPluginAttribute>("DataSources");

            foreach (var source in sources)
            {
                var c = new LineGraphListItem
                {
                    Text = source.Metadata.Name,
                    Detail = source.Metadata.Description,
                    Tag = source
                };

                var min = source.Plugin.GetMin();
                var max = source.Plugin.GetMax();

                if (min.HasValue)
                    c.MinValue = min.Value;

                if (max.HasValue)
                    c.MaxValue = max.Value;

                c.Selected += (sender, args) => ChangeCenterGraph((LineGraphListItem)sender);

                graphPreviewPanel.Controls.Add(c);
            }
        }

        private void ChangeCenterGraph(LineGraphListItem sender)
        {
            var source = (PluginInstance<XPerfDataProvider, XPerfPluginAttribute>)sender.Tag;

            var c = new LineGraph
            {
                Text = source.Plugin.GraphHeader,
                Detail = source.Plugin.GraphDetailHeader,
                SubHeader = source.Plugin.UnitHeader,
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            var min = source.Plugin.GetMin();
            var max = source.Plugin.GetMax();

            if (min.HasValue)
                c.MinValue = min.Value;

            if (max.HasValue)
                c.MaxValue = max.Value;

            c.SetData(sender.GetData());

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(c);
        }

        private void CollectData(object sender, EventArgs e)
        {
            foreach (Control control in graphPreviewPanel.Controls)
            {
                if (!(control is LineGraphListItem li))
                    continue;

                var instance = (PluginInstance<XPerfDataProvider, XPerfPluginAttribute>)li.Tag;
                instance.Plugin.Poll();

                li.AddDataPoint(instance.Plugin.GetValue());
                if (li.IsSelected) ((LineGraph)splitContainer.Panel2.Controls[0]).SetData(li.GetData());
            }
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
