//2024 Levi D. Smith
#include <stdio.h>
#include <GL/glut.h>

void init(void) {
  glClearColor(0.392, 0.584, 0.929, 1.0);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();

  glOrtho(-12.8, 12.8, -7.2, 7.2, 0.1, 10);
  gluLookAt(-1, 1, -3, 
            0, 0,  0,
            0, 1,  0);
  glEnable(GL_CULL_FACE);
  glEnable(GL_DEPTH_TEST);
  glDepthMask(GL_TRUE); 
}

void display(void) {
  glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);

  glBegin(GL_QUADS);

  //top
  glColor3f(1.0, 1.0, 1.0);
  glVertex3f(-2, 2,  2);
  glVertex3f( 2, 2,  2);
  glVertex3f( 2, 2, -2);
  glVertex3f(-2, 2, -2);

  //front
  glColor3f(0.0, 1.0, 0.0);
  glVertex3f(-2,  2, -2);
  glVertex3f( 2,  2, -2);
  glVertex3f( 2, -2, -2);
  glVertex3f(-2, -2, -2);

  //back
  glColor3f(1.0, 1.0, 0.0);
  glVertex3f(-2,  2, 2);
  glVertex3f( 2,  2, 2);
  glVertex3f( 2, -2, 2);
  glVertex3f(-2, -2, 2);
  
  //left
  glColor3f(1.0, 0.647, 0.0);
  glVertex3f(-2,  2,  2);
  glVertex3f(-2,  2, -2);
  glVertex3f(-2, -2, -2);
  glVertex3f(-2, -2,  2);

  //right
  glColor3f(1.0, 0.0, 0.0);
  glVertex3f(2,  2,  2);
  glVertex3f(2,  2, -2);
  glVertex3f(2, -2, -2);
  glVertex3f(2, -2,  2);

  //bottom
  glColor3f(0.0, 0.0, 1.0);
  glVertex3f(-2, -2,  2);
  glVertex3f( 2, -2,  2);
  glVertex3f( 2, -2, -2);
  glVertex3f(-2, -2, -2);

  glEnd();

  glFlush();
}

int main(int argc, char **argv) {
  printf("Hello OpenGL\n");

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
