using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BioRadioSDK_test
{
	public partial class Form1 : Form
	{
		typedef void UpdateStatusFunc(char *Message, bool *Abort);
		typedef UpdateStatusFunc* TUpdateStatusEvent;

		[DllImport("BioRadio150DLL.dll", EntryPoint="FindDevices")]
		public static extern unsafe int FindDevices(TDeviceInfo DeviceList, Int32 *DeviceCount, Int32 MaxCount, TUpdateStatusEvent UpdateStatusProc);
		[DllImport("BioRadio150DLL.dll", EntryPoint="FindDevicesSimple")]
		public static extern unsafe bool  FindDevicesSimple(String *DevicePortNameList[], Int32 *DeviceCount, Int32 MaxDeviceCount, Int32 PortNameStrLen);
		[DllImport("BioRadio150DLL.dll", EntryPoint="CreateBioRadio")] 
		public static extern unsafe UInt32  CreateBioRadio(bool useBaseStation);
		[DllImport("BioRadio150DLL.dll", EntryPoint="DestroyBioRadio")] 
		public static extern unsafe Int32  DestroyBioRadio(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll", EntryPoint="StartCommunication")] 
		public static extern unsafe Int32  StartCommunication(UInt32 BioRadio150 , String *PortName);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  StartAcq(UInt32 BioRadio150, String DisplayProgressDialog);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  StopAcq(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  DisableBuffering(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  EnableBuffering(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  StopCommunication(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  LoadConfig(UInt32 BioRadio150, String *Filename);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  SaveConfig(UInt32 BioRadio150, String *Filename);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  PingConfig(UInt32 BioRadio150, String DisplayProgressDialog);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetConfig(UInt32 BioRadio150, TBioRadioConfig *ConfigStruct, String PingDevice, String DisplayProgressDialog);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  ProgramConfig(UInt32 BioRadio150, String DisplayProgressDialog, String *Filename);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  SetConfig(UInt32 BioRadio150, TBioRadioConfig *ConfigStruct, String ProgramDevice, String DisplayProgressDialog);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe long  GetSampleRate(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe long  GetBitResolution(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetFastSweepsPerPacket(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetSlowSweepsPerPacket(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetNumEnabledInputs(UInt32 BioRadio150, Int32 *FastMainInputs, Int32 *FastAuxInputs, Int32 *SlowAuxInputs);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetNumEnabledSlowInputs(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetNumEnabledFastInputs(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe long  GetMode(UInt32 BioRadio150, ushort &Mode);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe long  GetNumChannels(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe ushort  GetEnabledFastInputs(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe String  GetEnabledSlowInputs(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  TransferBuffer(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  ReadScaledData(UInt32 BioRadio150, double *Data, Int32 Size, Int32 *NumRead);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  ReadRawData(UInt32 BioRadio150, ushort *Data, Int32 Size, Int32 *NumRead);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  ReadScaledFastAndSlowData(UInt32 BioRadio150, double *FastInputsData, Int32 FastInputsSize, Int32 *FastInputsNumRead, ushort *SlowInputsData, Int32 SlowInputsSize, Int32 *SlowInputsNumRead);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  ReadRawFastAndSlowData(UInt32 BioRadio150, ushort *FastInputsData, Int32 FastInputsSize, Int32 *FastInputsNumRead, ushort *SlowInputsData, Int32 SlowInputsSize, Int32 *SlowInputsNumRead);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  SetBadDataValues(UInt32 BioRadio150, double BadDataValueScaled, ushort BadDataValueRaw);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  SetPadWait(UInt32 BioRadio150, Int32 numMissingPackets);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetRFChannel(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  SetRFChannel(UInt32 BioRadio150, Int32 RFChannel);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetUsableRFChannelList(Int32 *UsableRFChannelList, Int32 Size);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetFreqHoppingMode(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetFreqHoppingModeIndicator();
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  SetFreqHoppingMode(UInt32 BioRadio150, bool HoppingEnabled);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe UInt32  GetGoodPackets(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe UInt32  GetBadPackets(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe UInt32  GetDroppedPackets(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetUpRSSI(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetDownRSSI(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetLinkBufferSize(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetBitErrCount(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetBitErrRate(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe double  GetBatteryStatus(UInt32 BioRadio150);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetBioRadioModelString(UInt32 BioRadio150, String *BioRadioModelString, Int32 StrLength);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetDeviceID(UInt32 BioRadio150, ushort *DeviceID);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetDeviceIDString(UInt32 BioRadio150, String *DeviceIDCStr, Int32 StrLength);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe Int32  GetFirmwareVersionString(UInt32 BioRadio150, String *VersionString, Int32 StrLength);
		[DllImport("BioRadio150DLL.dll")] 
		public static extern unsafe void  GetDLLVersionString(String *VersionString, Int32 StrLength);



		const int historyLength = 1000;//256;
		float[] history = new float[historyLength];
		int nextWrite = 0;
		Graphics bmg;
		Bitmap bm;

		public Form1()
		{
			InitializeComponent();
			this.SuspendLayout();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer( this.components );
			// 
			// pictureBox1
			// 
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point( 0, 0 );
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size( 950, 500 );
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.Controls.Add( this.pictureBox1 );
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1;
			this.timer1.Tick += new EventHandler(timer1_Tick);
			this.ResumeLayout( false );

		}

		private void Form1_Load( object sender, EventArgs e )
		{
			// set initial values
			for( int i = 0; i < historyLength; i++ ) {
				history[i] = 0;
			}
		}

		void AddValue( float y )
		{
			history[nextWrite] = y;
			nextWrite = (nextWrite + 1) % historyLength;
		}

		private void pictureBox1_Paint( object sender, PaintEventArgs e )
		{
			if( bm == null ) {
				bm = new Bitmap( historyLength, 501 );
				bmg = Graphics.FromImage( bm );
				bmg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				bmg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;
				bmg.Clear( Color.White );
			}
			Render();
			e.Graphics.DrawImage( bm, 0, 0 );
		}

		void Render()
		{
			bmg.Clear( Color.White);
			float y0 = 0;
			int x0 = 0;
			for( int i = 0; i < historyLength; i++ ) {
				float y = 250.0f + 250.0f * history[(nextWrite + i) % historyLength];
				int x = i;
				if( i != 0 ) {
					// draw a line
					bmg.DrawLine( Pens.Blue, x0, y0, x, y );
				}
				y0 = y;
				x0 = x;
			}
		}

		float t = 0.0f;
		private void timer1_Tick( object sender, EventArgs e )
		{
			AddValue( (float)Math.Sin( t ) );
			t += 0.4f;
			AddValue((float)Math.Sin(t));
			t += 0.4f;
			AddValue((float)Math.Sin(t));
			t += 0.4f;
			this.pictureBox1.Invalidate();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}
	}
}
