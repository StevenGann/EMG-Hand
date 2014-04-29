// Senior Design
// PSU

//int xjstick = A4; //for testing  
//int yjstick = A5; //for testing
int HL = 2;
//int RGB = 3;
int fan = 9;
//int R_fwd =11;
//int R_rev = 10;
//int L_fwd = 6; 
//int L_rev = 5;
//float I_basemotors = 0;
int Temp = 0;
//int fansignal = 0; // initializes the fan signal
int lightVal = 0;

//---H-Bridge Stuff---
//Digital pins
int motorA_1 = 11;
int motorA_2 = 10;
int motorB_1 = 6;
int motorB_2 = 5;

//Motor states
int motorA = 0; //0 = Brake
int motorB = 0; //1 = Forward
                //2 = Backward
int pwmA = 0;//PWM values
int pwmB = 0;

void setup() 
{ 
 pinMode (HL, OUTPUT);
 //pinMode (RGB, OUTPUT);
 pinMode (fan, OUTPUT);
 pinMode (motorA_1, OUTPUT);
 pinMode (motorA_2, OUTPUT);
 pinMode (motorB_1, OUTPUT);
 pinMode (motorB_2, OUTPUT);
 
 Serial.begin(115200);
} 
 
void loop() 
{ 
  int temp = analogRead(A0);       // read temperature from volt divider 0-1023 value
  //int I_6V = analogRead(A1);       // measure current from 6V SVR output (and 5V): 0-1023: 61 mV/level 
  //int I_motor1 = analogRead(A2);   // measure current from right base motor 0-1023
  //int I_motor2 = analogRead(A3);   // measure it from left base motor
  //float current_6V = I_6V*0.061; // current in amperes
  //float current_leftmotor = I_motor1*0.061;
  //float current_rightmotor = I_motor2*0.061;
  //int x = analogRead(xjstick); // origin is at 512
  //int y = analogRead(yjstick);
  
  Temp = analogRead(A0);// relate temp value to actual temperature
  
  int fansignal = map(Temp, 450, 735, 0, 255);
  
  analogWrite(fan, fansignal);
  
  readInput();
  updateHbridge();
  //I_basemotors = (((float)I_motor1 + (float)I_motor2)/16.0f) + 128.0f; // averages the currents drawn by the motors and offsets it at 128
  //analogWrite(RGB, I_basemotors);
  
  if (lightVal >= 1)//Turns LED on if button is pressed
  {
      digitalWrite(HL, HIGH);
  } 
  else 
  {
      digitalWrite(HL, LOW);
  }
  
}

void readInput()
{
    while(Serial.available() > 0) //when there's information being received through serial go into this loop
    {
        pwmA = Serial.parseInt();
        pwmB = Serial.parseInt();
        lightVal = Serial.parseInt();
        serialFlush();
    }
    Serial.println(String(pwmA) + ", " + String(pwmB) + ", " + String(lightVal));
}
  
void serialFlush()
{
  while(Serial.available()>0) Serial.read(); 
  //Flushes any other values in the buffer received from the xbee after the first four
}

void updateHbridge()
{
	//Give specific values for motor state and PWM duty cycle,
	//this code will interface with Juan's custom H-bridge design.
    motorA = 0;
    motorB = 0;
    
    if (pwmA > 0)
    {
        motorA = 1;
    }
    if (pwmA < 0)
    {
        motorA = 2;
    }
    if (pwmB > 0)
    {
        motorB = 1;
    }
    if (pwmB < 0)
    {
        motorB = 2;
    }
    
    pwmA = abs(pwmA);
    pwmB = abs(pwmB);
    
    switch (motorA)
    {
        case 0:
        analogWrite(motorA_1, 0);
        analogWrite(motorA_2, 0);
        break;
        case 1:
        analogWrite(motorA_1, 0);
        analogWrite(motorA_2, pwmA);
        break;
        case 2:
        analogWrite(motorA_1, pwmA);
        analogWrite(motorA_2, 0);
        break;
    }
    
    switch (motorB) 
    {
        case 0:
        analogWrite(motorB_1, 0);
        analogWrite(motorB_2, 0);
        break;
        case 1:
        analogWrite(motorB_1, 0);
        analogWrite(motorB_2, pwmB);
        break;
        case 2:
        analogWrite(motorB_1, pwmB);
        analogWrite(motorB_2, 0);
        break;
    }
}
