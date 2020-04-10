using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPerf.Controls;
using XPerf.Utils;

namespace XPerf.Drawing
{
    class LineGraph
    {
        public static void DrawGraph(Graphics g, Rectangle graphRect, Color foreColor, Color backColor, int dataCursor, float[] data,
            float[] altData, float minValue, float maxValue, int horizontalGridLines, int verticalGridLines,
            bool showAlternateData, bool scrollAxes)
        {
            var gridlinePen = new Pen(Color.FromArgb(48, foreColor.R, foreColor.G, foreColor.B));

            // Draw gridlines with no smoothing
            g.SmoothingMode = SmoothingMode.None;

            using (var b = new SolidBrush(backColor))
                g.FillRectangle(b, graphRect);

            if (horizontalGridLines != 0)
                for (var x = 0; x < horizontalGridLines; x++)
                {
                    var lineX = Remapper.Remap(x, 0, horizontalGridLines, graphRect.Left, graphRect.Right);

                    var offset = Remapper.Remap(dataCursor, 0, data.Length - 1, 0, graphRect.Width);

                    if (scrollAxes)
                    {
                        lineX -= offset;
                        if (lineX < 0)
                            lineX += graphRect.Width;
                    }

                    g.DrawLine(gridlinePen, lineX, graphRect.Top, lineX, graphRect.Bottom);
                }

            if (verticalGridLines != 0)
                for (var y = 0; y < verticalGridLines; y++)
                {
                    var lineY = Remapper.Remap(y, 0, verticalGridLines, graphRect.Top, graphRect.Bottom);
                    g.DrawLine(gridlinePen, graphRect.Left, lineY, graphRect.Right, lineY);
                }

            gridlinePen.Dispose();

            // Draw data with smoothing
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawData(g, graphRect, minValue, maxValue, data, dataCursor, foreColor);
            if (showAlternateData) DrawData(g, graphRect, minValue, maxValue, altData, dataCursor, foreColor, true);

            // Draw graph border
            using (var p = new Pen(foreColor))
                g.DrawRectangle(p, graphRect);
        }

        private static void DrawData(Graphics g, Rectangle graphRect, float minValue, float maxValue, float[] data, int dataCursor, Color foreColor, bool altStyle = false)
        {
            var points = new PointF[data.Length + 2];

            points[0] = new PointF(graphRect.Right, graphRect.Bottom);
            points[1] = new PointF(graphRect.Left, graphRect.Bottom);

            for (var i = 0; i < data.Length; i++)
            {
                var x = Remapper.Remap(i, 0, data.Length - 1, graphRect.Left, graphRect.Right);
                var y = data[(dataCursor + i) % data.Length].Remap(minValue, maxValue, graphRect.Bottom, graphRect.Top);

                points[i + 2] = new PointF(x, y);
            }

            // Draw translucent graph area
            var path = new GraphicsPath();
            path.AddPolygon(points);

            if (altStyle)
            {
                using (var p = new Pen(foreColor) { DashStyle = DashStyle.Dash })
                    g.DrawLines(p, points.Skip(2).ToArray());
            }
            else
            {
                using (var b = new SolidBrush(Color.FromArgb(48, foreColor.R, foreColor.G, foreColor.B)))
                    g.FillPath(b, path);

                // Draw the outline on top of the data, skip the lines which overlap the graph border
                using (var p = new Pen(foreColor))
                    g.DrawLines(p, points.Skip(2).ToArray());
            }
        }
    }
}
