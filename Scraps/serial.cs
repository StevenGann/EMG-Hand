namespace Read_serial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            serialPort1.PortName = "COM4";
            serialPort1.BaudRate = 9600;
            serialPort1.DtrEnable = true;
            serialPort1.Open();

            serialPort1.DataReceived += serialPort1_DataReceived;
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
            label1.Text = line;

            progressBar1.Value = int.Parse(line);
        }
    }
}