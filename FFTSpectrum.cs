using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Windows.Automation.Peers;
using System.Windows.Documents;
using Color = System.Drawing.Color;
using System.Diagnostics;

namespace CosPhiCalcTest;
public partial class FFTSpectrum : Form
{
	//X-axis Points:
	//(16, 50)
	//(33, 100)
	//(163, 500)
	//(326, 1000)
	//(652, 2000)
	//(668, 2048)
	//formula: 3.066746120799211x - 0.002382074155859

	private (float freq, float phase)[] noiseFreqs;
	private float mainFreq = 50f;
	private int sampleLength;
	private float noiseStrength = MathF.PI / 10;
	Random random = new Random();
	private float xAxisSine;
	private float xAxisSpectrum;
	private float xAxisFFT2;
	private float xAxisFFT3;
	private float xAxisInverse;
	private float phase;
	private float avg;

	Graphics gSine;
	Graphics gFFT;
	Graphics gInverseFFT;
	Graphics gFFTXaxis;
	Graphics gFFTXaxis3;

	float maxFreq;

	public FFTSpectrum()
	{
		InitializeComponent();
		avg = panel1.Height / 2f;
		sampleLength = panel2.Width;
		gSine = panel2.CreateGraphics();
		gSine.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		gFFT = panel1.CreateGraphics();
		gFFT.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		gInverseFFT = panel4.CreateGraphics();
		gInverseFFT.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		gFFTXaxis = panel3.CreateGraphics();
		gFFTXaxis.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		gFFTXaxis3 = panel6.CreateGraphics();
		gFFTXaxis3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		maxFreq = XValToFreq(panel1.Width);
		label3.Text = $"{maxFreq}Hz";
		trackBar1.Maximum = (int)maxFreq;
		trackBar1.Value = (int)mainFreq;
		tbrMaxBandFreq.Maximum = (int)maxFreq;
		tbrMaxBandFreq.Value = (int)mainFreq + 20;
		tbrMinBandFreq.Maximum = (int)tbrMaxBandFreq.Value;
		tbrMinBandFreq.Value = (int)mainFreq - 20;
		tbrMaxBandFreq.Minimum = (int)tbrMinBandFreq.Value;
		tbrMinBandFreq.Minimum = 0;

		phase = 0;

		xAxisSine = panel2.Height / 2f;
		xAxisSpectrum = avg;
		xAxisFFT2 = panel3.Height / 2f;
		xAxisFFT3 = panel6.Height / 2f;
		xAxisInverse = panel4.Height / 2;
	}

	private void Randomize(object sender, EventArgs e)
	{
		noiseFreqs = new (float freq, float phase)[]
		{
			(random.NextSingle() * XValToFreq(panel1.Width), 2f*MathF.PI*random.NextSingle()),
			(random.NextSingle() * XValToFreq(panel1.Width), 2f*MathF.PI*random.NextSingle()),
			(random.NextSingle() * XValToFreq(panel1.Width), 2f*MathF.PI*random.NextSingle()),
			(random.NextSingle() * XValToFreq(panel1.Width), 2f*MathF.PI*random.NextSingle())
		};
		lbActualFreq.Text = $"Main: {mainFreq}Hz + [{string.Join("Hz, ", noiseFreqs)}] noise";

		PointF[] noisySine = tbNoiseType.Checked ? ComputeGaussianNoiseSine(mainFreq, cbEnableNoise.Checked) : ComputeNoiseSine(mainFreq, (cbEnableNoise.Checked ? noiseFreqs : new (float freq, float phase)[0]));

		//lbActualFreq.Text = $"Main: {mainFreq}Hz + [80Hz, 5Hz, 1000Hz, 123456Hz] noise";
		//PointF[] noisySine = ComputeNoiseSine(mainFreq, (80, 5), (5, 0), (1000, 0), (123456, 4));

		gSine.Clear(panel2.BackColor);
		gSine.DrawLine(new(Color.Gray), new PointF(0, xAxisSine), new PointF(panel2.Width, xAxisSine));
		Complex[] sineComplex = new Complex[noisySine.Length];
		for (int i = 0; i < noisySine.Length; i++)
		{
			sineComplex[i] = new Complex(noisySine[i].Y, 0);
			noisySine[i].Y += xAxisSine;
		}
		gSine.DrawCurve(new(Color.Blue), noisySine);

		float maxFreq = tbrMaxBandFreq.Value;
		float minFreq = tbrMinBandFreq.Value;
		double fftComputeTime;
		Stopwatch sw = Stopwatch.StartNew();
		Complex[] spectrum = ComputeFFT(sineComplex);
		Complex[] clampedSpectrum = ClampSpectrum(spectrum, minFreq, maxFreq);
		Complex[] filteredSignal = ComputeInverseFFT(clampedSpectrum); //clamp = apply band pass
		fftComputeTime = sw.ElapsedTicks;
		sw.Stop();
		fftComputeTime /= 10;
		label8.Text = $"Filtered signal computed in: {fftComputeTime / 1000:0.000}ms (or {fftComputeTime:0.000}µs)";
		PointF[] amplitudeValForward = new PointF[spectrum.Length];
		float maxValForward = (float)spectrum.MaxBy(c => c.Magnitude).Magnitude;
		float scalingFactorForward = (panel1.Height - panel1.Height / 15f) / maxValForward;

		PointF[] amplitudeValClamped = new PointF[spectrum.Length];

		PointF[] amplitudeValInverse = new PointF[filteredSignal.Length];
		float maxValInverse = (float)filteredSignal.MaxBy(c => c.Magnitude).Magnitude;
		float scalingFactorInverse = maxValInverse == 0 ? 0 : (panel4.Height - panel4.Height / 15f) / maxValInverse;
		List<PointF> spectrumPoints = new();

		for (int i = 0; i < Max(spectrum.Length, clampedSpectrum.Length, filteredSignal.Length); i++)
		{
			if (i < spectrum.Length)
			{
				//float dBVal = 20 * MathF.Log10((float)fft[i].Magnitude);
				float val = (float)(spectrum[i].Magnitude);
				amplitudeValForward[i] = new PointF(i, -val * scalingFactorForward + panel1.Height - 1 - panel1.Height / 15f);
				 spectrumPoints.Add(new(i, val));
			}


			if (i < clampedSpectrum.Length)
			{
				//float dBVal = 20 * MathF.Log10((float)fft[i].Magnitude);
				float val = (float)clampedSpectrum[i].Magnitude;
				amplitudeValClamped[i] = new PointF(i, -val * scalingFactorForward + panel1.Height - 1 - panel1.Height / 15f);
			}


			if (i < filteredSignal.Length)
			{
				float val = (float)filteredSignal[i].Real;
				amplitudeValInverse[i] = new PointF(i, val * scalingFactorInverse / 2 + xAxisInverse);
			}
		}

		List<float> detectedFreqs = DetectFrequencies(spectrumPoints, out avg);
		//float transformedAvg = avg / spectrum.MaxBy(p => p.);

		lbCalcFreq.Text = string.Join("Hz ", detectedFreqs);
		
		gFFT.Clear(panel1.BackColor);
		gFFT.DrawLine(new(Color.Gray), 0, avg, panel1.Width, avg);
		gFFT.DrawCurve(new(Color.Blue), amplitudeValForward);
		gFFT.DrawCurve(new(Color.Red), amplitudeValClamped);

		gFFT.FillRectangle(new SolidBrush(Color.FromArgb(28, 255, 0, 0)), new(0, 0, (int)FreqToXVal(minFreq), panel1.Height));
		gFFT.FillRectangle(new SolidBrush(Color.FromArgb(28, 255, 0, 0)), new((int)FreqToXVal(maxFreq), 0, panel1.Width, panel1.Height));
		string intervalString = $"[{minFreq}, {maxFreq}]Hz";
		gFFTXaxis3.Clear(panel6.BackColor);
		float strLenCompensation = intervalString.Length * 3f;
		gFFTXaxis3.DrawString(intervalString, new("Arial", 10f), new SolidBrush(Color.Black), Clamp(FreqToXVal((minFreq + maxFreq) / 2) - strLenCompensation, 0, panel6.Width - strLenCompensation * 2.25f), xAxisFFT3);

		gInverseFFT.Clear(panel4.BackColor);
		gInverseFFT.DrawLine(new(Color.Gray), 0, xAxisInverse, panel4.Width, xAxisInverse);
		gInverseFFT.DrawCurve(new(Color.Blue), amplitudeValInverse);

		float mainFreqXVal = FreqToXVal(mainFreq);
		gFFTXaxis.Clear(panel3.BackColor);
		float strLenCompensation2 = mainFreq.ToString().Length * 6f;
		gFFTXaxis.DrawString($"{mainFreq}Hz", new("Arial", 12f), new SolidBrush(Color.Black), Clamp(mainFreqXVal - strLenCompensation2, 0, panel3.Width - strLenCompensation2 * 2.5f), xAxisFFT2);
	}

	private float Max(params float[] values) => values.Max();

	private float Clamp(float value, float min, float max) => value > max ? max : (value < min ? min : value);

	private Complex[] ClampSpectrum(Complex[] spectrum, double minFreq, double maxFreq)
	{
		Complex[] result = new Complex[spectrum.Length];
		for (int i = 0; i < spectrum.Length; i++)
		{
			Complex currentVal = spectrum[i];
			float freq = XValToFreq(i);
			result[i] = (freq >= minFreq && freq <= maxFreq) ? currentVal : Complex.Zero;
		}
		return result;
	}

	private float XValToFreq(float xVal) => 3.066746120799211f * xVal - 0.002382074155859f;
	private float FreqToXVal(float freq) => (freq + 0.002382074155859f) / 3.066746120799211f;

	private PointF[] ComputeGaussianNoiseSine(float mainFreq, bool enableNoise)
	{
		int paddedLength = NextPowerOfTwo(sampleLength);
		PointF[] sine = new PointF[paddedLength];
		for (int i = 0; i < sampleLength; i++)
		{
			float val = MathF.Sin((mainFreq / 1000f) * i + phase);
			if (enableNoise) val += noiseStrength * random.NextSingle();
			float max = sine.MaxBy(p => p.Y).Y;
			//sine[i] = new(i, (val / (noiseStrength + 2) * panel2.Height)); //center in panel
			sine[i] = new(i, (val / (noiseStrength + 2f)) * panel2.Height); //center in panel
		}

		for (int i = sampleLength; i < paddedLength; i++) sine[i] = new PointF(i, 0);

		return sine;
	}

	private PointF[] ComputeNoiseSine(float mainFreq, params (float freq, float phase)[] noiseFreqs)
	{
		int originalLength = sampleLength;
		int paddedLength = NextPowerOfTwo(originalLength);
		PointF[] sine = new PointF[paddedLength];
		for (int i = 0; i < originalLength; i++)
		{
			float val = MathF.Sin((mainFreq / 1000f) * i + phase);
			for (int j = 0; j < noiseFreqs.Length; j++) val += noiseStrength * MathF.Sin((noiseFreqs[j].freq / 1000) * i + noiseFreqs[j].phase);
			sine[i] = new(i, (val / (noiseFreqs.Length * noiseStrength + 2f)) * panel2.Height); //center in panel
		}

		for (int i = originalLength; i < paddedLength; i++) sine[i] = new PointF(i, 0);

		return sine;
	}

	//public List<float> DetectFrequenties(List<PointF> spectrum, out float avg)
	//{
	//	if (spectrum.Count == 0) 
	//	{
	//		avg = panel1.Height / 2f;
	//		return new();
	//	}
	//	avg = spectrum.Average(p => p.Y);
	//	List<List<PointF>> peaks = new();
	//	for (int i = 0, p = 0, j = 0; i < spectrum.Count; i++)
	//	{
	//		PointF currentPoint = spectrum[i];
	//		for (; currentPoint.Y > avg; j++)
	//		{
	//			int index = i + j;
	//			if (index >= spectrum.Count) break;
	//			currentPoint = spectrum[index];
	//			if (peaks.Count - 1 < p)
	//			{
	//				peaks.Add(new List<PointF>());
	//				p = peaks.Count - 1;
	//			}
	//			peaks[p].Add(currentPoint);
	//		}
	//		i += j;
	//		j = 0;
	//	}

	//	List<float> frequencies = new();

	//	for (int i = 0; i < peaks.Count; i++) frequencies.Add(XValToFreq(peaks[i].MaxBy(p => p.Y).X));
	//	return frequencies;
	//}

	public List<float> DetectFrequencies(List<PointF> spectrum, out float avg)
	{
		if (spectrum.Count == 0)
		{
			avg = panel1.Height / 2f;
			return new List<float>();
		}

		avg = spectrum.Average(p => p.Y);
		List<List<PointF>> peaks = new List<List<PointF>>();
		int i = 0;
		while (i < spectrum.Count)
		{
			if (spectrum[i].Y > avg)
			{
				List<PointF> currentPeak = new List<PointF>();
				while (i < spectrum.Count && spectrum[i].Y > avg)
				{
					currentPeak.Add(spectrum[i]);
					i++;
				}
				peaks.Add(currentPeak);
			}
			else
			{
				i++;
			}
		}

		List<float> frequencies = new List<float>();
		foreach (List<PointF> peak in peaks)
		{
			frequencies.Add(XValToFreq(peak.MaxBy(p => p.Y).X));
		}
		return frequencies;
	}

	private int NextPowerOfTwo(int value)
	{
		int result = 1;
		while (result < value)
		{
			result *= 2;
		}
		return result;
	}

	private Complex[] ComputeFFT(Complex[] signal) //intg(f(x)e^(-2PIifx), 0, array.Length)
	{
		int signalLength = signal.Length;
		if (signalLength <= 1) return signal;

		Complex[] even = ComputeFFT(signal.Where((e, i) => i % 2 == 0).ToArray());
		Complex[] odd = ComputeFFT(signal.Where((e, i) => i % 2 == 1).ToArray());
		Complex[] t = new Complex[signalLength / 2];
		Complex[] result = new Complex[signalLength];
		for (int k = 0; k < signalLength / 2; k++)
		{
			double angle = -2 * Math.PI * k / signalLength;
			Complex wk = new Complex(Math.Cos(angle), Math.Sin(angle));
			t[k] = wk * odd[k];

			result[k] = even[k] + t[k];
			result[k + signalLength / 2] = even[k] - t[k];

		}

		return result;
	}

	private Complex[] ComputeInverseFFT(Complex[] spectrum)
	{
		int signalLength = spectrum.Length;
		if (signalLength <= 1) return spectrum;

		Complex[] even = ComputeInverseFFT(spectrum.Where((e, i) => i % 2 == 0).ToArray());
		Complex[] odd = ComputeInverseFFT(spectrum.Where((e, i) => i % 2 == 1).ToArray());
		Complex[] t = new Complex[signalLength / 2];
		Complex[] result = new Complex[signalLength];
		for (int k = 0; k < signalLength / 2; k++)
		{
			double angle = 2 * Math.PI * k / signalLength;
			Complex wk = new Complex(Math.Cos(angle), Math.Sin(angle));
			t[k] = wk * odd[k];

			result[k] = (even[k] + t[k]) / signalLength;
			result[k + signalLength / 2] = (even[k] - t[k]) / signalLength;
		}

		return result;
	}


	private Complex SwapComplex(Complex value) => new(value.Imaginary, value.Real);

	private void trackBar1_Scroll(object sender, EventArgs e)
	{
		mainFreq = trackBar1.Value;
		label5.Text = $"Main freq: {mainFreq}Hz";
		label5.Update();
		Randomize(null, null);
	}

	private void UpdateMinMaxFilter(object sender, EventArgs e)
	{
		tbrMaxBandFreq.Maximum = (int)maxFreq;
		tbrMinBandFreq.Maximum = (int)tbrMaxBandFreq.Value;
		tbrMaxBandFreq.Minimum = (int)tbrMinBandFreq.Value;
		tbrMinBandFreq.Minimum = 0;

		tbrMaxBandFreq.Update();
		tbrMinBandFreq.Update();

		lblMaxBandFreq.Text = $"{tbrMaxBandFreq.Value}Hz";
		lblMaxBandFreq.Update();

		lblMinBandFreq.Text = $"{tbrMinBandFreq.Value}Hz";
		lblMinBandFreq.Update();

		Randomize(null, null);
	}

	private void trackBar2_Scroll(object sender, EventArgs e)
	{
		phase = trackBar2.Value / 1000f;
		Randomize(null, null);
	}
}

