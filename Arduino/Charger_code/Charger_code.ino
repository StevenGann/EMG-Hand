float voltage;
float voltage_plus;
float current;
int voltagepin = A0;
int currentpin = A1;
int dutypin = 11; //Output pin
int duty;//Duty cycle


 
void setup(){

Serial.begin(9600);

pinMode(dutypin, OUTPUT);
pinMode(voltagepin, INPUT);
pinMode(currentpin, INPUT);


}
 void loop()
{ 
  
  voltage_plus = (((analogRead(voltagepin))/1023.0)*17.2);

  current = (((analogRead(currentpin)/1023.0)*62.5)-6.25);
 
 
  if (current>=0.5)
  {
    digitalWrite(dutypin,HIGH);
    
  }

  if (current <0.5)
  
  {
   digitalWrite(dutypin,LOW);
    
  } 
     
  

Serial.print("voltage = ");
Serial.println(voltage_plus);
Serial.print("current = ");
Serial.println(current);
Serial.print("Duty Cycle= ");
Serial.println(duty);
delay(200);
 

}
  
