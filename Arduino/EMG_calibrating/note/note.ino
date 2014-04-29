void setup() {
  // put your setup code here, to run once:
    pinMode(10, OUTPUT);//Speaker
    pinMode(11, OUTPUT);//Blue
    pinMode(12, OUTPUT);//Green
    pinMode(13, OUTPUT);//Red
}

void loop() {
  // put your main code here, to run repeatedly:
    //digitalWrite(10, HIGH);
    delay(100);
    digitalWrite(10, LOW);
    digitalWrite(11, HIGH);
    delay(500);
    digitalWrite(11, LOW);
    digitalWrite(12, HIGH);
    delay(500);
    digitalWrite(12, LOW);
    //digitalWrite(13, HIGH);
    delay(500);
    digitalWrite(13, LOW);

}
