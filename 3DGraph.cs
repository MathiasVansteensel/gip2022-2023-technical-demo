using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;

namespace CosPhiCalcTest;
public partial class _3DGraph : Form
{
    public _3DGraph()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Random r = new();
        List<Point3D> dataPoints = new List<Point3D>();
        for (int i = 0; i < 100; i++)
        {
            double x = r.NextDouble() - 0.5f;
            double y = r.NextDouble() - 0.5f;
            double z = r.NextDouble() - 0.5f;

            dataPoints.Add(new(x, y, z));
        }

        OrthoGraphGenerator grapher = new(15, 2048, "X-axis", "Y-axis", "Z-axis", Color.Blue, GraphType.Dots, dataPoints);
        MemoryStream ms = new();
        grapher.GenerateGraph().Save(ms, ImageFormat.Png);
        pictureBox1.Image = Image.FromStream(ms);
    }
}
