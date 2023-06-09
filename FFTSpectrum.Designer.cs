namespace CosPhiCalcTest;

partial class FFTSpectrum
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		label1 = new Label();
		lbActualFreq = new Label();
		lbCalcFreq = new Label();
		label4 = new Label();
		groupBox1 = new GroupBox();
		panel1 = new Panel();
		panel6 = new Panel();
		label2 = new Label();
		label3 = new Label();
		panel3 = new Panel();
		lblMinBandFreq = new Label();
		button1 = new Button();
		groupBox2 = new GroupBox();
		label6 = new Label();
		label7 = new Label();
		panel2 = new Panel();
		cbPlotReal = new CheckBox();
		cbPlotImag = new CheckBox();
		cbEnableNoise = new CheckBox();
		trackBar1 = new TrackBar();
		label5 = new Label();
		label8 = new Label();
		groupBox3 = new GroupBox();
		panel4 = new Panel();
		tbrMinBandFreq = new TrackBar();
		tbrMaxBandFreq = new TrackBar();
		label9 = new Label();
		label10 = new Label();
		lblMaxBandFreq = new Label();
		trackBar2 = new TrackBar();
		tbNoiseType = new CheckBox();
		groupBox1.SuspendLayout();
		panel6.SuspendLayout();
		groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
		groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)tbrMinBandFreq).BeginInit();
		((System.ComponentModel.ISupportInitialize)tbrMaxBandFreq).BeginInit();
		((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
		SuspendLayout();
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(12, 9);
		label1.Name = "label1";
		label1.Size = new Size(183, 15);
		label1.TabIndex = 0;
		label1.Text = "Actual combined frequencies:......";
		// 
		// lbActualFreq
		// 
		lbActualFreq.AutoSize = true;
		lbActualFreq.Location = new Point(181, 9);
		lbActualFreq.Name = "lbActualFreq";
		lbActualFreq.Size = new Size(34, 15);
		lbActualFreq.TabIndex = 1;
		lbActualFreq.Text = "none";
		// 
		// lbCalcFreq
		// 
		lbCalcFreq.AutoSize = true;
		lbCalcFreq.Location = new Point(181, 24);
		lbCalcFreq.Name = "lbCalcFreq";
		lbCalcFreq.Size = new Size(34, 15);
		lbCalcFreq.TabIndex = 3;
		lbCalcFreq.Text = "none";
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Location = new Point(12, 24);
		label4.Name = "label4";
		label4.Size = new Size(175, 15);
		label4.TabIndex = 2;
		label4.Text = "Calculated frequencies:...............";
		// 
		// groupBox1
		// 
		groupBox1.Controls.Add(panel1);
		groupBox1.Controls.Add(panel6);
		groupBox1.Controls.Add(panel3);
		groupBox1.Location = new Point(12, 223);
		groupBox1.Name = "groupBox1";
		groupBox1.Size = new Size(1258, 264);
		groupBox1.TabIndex = 4;
		groupBox1.TabStop = false;
		groupBox1.Text = "Spectrum";
		// 
		// panel1
		// 
		panel1.Dock = DockStyle.Fill;
		panel1.Location = new Point(3, 19);
		panel1.Name = "panel1";
		panel1.Size = new Size(1252, 182);
		panel1.TabIndex = 0;
		// 
		// panel6
		// 
		panel6.Controls.Add(label2);
		panel6.Controls.Add(label3);
		panel6.Dock = DockStyle.Bottom;
		panel6.Location = new Point(3, 201);
		panel6.Name = "panel6";
		panel6.Size = new Size(1252, 30);
		panel6.TabIndex = 10;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new Point(0, 0);
		label2.Name = "label2";
		label2.Size = new Size(27, 15);
		label2.TabIndex = 1;
		label2.Text = "0Hz";
		label2.TextAlign = ContentAlignment.MiddleLeft;
		// 
		// label3
		// 
		label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		label3.Location = new Point(999, 0);
		label3.Name = "label3";
		label3.Size = new Size(253, 15);
		label3.TabIndex = 2;
		label3.Text = "100Hz";
		label3.TextAlign = ContentAlignment.MiddleRight;
		// 
		// panel3
		// 
		panel3.Dock = DockStyle.Bottom;
		panel3.Location = new Point(3, 231);
		panel3.Name = "panel3";
		panel3.Size = new Size(1252, 30);
		panel3.TabIndex = 9;
		// 
		// lblMinBandFreq
		// 
		lblMinBandFreq.AutoSize = true;
		lblMinBandFreq.Location = new Point(1268, 35);
		lblMinBandFreq.Name = "lblMinBandFreq";
		lblMinBandFreq.Size = new Size(36, 15);
		lblMinBandFreq.TabIndex = 17;
		lblMinBandFreq.Text = "30Hz ";
		// 
		// button1
		// 
		button1.Location = new Point(1195, 9);
		button1.Name = "button1";
		button1.Size = new Size(75, 23);
		button1.TabIndex = 5;
		button1.Text = "Randomize";
		button1.UseVisualStyleBackColor = true;
		button1.Click += Randomize;
		// 
		// groupBox2
		// 
		groupBox2.Controls.Add(label6);
		groupBox2.Controls.Add(label7);
		groupBox2.Controls.Add(panel2);
		groupBox2.Location = new Point(12, 52);
		groupBox2.Name = "groupBox2";
		groupBox2.Size = new Size(1258, 150);
		groupBox2.TabIndex = 5;
		groupBox2.TabStop = false;
		groupBox2.Text = "Input data";
		// 
		// label6
		// 
		label6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		label6.Location = new Point(258, 290);
		label6.Name = "label6";
		label6.Size = new Size(1235, 15);
		label6.TabIndex = 3;
		label6.Text = "50Hz";
		label6.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// label7
		// 
		label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		label7.Location = new Point(1499, 315);
		label7.Name = "label7";
		label7.Size = new Size(253, 15);
		label7.TabIndex = 2;
		label7.Text = "100Hz";
		label7.TextAlign = ContentAlignment.MiddleRight;
		// 
		// panel2
		// 
		panel2.Dock = DockStyle.Fill;
		panel2.Location = new Point(3, 19);
		panel2.Name = "panel2";
		panel2.Size = new Size(1252, 128);
		panel2.TabIndex = 0;
		// 
		// cbPlotReal
		// 
		cbPlotReal.AutoSize = true;
		cbPlotReal.Checked = true;
		cbPlotReal.CheckState = CheckState.Checked;
		cbPlotReal.Location = new Point(1098, 205);
		cbPlotReal.Name = "cbPlotReal";
		cbPlotReal.Size = new Size(72, 19);
		cbPlotReal.TabIndex = 6;
		cbPlotReal.Text = "Plot Real";
		cbPlotReal.UseVisualStyleBackColor = true;
		// 
		// cbPlotImag
		// 
		cbPlotImag.AutoSize = true;
		cbPlotImag.Location = new Point(1187, 205);
		cbPlotImag.Name = "cbPlotImag";
		cbPlotImag.Size = new Size(80, 19);
		cbPlotImag.TabIndex = 7;
		cbPlotImag.Text = "Plot Imag.";
		cbPlotImag.UseVisualStyleBackColor = true;
		// 
		// cbEnableNoise
		// 
		cbEnableNoise.AutoSize = true;
		cbEnableNoise.Location = new Point(1095, 12);
		cbEnableNoise.Name = "cbEnableNoise";
		cbEnableNoise.Size = new Size(94, 19);
		cbEnableNoise.TabIndex = 8;
		cbEnableNoise.Text = "Enable Noise";
		cbEnableNoise.UseVisualStyleBackColor = true;
		// 
		// trackBar1
		// 
		trackBar1.AutoSize = false;
		trackBar1.Location = new Point(15, 502);
		trackBar1.Minimum = 1;
		trackBar1.Name = "trackBar1";
		trackBar1.Size = new Size(1255, 26);
		trackBar1.TabIndex = 9;
		trackBar1.Value = 1;
		trackBar1.Scroll += trackBar1_Scroll;
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Location = new Point(15, 485);
		label5.Name = "label5";
		label5.Size = new Size(64, 15);
		label5.TabIndex = 10;
		label5.Text = "Main freq: ";
		// 
		// label8
		// 
		label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		label8.Location = new Point(12, 9);
		label8.Name = "label8";
		label8.Size = new Size(1337, 23);
		label8.TabIndex = 11;
		label8.Text = "FFT computed in:";
		label8.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// groupBox3
		// 
		groupBox3.Controls.Add(panel4);
		groupBox3.Location = new Point(12, 534);
		groupBox3.Name = "groupBox3";
		groupBox3.Size = new Size(1258, 175);
		groupBox3.TabIndex = 12;
		groupBox3.TabStop = false;
		groupBox3.Text = "Filtered signal";
		// 
		// panel4
		// 
		panel4.Dock = DockStyle.Fill;
		panel4.Location = new Point(3, 19);
		panel4.Name = "panel4";
		panel4.Size = new Size(1252, 153);
		panel4.TabIndex = 0;
		// 
		// tbrMinBandFreq
		// 
		tbrMinBandFreq.AutoSize = false;
		tbrMinBandFreq.Location = new Point(1268, 65);
		tbrMinBandFreq.Minimum = 1;
		tbrMinBandFreq.Name = "tbrMinBandFreq";
		tbrMinBandFreq.Orientation = Orientation.Vertical;
		tbrMinBandFreq.Size = new Size(20, 463);
		tbrMinBandFreq.TabIndex = 13;
		tbrMinBandFreq.Value = 1;
		tbrMinBandFreq.ValueChanged += UpdateMinMaxFilter;
		// 
		// tbrMaxBandFreq
		// 
		tbrMaxBandFreq.AutoSize = false;
		tbrMaxBandFreq.Location = new Point(1329, 65);
		tbrMaxBandFreq.Minimum = 1;
		tbrMaxBandFreq.Name = "tbrMaxBandFreq";
		tbrMaxBandFreq.Orientation = Orientation.Vertical;
		tbrMaxBandFreq.Size = new Size(20, 463);
		tbrMaxBandFreq.TabIndex = 14;
		tbrMaxBandFreq.Value = 1;
		tbrMaxBandFreq.ValueChanged += UpdateMinMaxFilter;
		// 
		// label9
		// 
		label9.AutoSize = true;
		label9.Location = new Point(18, 841);
		label9.Name = "label9";
		label9.Size = new Size(117, 15);
		label9.TabIndex = 15;
		label9.Text = "Band filter start freq: ";
		// 
		// label10
		// 
		label10.AutoSize = true;
		label10.Location = new Point(18, 889);
		label10.Name = "label10";
		label10.Size = new Size(111, 15);
		label10.TabIndex = 16;
		label10.Text = "Band filter end freq:";
		// 
		// lblMaxBandFreq
		// 
		lblMaxBandFreq.AutoSize = true;
		lblMaxBandFreq.Location = new Point(1316, 35);
		lblMaxBandFreq.Name = "lblMaxBandFreq";
		lblMaxBandFreq.Size = new Size(33, 15);
		lblMaxBandFreq.TabIndex = 18;
		lblMaxBandFreq.Text = "60Hz";
		// 
		// trackBar2
		// 
		trackBar2.AutoSize = false;
		trackBar2.Location = new Point(1266, 534);
		trackBar2.Maximum = 6283;
		trackBar2.Name = "trackBar2";
		trackBar2.Size = new Size(83, 26);
		trackBar2.TabIndex = 19;
		trackBar2.Value = 1;
		trackBar2.Scroll += trackBar2_Scroll;
		// 
		// tbNoiseType
		// 
		tbNoiseType.AutoSize = true;
		tbNoiseType.Location = new Point(853, 12);
		tbNoiseType.Name = "tbNoiseType";
		tbNoiseType.Size = new Size(224, 19);
		tbNoiseType.TabIndex = 20;
		tbNoiseType.Text = "Random noise[ON] / Sine noise [OFF]";
		tbNoiseType.UseVisualStyleBackColor = true;
		// 
		// FFTSpectrum
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1364, 718);
		Controls.Add(tbNoiseType);
		Controls.Add(trackBar2);
		Controls.Add(tbrMinBandFreq);
		Controls.Add(lblMaxBandFreq);
		Controls.Add(tbrMaxBandFreq);
		Controls.Add(lblMinBandFreq);
		Controls.Add(label5);
		Controls.Add(label10);
		Controls.Add(label9);
		Controls.Add(groupBox3);
		Controls.Add(trackBar1);
		Controls.Add(cbEnableNoise);
		Controls.Add(cbPlotImag);
		Controls.Add(cbPlotReal);
		Controls.Add(groupBox2);
		Controls.Add(button1);
		Controls.Add(groupBox1);
		Controls.Add(lbCalcFreq);
		Controls.Add(label4);
		Controls.Add(lbActualFreq);
		Controls.Add(label1);
		Controls.Add(label8);
		Name = "FFTSpectrum";
		Text = "FFT demo";
		groupBox1.ResumeLayout(false);
		panel6.ResumeLayout(false);
		panel6.PerformLayout();
		groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
		groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)tbrMinBandFreq).EndInit();
		((System.ComponentModel.ISupportInitialize)tbrMaxBandFreq).EndInit();
		((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label label1;
	private Label lbActualFreq;
	private Label lbCalcFreq;
	private Label label4;
	private GroupBox groupBox1;
	private Label label3;
	private Label label2;
	private Panel panel1;
	private Button button1;
	private GroupBox groupBox2;
	private Label label6;
	private Label label7;
	private Panel panel2;
	private CheckBox cbPlotReal;
	private CheckBox cbPlotImag;
	private CheckBox cbEnableNoise;
	private Panel panel3;
	private TrackBar trackBar1;
	private Label label5;
	private Label label8;
	private GroupBox groupBox3;
	private Panel panel4;
	private TrackBar tbrMinBandFreq;
	private TrackBar tbrMaxBandFreq;
	private Label label9;
	private Label label10;
	private Label lblMinBandFreq;
	private Label lblMaxBandFreq;
	private Panel panel6;
	private TrackBar trackBar2;
	private CheckBox tbNoiseType;
}
