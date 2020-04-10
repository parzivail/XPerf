using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XPerf.Controls
{
    public class StackPanel : FlowLayoutPanel
    {
        public StackPanel()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            WrapContents = false;
            ResumeLayout(false);

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_CLIPCHILDREN
                return cp;
            }
        }

        /// <inheritdoc />
        protected override void OnScroll(ScrollEventArgs se)
        {
            Invalidate();
            base.OnScroll(se);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            SuspendLayout();
            base.OnResize(eventargs);

            switch (FlowDirection)
            {
                case FlowDirection.BottomUp:
                case FlowDirection.TopDown:
                    foreach (Control control in Controls)
                        control.Width = ClientSize.Width - control.Margin.Size.Width;
                    break;
                case FlowDirection.LeftToRight:
                case FlowDirection.RightToLeft:
                    foreach (Control control in Controls)
                        control.Height = ClientSize.Height - control.Margin.Size.Width;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ResumeLayout(true);
        }
    }
}
