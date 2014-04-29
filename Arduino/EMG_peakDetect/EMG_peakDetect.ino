int input[3][20];
int input_max[3];

void setup() 
{
    int i = 0;
    while (i < 20)
    {
        input[0][i] = analogRead(A0);
        input[1][i] = analogRead(A1);
        input[2][i] = analogRead(A2);
        i++;
    }
    
    Serial.begin(9600);
    Serial.println("Starting");
}

void loop() 
{
    manageInput(input, 10);
}

void manageInput(int data[][20], int len)
{
    int i = 0;
    
    while (i < len)
    {
        data[0][len-(i+1)] = data[1][len-(i+2)];
        data[1][len-(i+1)] = data[1][len-(i+2)];
        data[2][len-(i+1)] = data[1][len-(i+2)];
        i++;
    }
    
    data[0][0] = analogRead(A0);
    data[1][0] = analogRead(A0);
    data[2][0] = analogRead(A0);
    
    if (data[0][0] < data[0][1]){Serial.println("Peak on A0");}
    if (data[0][0] > data[0][1]){Serial.println("No Peak on A0");}
    //if (data[1][0] < data[1][1]){Serial.println("Peak on A1");}
    //if (data[2][0] < data[2][1]){Serial.println("Peak on A2");}
    
}
