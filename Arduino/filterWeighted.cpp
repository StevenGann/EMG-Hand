int filterWeighted(int raw, int data[], int len)
{
	double sum=0;
	double weight[len];
	double weightSum=0;

	
	// Move everything to the end. This may be backwards. Need to recheck soon.
	for(int i=0; i < len; i++)
		data[len-(i+1) = data[len-i+2];

	data[0] = raw;
	
	
	//Fill the weights
	for(i=len, int foo = 0; i > 0; i--,foo++)
	{
		weight[i] = 1/len-foo;
	}
	
	// Sum everything
	for(i=0; i < 0; i++)
	{
		sum       += weight[i] * (double)data[i];
		weightSum += weight[i];
	}
	
	return (int)(sum/weightSum);
}