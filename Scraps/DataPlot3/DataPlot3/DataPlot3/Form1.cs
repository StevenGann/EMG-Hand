using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;
using ZedGraph;

namespace DataPlot3
{
    public partial class Form1 : Form
    {
        String COMRx;
        int NoOfCurves, Samples;
        int[,] colourList = {{128,0,0},{0,128,0},{0,0,128},{0,128,128},{128,128,0},{255,0,0},{0,255,0},
                            {0,0,255},{255,255,0},{255,0,255},{0,255,255}};

        List<PointPairList> Data = new List<PointPairList>();

        delegate void SerialDataReceivedDelegate(object sender, SerialDataReceivedEventArgs e);

        delegate void SerialErrorReceivedDelegate(object sender, SerialErrorReceivedEventArgs e);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            // Set the titles and axis labels
            // MAA
            ZedGraphControl1.GraphPane.Title.Text = "Time History";
            ZedGraphControl1.GraphPane.XAxis.Title.Text = "Samples";
            ZedGraphControl1.GraphPane.YAxis.Title.Text = "";

            ZedGraphControl1.GraphPane.XAxis.Scale.MaxGrace = 0;

            ZedGraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = true;

            ZedGraphControl1.GraphPane.XAxis.MajorGrid.IsVisible = true;

            ZedGraphControl1.GraphPane.Legend.Position = ZedGraph.LegendPos.Right;
        }

        private void COMConnectBtn_Click(object sender, EventArgs e)
        {
            COMConnectButton.Enabled = false;

            COMPortStatusLight.Value = 3;

            if (SerialPort.IsOpen)
            {
                timer1.Enabled = false;

                SerialPort.Close();

                COMPortStatusLight.Value = -1;
                COMConnectButton.Text = "Connect";
            }
            else
            {
                SerialPort.PortName = COMPortComboBox.Text;
                SerialPort.BaudRate = int.Parse(COMBaudComboBox.Text);

                try
                {
                    SerialPort.Open();
                }
                catch
                {
                    COMPortStatusLight.Value = -1;
                    MessageBox.Show("Could not open " + COMPortComboBox.Text);
                }

                if (SerialPort.IsOpen)
                {
                    COMPortStatusLight.Value = 1;
                    COMConnectButton.Text = "Disconnect";

                    Data.Clear();
                    deleteCurves();
                    initCurves();

                    timer1.Enabled = true;
                }
            }

            COMConnectButton.Enabled = true;
        }

        private void initCurves()
        {
            Samples = int.Parse(SampleTextBox.Text);
            NoOfCurves = (int)NoOfDataNumericUpDown.Value;


            for (int j = 0; j < NoOfCurves; j++)
            {
                PointPairList tempppl = new PointPairList();

                for (double x = 0; x < Samples; x++)
                {
                    tempppl.Add(x, 0);
                }

                Data.Add(tempppl);
                ZedGraphControl1.GraphPane.AddCurve("Data " + j.ToString(), Data[j],
                    Color.FromArgb(colourList[j, 0], colourList[j, 1], colourList[j, 2]), SymbolType.None);
            }
        }

        private void deleteCurves()
        {
            ZedGraphControl1.GraphPane.CurveList.Clear();
        }

        // event handler for getting serial data
        private void ProcessCOMRx(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(COMRx))
            {
                string[] parsed = COMRx.Split(',');
                int curveNo;

                if (parsed.Count() > ZedGraphControl1.GraphPane.CurveList.Count())
                    curveNo = ZedGraphControl1.GraphPane.CurveList.Count();
                else
                    curveNo = parsed.Count();


                for (int k = 0; k < curveNo; k++)
                {
                    for (int j = ZedGraphControl1.GraphPane.CurveList[k].NPts - 1; j > 0; j--)
                    {
                        ZedGraphControl1.GraphPane.CurveList[k].Points[j].Y = ZedGraphControl1.GraphPane.CurveList[k].Points[j - 1].Y;
                    }

                    double temp = 0;

                    try
                    {
                        temp = double.Parse(parsed[k]);
                    }
                    catch
                    {
                        RawTextBox.AppendText("Parse Error\n");
                    }

                    //RawTextBox.AppendText(temp.ToString() + "-" + k.ToString() + ", ");
                    ZedGraphControl1.GraphPane.CurveList[k].Points[0].X = 0;
                    ZedGraphControl1.GraphPane.CurveList[k].Points[0].Y = temp;
                }
                //RawTextBox.AppendText("\n");
                RawTextBox.AppendText(COMRx + '\n');
                COMRx = "";
            }
        }

        private void ReadLineError(object sender, EventArgs e)
        {
            RawTextBox.AppendText("Read Line Error\n");
        }

        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int BufferLength = SerialPort.BytesToRead;
            //StringBuilder tempS = new StringBuilder();

            while (BufferLength > 0 && SerialPort.IsOpen)
            {
                try
                {
                    //tempS.Append(MAASerialPort.ReadLine());
                    COMRx = SerialPort.ReadLine();
                    //tempS.Length = 0;


                    // This is the fast way - run in the same thread (causes issues with seril.close)
                    //if (this.InvokeRequired)
                    //{
                    //    this.Invoke(new SerialDataReceivedDelegate(ProcessCOMRx), sender, e);
                    //}
                    //else
                    //{
                    //    ProcessCOMRx(sender, e);
                    //}

                    // This is the slow way - run in a new thread
                    this.BeginInvoke(new EventHandler(ProcessCOMRx));

                    if (SerialPort.IsOpen)
                        BufferLength = SerialPort.BytesToRead;
                }
                catch
                {
                    this.BeginInvoke(new EventHandler(ReadLineError));
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ZedGraphControl1.AxisChange();
            ZedGraphControl1.Invalidate();
        }
    }
}
