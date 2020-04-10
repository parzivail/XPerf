using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPerf.Controls;

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

            for (var i = 0; i < 10; i++)
            {
                var c = new LineGraphListItem
                {
                    Text = $"Line Graph {i}",
                    Detail = $"Detail string {i}"
                };

                graphPreviewPanel.Controls.Add(c);
            }
        }

        private void CollectData(object sender, EventArgs e)
        {
            foreach (Control control in graphPreviewPanel.Controls)
            {
                if (!(control is LineGraphListItem li))
                    continue;
                
                AddRandomData(li);
            }
        }

        private void AddRandomData(LineGraphListItem graph)
        {
            var height = (graph.MaxValue - graph.MinValue);
            graph.AddDataPoint((float)(_random.NextDouble() * height) + graph.MinValue, (float)(_random.NextDouble() * height) + graph.MinValue);
        }
    }
}
