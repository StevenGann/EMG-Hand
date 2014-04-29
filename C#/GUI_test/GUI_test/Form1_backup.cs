using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using OpenTK.Graphics;
//using OpenTK.Graphics.OpenGL;

using System.Diagnostics;
using System.Net;

namespace GUI_test
{
	public partial class Form1 : Form
	{
		//TCP fields
		public IPAddress robotIP = IPAddress.Parse("192.168.1.1");
		public int robotPort = 1212;

		//Line Graph fields
		float t = 0.0f;
        Random rnd = new Random();
		public const int EMGLength = 575;
		//=====Line Graph Data=========================
        //EMG
        //float[] data_EMGthumb  = new float[EMGLength];
		//int nextWrite = 0;
		//Graphics bmg;
		//Bitmap bm;

		public Form1()
		{
			InitializeComponent();

			this.timer1.Enabled = true;
			this.timer1.Interval = 1;
			this.timer1.Tick += new EventHandler(timer1_Tick);
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void connectToRobotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string rawIP = Microsoft.VisualBasic.Interaction.InputBox("Input the robot's IP", "Connect to Robot", robotIP.ToString());
			robotIP = IPAddress.Parse(rawIP);
			robotPort = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Input the robot's Port", "Connect to Robot", robotPort.ToString()));
			MessageBox.Show("IP: " + robotIP.ToString() + ":" + robotPort.ToString());
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
            t += rnd.Next(100)/500.0f;
            t -= rnd.Next(100)/500.0f;
            if (t <= 0) { t = 0; }
            if (t >= 1) { t = 1; }
            AddValue(t, EMGLength);

			this.linegraph_EMGthumb.Invalidate();
		}

		/*void AddValue(float y, int Length)
		{

            data_EMGthumb[nextWrite] = -y;
			nextWrite = (nextWrite + 1) % Length;
		}*/

		private void linegraph_EMGthumb_Paint(object sender, PaintEventArgs e)
		{
			/*if (bm == null)
			{
				bm = new Bitmap(EMGLength, 501);
				bmg = Graphics.FromImage(bm);
				bmg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				bmg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;
				bmg.Clear(Color.White);
			}
			Render();
			e.Graphics.DrawImage(bm, 0, 0);*/
		}

		/*void Render()
		{
			bmg.Clear(Color.White);
			float y0 = 0;
			int x0 = 0;
			for (int i = 0; i < EMGLength; i++)
			{
				float y = 60.0f + 60.0f * data_EMGthumb[(nextWrite + i) % EMGLength];
				int x = i;
				if (i != 0)
				{
					// draw a line
					bmg.DrawLine(Pens.Blue, x0, y0, x, y);
				}
				y0 = y;
				x0 = x;
			}
		}*/

        private void button1_Click(object sender, EventArgs e)
        {

        }
	}

    class Linegraph
    {
        public const int length = 575;
        public float[] data = new float[length];
        public int nextWrite = 0;
        public Graphics bmg;
        public Bitmap bm;
        public 

        public void AddValue(float y, int Length)
        {

            data[nextWrite] = -y;
            nextWrite = (nextWrite + 1) % Length;
        }

        public void paint(PaintEventArgs e)
        {
            if (bm == null)
			{
				bm = new Bitmap(length, 501);
				bmg = Graphics.FromImage(bm);
				bmg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				bmg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;
				bmg.Clear(Color.White);
			}
			Render();
			e.Graphics.DrawImage(bm, 0, 0);
        }

        public void Render()
        {
            bmg.Clear(Color.White);
            float y0 = 0;
            int x0 = 0;
            for (int i = 0; i < length; i++)
            {
                float y = 60.0f + 60.0f * data[(nextWrite + i) % length];
                int x = i;
                if (i != 0)
                {
                    // draw a line
                    bmg.DrawLine(Pens.Blue, x0, y0, x, y);
                }
                y0 = y;
                x0 = x;
            }
        }


    }
}
