void setup()
{
    //First clear all three prescaler bits:
    int prescalerVal = 0x07; //create a variable called prescalerVal and set it equal to the binary number "00000111"
    TCCR0B &= ~prescalerVal; //AND the value in TCCR0B with binary number "11111000"
    //Now set the appropriate prescaler bits:
    int prescalerVal = 1; //set prescalerVal equal to binary number "00000001"
    TCCR0B |= prescalerVal; //OR the value in TCCR0B with binary number "00000001"
}

void loop()
{
  // put your main code here, to run repeatedly:

}
