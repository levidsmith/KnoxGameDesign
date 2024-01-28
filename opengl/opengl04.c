//2024 Levi D. Smith
#include <stdio.h>
#include <GL/glut.h>

void init(void) {
  glClearColor(0.392, 0.584, 0.929, 1.0);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  gluOrtho2D(-12.8, 12.8, -7.2, 7.2);
}

void display(void) {
  glClear(GL_COLOR_BUFFER_BIT);
  glColor3f(0.0, 0.0, 1.0);

  glBegin(GL_QUADS);

  glVertex2f(-5, 5);
  glVertex2f(5, 5);
  glVertex2f(5, -5);
  glVertex2f(-5, -5);

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
