﻿using System;
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
using System.Net.Sockets;
using System.IO;

namespace GUI_test
{
	public partial class Form1 : Form
	{
		//TCP fields
		//public IPAddress robotIP = IPAddress.Parse("192.168.1.1");
		public string robotIP = "192.168.0.1";
		public int robotPort = 1212;

		//Line Graph fields
		Random rnd = new Random();
		float a = 0.0f;
		//Line Graph objects
		//EMG
		Linegraph EMG_1 = new Linegraph();
		Linegraph EMG_2 = new Linegraph();
		Linegraph EMG_3 = new Linegraph();
		Linegraph EMG_4 = new Linegraph();
		//Finger pressure sensors
		Linegraph Press_1 = new Linegraph();
		Linegraph Press_2 = new Linegraph();
		Linegraph Press_3 = new Linegraph();
		Linegraph Press_4 = new Linegraph();
		Linegraph Press_5 = new Linegraph();
		//Palm pressure sensors
		Linegraph PressP_1 = new Linegraph();
		Linegraph PressP_2 = new Linegraph();
		Linegraph PressP_3 = new Linegraph();
		Linegraph PressP_4 = new Linegraph();
		Linegraph PressP_5 = new Linegraph();
		//DC Motors and Chassis
		Linegraph Motor_1 = new Linegraph();
		Linegraph Motor_2 = new Linegraph();
		Linegraph Motor_3 = new Linegraph();
		Linegraph Motor_4 = new Linegraph();
		Linegraph Motor_5 = new Linegraph();
		//Diagnostic page
		Linegraph Diag_1 = new Linegraph();
		Linegraph Diag_2 = new Linegraph();
		Linegraph Diag_3 = new Linegraph();


		//Network Code
		TcpClient tcpclnt = new TcpClient();
		bool TCPready = false;
		//Serial Comm
		String COMRx;

        int servoOut1 = 90;
        int servoOut2 = 90;
        int servoOut3 = 90;
        int servoOut4 = 90;
        int servoOut15 = 90;

		public Form1()
		{
			InitializeComponent();

			this.timer1.Enabled = true;
			this.timer1.Interval = 1;
			this.timer1.Tick += new EventHandler(timer1_Tick);

			EMG_1.picturebox = linegraph_EMGthumb;
			EMG_2.picturebox = linegraph_EMGindex;
			EMG_3.picturebox = linegraph_EMGmiddle;
			EMG_4.picturebox = linegraph_EMGring;

			Press_1.picturebox = linegraph_indexPressure;
			Press_2.picturebox = linegraph_middlePressure;
			Press_3.picturebox = linegraph_ringPressure;
			Press_4.picturebox = linegraph_pinkiePressure;
			Press_5.picturebox = linegraph_thumbPressure;

			PressP_1.picturebox = linegraph_indexPPressure;
			PressP_2.picturebox = linegraph_middlePPressure;
			PressP_3.picturebox = linegraph_ringPPressure;
			PressP_4.picturebox = linegraph_pinkiePPressure;
			PressP_5.picturebox = linegraph_thumbPPressure;

			Motor_1.picturebox = linegraph_leftCurrent;
			Motor_2.picturebox = linegraph_rightCurrent;
			Motor_3.picturebox = linegraph_shoulderCurrent;
			Motor_4.picturebox = linegraph_elbowCurrent;
			Motor_5.picturebox = linegraph_totalCurrent;

			Diag_1.picturebox = linegraph_battery;
			Diag_2.picturebox = linegraph_chassisTemp;
			Diag_3.picturebox = linegraph_servoTemp;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Application.EnableVisualStyles();
		}

		private void connectToRobotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string rawIP = Microsoft.VisualBasic.Interaction.InputBox("Input the robot's IP", "Connect to Robot", robotIP.ToString());
			robotIP = rawIP;//IPAddress.Parse(rawIP);
			robotPort = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Input the robot's Port", "Connect to Robot", robotPort.ToString()));
			MessageBox.Show("IP: " + robotIP.ToString() + ":" + robotPort.ToString());
			
			try
			{
				tcpclnt.Connect(robotIP, robotPort);

				MessageBox.Show("Connected to " + robotIP + ":" + Convert.ToString(robotIP));
				TCPready = true;
			}
			catch (Exception o)
			{
				MessageBox.Show("ERROR: Could not connect. \n STACK TRACE: " + o.StackTrace);
				TCPready = false;
			}
			
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (a < 0) { a = 0; }
			if (a > 1) { a = 1; }

			a += rnd.Next(100) / 1000.0f;
			a -= rnd.Next(100) / 1000.0f;

			string TCPoutput = "165,49,12,255," + Convert.ToString((int)(a*255));
			string TCPinput = "";

			if (TCPready == true)
			{
				//Send data
				Stream stm = tcpclnt.GetStream();
				ASCIIEncoding asen = new ASCIIEncoding();
				byte[] ba = asen.GetBytes(TCPoutput);
				stm.Write(ba, 0, ba.Length);

				byte[] bb = new byte[100];
				int k = stm.Read(bb, 0, 100);

				//TCPinput = System.Text.Encoding.UTF8.GetString(bb);
				TCPinput = System.Text.Encoding.ASCII.GetString(bb);

			}

			//EMG_1.AddValue(a);
			//EMG_2.AddValue(a);
			//EMG_3.AddValue(a);
			//EMG_4.AddValue(a);
			Press_1.AddValue(a);
			Press_2.AddValue(a);
			Press_3.AddValue(a);
			Press_4.AddValue(a);
			Press_5.AddValue(a);
			PressP_1.AddValue(a);
			PressP_2.AddValue(a);
			PressP_3.AddValue(a);
			PressP_4.AddValue(a);
			PressP_5.AddValue(a);
			Motor_1.AddValue(a);
			Motor_2.AddValue(a);
			Motor_3.AddValue(a);
			Motor_4.AddValue(a);
			Motor_5.AddValue(a);
			Diag_1.AddValue(a);
			Diag_2.AddValue(a);
			Diag_3.AddValue(a);

			//EMG_1.refresh();
			//EMG_2.refresh();
			//EMG_3.refresh();
			//EMG_4.refresh();
			
			
			
			
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			Press_1.refresh();
			Press_2.refresh();
			Press_3.refresh();
			Press_4.refresh();
			Press_5.refresh();
		}

		private void timer3_Tick(object sender, EventArgs e)
		{
			PressP_1.refresh();
			PressP_2.refresh();
			PressP_3.refresh();
			PressP_4.refresh();
			PressP_5.refresh();
		}

		private void timer4_Tick(object sender, EventArgs e)
		{
			Motor_1.refresh();
			Motor_2.refresh();
			Motor_3.refresh();
			Motor_4.refresh();
			Motor_5.refresh();
		}

		private void timer5_Tick(object sender, EventArgs e)
		{
			Diag_1.refresh();
			Diag_2.refresh();
			Diag_3.refresh();
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			timer2.Enabled = false;
			timer3.Enabled = false;
			timer4.Enabled = false;
			timer5.Enabled = false;

			if (tabControl1.SelectedIndex == 1)
			{
				timer2.Enabled = true;
				timer3.Enabled = false;
				timer4.Enabled = false;
				timer5.Enabled = false;
			}

			if (tabControl1.SelectedIndex == 2)
			{
				timer2.Enabled = false;
				timer3.Enabled = true;
				timer4.Enabled = false;
				timer5.Enabled = false;
			}

			if (tabControl1.SelectedIndex == 3)
			{
				timer2.Enabled = false;
				timer3.Enabled = false;
				timer4.Enabled = true;
				timer5.Enabled = false;
			}

			if (tabControl1.SelectedIndex == 4)
			{
				timer2.Enabled = false;
				timer3.Enabled = false;
				timer4.Enabled = false;
				timer5.Enabled = true;
			}
		}

		private void linegraph_EMGthumb_Paint(object sender, PaintEventArgs e)
		{
			EMG_1.paint(e);
		}
		private void linegraph_EMGindex_Paint(object sender, PaintEventArgs e)
		{
			EMG_2.paint(e);
		}
		private void linegraph_EMGmiddle_Paint(object sender, PaintEventArgs e)
		{
			EMG_3.paint(e);
		}
		private void linegraph_EMGring_Paint(object sender, PaintEventArgs e)
		{
			EMG_4.paint(e);
		}
		private void linegraph_indexPressure_Paint(object sender, PaintEventArgs e)
		{
			Press_1.paint(e);
		}
		private void linegraph_middlePressure_Paint(object sender, PaintEventArgs e)
		{
			Press_2.paint(e);
		}
		private void linegraph_ringPressure_Paint(object sender, PaintEventArgs e)
		{
			Press_3.paint(e);
		}
		private void linegraph_pinkiePressure_Paint(object sender, PaintEventArgs e)
		{
			Press_4.paint(e);
		}
		private void linegraph_thumbPressure_Paint(object sender, PaintEventArgs e)
		{
			Press_5.paint(e);
		}
		private void linegraph_indexPPressure_Paint(object sender, PaintEventArgs e)
		{
			PressP_1.paint(e);
		}
		private void linegraph_middlePPressure_Paint(object sender, PaintEventArgs e)
		{
			PressP_2.paint(e);
		}
		private void linegraph_ringPPressure_Paint(object sender, PaintEventArgs e)
		{
			PressP_3.paint(e);
		}
		private void linegraph_pinkiePPressure_Paint(object sender, PaintEventArgs e)
		{
			PressP_4.paint(e);
		}
		private void linegraph_thumbPPressure_Paint(object sender, PaintEventArgs e)
		{
			PressP_5.paint(e);
		}
		private void linegraph_leftCurrent_Paint(object sender, PaintEventArgs e)
		{
			Motor_1.paint(e);
		}
		private void linegraph_rightCurrent_Paint(object sender, PaintEventArgs e)
		{
			Motor_2.paint(e);
		}
		private void linegraph_shoulderCurrent_Paint(object sender, PaintEventArgs e)
		{
			Motor_3.paint(e);
		}
		private void linegraph_elbowCurrent_Paint(object sender, PaintEventArgs e)
		{
			Motor_4.paint(e);
		}
		private void linegraph_totalCurrent_Paint(object sender, PaintEventArgs e)
		{
			Motor_5.paint(e);
		}
		private void linegraph_battery_Paint(object sender, PaintEventArgs e)
		{
			Diag_1.paint(e);
		}
		private void linegraph_chassisTemp_Paint(object sender, PaintEventArgs e)
		{
			Diag_2.paint(e);
		}
		private void linegraph_servoTemp_Paint(object sender, PaintEventArgs e)
		{
			Diag_3.paint(e);
		}

		private void connectEMG_butt_Click(object sender, EventArgs e)
		{
			connectEMG_butt.Enabled = false;

			//COMPortStatusLight.Value = 3;

			if (serialPort1.IsOpen)
			{
				timer1.Enabled = false;

				serialPort1.Close();

				//COMPortStatusLight.Value = -1;
				connectEMG_butt.Text = "Connect";
			}
			else
			{
				serialPort1.PortName = "COM14";//COMPortComboBox.Text;
				serialPort1.BaudRate = 115200;//int.Parse(COMBaudComboBox.Text);

				try
				{
					serialPort1.Open();
				}
				catch
				{
					//COMPortStatusLight.Value = -1;
					MessageBox.Show("Could not open port"); //+ COMPortComboBox.Text);
				}

				if (serialPort1.IsOpen)
				{
					//COMPortStatusLight.Value = 1;
					connectEMG_butt.Text = "Disconnect";

					//Data.Clear();
					//deleteCurves();
					//initCurves();

					timer1.Enabled = true;
				}
			}

			connectEMG_butt.Enabled = true;
		}

		private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			int BufferLength = serialPort1.BytesToRead;
			//StringBuilder tempS = new StringBuilder();

			while (BufferLength > 0 && serialPort1.IsOpen)
			{
				try
				{
					COMRx = serialPort1.ReadLine();
					
					this.BeginInvoke(new EventHandler(ProcessCOMRx));

					if (serialPort1.IsOpen)
						BufferLength = serialPort1.BytesToRead;
				}
				catch
				{
					this.BeginInvoke(new EventHandler(ReadLineError));
				}
			}
		}

		private void ReadLineError(object sender, EventArgs e)
		{
			MessageBox.Show("Read Line Error"); 
		}

		private void ProcessCOMRx(object sender, EventArgs e)
		{
			//MessageBox.Show("In ProcessCOMRx"); 
			if (!string.IsNullOrEmpty(COMRx))
			{
				string[] parsed = COMRx.Split(',');
				TempDebug.Text = COMRx;




				EMG_1.AddValue(Convert.ToSingle(parsed[0]) / 200.0f);
				EMG_2.AddValue(Convert.ToSingle(parsed[1]) / 200.0f);
				EMG_3.AddValue(Convert.ToSingle(parsed[2]) / 200.0f);
				EMG_4.AddValue(Convert.ToSingle(parsed[3]) / 200.0f);

				EMG_1.refresh();
				EMG_2.refresh();
				EMG_3.refresh();
				EMG_4.refresh();

				COMRx = "";
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void trackBar3_Scroll(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		

		private void button1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("KILL BUTTON PRESSED!\nKilling Robot!");
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				button1_Click(this, e); //Doesn't work. Why?
			}
		}

		

		

		

		

		


		
	}
}
