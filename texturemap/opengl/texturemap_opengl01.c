//2024 Levi D. Smith
#include <stdio.h>
#include <stdlib.h>
#include <GL/gl.h>
#include <GL/glu.h>
#include <GL/glut.h>

unsigned char *load_RGB_data(char *filename) {
  unsigned char header[54];
  unsigned int dataPos;
  unsigned int width, height;
  unsigned int imageSize;
  unsigned char *data;

  FILE *file = fopen(filename, "rb");
  if (!file) {
    printf("File could not be opened: %s\n", filename);
    exit(1);
  }
  if (fread(header, 1, 54, file) != 54) {
    printf("Error in BMP file\n");
    exit(1);
  }

  if (header[0] != 'B' || header[1] != 'M') {
    printf("Not a BMP file\n");
    exit(1);    
  }

  dataPos   = *(int*)&(header[0x0A]);
  imageSize = *(int*)&(header[0x22]);
  width     = *(int*)&(header[0x12]);
  height    = *(int*)&(header[0x16]);

//  printf("dataPos: %d\n", dataPos);
  data = malloc((width * height * 3));
  fseek(file, dataPos, SEEK_SET);
  fread(data, 1, width * height * 3, file);
  fclose(file);

  int i = 0;
  int r, g, b;
  while (i < imageSize) {
    r = data[i + 2];
    g = data[i + 1];
    b = data[i + 0];

    data[i + 0] = r;
    data[i + 1] = g;
    data[i + 2] = b;
    i += 3;
  }


  return data; 
}

void init(void) {
  glClearColor(0.392, 0.584, 0.929, 1.0);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();


  gluPerspective(60, 1.7778, 0.1, 50);
  glViewport(0, 0, 1280 / 2, 720 / 2);

  gluLookAt(0.5, 0.5,  -1, 
            0.5, 0.5,  0,
            0, 1,  0);
  glEnable(GL_TEXTURE_2D);
  glEnable(GL_CULL_FACE);
  glEnable(GL_DEPTH_TEST);
  glDepthMask(GL_TRUE); 
}

void display(void) {
  glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);

  unsigned char *data = load_RGB_data("tennessee_flag.bmp");
  GLuint textureID;
  glGenTextures(1, &textureID);
  glBindTexture(GL_TEXTURE_2D, textureID);
  glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, 512, 512, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
  glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
  glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);

  glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
  glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
  free(data);

  glBegin(GL_QUADS);

  //front
  glTexCoord2f(0.0, 1.0);
  glVertex3f(0,  1, 0);

  glTexCoord2f(1.0, 1.0);
  glVertex3f( 1,  1, 0);

  glTexCoord2f(1.0, 0.0);
  glVertex3f( 1, 0, 0);

  glTexCoord2f(0.0, 0.0);
  glVertex3f(0, 0, 0);

  glEnd();

  glFlush();


}

int main(int argc, char **argv) {

  glutInit(&argc, argv);
  glutInitDisplayMode(GLUT_SINGLE|GLUT_RGB);

  glutInitWindowSize(1280, 720);
  glutInitWindowPosition(0, 0);
  glutCreateWindow("OpenGL Demo");
  init();

  glutDisplayFunc(display);
  glutMainLoop();

  return 0;
}
