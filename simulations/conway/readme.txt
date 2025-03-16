Conway - 2001 Levi D. Smith
This is an implementation of Conway's Game of Life in Allegro that I wrote sometime while I was at Gerogia Tech.
It was not a class project, just for fun.

Required:  DOSBox, DJGPP compiler, Allegro (4)

Download DJGPP from https://www.delorie.com/djgpp/zip-picker.html
Select MS-DOS, OpenDOS, PC-DOS
Important - Under Extra Stuff, check Toolkits > Allegro - game graphics/sound/keyboard
(Trying to compile Allegro 4 from source would be a nightmare.  Using the Allegro packages with the 
DJGPP ZIP picker is much easier)

Create "DJGPP" folder next to the "CONWAY" folder
Extract all zip files into the same DJGPP folder

Run DOSBox and run the following
mount c <drive>:\<path_to_files>
Example
mount c d:\ldsmith\presentations\demos\simulations

Run the following from the DOSBox prompt
set PATH=s:\djgpp\bin;%PATH%
set DJGPP=s:\djgpp\djgpp.env

Run make.bat or
gcc conway.c -oconway -lalleg
