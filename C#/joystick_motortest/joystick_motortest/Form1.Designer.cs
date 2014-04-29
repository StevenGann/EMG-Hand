namespace joystick_motortest
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
            this.serialOutTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.portTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comConnectButt = new System.Windows.Forms.Button();
            this.tmrUpdateStick = new System.Windows.Forms.Timer(this.components);
            this.Zbar = new System.Windows.Forms.TrackBar();
            this.Ybar = new System.Windows.Forms.TrackBar();
            this.Xbar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.serialInTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Zbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ybar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xbar)).BeginInit();
            this.SuspendLayout();
            // 
            // serialOutTextbox
            // 
            this.serialOutTextbox.Location = new System.Drawing.Point(11, 103);
            this.serialOutTextbox.Name = "serialOutTextbox";
            this.serialOutTextbox.ReadOnly = true;
            this.serialOutTextbox.Size = new System.Drawing.Size(260, 20);
            this.serialOutTextbox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Serial Output";
            // 
            // portTextbox
            // 
            this.portTextbox.Location = new System.Drawing.Point(72, 12);
            this.portTextbox.Name = "portTextbox";
            this.portTextbox.Size = new System.Drawing.Size(52, 20);
            this.portTextbox.TabIndex = 8;
            this.portTextbox.Text = "COM1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "COM Port";
            // 
            // comConnectButt
            // 
            this.comConnectButt.Location = new System.Drawing.Point(187, 9);
            this.comConnectButt.Name = "comConnectButt";
            this.comConnectButt.Size = new System.Drawing.Size(84, 23);
            this.comConnectButt.TabIndex = 10;
            this.comConnectButt.Text = "Connect Serial";
            this.comConnectButt.UseVisualStyleBackColor = true;
            this.comConnectButt.Click += new System.EventHandler(this.comConnectButt_Click);
            // 
            // tmrUpdateStick
            // 
            this.tmrUpdateStick.Tick += new System.EventHandler(this.tmrUpdateStick_Tick);
            // 
            // Zbar
            // 
            this.Zbar.Location = new System.Drawing.Point(11, 244);
            this.Zbar.Maximum = 100;
            this.Zbar.Name = "Zbar";
            this.Zbar.Size = new System.Drawing.Size(260, 45);
            this.Zbar.TabIndex = 11;
            this.Zbar.TickFrequency = 50;
            this.Zbar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.Zbar.Value = 50;
            // 
            // Ybar
            // 
            this.Ybar.Location = new System.Drawing.Point(11, 193);
            this.Ybar.Maximum = 100;
            this.Ybar.Name = "Ybar";
            this.Ybar.Size = new System.Drawing.Size(260, 45);
            this.Ybar.TabIndex = 12;
            this.Ybar.TickFrequency = 50;
            this.Ybar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.Ybar.Value = 50;
            // 
            // Xbar
            // 
            this.Xbar.Location = new System.Drawing.Point(11, 142);
            this.Xbar.Maximum = 100;
            this.Xbar.Name = "Xbar";
            this.Xbar.Size = new System.Drawing.Size(260, 45);
            this.Xbar.TabIndex = 13;
            this.Xbar.TickFrequency = 50;
            this.Xbar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.Xbar.Value = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Z Axis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Y Axis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "X Axis";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Connect Joystick";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // serialInTextbox
            // 
            this.serialInTextbox.Location = new System.Drawing.Point(11, 64);
            this.serialInTextbox.Name = "serialInTextbox";
            this.serialInTextbox.ReadOnly = true;
            this.serialInTextbox.Size = new System.Drawing.Size(260, 20);
            this.serialInTextbox.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Serial Input";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 297);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.serialInTextbox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Xbar);
            this.Controls.Add(this.Ybar);
            this.Controls.Add(this.Zbar);
            this.Controls.Add(this.comConnectButt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.portTextbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.serialOutTextbox);
            this.Name = "Form1";
            this.Text = "Joystick Motor Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Zbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ybar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serialOutTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox portTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button comConnectButt;
        private System.Windows.Forms.Timer tmrUpdateStick;
        private System.Windows.Forms.TrackBar Zbar;
        private System.Windows.Forms.TrackBar Ybar;
        private System.Windows.Forms.TrackBar Xbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox serialInTextbox;
        private System.Windows.Forms.Label label6;
    }
}

