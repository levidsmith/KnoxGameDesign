//2024 Levi D. Smith
#include <stdio.h>
#include <GL/glut.h>

float loc_x, loc_y, loc_z;
float rot;

void init(void) {
  glClearColor(0.392, 0.584, 0.929, 1.0);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();

  glOrtho(-12.8, 12.8, -7.2, 7.2, -20, 20);
  gluLookAt(-1, 1, -3, 
            0, 0,  0,
            0, 1,  0);
  glEnable(GL_CULL_FACE);
  glEnable(GL_DEPTH_TEST);
  glDepthMask(GL_TRUE); 
}

void display(void) {
  glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);

  glPushMatrix();
  glTranslatef(loc_x, loc_y, loc_z);
  glRotatef(rot, 0, 1, 0);

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
  glVertex3f(-2, -2, 2);
  glVertex3f( 2, -2, 2);
  glVertex3f( 2,  2, 2);
  glVertex3f(-2,  2, 2);
  
  //left
  glColor3f(1.0, 0.647, 0.0);
  glVertex3f(-2,  2,  2);
  glVertex3f(-2,  2, -2);
  glVertex3f(-2, -2, -2);
  glVertex3f(-2, -2,  2);

  //right
  glColor3f(1.0, 0.0, 0.0);
  glVertex3f(2,  2, -2);
  glVertex3f(2,  2,  2);
  glVertex3f(2, -2,  2);
  glVertex3f(2, -2, -2);

  //bottom
  glColor3f(0.0, 0.0, 1.0);
  glVertex3f(-2, -2, -2);
  glVertex3f( 2, -2, -2);
  glVertex3f( 2, -2,  2);
  glVertex3f(-2, -2,  2);

  glEnd();

  glPopMatrix();

  glFlush();
}

void keyboardHandler(unsigned char key, int x, int y) {
  switch(key) {
    case 'a':
      printf("rotate left\n");    
      rot -= 10;
      break;   
    case 's':
      printf("move back\n");    
      loc_z -= 0.1;
      break;   
    case 'd':
      rot += 10;
      printf("rotate right\n");    
      break;   
    case 'w':
      printf("move forward\n");    
      loc_z += 0.1;
      break;   
    case 'q':
      printf("move left\n");    
      loc_x -= 0.1;
      break;   
    case 'e':
      printf("move right\n");    
      loc_x += 0.1;
      break;   
    case 27:
      exit(1);
      break; 
  }
  glutPostRedisplay();
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
  glutKeyboardFunc(keyboardHandler);
  glutMainLoop();

  return 0;
}
