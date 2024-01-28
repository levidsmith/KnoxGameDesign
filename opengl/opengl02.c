//2024 Levi D. Smith
#include <stdio.h>
#include <GL/glut.h>

void display(void) {
}

int main(int argc, char **argv) {
  printf("Hello OpenGL\n");

  glutInit(&argc, argv);
  glutInitDisplayMode(GLUT_SINGLE|GLUT_RGB);

  glutInitWindowSize(1280, 720);
  glutInitWindowPosition(0, 0);
  glutCreateWindow("OpenGL Demo");

  glutDisplayFunc(display);
  glutMainLoop();

  return 0;
}
