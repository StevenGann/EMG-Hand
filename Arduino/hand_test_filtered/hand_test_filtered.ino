/*
My dear Frodo,
	You asked me once if I had told you everything there was
to know about my adventures. While I can honestly say that I
have told you the truth, I may not have told you all of it...

Crap. Wrong letter.

Dear Craig,
	You said once that you wanted to learn to code. In the
interest of efficiency, I'll teach you everything I know by
example. All of the code I do will be heavily commented, with
the assumption that the reader doesn't know anything at all 
about programming for Arduino. Read along and e-mail me any
questions.

	~ Steven
*/


#include <Servo.h> //This line includes a code file (library) that
                   //comes with the Arduino IDE. This library is
				   //for controlling servos.

Servo servoPinkie; //Part of that library creates a new data structure
Servo servoRing;   //that it names a "Servo". This is effectively a
Servo servoMiddle; //variable, just like an int, an integer, or String,
Servo servoIndex;  //which is a list of text characters.
Servo servoThumb;  //Note the naming convention. I could name these
                   //anything I want, but by putting the type in the
                   //name I'll be able to keep track that this is a
				   //Servo, not some number or string.

int values[5][4] = {{0,0,0,0},
                    {0,0,0,0},
                    {0,0,0,0},
                    {0,0,0,0},
                    {0,0,0,0}};
               //Here I declare an array of integers of size 5.
               //Simply put, an array is a line of variables of
			   //the same type that travel around the program as
			   //a set. Imagine a row of 5 cups, numbered 0 to 4,
			   //and each cup can hold a single integer.

int maximum[5] = { 0, 0, 0, 0, 0 };
int minimum[5] = { 0, 0, 0, 0, 0 };


			   
//The setup() function is called when the Arduino starts up, and only
//runs once. It runs again if the Arduino restarts.
void setup() {
  servoPinkie.attach(3);  //The dot between servoPinkie and attach()
  servoRing.attach(4);    //is called the dot operator. It tells the
  servoMiddle.attach(5);  //compiler that it can find the attach()
  servoIndex.attach(6);   //function inside that Servo.h.
  servoThumb.attach(7);   //The Servo.attach(int) function tells the
                          //program that the servo is physically
						  //connected to the digital pin corresponding
						  //to the number.
  
  Serial.begin(9600); //Serial is another class, like Servo, but is
                      //always included. The Uno only has one physical
					  //serial port on digital pins 0 and 1. These are
					  //connected to the USB cable.
					  //You can communicate between the Arduino and
					  //PC this way. It is useful for debugging.
  
}


//The loop() function is called over and over as long as the Arduino
//is still turned on.
void loop() {
 sensorsRead(); //These two functions I wrote myself. They are declared below.
 checkLimits();
 processValues();
 servosWrite();
}

//This function reads the sensors and stores them in that values[] array.
//Note again the naming convention. This function deals with the sensors,
//so I put "sensors" in the name.
void sensorsRead()
{
  
  delay(100);
  //Queue push loop!
  int a = 0;
  while (a < 5)
  {
	values[a][3] = values[a][2];
	values[a][2] = values[a][1];
	values[a][1] = values[a][0];
	
	a++;
  }
  
	//The analogRead() function reads the analog input pins and returns
	//an integer between 0 and 1023.
	values[0][0] = analogRead(A0);//(175 - analogRead(A0))*27/10;//Tweak these numbers as needed.
        values[1][0] = analogRead(A1);//(analogRead(A1)*22/10)-208;
        values[2][0] = analogRead(A2);//(analogRead(A2)*24/10) - 230;
        values[3][0] = analogRead(A3);//analogRead(A3);
        values[4][0] = analogRead(A4);//(analogRead(A4)*18/10) - 225;
	//See how I can assign values to parts of an array just like I could
	//any variable.
		
		
        //Serial.println("============================");
        //Serial.println(String(values[0]));
        //Serial.println(String(values[1]));
        //Serial.println(String(values[2]));
        //Serial.println(String(values[3]));
        //Serial.println(String(values[4]));
        //Serial.println("----------------------------");
}

//This function writes the numbers from values[] to the servos.
//For most servos, the number you give it directly corresponds
//to the angle the servo will move to.
void servosWrite()
{
  
  delay(100);
	servoPinkie.write(values[0][0]); //Servo.write(int) is another member
	  servoRing.write(values[1][0]);   //function of the Servo class.
	servoMiddle.write(values[2][0]);
	 servoIndex.write(values[3][0]);
	 servoThumb.write(values[4][0]);
}


void checkLimits()
{
	int index = 0;
	while (index < 5)
	{
		if (values[index][0] > maximum[index])
		{maximum[index] = values[index][0];}
		else
		{maximum[index] = maximum[index] - (maximum[index] * 0.001);}
		
		if (values[index][0] < minimum[index])
		{minimum[index] = values[index][0];}
		else
		{minimum[index] = minimum[index] + (minimum[index] * 0.001);}
		
		//Value clamping and error preventing
		if (maximum[index] < 0) {maximum[index] = 0;}
		if (minimum[index] < 0) {minimum[index] = 0;}
		if (maximum[index] < minimum[index]) {maximum[index] = minimum[index];}
		
		index++;
	}
}

void processValues()
{
	int index = 0;
	while (index < 5)
	{
		values[index][0] = (values[index][0]/(maximum[index]-minimum[index]))*180;
		index++;
	}
	
	index = 0;
	while (index < 5)
	{
		values[index][0] = (values[index][0] + values[index][1] + values[index][2] + values[index][3])/4;
		index++;
	}
}


