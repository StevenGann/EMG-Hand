/*************************************************** 
  This is an example for our Adafruit 16-channel PWM & Servo driver
  Servo test - this will drive 16 servos, one after the other

  Pick one up today in the adafruit shop!
  ------> http://www.adafruit.com/products/815

  These displays use I2C to communicate, 2 pins are required to  
  interface. For Arduino UNOs, thats SCL -> Analog 5, SDA -> Analog 4

  Adafruit invests time and resources providing this open source code, 
  please support Adafruit and open-source hardware by purchasing 
  products from Adafruit!

  Written by Limor Fried/Ladyada for Adafruit Industries.  
  BSD license, all text above must be included in any redistribution
 ****************************************************/

#include <Wire.h>
#include <Adafruit_PWMServoDriver.h>

// called this way, it uses the default address 0x40
Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver();
// you can also call it with a different address you want
//Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver(0x41);

// Depending on your servo make, the pulse width min and max may vary, you 
// want these to be as small/large as possible without hitting the hard stop
// for max range. You'll have to tweak them as necessary to match the servos you
// have!
#define SERVOMIN  150 // this is the 'minimum' pulse length count (out of 4096)
#define SERVOMAX  600 // this is the 'maximum' pulse length count (out of 4096)

// our servo # counter
uint8_t servonum = 0;
int val[5] = {0,0,0,0,0};

void setup() {
  Serial.begin(115200);
  Serial.println("16 channel Servo test!");

  pwm.begin();
  
  pwm.setPWMFreq(60);  // Analog servos run at ~60 Hz updates
}

void serialFlush()
{
  while(Serial.available()>0) Serial.read(); 
  //Flushes any other values in the buffer received from the xbee after the first four
}

void loop() 
{
    while(Serial.available() > 0) //when there's information being received through serial go into this loop
    {
        val[0] = Serial.parseInt(); //takes each value seperated by the commas and defines them to the four different values
        val[1] = Serial.parseInt();
        val[2] = Serial.parseInt();
        val[3] = Serial.parseInt();
        val[4] = Serial.parseInt();
        
        val[0] = map(val[0],0,100,SERVOMIN,SERVOMAX);
        val[1] = map(val[1],0,100,SERVOMIN,SERVOMAX);
        val[2] = map(val[2],0,100,SERVOMIN,SERVOMAX);
        val[3] = map(val[3],0,100,SERVOMIN,SERVOMAX);
        val[4] = map(val[4],0,100,SERVOMIN,SERVOMAX);
        
        serialFlush(); //flushes any other values in the buffer
    }
    
    pwm.setPWM(0, 0, val[0]);
    pwm.setPWM(1, 0, val[1]);
    pwm.setPWM(2, 0, val[2]);
    pwm.setPWM(3, 0, val[3]);
    pwm.setPWM(4, 0, val[4]);
  
    delay(100);
}
