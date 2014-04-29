/*
  Example for MCP3008 - Library for communicating with MCP3008 Analog to digital converter.
  Created by Uros Petrevski, Nodesign.net 2013
  Released into the public domain.
*/

#include <MCP3008.h>


// define pin connections
#define CLOCK_PIN 38
#define MOSI_PIN 34
#define MISO_PIN 36
#define CS_PIN 32

#define CLOCK_PIN1 37
#define MOSI_PIN1 33
#define MISO_PIN1 35
#define CS_PIN1 31

// put pins inside MCP3008 constructor
MCP3008 adc1(CLOCK_PIN, MOSI_PIN, MISO_PIN, CS_PIN);
MCP3008 adc2(CLOCK_PIN1, MOSI_PIN1, MISO_PIN1, CS_PIN1);
int val[10];
void setup() {
 
 // open serial port
 Serial.begin(9600);
 val[0] = adc1.readADC(0);
 val[1] = adc1.readADC(1);
 val[2] = adc1.readADC(2);
 val[3] = adc1.readADC(3);
 val[4] = adc1.readADC(4);
 val[5] = adc2.readADC(0);
 val[6] = adc2.readADC(1);
 val[7] = adc2.readADC(2);
 val[8] = adc2.readADC(3);
 val[9] = adc2.readADC(4);
  
}


void loop() {
  val[0] = adc1.readADC(0);
  val[1] = adc1.readADC(1);
  val[2] = adc1.readADC(2);
  val[3] = adc1.readADC(3);
  val[4] = adc1.readADC(4);
  val[5] = adc2.readADC(0);
  val[6] = adc2.readADC(1);
  val[7] = adc2.readADC(2);
  val[8] = adc2.readADC(3);
  val[9] = adc2.readADC(4);
  
  
  
  Serial.print(String(val[0])+",");
  Serial.print(String(val[1])+",");
  Serial.print(String(val[2])+",");
  Serial.print(String(val[3])+",");
  Serial.print(String(val[4])+",");
  Serial.print(String(val[5])+",");
  Serial.print(String(val[6])+",");
  Serial.print(String(val[7])+",");
  Serial.print(String(val[8])+",");
  Serial.print(String(val[9])+",");
  Serial.println();
}
