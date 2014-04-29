using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace joystick_motortest
{
    public partial class Form1 : Form
    {
        private JoystickInterface.Joystick jst;
        private bool serialIsReady = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void comConnectButt_Click(object sender, EventArgs e)
        {
            

            if (!serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.PortName = portTextbox.Text;
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

        private void tmrUpdateStick_Tick(object sender, EventArgs e)
        {
            // get status
            jst.UpdateStatus();
            // update the axes positions
            Zbar.Value = (int)(100.0f * (((float)jst.AxisA) / 65535.0f));
            Xbar.Value = (int)(100.0f * (((float)jst.AxisC) / 65535.0f));
            Ybar.Value = 100 - (int)(100.0f * (((float)jst.AxisD) / 65535.0f));

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string Xout = Convert.ToString(Xbar.Value);
            string Yout = Convert.ToString(Ybar.Value);
            string Zout = Convert.ToString(Zbar.Value);

            if (serialIsReady == true)
            {
                serialOutTextbox.Text = Xout + "," + Yout + "," + Zout;
                serialPort1.Write(serialOutTextbox.Text + "/n");

                //serialInTextbox.Text = serialPort1.ReadLine();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Loading");
            // grab the joystick
            jst = new JoystickInterface.Joystick(this.Handle);
            string[] sticks = jst.FindJoysticks();
            jst.AcquireJoystick(sticks[0]);
            //MessageBox.Show("Joystick Acquired");
            // start updating positions
            tmrUpdateStick.Enabled = true;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string line = serialPort1.ReadLine();
            this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
        }

        private delegate void LineReceivedEvent(string line);
        private void LineReceived(string line)
        {
            //What to do with the received line here
            serialInTextbox.Text = line;
        }
    }
}
