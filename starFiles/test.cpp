// g++ test.cpp -o test -O2 -L/usr/X11R6/lib -lm -lpthread -lX11
// ./test "carte.png"

#include <stdlib.h>
#include <stdio.h>
#include <cmath>
#include <iostream>
#include <sstream>
#include "CImg.h"
using namespace cimg_library;

float distanceFromOrigin(float x, float y) {
	return sqrt( x*x + y*y ) ;
}

int main(int argc, char* argv[]) {

	char filename[250] ;

	if (argc != 2) {
		printf("Usage: <filename> \n");
		exit (1) ;
	}
	sscanf (argv[1], "%s", filename) ;

	CImg<float> img ;
	CImgDisplay disp(800,800,"") ;
	img.load(filename);
	bool redraw = true ;

	float mX, mY, x, y, z ;
	int rayon = 10 ;

	std::stringstream ss ;


	FILE* fichier = fopen("test.txt", "w+");

	while (!disp.is_closed() && !disp.is_keyESC()) {

		if (disp.is_resized()) {
			disp.resize();
			redraw = true;
		}

		if(disp.button()&1 && disp.mouse_x()>=0 && disp.mouse_y()>=0) {
			mX = disp.mouse_x() * ((float) img.width()/disp.width()) ;
			mY = disp.mouse_y() * ((float) img.height()/disp.height()) ;

			//printf("MouseX : %f MouseY : %f \n", mX, mY);

			x = (mX-424) / 42.4 ;
			z = (mY-424) / 42.4 ;
			y = sqrt(rayon*rayon - (pow(distanceFromOrigin(x,z),2))) ;

			printf("x : %f y : %f z : %f \n", x, y, z);

			// Ecriture fichier
			fprintf(fichier, "%f %f %f magnitude Dragon {1, 2} \n", x,y,z) ;

		}
		disp.wait();


		if (redraw) {
			disp.display(img);
		}
		redraw = false ;

	}
	fclose(fichier);

	return 0;

}
