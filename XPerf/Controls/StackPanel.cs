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
            
            // Disable horizontal scrollbar
            AutoScroll = false;
            HorizontalScroll.Maximum = 0;
            AutoScroll = true;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool WrapContents
        {
            get => base.WrapContents;
            set => base.WrapContents = value;
        }

        /// <summary>
        /// Get or set a value that when is true forces the resizing of each control.
        /// If this value is false then only control that have AutoSize == true will be resized to
        /// fit the client size of this container.
        /// </summary>
        [DefaultValue(true)]
        public bool ForceAutoresizeOfControls { get; set; } = true;

        /// <inheritdoc />
        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            
            switch (FlowDirection)
            {
                case FlowDirection.BottomUp:
                case FlowDirection.TopDown:
                    foreach (Control control in Controls)
                        if (ForceAutoresizeOfControls || control.AutoSize)
                            control.Width = ClientSize.Width - control.Margin.Left - control.Margin.Right;
                    break;
                case FlowDirection.LeftToRight:
                case FlowDirection.RightToLeft:
                    foreach (Control control in Controls)
                        if (ForceAutoresizeOfControls || control.AutoSize)
                            control.Height = ClientSize.Height - control.Margin.Top - control.Margin.Bottom;
                    break;
            }
        }
    }
}
