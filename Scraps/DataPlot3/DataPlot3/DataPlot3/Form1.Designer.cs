namespace DataPlot3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ComPortGroupBox = new System.Windows.Forms.GroupBox();
            this.COMPortStatusLight = new PolyMonControls.StatusLight();
            this.COMPortComboBox = new System.Windows.Forms.ComboBox();
            this.COMBaudComboBox = new System.Windows.Forms.ComboBox();
            this.COMConnectButton = new System.Windows.Forms.Button();
            this.SerialPort = new System.IO.Ports.SerialPort(this.components);
            this.RawTextBox = new System.Windows.Forms.TextBox();
            this.ZedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.NoOfDataNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SampleTextBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ComPortGroupBox.SuspendLayout();
            this.OptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NoOfDataNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ComPortGroupBox
            // 
            this.ComPortGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ComPortGroupBox.Controls.Add(this.COMPortStatusLight);
            this.ComPortGroupBox.Controls.Add(this.COMPortComboBox);
            this.ComPortGroupBox.Controls.Add(this.COMBaudComboBox);
            this.ComPortGroupBox.Controls.Add(this.COMConnectButton);
            this.ComPortGroupBox.Location = new System.Drawing.Point(978, 603);
            this.ComPortGroupBox.Name = "ComPortGroupBox";
            this.ComPortGroupBox.Size = new System.Drawing.Size(140, 115);
            this.ComPortGroupBox.TabIndex = 0;
            this.ComPortGroupBox.TabStop = false;
            this.ComPortGroupBox.Text = "COM Port";
            // 
            // COMPortStatusLight
            // 
            this.COMPortStatusLight.BackgroundImage = System.Drawing.Color.Empty;
            this.COMPortStatusLight.BackgroundImageLayout = System.Drawing.Color.Empty;
            this.COMPortStatusLight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.COMPortStatusLight.Location = new System.Drawing.Point(13, 82);
            this.COMPortStatusLight.Name = "COMPortStatusLight";
            this.COMPortStatusLight.OffColor = System.Drawing.Color.Black;
            this.COMPortStatusLight.Size = new System.Drawing.Size(24, 27);
            this.COMPortStatusLight.TabIndex = 3;
            this.COMPortStatusLight.Text = "COMPortStatusLight";
            // 
            // COMPortComboBox
            // 
            this.COMPortComboBox.FormattingEnabled = true;
            this.COMPortComboBox.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM12",
            "COM13",
            "COM14"});
            this.COMPortComboBox.Location = new System.Drawing.Point(6, 51);
            this.COMPortComboBox.Name = "COMPortComboBox";
            this.COMPortComboBox.Size = new System.Drawing.Size(128, 24);
            this.COMPortComboBox.TabIndex = 2;
            this.COMPortComboBox.Text = "COM3";
            // 
            // COMBaudComboBox
            // 
            this.COMBaudComboBox.FormattingEnabled = true;
            this.COMBaudComboBox.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "14400",
            "28800",
            "38400",
            "57600",
            "115200"});
            this.COMBaudComboBox.Location = new System.Drawing.Point(6, 21);
            this.COMBaudComboBox.Name = "COMBaudComboBox";
            this.COMBaudComboBox.Size = new System.Drawing.Size(128, 24);
            this.COMBaudComboBox.TabIndex = 1;
            this.COMBaudComboBox.Text = "9600";
            // 
            // COMConnectButton
            // 
            this.COMConnectButton.Location = new System.Drawing.Point(43, 82);
            this.COMConnectButton.Name = "COMConnectButton";
            this.COMConnectButton.Size = new System.Drawing.Size(91, 27);
            this.COMConnectButton.TabIndex = 0;
            this.COMConnectButton.Text = "Connect";
            this.COMConnectButton.UseVisualStyleBackColor = true;
            this.COMConnectButton.Click += new System.EventHandler(this.COMConnectBtn_Click);
            // 
            // SerialPort
            // 
            this.SerialPort.ReadBufferSize = 1024;
            this.SerialPort.ReadTimeout = 2000;
            this.SerialPort.ReceivedBytesThreshold = 2;
            this.SerialPort.WriteBufferSize = 1024;
            this.SerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort_DataReceived);
            // 
            // RawTextBox
            // 
            this.RawTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RawTextBox.Location = new System.Drawing.Point(12, 603);
            this.RawTextBox.MaxLength = 1000;
            this.RawTextBox.Multiline = true;
            this.RawTextBox.Name = "RawTextBox";
            this.RawTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.RawTextBox.Size = new System.Drawing.Size(960, 115);
            this.RawTextBox.TabIndex = 1;
            // 
            // ZedGraphControl1
            // 
            this.ZedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ZedGraphControl1.AutoSize = true;
            this.ZedGraphControl1.BackColor = System.Drawing.SystemColors.InfoText;
            this.ZedGraphControl1.IsAutoScrollRange = true;
            this.ZedGraphControl1.Location = new System.Drawing.Point(12, 13);
            this.ZedGraphControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ZedGraphControl1.Name = "ZedGraphControl1";
            this.ZedGraphControl1.ScrollGrace = 0D;
            this.ZedGraphControl1.ScrollMaxX = 0D;
            this.ZedGraphControl1.ScrollMaxY = 0D;
            this.ZedGraphControl1.ScrollMaxY2 = 0D;
            this.ZedGraphControl1.ScrollMinX = 0D;
            this.ZedGraphControl1.ScrollMinY = 0D;
            this.ZedGraphControl1.ScrollMinY2 = 0D;
            this.ZedGraphControl1.Size = new System.Drawing.Size(960, 583);
            this.ZedGraphControl1.TabIndex = 2;
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.OptionsGroupBox.Controls.Add(this.NoOfDataNumericUpDown);
            this.OptionsGroupBox.Controls.Add(this.label2);
            this.OptionsGroupBox.Controls.Add(this.label1);
            this.OptionsGroupBox.Controls.Add(this.SampleTextBox);
            this.OptionsGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OptionsGroupBox.Location = new System.Drawing.Point(978, 13);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(140, 584);
            this.OptionsGroupBox.TabIndex = 3;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            // 
            // NoOfDataNumericUpDown
            // 
            this.NoOfDataNumericUpDown.Location = new System.Drawing.Point(6, 53);
            this.NoOfDataNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NoOfDataNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NoOfDataNumericUpDown.Name = "NoOfDataNumericUpDown";
            this.NoOfDataNumericUpDown.Size = new System.Drawing.Size(128, 22);
            this.NoOfDataNumericUpDown.TabIndex = 5;
            this.NoOfDataNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Samples";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "No. of data sets";
            // 
            // SampleTextBox
            // 
            this.SampleTextBox.Location = new System.Drawing.Point(6, 101);
            this.SampleTextBox.Name = "SampleTextBox";
            this.SampleTextBox.Size = new System.Drawing.Size(128, 22);
            this.SampleTextBox.TabIndex = 1;
            this.SampleTextBox.Text = "1000";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 730);
            this.Controls.Add(this.OptionsGroupBox);
            this.Controls.Add(this.ZedGraphControl1);
            this.Controls.Add(this.RawTextBox);
            this.Controls.Add(this.ComPortGroupBox);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "DataPlot V3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ComPortGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NoOfDataNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ComPortGroupBox;
        private System.Windows.Forms.ComboBox COMPortComboBox;
        private System.Windows.Forms.ComboBox COMBaudComboBox;
        private System.Windows.Forms.Button COMConnectButton;
        private PolyMonControls.StatusLight COMPortStatusLight;
        private System.IO.Ports.SerialPort SerialPort;
        private System.Windows.Forms.TextBox RawTextBox;
        private ZedGraph.ZedGraphControl ZedGraphControl1;
        private System.Windows.Forms.GroupBox OptionsGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SampleTextBox;
        private System.Windows.Forms.NumericUpDown NoOfDataNumericUpDown;
        private System.Windows.Forms.Timer timer1;
    }
}

