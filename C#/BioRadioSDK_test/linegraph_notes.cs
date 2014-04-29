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