//Run all calculations

a1 = mouse_y - y;  //Hand y (offset from shoulder)
b1 = mouse_x - x;  //Hand x (offset from shoulder)

c = sqrt(sqr(a1)+sqr(b1));

B1 = arcsin(b1/c); //All angles are in RADIANS

A1 = arcsin(a1/c);

A = arccos((sqr(b)+sqr(c)-sqr(a))/(2*b*c));

B = arccos((sqr(a)+sqr(c)-sqr(b))/(2*a*c));

C = pi - (A + B);

Ax = (pi/2) - (A1 + B);

Ay = pi - (Ax + (pi/2));

ax = a * sin(Ax);  //Elbow x (offset from shoulder)

ay = a * sin(Ay);  //Elbow y (offset from shoulder)



//Turns out I don't need these for now...
//By = 0;
//Bx = 0;
//bx = 0;
//by = 0;
