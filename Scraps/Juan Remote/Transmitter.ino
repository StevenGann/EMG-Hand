//Xbee Transmitter for Airforce Juan

//Define the pins and variables
#define xJoyPin A0
#define yJoyPin A1
#define auxButtonPin A2
#define speedPotPin A3

unsigned int xJoy;
unsigned int yJoy;
unsigned int speedPot;
unsigned int auxButton;

void setup()
{
  Serial.begin(9600);
  pinMode(xJoyPin, INPUT);
  pinMode(yJoyPin, INPUT);
  pinMode(auxButtonPin, INPUT);
  pinMode(speedPotPin, INPUT);
}

void loop()
{
  xJoy = analogRead(xJoyPin); //read all analog values
  yJoy = analogRead(yJoyPin);
  auxButton = analogRead(auxButtonPin);
  speedPot = analogRead(speedPotPin);
  sendData();
  delay(25); //give some time for the receiver to get the string
}

void sendData()
{
  Serial.print(xJoy); //Write values of all four sensors through one string of serial separated by commas
  Serial.print(',');
  Serial.print(yJoy);
  Serial.print(',');
  Serial.print(auxButton);
  Serial.print(',');
  Serial.print(speedPot);
  Serial.print(',');
}
