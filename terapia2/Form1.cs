using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace terapia2
{
    public partial class Form1 : Form
    {
        int x1, x2, x3, x4;
        int backcolor = 250;
        int randomval = 0;
        int volumevalue = 0;

        public Form1()
        {
            InitializeComponent();
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            comboBox1.Items.AddRange(devices.ToArray());
            x1 = 50;
            x2 = 50;
            x3 = 50;
            x4 = 50;
        }
        Random rnd = new Random();
        private NAudio.Wave.WaveFileReader wave = null;
        private NAudio.Wave.DirectSoundOut output = null;
        int firstcolor = 250;
        int secondcolor, thirdcolor = 0;

        int y1, y2, y3, y4;

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (30 < volumevalue && volumevalue < 35)
            {
                x2 = 200;
                if (x1 > 10) { x1 = x1 - 10; }
                if (x3 > 10) { x3 = x3 - 10; }
                if (x4 > 10) { x4 = x4 - 10; }
            }
            if (20 < volumevalue && volumevalue < 30)
            {
                x3 = 100;
                x4 = 100;
                if (x1 > 10) { x1 = x1 - 10; }
                if (x2 > 10) { x2 = x2 - 10; }
            }
            if (10 < volumevalue && volumevalue < 20)
            {
                x3 = 200;
                if (x1 > 10) { x1 = x1 - 10; }
                if (x2 > 10) { x2 = x2 - 10; }
                if (x4 > 10) { x4 = x4 - 10; }
            }
            if (50 < volumevalue && volumevalue < 55)
            {
                x1 = 200;
                if (x3 > 10) { x3 = x3 - 10; }
                if (x4 > 10) { x4 = x4 - 10; }
                if (x3 > 10) { x3 = x3 - 10; }
            }
            if (60 < volumevalue && volumevalue < 80)
            {
                if (backcolor < 250) { backcolor++; }
                else
                {
                    backcolor = 50;
                    randomval = rnd.Next(50, 200);
                    
                }
                this.BackColor = System.Drawing.Color.FromArgb(randomval, backcolor, 250 - backcolor);
            }
            else
            {
                if (x1 > 10) { x1 = x1 - 10; }
                if (x3 > 10) { x3 = x3 - 10; }
                if (x2 > 10) { x2 = x2 - 10; }
                if (x4 > 10) { x4 = x4 - 10; }

            }

            /*
            if (volumevalue < 30 && volumevalue > 1)
            {
                x1 = 200;
                if (x3 > 10) x1 = x1 - 10;
                if (x2 > 10) x3 = x3 - 10;
                if (x4 > 10) x4 = x4 - 10;
            }
            else
            {
                x4 = 200;
                if (x1 > 10) x1 = x1 - 10;
                if (x2 > 10) x3 = x3 - 10;
                if (x3 > 10) x4 = x4 - 10;
            }*/
            Invalidate();
        }

        int Height, Width = 100;
        private void button1_Click(object sender, EventArgs e)
        {
            #region INICJOWANIE DŹWIĘKU
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Wave File (*.wav)|*.wav;";
            if (open.ShowDialog() != DialogResult.OK) return;

            wave = new NAudio.Wave.WaveFileReader(open.FileName);
            output = new NAudio.Wave.DirectSoundOut();
            output.Init(new NAudio.Wave.WaveChannel32(wave));
            output.Play();
            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var device = (MMDevice)comboBox1.SelectedItem;
                button1.Enabled = true;
                volumevalue = (int)Math.Round(device.AudioMeterInformation.MasterPeakValue * 100);
                progressBar1.Value = (int)(Math.Round(device.AudioMeterInformation.MasterPeakValue * 100));
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Brown, 200, 300, x1, x1);
            e.Graphics.FillEllipse(Brushes.GreenYellow, 400, 300, x2, x2);
            e.Graphics.FillEllipse(Brushes.Yellow, 600, 300, x3, x3);
            e.Graphics.FillEllipse(Brushes.Blue, 800, 300, x4, x4);
            e.Graphics.FillEllipse(Brushes.Yellow, 600, 600, x3, x3);
            e.Graphics.FillEllipse(Brushes.Blue, 300, 600, x3, x3);
            e.Graphics.FillEllipse(Brushes.GreenYellow, 400, 800, x2, x2);
            e.Graphics.FillEllipse(Brushes.Brown, 700, 700, x1, x1);
        }
    }
}
