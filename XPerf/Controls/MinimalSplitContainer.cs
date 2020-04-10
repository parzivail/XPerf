using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace XPerf.Controls
{
    [Designer(typeof(Designer), typeof(IRootDesigner))]
    public partial class MinimalSplitContainer : UserControl
    {
        private bool _dragging;

        private int _splitterDistance;

        public int SplitterDistance
        {
            get => _splitterDistance;
            set
            {
                if (value <= Panel1.Margin.Size.Width + 1 || value >= Width - Panel2.Margin.Size.Width - 1)
                    return;

                _splitterDistance = value;
                UpdateContainerSizes();
            }
        }

        public MinimalSplitContainer()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left)
                return;
            _dragging = true;
        }

        /// <inheritdoc />
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button != MouseButtons.Left)
                return;
            _dragging = false;
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_dragging)
                SplitterDistance = e.X;
        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateContainerSizes();
        }

        private void UpdateContainerSizes()
        {
            using (this.SuspendPainting())
            {
                Panel1.Width = SplitterDistance - Panel1.Margin.Left - Panel1.Margin.Right;

                Panel2.Location = new Point(Panel1.Width + Panel1.Margin.Right + Panel2.Margin.Left, Panel2.Location.Y);
                Panel2.Width = (Width - SplitterDistance) - Panel2.Margin.Left - Panel2.Margin.Right;
            }
        }

        public class Designer : ScrollableControlDesigner
        {
        }
    }
}
