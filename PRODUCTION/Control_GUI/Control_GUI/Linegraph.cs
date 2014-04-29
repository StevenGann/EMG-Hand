using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Control_GUI
{
	class Linegraph
	{
		public const int length = 575;
		public float[] data = new float[length];
		public int nextWrite = 0;
		public Graphics bmg;
		public Bitmap bm;
		public PictureBox picturebox;

		public Linegraph(PictureBox pic)
		{
            //This constructor doesn't work. Why?
			picturebox = pic;
		}

		public Linegraph()
		{
		}

		public void AddValue(float y)
		{
			if (y <= 0) { y = 0; }
			if (y >= 10) { y = 10; }
			data[nextWrite] = -y;
			nextWrite = (nextWrite + 1) % length;
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

		public void refresh()
		{
			picturebox.Invalidate();
		}
	}
}
