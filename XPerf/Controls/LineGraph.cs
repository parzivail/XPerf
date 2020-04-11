using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Humanizer;
using Humanizer.Localisation;
using XPerf.Drawing;

namespace XPerf.Controls
{
    public class LineGraph : Control
    {
        private readonly float[] _data;
        private readonly float[] _altData;

        private int _dataCursor;

        [DefaultValue(false)] public bool ShowAlternateData { get; set; } = false;

        [DefaultValue(true)] public bool ScrollAxes { get; set; } = true;

        [DefaultValue(false)] public bool AutoSizeAxes { get; set; } = false;

        [DefaultValue(true)] public bool DrawHeaders { get; set; } = true;

        [DefaultValue(true)] public bool DrawSubHeaders { get; set; } = true;

        [DefaultValue(0)] public float MinValue { get; set; } = 0;

        [DefaultValue(1)] public float MaxValue { get; set; } = 1;

        [DefaultValue(18)] public int HorizontalGridLines { get; set; } = 18;

        [DefaultValue(10)] public int VerticalGridLines { get; set; } = 10;

        [DefaultValue(30)] public int NumDataPoints { get; set; } = 30;

        [DefaultValue(typeof(TimeSpan), "00:00:01")]
        public TimeSpan UpdateFrequency { get; set; } = TimeSpan.FromSeconds(1);

        [DefaultValue(typeof(Color), "#117DBB")]
        public override Color ForeColor { get; set; } = Color.FromArgb(17, 125, 187);

        [DefaultValue(typeof(Color), "Black")]
        public Color HeaderColor { get; set; } = Color.Black;

        [DefaultValue("")]
        public string Detail { get; set; } = "";

        [DefaultValue("")]
        public string SubHeader { get; set; } = "";

        [DefaultValue(typeof(Font), "Segoe UI, 21.3pt")]
        public override Font Font { get; set; } = new Font(new FontFamily("Segoe UI"), 21.3f);

        [DefaultValue(typeof(Font), "Segoe UI, 12pt")]
        public Font DetailFont { get; set; } = new Font(new FontFamily("Segoe UI"), 12);

        [DefaultValue(typeof(Color), "Gray")]
        public Color SubheaderColor { get; set; } = Color.Gray;

        [DefaultValue(typeof(Font), "Segoe UI, 8.25pt")]
        public Font SubheaderFont { get; set; } = new Font(new FontFamily("Segoe UI"), 8.25f);

        [DefaultValue("")]
        public string BaseSiUnit { get; set; } = string.Empty;

        [Bindable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get; set; }

        public LineGraph()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint| ControlStyles.OptimizedDoubleBuffer, true);
            ResizeRedraw = true;

            _data = new float[NumDataPoints];
            _altData = new float[NumDataPoints];
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
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGraph(e.Graphics, ClientRectangle);
        }

        private void DrawGraph(Graphics g, Rectangle rect)
        {
            var minValue = MinValue;
            var maxValue = MaxValue;

            g.CompositingQuality = CompositingQuality.HighQuality;

            if (AutoSizeAxes)
            {
                minValue = _data.Min();
                maxValue = _data.Max();

                if (maxValue == minValue)
                {
                    maxValue++;
                    minValue--;
                }
            }

            rect.Width--;
            rect.Height--;

            rect.X += Padding.Left;
            rect.Y += Padding.Top;
            rect.Width -= Padding.Size.Width;
            rect.Height -= Padding.Size.Height;

            var headerHeight = DrawHeaders ? 40 : 0;
            var subHeaderHeight = DrawSubHeaders ? 20 : 0;

            var graphRect = new Rectangle(rect.X, rect.Y + headerHeight + subHeaderHeight, rect.Width, rect.Height - headerHeight - subHeaderHeight - subHeaderHeight);
            var headerRect = new Rectangle(rect.X, rect.Y, rect.Width, headerHeight);
            var topSubHeaderRect = new Rectangle(rect.X, rect.Y + headerHeight, rect.Width, subHeaderHeight);
            var bottomSubHeaderRect = new Rectangle(rect.X, rect.Y + rect.Height - subHeaderHeight, rect.Width, subHeaderHeight);
            
            GraphPainting.DrawLineGraph(g, graphRect, ForeColor, BackColor, _dataCursor, _data, _altData, minValue, maxValue, HorizontalGridLines, VerticalGridLines, ShowAlternateData, ScrollAxes);

            // Draw headers
            if (DrawHeaders)
            {
                TextRenderer.DrawText(g, Text, Font, headerRect, HeaderColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

                TextRenderer.DrawText(g, Detail, DetailFont, headerRect, HeaderColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }

            if (DrawSubHeaders)
            {
                // Top sub headers
                TextRenderer.DrawText(g, SubHeader, SubheaderFont, topSubHeaderRect, SubheaderColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

                TextRenderer.DrawText(g, $"{MetricNumeralExtensions.ToMetric(maxValue, true)}{BaseSiUnit}",
                    SubheaderFont, topSubHeaderRect, SubheaderColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

                // Bottom sub headers
                var seconds = (UpdateFrequency.TotalSeconds * _data.Length).Seconds();
                TextRenderer.DrawText(g, $"{seconds}", SubheaderFont, bottomSubHeaderRect, SubheaderColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

                TextRenderer.DrawText(g, $"{MetricNumeralExtensions.ToMetric(minValue, true)}{BaseSiUnit}",
                    SubheaderFont, bottomSubHeaderRect, SubheaderColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }
        }
    }
}
