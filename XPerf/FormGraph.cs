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

            splitContainer.Panel1.Controls.Add(graphPreviewPanel);
            var plugins = PluginProvider.LoadPlugins<XPerfSourceProvider, XPerfPluginAttribute>("DataSources");

            foreach (var pluginInstance in plugins)
            {
                var tsmi = new ToolStripMenuItem(pluginInstance.Metadata.Name);
                tsmi.Click += (sender, args) =>
                {
                    var source = pluginInstance.Plugin.CreateDataProvider();
                    if (source == null)
                        return;

                    CreateGraph(source);
                };
                bAddSource.DropDownItems.Add(tsmi);
            }
        }

        private void CreateGraph(XPerfDataProvider source)
        {
            var c = new LineGraphListItem
            {
                Text = source.GraphHeader,
                Detail = source.UnitHeader,
                Tag = source
            };

            var min = source.GetMin();
            var max = source.GetMax();

            if (min.HasValue)
                c.MinValue = min.Value;

            if (max.HasValue)
                c.MaxValue = max.Value;

            c.Selected += (sender, args) => ChangeCenterGraph((LineGraphListItem)sender);

            graphPreviewPanel.Controls.Add(c);
        }

        private void ChangeCenterGraph(LineGraphListItem sender)
        {
            var source = (XPerfDataProvider)sender.Tag;

            var c = new LineGraph
            {
                Text = source.GraphHeader,
                Detail = source.GraphDetailHeader,
                SubHeader = source.UnitHeader,
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            var min = source.GetMin();
            var max = source.GetMax();

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

                var instance = (XPerfDataProvider)li.Tag;
                instance.Poll();

                li.AddDataPoint(instance.GetValue());
                if (li.IsSelected) ((LineGraph)splitContainer.Panel2.Controls[0]).SetData(li.GetData());
            }
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
