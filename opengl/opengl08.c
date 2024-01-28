//2024 Levi D. Smith
#include <stdio.h>
#include <GL/glut.h>

void drawFloor();

float loc_x, loc_y, loc_z;
float rot;

void init(void) {
  glClearColor(0.392, 0.584, 0.929, 1.0);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();

  gluPerspective(60, 1.7778, 0.1, 50); 
  glViewport(0, 0, 1280 / 2, 720 / 2);
//  glOrtho(-12.8, 12.8, -7.2, 7.2, -20, 20);
  gluLookAt(-5, 10, -20, 
            0, 0,  0,
            0, 1,  0);
  glEnable(GL_CULL_FACE);
  glEnable(GL_DEPTH_TEST);
  glDepthMask(GL_TRUE); 
}

void display(void) {
  glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);

  glMatrixMode(GL_MODELVIEW);

  drawFloor();

  glPushMatrix();
  glTranslatef(loc_x, loc_y, loc_z);
  glRotatef(rot, 0, 1, 0);


  glBegin(GL_QUADS);

  //top
  glColor3f(1.0, 1.0, 1.0);
  glVertex3f(-2, 4,  2);
  glVertex3f( 2, 4,  2);
  glVertex3f( 2, 4, -2);
  glVertex3f(-2, 4, -2);

  //front
  glColor3f(0.0, 1.0, 0.0);
  glVertex3f(-2, 4, -2);
  glVertex3f( 2, 4, -2);
  glVertex3f( 2, 0, -2);
  glVertex3f(-2, 0, -2);

  //back
  glColor3f(1.0, 1.0, 0.0);
  glVertex3f(-2, 0, 2);
  glVertex3f( 2, 0, 2);
  glVertex3f( 2, 4, 2);
  glVertex3f(-2, 4, 2);
  
  //left
  glColor3f(1.0, 0.647, 0.0);
  glVertex3f(-2, 4,  2);
  glVertex3f(-2, 4, -2);
  glVertex3f(-2, 0, -2);
  glVertex3f(-2, 0,  2);

  //right
  glColor3f(1.0, 0.0, 0.0);
  glVertex3f(2,  4, -2);
  glVertex3f(2,  4,  2);
  glVertex3f(2, -0,  2);
  glVertex3f(2, -0, -2);

  //bottom
  glColor3f(0.0, 0.0, 1.0);
  glVertex3f(-2, 0, -2);
  glVertex3f( 2, 0, -2);
  glVertex3f( 2, 0,  2);
  glVertex3f(-2, 0,  2);

  glEnd();

  glPopMatrix();

  glFlush();
}

void drawFloor() {
  int i, j;
  glBegin(GL_QUADS);

  for (i = -20; i < 20; i++) {
    for (j = -20; j < 20; j++) {
      if ((i + j) % 2 == 0) {
        glColor3f(0.0, 0.0, 0.0);
      } else {
        glColor3f(0.5, 0.5, 0.5);
      }
      glVertex3f((j * 4) - 2, 0, (i * 4) + 2);
      glVertex3f((j * 4) + 2, 0, (i * 4) + 2);
      glVertex3f((j * 4) + 2, 0, (i * 4) - 2);
      glVertex3f((j * 4) - 2, 0, (i * 4) - 2);


    }
  }

  glEnd();

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
