int shoulderpin = A0; //input from arm
int forearmpin = A1; // input from arm
int shoulder_control = A2; //pots on user side
int forearm_control = A3;
int shouldercontrol;
int forearmcontrol;
int shoulderout1 = 5;  //to motor driver
int forearmout1 = 6;
int shoulderout2 = 9;  //to motor driver in opposite direction
int forearmout2 = 11;
int forearm;
int shoulder;

int shouldermax =627;

int shouldermin = 352;

int forearmmax = 900;
int forearmmid = 820;
int forearmmin = 515 ;

void setup() {
  // put your setup code here, to run once:
Serial.begin(9600); 
pinMode(shoulderout1, OUTPUT);
pinMode(forearmout1, OUTPUT);
pinMode(shoulderout2, OUTPUT);
pinMode(forearmout2, OUTPUT);

}

void loop() {
  // put your main code here, to run repeatedly:

 shoulder = analogRead(shoulderpin);
 forearm = analogRead(forearmpin);
 shouldercontrol = analogRead(shoulder_control);
 forearmcontrol = analogRead(forearm_control);
 
 //int olds = 
 
 Serial.println(String(potToAngle_forearm(shoulder)) + "," + String(potToAngle_shoulder(forearm)));
 
  analogWrite(shoulderout1 , 0);
  analogWrite(shoulderout2 , 0);
  analogWrite(forearmout1 , 0);
  analogWrite(forearmout2 , 0);


if (shouldercontrol >= 800 )
{
  analogWrite(shoulderout1 , 50);
  analogWrite(shoulderout2 , 0);
  //Serial.println("shoulder up");
}

if (shouldercontrol <= 250 )
{
  analogWrite(shoulderout1 , 0);
  analogWrite(shoulderout2, 50);
  //Serial.println("shoulder down");
  
}

if (forearmcontrol >= 800)      //forearm control for one direction
{
  analogWrite(forearmout1 , 90);
  analogWrite(forearmout2 , 0);
   //Serial.println("forearm up");
}
if (forearmcontrol <= 250)    //forearm control for other direction
{
  analogWrite(forearmout1 , 0);
  analogWrite(forearmout2 , 90);
   //Serial.println("forearm down");
}

}

int potToAngle_shoulder(int p)
{
    int a = 0;
    a = (0.2682f * (float)p) - 142.6f;
    return a;
}

int potToAngle_forearm(int p)
{
    int a = 0;
    a = (0.2256f * (float)p) - 186.1f;
    return a;
}

int angleToPot_shoulder(int a)
{
    int p = 0;
    p = (3.494f * (float)a) + 498.253f;
    return p;
}

int angleToPot_forearm(int a)
{
    int p = 0;
    p = (4.4326f * (float)a) + 824.911;
    return p;
}
