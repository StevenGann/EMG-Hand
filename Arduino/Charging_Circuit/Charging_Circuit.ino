
#include <SPI.h>
#include <LiquidCrystal.h>
#include <EEPROM.h>

LiquidCrystal lcd(12, 11, 5, 4, 3, 2);

int res, savecharge, time, old_time, lastcharge;
int new_in = 0, new_out = 0, in, out;
int analogPin = 0;
int address1 = 10;
int address2 = 11;
//int reset = 6;
byte value;
//int interrupt = 2;
volatile int state = LOW;
float charge = 40.000000000;
float percentage = 100;
float current_in, y, voltage_in, signed_current_in, x;
char sign, charging, discharging;
//int percent_change;



void setup() 
{  
  //Serial.begin(9600);
  
  res = 0;
  //pinMode(interrupt, INPUT);
  //attachInterrupt(0, reset, HIGH);
  value = (float) EEPROM.read(address1);
  lastcharge = (float) EEPROM.read(address2);
  if (lastcharge > .1 & lastcharge < 40.01)
  {
    charge = lastcharge;
  }
  //percentage = int (value);
  
  
  lcd.begin(16,2);
  lcd.print("charge:");
  lcd.print(value);
  lcd.print("%");
  lcd.setCursor(0, 1);
  lcd.print("i:");
  lcd.print(current_in);
  lcd.print("A");
  
  //pinMode(reset, INPUT);

  
}

void loop() 
{

  
  lcd.setCursor(0, 1);
  
  //attach interrupt for reset (not used)
  if(res != 0)
  {
    //print reset
    lcd.clear();
    lcd.print("RESET!");
    res = 0;
    delay(3000);
    lcd.clear();
    percentage = 100;
    lcd.print("display %");
    //reset value to 100%
  }


//begin current sensor code

  in = analogRead(analogPin);
  float x = float (in);
  float y = float (x/ 1023);
  float voltage_in = float(float(y)*5);
  signed_current_in = ((voltage_in-2.5)/.04);
  current_in = abs(signed_current_in);
 
  
  //   (voltage-2.5V offset)/(40 mV/A)


if(current_in > .05 )
{
     if (signed_current_in > 0)
     {
       charge = (charge + (current_in*0.000139f));
     }
     else
     {
       charge = (charge - (current_in*0.000139f));
     }
     //charge = old charge + current change* time measured
     
     percentage = ((charge/40.0f)*100.0f);
     if (percentage > 100)
     {
      percentage = 100; 
     }
   
     lcd.clear();
     lcd.print("charge:");
     lcd.print(percentage);
     lcd.print("%"); 
     //new line
     lcd.setCursor(0, 1);
     if (signed_current_in > 0 )
       {
         lcd.print("i:+");
       }
     else
       {
         lcd.print("i:-");
       }
     lcd.print(current_in);
     lcd.print("A");
   
     //for (int positionCounter = 0; positionCounter < 29; positionCounter++) 
     //{
     // scroll one position right:
     //lcd.scrollDisplayRight(); 
     //}
    
    
}
  
if (current_in < .05)
  {
   lcd.clear();
   lcd.print("charge:");
   lcd.print(percentage);
   lcd.print("%");
   lcd.setCursor(0, 1);
   if (signed_current_in > 0 )
       {
         lcd.print("i:+");
       }
   else
       {
         lcd.print("i:-");
       }
   lcd.print(current_in);
   lcd.print("A");
  }
 
  new_in = in;
  new_out = out;
  
  
  //save to EEProm code
  value =  (byte) percentage;
  lastcharge = (byte) charge;
  delay(500);
  time = millis();
  savecharge = (time - old_time);
  if ( savecharge > 30000)
     {
       EEPROM.write(address1, value);
       EEPROM.write(address2, lastcharge);
     } 
  old_time = time;
  
  if ( reset == 0)
    {
      delay(2000);
      if (reset == 0 )
         { 
            lcd.clear();
            percentage = 100;
            lcd.print("RESET!");
            delay(2000);
            lcd.print("charge:");
            lcd.print(percentage);
            lcd.print("%");
         }
    }
}
//Serial.println("in/1023");
//Serial.println(y);
//Serial.println("analogread");
//Serial.println(in);
//Serial.println("voltage");
//Serial.println(voltage_in);
//Serial.println("current");
//Serial.println(current_in);
//Serial.println("charge");
//Serial.println(charge);
//Serial.println("percentage");
//Serial.println(percentage);


void reset()
{
 //reset charge to full 
 //display reset
 res = 1;
}
//end of lcd code




