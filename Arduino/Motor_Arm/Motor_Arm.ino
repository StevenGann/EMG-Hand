//Digital pins
int motorA_1 = 2;
int motorA_2 = 3;
int motorB_1 = 4;
int motorB_2 = 5;

//Motor states
int motorA = 0; //0 = Brake
int motorB = 0; //1 = Forward
                //2 = Backward
int pwmA = 0;
int pwmB = 0;

int currentA = 0;
int currentB = 0;

//Maths
int deadzone = 10;
int xJoy = 50;
int yJoy = 50;
int zJoy = 50;
float x = 0;
float y = 0;
float z = 0;
float theta = 0;
float magnitude = 0;
float pi = 3.141592;

int counter = 0;
                
void updateHbridge()
{
    switch (motorA) 
    {
        case 0:
        analogWrite(motorA_1, 0);
        analogWrite(motorA_2, 0);
        Serial.println("0,0");
        break;
        case 1:
        analogWrite(motorA_1, 0);
        analogWrite(motorA_2, pwmA);
        Serial.println("0," + String(pwmA));
        break;
        case 2:
        analogWrite(motorA_1, pwmA);
        analogWrite(motorA_2, 0);
        Serial.println(String(pwmA) + ",0");
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
    
    currentA = analogRead(A0);
    currentB = analogRead(A1);
    
    //Serial.println(String(currentA) + "," + String(currentB));
    
}

void serialFlush()
{
  while(Serial.available()>0) Serial.read(); 
  //Flushes any other values in the buffer received from the xbee after the first four
}            
                
void setup() 
{
    pinMode(motorA_1, OUTPUT);
    pinMode(motorA_2, OUTPUT);
    pinMode(motorB_1, OUTPUT);
    pinMode(motorB_2, OUTPUT);
    
    Serial.begin(115200);
}

void axisDead(int zone)
{
    if (xJoy > (50 - zone) && xJoy < (50 + zone)){xJoy = 50;}
    if (yJoy > (50 - zone) && yJoy < (50 + zone)){yJoy = 50;}
    if (zJoy > (50 - zone) && zJoy < (50 + zone)){zJoy = 50;}
}


void loop() 
{
    //pwmA = 15;
    //pwmB = 15;
    
    //xJoy = 50;
    //yJoy = 50;
    //zJoy = 50;
    //x = 0;
    //y = 0;
    //z = 0;
    theta = 0;
    magnitude = 0;
    
    while(Serial.available() > 0) //when there's information being received through serial go into this loop
    {
        xJoy = Serial.parseInt(); //takes each value seperated by the commas and defines them to the four different values
        yJoy = Serial.parseInt();
        zJoy = Serial.parseInt();
        //Serial.println("=========" + String(xJoy) + "," + String(yJoy) + "," + String(zJoy));
        serialFlush(); //flushes any other values in the buffer
    }
    //axisDead(deadzone);
    //theta = atan(y/x) * (180.0f/pi);
    int base_val = 95;
    float ax_val = Map(xJoy, 0, 100, -base_val, base_val);
    float bx_val = -ax_val;
    float ay_val = Map(yJoy, 0, 100, -base_val, base_val);
    float by_val = -ay_val;
    float az_val = Map(zJoy, 0, 100, -base_val, base_val);
    float bz_val = az_val;
    
    int a_val = ax_val + ay_val + az_val;
    int b_val = bx_val + by_val + bz_val;
    
    /*Serial.print(String(ax_val) + "," + String(ay_val) + "," + String(az_val));
    Serial.print(",");
    Serial.print(String(bx_val) + "," + String(by_val) + "," + String(bz_val));
    Serial.print("\n");*/
    if (a_val > 255){a_val = 255;}
    if (a_val < -255){a_val = -255;}
    if (b_val > 255){b_val = 255;}
    if (b_val < -255){b_val = -255;}
    //Hbridge mode notes.
    //0 = Brake
    //1 = Forward
    //2 = Backward
    //motorA, motorB
    //pwmA, pwmB
    
    counter += 1;
    if (counter > 100)
    {
        counter = 0;
        //Serial.println(String(ax_val) + "," + String(bx_val));
    }
    
    if (a_val < 5 && a_val > -5)
    {
        motorA = 0;
        pwmA = 0;
    }
    if (a_val >= 5)
    {
        motorA = 1;
        pwmA = a_val;
    }
    if (a_val <= -5)
    {
        motorA = 2;
        pwmA = -a_val;
    }
    
    if (b_val < 5 && b_val > -5)
    {
        motorB = 0;
        pwmB = 0;
    }
    if (b_val >= 5)
    {
        motorB = 1;
        pwmB = b_val;
    }
    if (b_val <= -5)
    {
        motorB = 2;
        pwmB = -b_val;
    }
    
    //Serial.println(String(pwmA) + "," + String(pwmB) + "|" + String(motorA) + "," + String(motorB));
    
    updateHbridge();
}

float Map(float x, float in_min, float in_max, float out_min, float out_max)
{
  return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}
