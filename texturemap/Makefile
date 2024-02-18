CC=gcc
FLAGS=-lfreeglut -lopengl32 -lglu32 -lm

all: texturemap_opengl01.exe texturemap_opengl02.exe

texturemap_opengl01.exe: texturemap_opengl01.c
	$(CC) texturemap_opengl01.c $(FLAGS) -o texturemap_opengl01 
texturemap_opengl02.exe: texturemap_opengl02.c
	$(CC) texturemap_opengl02.c $(FLAGS) -o texturemap_opengl02 

clean:
	rm texturemap_opengl01.exe
	rm texturemap_opengl02.exe
