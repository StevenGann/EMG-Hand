void setup() {
  Serial.begin(9600);

}

void loop() {
  Serial.println("=============");
  Serial.println(String(analogRead(A0)));
  Serial.println(String(analogRead(A1)));
  Serial.println(String(analogRead(A2)));
  Serial.println(String(analogRead(A3)));
  Serial.println(String(analogRead(A4)));
  Serial.println("-------------");
  delay(1000);
}
