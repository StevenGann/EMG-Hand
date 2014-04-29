unsigned long lastmillis = 0;
unsigned long newmillis = 0;
unsigned long deltatime = 0;

float freq = 0;

void setup() 
{
    Serial.begin(115200);
}

void loop() 
{
    newmillis = micros();
    deltatime = lastmillis-newmillis;
    lastmillis = newmillis;
    
    freq = 1.0f/(((float)deltatime) * (10^-6));
    
    Serial.print(analogRead(A0)); Serial.print(",");
    Serial.print(String(deltatime)); 
    Serial.print("\n");
}
