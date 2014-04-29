// Airforce Juan: Servos for Ailerons
 
#include <Servo.h> 
 
//xjstick = A0;  
//yjstick = A1;
//button = A2;
//speedpot = A3;
const int LED = 13;
int prop = 11;
int leftServo = 9;
int rightServo = 10;
int lefttail = 5;
int righttail = 6;
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
  servo1.attach(leftServo); 
  servo2.attach(rightServo); 
  servo3.attach(lefttail);
  servo4.attach(righttail);
  //pinMode(prop, OUTPUT);
} 
 
void loop() 
{ 
  int reading1 = analogRead(A0);        // 0 to 1023 read joystick x-position
  int targetAngle1 = reading1 / 6;      // 0 to 180-ish convert the digital level reading (0-1023) into an angle (0-180)
  
  if(servo1.read() < targetAngle1)
  {angle1 += 0.15;}
  if(servo1.read() > targetAngle1)
  {angle1 -= 0.15;}
  
  servo1.write(angle1);  
  servo2.write(angle1);
  
  int reading2 = analogRead(A1);        // 0 to 1023 read joystick y-position
  int targetAngle2 = reading2 / 6;      // 0 to 180-ish convert the digital level reading (0-1023) into an angle (0-180)
  
  if(servo3.read() < targetAngle2)
  {angle2 += 0.15;}
  if(servo3.read() > targetAngle2)
  {angle2 -= 0.15;}
  
  if(angle2 < 90)
  {servo3.write(angle2);
  servo4.write(180 - angle2);}
  
  analogWrite(prop, (255 - analogRead(A3)/4)); // Read speed pot position (0-1023) and convert to (0-255) discrete levels for pwm
  
  int buttonval = analogRead(A2);
  if (buttonval < 50)
  {digitalWrite(LED, HIGH);} //Turns LED on if button is pressed
  else 
  {digitalWrite(LED, LOW);}
} 



