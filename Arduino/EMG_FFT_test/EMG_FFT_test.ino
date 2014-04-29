

#include <fix_fft.h>

char im[128];
char data[128]; 

int counter = 0;

void setup()
{
    Serial.begin(9600);
}

void loop(){
  int static i = 0;
  static long tt;
  int val;
   
   if (millis() > tt){
      if (i < 128){
        val = analogRead(A0);
        Serial.print((int)val); Serial.print("\n"); delay(100);
        data[i] = val / 4 - 128;
        im[i] = 0;
        i++;   
        
      }
      else{
          
        //this could be done with the fix_fftr function without the im array.
        fix_fft(data,im,7,0);
        // I am only interessted in the absolute value of the transformation
        for (i=0; i< 64;i++){
           data[i] = sqrt(data[i] * data[i] + im[i] * im[i]); 
        }
        
        /*Serial.print((int)data[16]); Serial.print(",");
        Serial.print((int)data[32]); Serial.print(",");
        Serial.print((int)data[64]); Serial.print(",");
        Serial.print((int)data[96]); Serial.print(",");
        Serial.print((int)data[127]);
        Serial.print("\n");*/
      }
    
    tt = millis();
   }
}
