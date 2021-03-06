/* Sweep
 by BARRAGAN <http://barraganstudio.com> 
 This example code is in the public domain.

 modified 8 Nov 2013
 by Scott Fitzgerald
 http://arduino.cc/en/Tutorial/Sweep
*/ 

#include <Servo.h> 
 
Servo myservo;  // create servo object to control a servo 
                // twelve servo objects can be created on most boards
 
int pos = 0;    // variable to store the servo position 

 
void setup() 
{ 
  myservo.attach(9);  // attaches the servo on pin 9 to the servo object 
  Serial.begin(9600);
} 
 
void loop() 
{ 
  for(pos = 60; pos <= 135; pos += 1) // goes from 0 degrees to 180 degrees 
  {                                  // in steps of 1 degree 
    myservo.write(pos);              // tell servo to go to position in variable 'pos'
    Serial.print(String(pos));
    Serial.println(); 
    delay(20);                       // waits 15ms for the servo to reach the position 
  } 
  
  delay(5000);
  for(pos = 135; pos>=60; pos-=1)     // goes from 180 degrees to 0 degrees 
  {                                
    myservo.write(pos);              // tell servo to go to position in variable 'pos'
    Serial.print(String(pos));  
    Serial.println();
    delay(20);                       // waits 15ms for the servo to reach the position 
  }
  delay(2000);
 
} 

