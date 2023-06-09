using System.Numerics;

namespace CosPhiCalcTest;

public partial class SineRootDetection : Form
{
    Graphics g;

    float amplitude = 0;
    const int simSampleScalar = 25; //lower number = more samples (1 = max)

    Func<float, float, float, float, float> func = (height, offset, freq, x) =>
    {
        return (float)(height * Math.Sin(x/freq + offset));
    };

    public SineRootDetection()
    {
        InitializeComponent();
        amplitude = panel1.Height / 2f;
        g = panel1.CreateGraphics();
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    }

    private void Redraw(object sender = null, EventArgs e = null)
    {
        g.Clear(panel1.BackColor);
        int middle = Convert.ToInt32(amplitude);
        g.DrawLine(new(Color.Gray, 0.5f), new(0, middle), new(panel1.Width, middle));
        PointF[] sine1 = DrawSine(-((float)numericUpDown1.Value * MathF.PI) / 180, Color.Red);
        PointF[] sine2 = DrawSine(((float)numericUpDown1.Value * MathF.PI) / 180, Color.Blue);

        CalculatePhaseShift(sine1, sine2);
    }

    float CalculatePhaseShift(PointF[] sine1, PointF[] sine2) 
    {
        PointF prevP1 = new();
        PointF prevP2 = new();

        for (int i = 0; i < sine1.Length; i+= simSampleScalar)
        {
            if (Math.Sign(sine1[i].Y - amplitude) != Math.Sign(prevP1.Y - amplitude)) //sine 1 root found
            {
                g.DrawLine(new(Color.Green, 4), prevP1, sine1[i]);
                PointF root = ComputeRoot(prevP1, sine1[i]);
                if (float.IsNaN(root.X)) continue;
                g.DrawEllipse(new(Color.Black, 3), root.X - 2, root.Y + amplitude - 2, 4, 4);
            }

            if (Math.Sign(sine2[i].Y - amplitude) != Math.Sign(prevP2.Y - amplitude)) // sine 2 root found
            {
                g.DrawLine(new(Color.Green, 4), prevP2, sine2[i]);
                PointF root = ComputeRoot(prevP2, sine2[i]);
                if (float.IsNaN(root.X)) continue;
                g.DrawEllipse(new(Color.Black, 3), root.X - 2, root.Y + amplitude - 2, 4, 4);
                
            }

            g.DrawEllipse(new(Color.HotPink, 2), sine1[i].X - 1, sine1[i].Y - 1, 2, 2);
            g.DrawEllipse(new(Color.HotPink, 2), sine2[i].X - 1, sine2[i].Y - 1, 2, 2);
            prevP1 = sine1[i];
            prevP2 = sine2[i];
        }

        return 0;
    }

    PointF ComputeRoot(PointF pBefore, PointF pAfter) 
    {
        float m = (pAfter.Y - pBefore.Y) / (pAfter.X - pBefore.X);
        float q = pBefore.Y - (m * pBefore.X);
        return new PointF((amplitude-q) / m, 0); //amplitude here for zeroing the coords
    }

    PointF[] DrawSine(float hOffset, Color color) 
    {
        List<PointF> points = new();
        for (int x = 0; x < panel1.Width; x++)
        {
            points.Add(new(x, func(amplitude, hOffset, 35, x) + 200));
        }
        PointF[] result = points.ToArray();
        g.DrawCurve(new(color), result);
        return result;
    }
}
