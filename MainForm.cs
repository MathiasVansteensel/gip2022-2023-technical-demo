namespace CosPhiCalcTest;
public partial class MainForm : Form
{
    private Size defSize = new Size(119, 87);

    private Dictionary<string, FormType> btns = new Dictionary<string, FormType>
    {
        { "Sine root approximation", new FormType<SineRootDetection>() },
        { "3D Graph rendering", new FormType<_3DGraph>() },
        { "Spectrum analyses using fft", new FormType<FFTSpectrum>() },
        { "Band-pass filter using fft", null },
        { "Full preview [Phase | Voltage | Current | Power]", null }

    };


    public MainForm()
    {
        InitializeComponent();
        for (int i = 0; i < btns.Count; i++)
        {
            Button btn = new Button
            {
                Size = defSize,
                Text = $"{i+1}: {btns.ElementAt(i).Key}",
                Tag = i
            };
            btn.Click += button_Click;
            flowLayoutPanel1.Controls.Add(btn);
        }
        flowLayoutPanel1.Refresh();
    }

    private void button_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int index = (int)btn?.Tag;
        Form form = btns.ElementAt(index).Value?.CreateInstance();
        form?.Show();
    }
}
