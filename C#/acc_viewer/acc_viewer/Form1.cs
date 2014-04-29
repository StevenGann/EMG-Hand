using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace acc_viewer
{
    public partial class Form1 : Form
    {
        private Stopwatch stopwatch = new Stopwatch();
        private long basetime;
        private long time ;
        private bool serialIsReady = false;
        private string serialRaw = "0,0,0";
        private char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
        private int accX = 0;
        private int accY = 0;
        private int accZ = 0;
        private int[] accX_buffer = { 0, 0, 0, 0, 0, 0 };
        private int[] accY_buffer = { 0, 0, 0, 0, 0, 0 };
        private int[] accZ_buffer = { 0, 0, 0, 0, 0, 0 };
        private int avgX = 0;
        private int avgY = 0;
        private int avgZ = 0;
        private float dY = 0;
        private float dZ = 0;
        private float dX = 0;
        private float vY = 0;
        private float vZ = 0;
        private float vX = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            stopwatch.Start();
            
            
        }

        public float calcDisp(float a, long t)
        {
            //x = ½at^2

            float r = (0.5f) * a * (float)(Math.Pow(t, 2));

            return r;
        }
        public float calcDisp(int a, long t)
        {
            return calcDisp((float)a, t);
        }

        public void filter()
        {
            if (avgX == 0)
            {
                accX_buffer[5] = accX;
                accX_buffer[4] = accX;
                accX_buffer[3] = accX;
                accX_buffer[2] = accX;
                accX_buffer[1] = accX;
                accX_buffer[0] = accX;
            }

            if (avgY == 0)
            {
                accY_buffer[5] = accY;
                accY_buffer[4] = accY;
                accY_buffer[3] = accY;
                accY_buffer[2] = accY;
                accY_buffer[1] = accY;
                accY_buffer[0] = accY;
            }

            if (avgZ == 0)
            {
                accZ_buffer[5] = accZ;
                accZ_buffer[4] = accZ;
                accZ_buffer[3] = accZ;
                accZ_buffer[2] = accZ;
                accZ_buffer[1] = accZ;
                accZ_buffer[0] = accZ;
            }



            accX_buffer[5] = accX_buffer[4];
            accX_buffer[4] = accX_buffer[3];
            accX_buffer[3] = accX_buffer[2];
            accX_buffer[2] = accX_buffer[1];
            accX_buffer[1] = accX_buffer[0];

            if (Math.Abs(accX_buffer[0] - accX) > 500)
            {
                accX_buffer[0] = accX;
            }

            accY_buffer[5] = accY_buffer[4];
            accY_buffer[4] = accY_buffer[3];
            accY_buffer[3] = accY_buffer[2];
            accY_buffer[2] = accY_buffer[1];
            accY_buffer[1] = accY_buffer[0];

            if (Math.Abs(accY_buffer[0] - accY) > 500)
            {
                accY_buffer[0] = accY;
            }


            accZ_buffer[5] = accZ_buffer[4];
            accZ_buffer[4] = accZ_buffer[3];
            accZ_buffer[3] = accZ_buffer[2];
            accZ_buffer[2] = accZ_buffer[1];
            accZ_buffer[1] = accZ_buffer[0];

            if (Math.Abs(accZ_buffer[0] - accZ) > 500)
            {
                accZ_buffer[0] = accZ;
            }
            
            
            
            avgX = (int)((float)(accX_buffer[0] + accX_buffer[1] + accX_buffer[2] + accX_buffer[3] + accX_buffer[4] + accX_buffer[5]) / 6.0f);
            avgY = (int)((float)(accY_buffer[0] + accY_buffer[1] + accY_buffer[2] + accY_buffer[3] + accY_buffer[4] + accY_buffer[5]) / 6.0f);
            avgZ = (int)((float)(accZ_buffer[0] + accZ_buffer[1] + accZ_buffer[2] + accZ_buffer[3] + accZ_buffer[4] + accZ_buffer[5]) / 6.0f);

            vX += ((float)(accX_buffer[0] + accX_buffer[1] + accX_buffer[2] + accX_buffer[3] + accX_buffer[4] + accX_buffer[5]) / (6.0f * time));
            vY += ((float)(accY_buffer[0] + accY_buffer[1] + accY_buffer[2] + accY_buffer[3] + accY_buffer[4] + accY_buffer[5]) / (6.0f * time));
            vZ += ((float)(accZ_buffer[0] + accZ_buffer[1] + accZ_buffer[2] + accZ_buffer[3] + accZ_buffer[4] + accZ_buffer[5]) / (6.0f * time));

            dX += vX * (float)time;
            dY += vY * (float)time;
            dZ += vZ * (float)time;
        }

        private void serialTimer_Tick(object sender, EventArgs e)
        {
            if (stopwatch != null)
            {
                long x = stopwatch.ElapsedMilliseconds;
                time = x - basetime;
            }

            string[] words = serialRaw.Split(delimiterChars);

            accX = Convert.ToInt32(words[0]);
            accY = Convert.ToInt32(words[1]);
            accZ = Convert.ToInt32(words[2]);

            filter();

            label2.Text = Convert.ToString(dX);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialIsReady == true)
            {
                serialPort1.Close();
            }

            if (!serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.PortName = textBox1.Text;
                    serialPort1.Open();
                    serialPort1.Write("T");
                    serialIsReady = true;
                    basetime = stopwatch.ElapsedMilliseconds;
                }
                catch
                {
                    MessageBox.Show("There was an error. Please make sure that the correct port was selected, and the device, plugged in.");
                    serialPort1.Close();
                    serialIsReady = false;
                }
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string line = serialPort1.ReadLine();
                this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
            }
            catch { }
        }

        private delegate void LineReceivedEvent(string line);
        private void LineReceived(string line)
        {
            serialRaw = line;
        }
    }
}
