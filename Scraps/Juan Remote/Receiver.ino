//Xbee Reciever for Airforce Juan
#include <Servo.h>

//Define all of the pins and variables
#define LED 12
#define prop 11
#define leftServo 9
#define rightServo 10
#define lefttail 5
#define righttail 6

unsigned int xJoy;
unsigned int yJoy;
unsigned int speedPot;
unsigned int auxButton;
unsigned int speedProp;

float angle1;
float angle2;
float targetAngle1;
float targetAngle2;

Servo servo1;
Servo servo2;
Servo servo3;
Servo servo4;

void setup()
{
  Serial.begin(9600);
  servo1.attach(leftServo); 
  servo2.attach(rightServo); 
  servo3.attach(lefttail);
  servo4.attach(righttail);
  pinMode(prop, OUTPUT);
}

void loop()
{
  while(Serial.available() > 0) //when there's information being received through serial go into this loop
  {
    xJoy = Serial.parseInt(); //takes each value seperated by the commas and defines them to the four different values
    yJoy = Serial.parseInt();
    auxButton = Serial.parseInt();
    speedPot = Serial.parseInt();
    serialFlush(); //flushes any other values in the buffer
  }
  targetAngle1 = map(xJoy, 0, 1023, 0, 180); //converts the 10 bit value to an angle between 0 and 180
  targetAngle2 = map(yJoy, 0, 1023, 0, 180);
  
  if(servo1.read() < targetAngle1) //slows down the movement of the servos
  {angle1 += 0.2;}
  if(servo1.read() > targetAngle1)
  {angle1 -= 0.2;}
  
  servo1.write(angle1); //write the same angle to the servos
  servo2.write(angle1);
  
  if(servo3.read() < targetAngle2) //slows down movement of servos
  {angle2 += 0.2;}
  if(servo3.read() > targetAngle2)
  {angle2 -= 0.2;}
  
  if(angle2 < 100) //makes sure the elevators when the y-joystick is moved upwards
  {
  servo3.write(angle2); //write opposite angles to the servos
  servo4.write(180 - angle2);
  }
  
  speedProp = map(speedPot, 0, 1023, 0, 255); //converts the 10 bit reading from the pot into a 8 bit reading
  analogWrite(prop, speedProp);
  
  if(auxButton < 50) //If the value is lower than 50 or 240 mV since switch pulls the value low
  {
    digitalWrite(LED, HIGH);
  }
  else 
  {
      digitalWrite(LED, LOW);
  }
}

void serialFlush()
{
  while(Serial.available()>0) Serial.read(); 
  //Flushes any other values in the buffer received from the xbee after the first four
}


