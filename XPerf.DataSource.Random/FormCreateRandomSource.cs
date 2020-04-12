using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XPerf.DataSource.Random
{
    public partial class FormCreateRandomSource : Form
    {
        public string SourceName { get; private set; }
        public float SourceMin { get; private set; }
        public float SourceMax { get; private set; }

        public FormCreateRandomSource()
        {
            InitializeComponent();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            SourceName = tbName.Text;
        }

        private void nudMinimum_ValueChanged(object sender, EventArgs e)
        {
            SourceMin = (float) nudMinimum.Value;
        }

        private void nudMaximum_ValueChanged(object sender, EventArgs e)
        {
            SourceMax = (float) nudMaximum.Value;
        }
    }
}
