#include <SoftwareSerial.h> 

int outMode = 2;//0: Verbose
                //1: DataPlot Filtered Amplitudes
                //2: DataPlot Envelope
                //3: 4 input pose prediction
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
	gestureVals[gestureID][0] = 0;
	gestureVals[gestureID][1] = 0;
	gestureVals[gestureID][2] = 0;
	gestureVals[gestureID][3] = 0;
	gestureVals[gestureID][4] = 0;
	gestureVals[gestureID][5] = 0;
	gestureVals[gestureID][6] = 0;
	gestureVals[gestureID][7] = 0;
	
	return;
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
      if (outMode == 3)
      {
          if (clean_max1 <= 125 && clean_min1 >= 60 && clean_max2 <= 125 && clean_min2 >= 60 && clean_max3 <= 100 && clean_min3 >= 40 && clean_max4 <= 300 && clean_min4 >= 175)
          {
              Serial.println("Index");
          }
          if (clean_max1 <= 325 && clean_min1 >= 190 && clean_max2 <= 350  && clean_min2 >= 210 && clean_max3 <= 400 && clean_min3 >= 250 && clean_max4 <= 600 && clean_min4 >= 250)
          {
              Serial.println("Middle");
          }
          if (clean_max1 <= 230 && clean_min1 >= 130  && clean_max2 <= 250 && clean_min2 >= 110 && clean_max3 <= 100 && clean_min3 >= 25 && clean_max4 <= 250 && clean_min4 >= 160)
          {
              Serial.println("Middle, index and thumb(pinching)");
          }
          if (clean_max1 <= 900 && clean_min1 >= 500 && clean_max2 <= 300 && clean_min2 >= 100 && clean_max3 <= 200 && clean_min3 >= 0 && clean_max4 <= 700 && clean_min4 >= 300)
          {
              Serial.println("Ring");
          }
		  
          if (clean_max1 <= 150 && clean_max2 <= 150 && clean_max3 <= 150 && clean_max4 <= 150 )
          {
              Serial.println("open");
          }
         //Serial.println("Max1="+String(clean_max1)+"Min1="+String(clean_min1)+"Max2="+String(clean_max2)+"Min2="+String(clean_min2)+"Max3="+String(clean_max3)+"Min3="+String(clean_min3)+"Max4="+String(clean_max4)+"Min4="+String(clean_min4));
      }
      
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
			  for (int i=0; i<250; i++)//Read through 50 iterations
			  {
                            readAndFilter();
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
			  for (int i=0; i<250; i++)//Read through 50 iterations
			  {
                            readAndFilter();
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
			  for (int i=0; i<250; i++)//Read through 50 iterations
			  {
                            readAndFilter();
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
			  if (clean_max1 <= gestureVals[0][0] && 
				  clean_min1 >= gestureVals[0][1] && 
				  clean_max2 <= gestureVals[0][2] && 
				  clean_min2 >= gestureVals[0][3] && 
				  clean_max3 <= gestureVals[0][4] && 
				  clean_min3 >= gestureVals[0][5] && 
				  clean_max4 <= gestureVals[0][6] && 
				  clean_min4 >= gestureVals[0][7])
				{
					Serial.println("Gesture 0");
				}
				
			  if (clean_max1 <= gestureVals[1][0] && 
				  clean_min1 >= gestureVals[1][1] && 
				  clean_max2 <= gestureVals[1][2] && 
				  clean_min2 >= gestureVals[1][3] && 
				  clean_max3 <= gestureVals[1][4] && 
				  clean_min3 >= gestureVals[1][5] && 
				  clean_max4 <= gestureVals[1][6] && 
				  clean_min4 >= gestureVals[1][7])
				{
					Serial.println("Gesture 1");
				}

                           if (clean_max1 <= gestureVals[2][0] && 
				  clean_min1 >= gestureVals[2][1] && 
				  clean_max2 <= gestureVals[2][2] && 
				  clean_min2 >= gestureVals[2][3] && 
				  clean_max3 <= gestureVals[2][4] && 
				  clean_min3 >= gestureVals[2][5] && 
				  clean_max4 <= gestureVals[2][6] && 
				  clean_min4 >= gestureVals[2][7])
				{
					Serial.println("Gesture 2");
				}
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


