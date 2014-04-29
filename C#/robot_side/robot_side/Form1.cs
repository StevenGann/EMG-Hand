using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace robot_side
{
    public partial class Form1 : Form
    {
        string serialRaw = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string line = serialPort1.ReadLine();
            this.BeginInvoke(new LineReceivedEvent(LineReceived1), line);
        }

        private delegate void LineReceivedEvent(string line);
        private void LineReceived1(string line)
        {
            textBox1.Text = line;
            serialRaw = line;

            //Mirror out for testing.
            serialPort1.Write(line);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.Open();
                    toolStripStatusLabel1.Text = serialPort1.PortName + " Connected";

                }
                catch
                {
                    toolStripStatusLabel1.Text = serialPort1.PortName + " Failed";
                }
            
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Nothing";
        }
    }
}
