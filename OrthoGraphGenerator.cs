using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Media3D;

enum GraphType { Line, Dots, Poly };

class OrthoGraphGenerator
{
    private float angleRad;
    private float bmpSize; //float to avoid casts
    private float halfBmpSize;
    private float resScalar;
    private string xAxisName;
    private string yAxisName;
    private string zAxisName;
    private Color graphColor;
    private GraphType graphType;
    private List<Point3D> dataPoints;

    public OrthoGraphGenerator(float angle, int bmpSize, string xAxisName, string yAxisName, string zAxisName, Color graphColor, GraphType graphType, List<Point3D> dataPoints)
    {
        this.angleRad = (angle * MathF.PI) / 180f;
        this.bmpSize = bmpSize;
        this.halfBmpSize = bmpSize/2f;
        this.resScalar = bmpSize / 500; //500 was test size
        this.xAxisName = xAxisName;
        this.yAxisName = yAxisName;
        this.zAxisName = zAxisName;
        this.graphColor = graphColor;
        this.graphType = graphType;
        this.dataPoints = dataPoints;
    }

    public Bitmap GenerateGraph()
    {
        Bitmap bitmap = new Bitmap((int)bmpSize, (int)bmpSize, PixelFormat.Format24bppRgb);
        Graphics g = Graphics.FromImage(bitmap);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        g.Clear(Color.White);

        Pen axisPen = new Pen(Color.Black, 2);
        Pen graphPen = new Pen(graphColor, 2);

        // Draw X axis
        g.DrawLine(axisPen, new PointF(0, MathF.Tan(angleRad)*(-halfBmpSize)), new PointF(bmpSize, MathF.Tan(angleRad) * halfBmpSize)); //fix const axis
        g.DrawString(xAxisName, new Font("Arial", 12 * resScalar), Brushes.Black, new PointF(5 * resScalar, halfBmpSize + 5 * resScalar));

        // Draw Y axis
        g.DrawLine(axisPen, new PointF(halfBmpSize, 0), new PointF(halfBmpSize, bmpSize)); //fix const axis
        g.DrawString(yAxisName, new Font("Arial", 12 * resScalar), Brushes.Black, new PointF(halfBmpSize + 5 * resScalar, 10 * resScalar));

        // Draw Z axis
        g.DrawLine(axisPen, new PointF(0, MathF.Tan(angleRad) * halfBmpSize), new PointF(bmpSize, -MathF.Tan(angleRad) * bmpSize)); //fix const axis
        g.DrawString(zAxisName, new Font("Arial", 12 * resScalar ), Brushes.Black, new PointF(bmpSize - 100 * resScalar, bmpSize - 120 * resScalar));

        switch (graphType)
        {
            case GraphType.Line:
                for (int i = 0; i < dataPoints.Count - 1; i++)
                {
                    Point3D p1 = dataPoints[i];
                    Point3D p2 = dataPoints[i + 1];
                    g.DrawLine(graphPen, new PointF((float)p1.X + halfBmpSize, (float)p1.Y + halfBmpSize), new PointF((float)p2.X + halfBmpSize, (float)p2.Y + halfBmpSize));
                }
                break;
            case GraphType.Dots:
                foreach (Point3D p in dataPoints) //TODO apply new transform to all cases, make a bit more readable :), and redraw axis to angle
                {
                    float falloff = 0.0065f;
                    float offset = 5f; 
                    float size = (float)((falloff * p.Z*bmpSize) + offset) * resScalar; // Smaller size for points further away in Z direction
                    float tx = (float)(p.X + p.Z);
                    float ty = (float)(MathF.Tan(angleRad) * p.X + MathF.Tan(angleRad) * p.Z + p.Y);
                    g.FillEllipse(new SolidBrush(graphColor), (tx+0.5f) * (halfBmpSize), (ty+0.5f) * (halfBmpSize), size, size);
                }
                break;
            case GraphType.Poly:
                PointF[] points = new PointF[dataPoints.Count];
                for (int i = 0; i < dataPoints.Count; i++)
                {
                    Point3D p = dataPoints[i];
                    points[i] = new PointF((float)p.X + halfBmpSize, (float)p.Y + halfBmpSize);
                }
                g.DrawPolygon(new(Color.FromArgb(128, graphColor)), points);
                break;
        }
        return bitmap;
    }
}

