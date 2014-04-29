#include <SoftwareSerial.h> 

int outMode = 4;//0: Verbose
                //1: DataPlot Filtered Amplitudes
                //2: DataPlot Envelope
                //3: 4 input pose prediction - REMOVED -
                
                //4: 4 input autocalibration

int sensorValue1 = 0;
int sensorValue2 = 0;
int sensorValue3 = 0;
int sensorValue4 = 0;
int filter1[25];
int filter2[25];
int filter3[25];
int filter4[25];
int clean1 = 0;
int clean2 = 0;
int clean3 = 0;
int clean4 = 0;
float clean_max1 = 0;
float clean_min1 = 1024;
float clean_max2 = 0;
float clean_min2 = 1024;
float clean_max3 = 0;
float clean_min3 = 1024;
float clean_max4 = 0;
float clean_min4 = 1024;

float gestureVals[4][8];
bool calibrated = false;

bool state;
int emg_thresh = 60;

int counter = 0;
int counter2 = 0;

void setup() {
  // declare the ledPin as an OUTPUT:

  Serial.begin(115200);
  
  //Populate 
  filter1[0] = analogRead(A0);
  filter1[1] = analogRead(A0);
  filter1[2] = analogRead(A0);
  filter1[3] = analogRead(A0);
  filter1[4] = analogRead(A0);
  filter1[5] = analogRead(A0);
  filter1[6] = analogRead(A0);
  filter1[7] = analogRead(A0);
  filter1[8] = analogRead(A0);
  filter1[9] = analogRead(A0);
  
  filter2[0] = analogRead(A1);
  filter2[1] = analogRead(A1);
  filter2[2] = analogRead(A1);
  filter2[3] = analogRead(A1);
  filter2[4] = analogRead(A1);
  filter2[5] = analogRead(A1);
  filter2[6] = analogRead(A1);
  filter2[7] = analogRead(A1);
  filter2[8] = analogRead(A1);
  filter2[9] = analogRead(A1);
  
  filter3[0] = analogRead(A2);
  filter3[1] = analogRead(A2);
  filter3[2] = analogRead(A2);
  filter3[3] = analogRead(A2);
  filter3[4] = analogRead(A2);
  filter3[5] = analogRead(A2);
  filter3[6] = analogRead(A2);
  filter3[7] = analogRead(A2);
  filter3[8] = analogRead(A2);
  filter3[9] = analogRead(A2);
  
  filter4[0] = analogRead(A3);
  filter4[1] = analogRead(A3);
  filter4[2] = analogRead(A3);
  filter4[3] = analogRead(A3);
  filter4[4] = analogRead(A3);
  filter4[5] = analogRead(A3);
  filter4[6] = analogRead(A3);
  filter4[7] = analogRead(A3);
  filter4[8] = analogRead(A3);
  filter4[9] = analogRead(A3);
  
  //Digital IO for the calibration interface
  pinMode(10, OUTPUT);//Speaker
  pinMode(11, OUTPUT);//Blue LED
  pinMode(12, OUTPUT);//Green LED
  pinMode(13, OUTPUT);//Red LED
}

void readAndFilter()
{
	// read the value from the sensor:
  sensorValue1 = analogRead(A0);
  sensorValue2 = analogRead(A1);
  sensorValue3 = analogRead(A2);
  sensorValue4 = analogRead(A3);
  
  clean1 = filterAverage(sensorValue1, filter1, 20);
  clean2 = filterAverage(sensorValue2, filter2, 20);
  clean3 = filterAverage(sensorValue3, filter3, 20);
  clean4 = filterAverage(sensorValue4, filter4, 20);
  
  adaptiveMaxMin(clean1, clean_max1, clean_min1);
  adaptiveMaxMin(clean2, clean_max2, clean_min2);
  adaptiveMaxMin(clean3, clean_max3, clean_min3);
  adaptiveMaxMin(clean4, clean_max4, clean_min4);
  
  if (clean1 >= emg_thresh){state = 1;}
  if (clean1 < emg_thresh) {state = 0;}
  
  counter += 1;
  counter2 += 1;
  
  if (counter2 >= 100)
  {
	resetMaxMin(clean1, clean_max1, clean_min1);
    resetMaxMin(clean2, clean_max2, clean_min2);
    resetMaxMin(clean3, clean_max3, clean_min3);
    resetMaxMin(clean4, clean_max4, clean_min4);
  }
  
  return;
}

void storeGesture(int gestureID)
{
	//Inputs: clean1, clean2, clean3, clean4
	//Values: 0 = ~Equal, 1 = Less than, 2 = Greater than
	//~Equal represents being equal within a certain percentage.
	
	//gestureVals[n][0] = clean1 (?) clean2
	//gestureVals[n][1] = clean1 (?) clean3
	//gestureVals[n][2] = clean1 (?) clean4
	//gestureVals[n][3] = clean2 (?) clean3
	//gestureVals[n][4] = clean2 (?) clean4
	//gestureVals[n][5] = clean3 (?) clean4

	gestureVals[gestureID][0] = findRelation(clean1, clean2);
	gestureVals[gestureID][1] = findRelation(clean1, clean3);
	gestureVals[gestureID][2] = findRelation(clean1, clean4);
	gestureVals[gestureID][3] = findRelation(clean2, clean3);
	gestureVals[gestureID][4] = findRelation(clean2, clean4);
	gestureVals[gestureID][5] = findRelation(clean3, clean4);
	
	return;
}

bool matchesProfile(int gestureID)
{
	bool r = false;
	int score = 0;
	
	if (gestureVals[gestureID][0] == findRelation(clean1, clean2))
	{
		score += 1;
	}
	if (gestureVals[gestureID][1] == findRelation(clean1, clean3))
	{
		score += 1;
	}
	if (gestureVals[gestureID][2] == findRelation(clean1, clean4))
	{
		score += 1;
	}
	if (gestureVals[gestureID][3] == findRelation(clean2, clean3))
	{
		score += 1;
	}
	if (gestureVals[gestureID][4] == findRelation(clean2, clean4))
	{
		score += 1;
	}
	if (gestureVals[gestureID][5] == findRelation(clean3, clean4))
	{
		score += 1;
	}
	
	if (score >= 6)
	{
		r = true;
	}
	
	return r;
}

int findRelation(int A, int B)
{
	int r = 0;
	float margin = 0.15f; //10% margin of error for relationships.
	
	if (isGreater(A, B, margin))
	{
		r = 2;
	}
	
	if (isLess(A, B, margin))
	{
		r = 1;
	}
	
	if (isEqual(A, B, 0.10f))
	{
		r = 0;
	}
	
	return r;
}

bool isEqual(int A, int B, float margin)
{
	bool r = false;
	
	if (A > (B*(1.0f - margin)) && A < (B*(1.0f + margin)))
	{
		r = true;
	}
	
	if (B > (A*(1.0f - margin)) && B < (A*(1.0f + margin)))
	{
		r = true;
	}
	
	return r;
}

bool isGreater(int A, int B, float margin)
{
	bool r = false;
	
	if (A > (B*(1.0f + margin)))
	{
		r = true;
	}
	
	return r;
}

bool isLess(int A, int B, float margin)
{
	bool r = false;
	
	if (A < (B*(1.0f - margin)))
	{
		r = true;
	}
	
	return r;
}


void loop() {

    readAndFilter();
  
  
  if (counter >= 100)
  {
      counter = 0;
      
      if (outMode == 0)
      {
          Serial.print("[");
          Serial.print(String(clean1));
          Serial.print("]---[");
          Serial.print("Max: ");
          Serial.print(String(clean_max1));
          Serial.print(". Min: ");
          Serial.print(String(clean_min1));
          Serial.print(". Range: ");
          Serial.print(String(clean_max1 - clean_min1));
          Serial.print(". Raw: ");
          Serial.print(String(sensorValue1));
          
          if (state == 1)
          {
              Serial.print(". Closed. ");
              //myservo.write(135);
          }
          if (state == 0)
          {
              Serial.print(". Open.   ");
              //myservo.write(45);
          }
          
          Serial.print("Error: ");
          Serial.print(String(sensorValue1 - clean1));
          Serial.print("]\n");
      }
      
      if (outMode == 1)
      {
          Serial.print(String(clean1));Serial.print(",");
          Serial.print(String(clean2));Serial.print(",");
          Serial.print(String(clean3));Serial.print(",");
          Serial.print(String(clean4));Serial.print("\n");
      }
      
      if (outMode == 2)
      {
          Serial.print(String(clean_max1));Serial.print(",");
		  Serial.print(String(clean_min1));Serial.print(",");
          Serial.print(String(clean_max2));Serial.print(",");
		  Serial.print(String(clean_min2));Serial.print(",");
		  Serial.print(String(clean_max3));Serial.print(",");
		  Serial.print(String(clean_min3));Serial.print(",");
		  Serial.print(String(clean_max4));Serial.print(",");
		  Serial.print(String(clean_min4));Serial.print("\n");
      }
      
	  //Manual detection values. Tedious, inaccurate.
      //REMOVED
      
	  //Automated detection value calibration sequence
      if (outMode == 4)
      {
          if (calibrated == false)
          {
              Serial.println("Starting calibration sequence.");
			  Serial.println("You'll be prompted for a hand pose. When prompted, make and hold that pose without straining.");
			  Serial.println("If the calibration must be repeated, reset the EMG unit.");
			  Serial.println("------------------------------------");
			  Serial.println();
			  delay(500);
			  
			  //10 - Speaker
			  //11 - Blue
			  //12 - Green
			  //13 - Red
			  //-----------------------------------------------------------------
			  Serial.println("Pose: Neutral");
			  digitalWrite(13, HIGH);//Red light on
			  delay(1000);
			  for (int i=0; i<1000; i++)//Read through 50 iterations
			  {
                            readAndFilter();
                            Serial.print(String(clean1));Serial.print(",");
                            Serial.print(String(clean2));Serial.print(",");
                            Serial.print(String(clean3));Serial.print(",");
                            Serial.print(String(clean4));Serial.print("\n");
                            delay(10);
                          }
			  storeGesture(0);
			  Serial.println("Pose: Neutral - Stored!");
			  digitalWrite(13, LOW);//Red light off
			  digitalWrite(12, HIGH);//Green light on
			  delay(1000);
			  digitalWrite(12, LOW);//Green light off
			  //-----------------------------------------------------------------
			  Serial.println("Pose: Fist");
			  digitalWrite(13, HIGH);//Red light on
			  delay(1000);
			  for (int i=0; i<1000; i++)//Read through 50 iterations
			  {
                            readAndFilter();
                            Serial.print(String(clean1));Serial.print(",");
                            Serial.print(String(clean2));Serial.print(",");
                            Serial.print(String(clean3));Serial.print(",");
                            Serial.print(String(clean4));Serial.print("\n");
                            delay(10);
                          }
			  storeGesture(1);
			  Serial.println("Pose: Fist - Stored!");
			  digitalWrite(13, LOW);//Red light off
			  digitalWrite(12, HIGH);//Green light on
			  delay(5000);
			  digitalWrite(12, LOW);//Green light off
                          //-----------------------------------------------------------------
                          Serial.println("Pose: Pinch");
			  digitalWrite(13, HIGH);//Red light on
			  delay(1000);
			  for (int i=0; i<1000; i++)//Read through 50 iterations
			  {
                            readAndFilter();
                            Serial.print(String(clean1));Serial.print(",");
                            Serial.print(String(clean2));Serial.print(",");
                            Serial.print(String(clean3));Serial.print(",");
                            Serial.print(String(clean4));Serial.print("\n");
                            delay(10);
                          }
			  storeGesture(2);
			  Serial.println("Pose: Pinch - Stored!");
			  digitalWrite(13, LOW);//Red light off
			  digitalWrite(12, HIGH);//Green light on
			  delay(5000);
			  digitalWrite(12, LOW);//Green light off
			  //-----------------------------------------------------------------
			  digitalWrite(11, HIGH);//Blue light on. Calibrated!
			  calibrated = true;
          }
          if (calibrated == true)
          {
            
            Serial.print(String(clean1));Serial.print(",");
            Serial.print(String(clean2));Serial.print(",");
            Serial.print(String(clean3));Serial.print(",");
            Serial.print(String(clean4));Serial.print(",");
            
	      if (matchesProfile(0))
	      {
	        Serial.print("Gesture 0 ");
	      }
				
	      if (matchesProfile(1))
	      {
		Serial.print("Gesture 1 ");
	      }

              if (matchesProfile(2))
	      {
		Serial.print("Gesture 2 ");
	      }

              Serial.println();
          }
      }
   }
}


/* int filterAverage(int raw, int data[], int len)
|
| Description: Returns filtered value based on averaging against previous values.
|
| raw:    The value to be filtered.
| data[]: Array to server as a buffer for previous values.
| len:    Number of elements to include in average. MUST be smaller or equal to the size of data[]!
*/
int filterAverage(int raw, int data[], int len)
{
    int i = 0;
    int sum = 0; 
    
    while (i < len)
    {
        data[len-(i+1)] = data[len-(i+2)];
        i++;
    }
    
    data[0] = raw;
    
    i = 0;
    while (i < len)
    {
        sum += data[i];
        i++;
    }
    
    data[0] = sum/len;
    
    return data[0];
}

int filterWeighted(int raw, int data[], int len)
{
	double sum=0;
	double weight[len];//Figure out how to replace this!!!
	double weightSum=0;
        int foo = 0;
        
	
	// Move everything to the end. This may be backwards. Need to recheck soon.
	for(int i=0; i < len; i++)
        {
		data[len-(i+1)] = data[len-i+2];
        }

	data[0] = raw;
	
	
	//Fill the weights
	for(int i=len; i > 0; i--,foo++)
	{
		weight[i] = 1/len-foo;
	}
	
	// Sum everything
	for(int i=0; i < 0; i++)
	{
		sum       += weight[i] * (double)data[i];
		weightSum += weight[i];
	}
	
	return (int)(sum/weightSum);
}



void analogMaxMin(int value, float& value_max, float& value_min)
{
    if (value > value_max){value_max = value;}
    if (value < value_min){value_min = value;}
}
void analogMaxMin(int value, int& value_max, int& value_min)
{
    if (value > value_max){value_max = value;}
    if (value < value_min){value_min = value;}
}

void adaptiveMaxMin(int value, float& value_max, float& value_min)
{
    value_max -= 0.01f;
    value_min += 0.01f;
    if (value > value_max){value_max += 0.05f;}
    if (value < value_min){value_min -= 0.05f;}
    //if (value > value_max){value_max = value;}
    //if (value < value_min){value_min = value;}
}

void resetMaxMin(int value, float& value_max, float& value_min)
{
	value_max = value;
	value_min = value;
}


