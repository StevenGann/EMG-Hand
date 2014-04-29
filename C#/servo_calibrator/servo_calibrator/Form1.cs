using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace servo_calibrator
{
    public partial class Form1 : Form
    {
        private bool serialIsReady = false;
        private bool EMGIsReady = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.PortName = textBox1.Text;
                    serialPort1.Open();
                    serialPort1.Write("T");
                    serialIsReady = true;
                    //serialPort1.Close();
                }
                catch
                {
                    MessageBox.Show("There was an error. Please make sure that the correct port was selected, and the device, plugged in.");
                    serialPort1.Close();
                    serialIsReady = false;
                }
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (!serialPort2.IsOpen)
            {
                try
                {
                    //serialPort1.PortName = textBox1.Text;
                    serialPort2.Open();
                    serialPort2.Write("T");
                    EMGIsReady = true;
                    //serialPort1.Close();
                }
                catch
                {
                    //MessageBox.Show("There was an error. Please make sure that the correct port was selected, and the device, plugged in.");
                    serialPort2.Close();
                    EMGIsReady = false;
                }
            }

            label1.Text = Convert.ToString(trackBar1.Value);
            label2.Text = Convert.ToString(trackBar2.Value);
            label3.Text = Convert.ToString(trackBar3.Value);
            label4.Text = Convert.ToString(trackBar4.Value);
            label5.Text = Convert.ToString(trackBar5.Value);
            label6.Text = Convert.ToString(trackBar6.Value);
            label7.Text = Convert.ToString(trackBar7.Value);

            string output = Convert.ToString(trackBar6.Value) + "," + Convert.ToString(trackBar7.Value) + "," + Convert.ToString(trackBar1.Value) + "," + Convert.ToString(trackBar2.Value) + "," + Convert.ToString(180 - trackBar3.Value) + "," + Convert.ToString(trackBar4.Value) + "," + Convert.ToString(trackBar5.Value) + ",0,0,0\n";
            
            textBox3.Text = output;

            if (serialIsReady == true)
            {
                serialPort1.Write(output);
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string line = serialPort1.ReadLine();
                this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
            }
            catch
            {

            }
            
        }

        private delegate void LineReceivedEvent(string line);
        private void LineReceived(string line)
        {
            //What to do with the received line here
            textBox2.Text = line;
        }

        private void openButt_Click(object sender, EventArgs e)
        {
            trackBar1.Value = trackBar1.Maximum;
            trackBar2.Value = trackBar2.Maximum;
            trackBar3.Value = trackBar3.Maximum;
            trackBar4.Value = trackBar4.Maximum;
            trackBar5.Value = trackBar5.Maximum;
        }

        private void closeButt_Click(object sender, EventArgs e)
        {
            trackBar1.Value = trackBar1.Minimum;
            trackBar2.Value = trackBar2.Minimum;
            trackBar3.Value = trackBar3.Minimum;
            trackBar4.Value = trackBar4.Minimum;
            trackBar5.Value = trackBar5.Minimum;
        }

        private void serialPort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string line = serialPort2.ReadLine();
            this.BeginInvoke(new LineReceivedEvent2(LineReceived2), line);
        }

        private delegate void LineReceivedEvent2(string line);
        private void LineReceived2(string line)
        {
            try
            {
                //What to do with the received line here
                textBox2.Text = line;

                char[] delimiterChars = { ',' };
                string[] words = line.Split(delimiterChars);

                trackBar1.Value = Convert.ToInt32(words[0]);
                trackBar2.Value = Convert.ToInt32(words[1]);
                trackBar3.Value = Convert.ToInt32(words[2]);
                trackBar4.Value = Convert.ToInt32(words[3]);
                trackBar5.Value = Convert.ToInt32(words[4]);
            }
            catch { }
        }

    }
}
