using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Humanizer;
using XPerf.Drawing;

namespace XPerf.Controls
{
    public class LineGraphListItem : Control
    {
        private readonly float[] _data;
        private readonly float[] _altData;

        private int _dataCursor;

        private bool _hover = false;
        private bool _isSelected = false;

        [DefaultValue(false)] public bool ShowAlternateData { get; set; } = false;

        [DefaultValue(true)] public bool ScrollAxes { get; set; } = true;

        [DefaultValue(true)] public bool DrawHeaders { get; set; } = true;

        [DefaultValue(false)]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value && !_isSelected)
                {
                    DeselectOthers();
                    _isSelected = true;
                    Selected?.Invoke(this, EventArgs.Empty);
                }
                
                _isSelected = value;
                Invalidate();
            }
        }

        [DefaultValue(0)] public float? MinValue { get; set; } = 0;

        [DefaultValue(1)] public float? MaxValue { get; set; } = 1;

        [DefaultValue(0)] public int HorizontalGridLines { get; set; } = 0;

        [DefaultValue(0)] public int VerticalGridLines { get; set; } = 0;

        [DefaultValue(30)] public int NumDataPoints { get; set; } = 30;

        [DefaultValue(60)] public int GraphWidth { get; set; } = 60;

        [DefaultValue(5)] public int GraphPadding { get; set; } = 5;

        [DefaultValue(typeof(TimeSpan), "00:00:01")]
        public TimeSpan UpdateFrequency { get; set; } = TimeSpan.FromSeconds(1);

        [DefaultValue(typeof(Color), "#117DBB")]
        public override Color ForeColor { get; set; } = Color.FromArgb(17, 125, 187);

        [DefaultValue(typeof(Color), "Black")]
        public Color HeaderColor { get; set; } = Color.Black;

        [DefaultValue(typeof(Color), "#058EFF")]
        public Color FocusColor { get; set; } = Color.FromArgb(5, 142, 255);

        [DefaultValue(typeof(Color), "#070707")]
        public Color SelectedColor { get; set; } = Color.FromArgb(7, 7, 7);

        [DefaultValue("")]
        public string Detail { get; set; } = "";

        [DefaultValue(typeof(Font), "Segoe UI, 12pt")]
        public override Font Font { get; set; } = new Font(new FontFamily("Segoe UI"), 12);

        [DefaultValue(typeof(Font), "Segoe UI, 9.75pt")]
        public Font DetailFont { get; set; } = new Font(new FontFamily("Segoe UI"), 9.75f);

        [DefaultValue("")]
        public string BaseSiUnit { get; set; } = string.Empty;

        [DefaultValue("3, 0, 3, 0")]
        protected override Padding DefaultMargin { get; } = new Padding(3, 0, 3, 0);

        [DefaultValue("5, 5, 5, 5")]
        protected override Padding DefaultPadding { get; } = new Padding(5, 5, 5, 5);

        [DefaultValue("200, 60")]
        protected override Size DefaultSize { get; } = new Size(200, 60);

        [Bindable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get; set; }

        public event EventHandler<EventArgs> Selected;

        public LineGraphListItem()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            SuspendLayout();
            Name = "LineGraphListItem";

            ResizeRedraw = true;

            ResumeLayout(false);

            _data = new float[NumDataPoints];
            _altData = new float[NumDataPoints];
        }

        private void DeselectOthers()
        {
            var siblings = Parent?.Controls;
            if (siblings == null)
                return;

            foreach (Control sibling in siblings)
            {
                if (!(sibling is LineGraphListItem li))
                    continue;

                li.IsSelected = false;
            }
        }

        public void AddDataPoint(float point)
        {
            AddDataPoint(point, 0);
        }

        public void AddDataPoint(float point, float altPoint)
        {
            _data[_dataCursor] = point;
            _altData[_dataCursor] = altPoint;
            _dataCursor = (_dataCursor + 1) % _data.Length;

            Invalidate();
        }

        public void SetData(float[] data)
        {
            if (data.Length != NumDataPoints)
                throw new ArgumentOutOfRangeException(nameof(data));

            for (var i = 0; i < data.Length; i++) _data[(_dataCursor + i) % data.Length] = data[i];

            Invalidate();
        }

        public float[] GetData()
        {
            var data = new float[NumDataPoints];
            
            for (var i = 0; i < data.Length; i++) data[i] = _data[(_dataCursor + i) % data.Length];

            return data;
        }

        /// <inheritdoc />
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            _hover = true;
            Invalidate();
        }

        /// <inheritdoc />
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _hover = false;
            Invalidate();
        }

        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGraph(e.Graphics, ClientRectangle);
        }

        /// <inheritdoc />
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Focus();
        }

        /// <inheritdoc />
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            IsSelected = true;
            Invalidate();
        }

        /// <inheritdoc />
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        private void DrawGraph(Graphics g, Rectangle rect)
        {
            var minValue = MinValue ?? _data.Min();
            var maxValue = MaxValue ?? _data.Max();

            if (maxValue == minValue)
            {
                maxValue++;
                minValue--;
            }

            rect.Width--;
            rect.Height--;

            var clientRect = rect;

            rect.X += Padding.Left;
            rect.Y += Padding.Top;
            rect.Width -= Padding.Size.Width;
            rect.Height -= Padding.Size.Height;

            var headerHeight = Font.Height;

            var graphRect = new Rectangle(rect.X + GraphPadding, rect.Y + GraphPadding, GraphWidth, rect.Height - 2 * GraphPadding);
            var headerRect = new Rectangle(rect.X + GraphWidth + 3 * GraphPadding - 1, rect.Y, rect.Width - GraphWidth - 2 * GraphPadding, headerHeight);
            var detailRect = new Rectangle(rect.X + GraphWidth + 3 * GraphPadding, headerHeight, rect.Width - GraphWidth - 2 * GraphPadding, rect.Height - headerHeight);

            if (Focused)
            {
                using (var b = new SolidBrush(Color.FromArgb(50, FocusColor.R, FocusColor.G, FocusColor.B)))
                    g.FillRectangle(b, clientRect);
            }
            else if (IsSelected)
            {
                using (var b = new SolidBrush(Color.FromArgb(38, SelectedColor.R, SelectedColor.G, SelectedColor.B)))
                    g.FillRectangle(b, clientRect);
            }
            else if (_hover)
            {
                using (var b = new SolidBrush(Color.FromArgb(38, FocusColor.R, FocusColor.G, FocusColor.B)))
                    g.FillRectangle(b, clientRect);
                using (var p = new Pen(Color.FromArgb(50, FocusColor.R, FocusColor.G, FocusColor.B)))
                    g.DrawRectangle(p, clientRect.X, clientRect.Y, clientRect.Width - 1, clientRect.Height - 1);
            }

            GraphPainting.DrawLineGraph(g, graphRect, ForeColor, BackColor, _dataCursor, _data, _altData, minValue, maxValue, HorizontalGridLines, VerticalGridLines, ShowAlternateData, ScrollAxes);

            // Draw headers
            if (DrawHeaders)
            {
                TextRenderer.DrawText(g, Text, Font, headerRect, HeaderColor, TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
                TextRenderer.DrawText(g, Detail, DetailFont, detailRect, HeaderColor, TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
            }
        }
    }
}
