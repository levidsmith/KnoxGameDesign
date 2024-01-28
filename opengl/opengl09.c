//2024 Levi D. Smith
#include <stdio.h>
#include <GL/glut.h>

void drawCube();
void drawFloor();
void drawSphere();
void setLightPosition();
void init_lights();

float loc_x, loc_y, loc_z;
float rot;

void init(void) {
//  glClearColor(0.392, 0.584, 0.929, 1.0);
  glClearColor(0.0, 0.0, 0.0, 0.0);
  glViewport(0, 0, 1280 / 2, 720 / 2);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();

  gluPerspective(60, 1.7778, 0.1, 50); 
  gluLookAt(0, 10, 20, 
            0, 0,  0,
            0, 1,  0);
  glEnable(GL_CULL_FACE);
  glEnable(GL_DEPTH_TEST);
  glDepthMask(GL_TRUE); 
  glDepthFunc(GL_LEQUAL);

  init_lights();

  glMatrixMode(GL_MODELVIEW);
  glLoadIdentity();
}

void init_lights() {
   GLfloat mat_specular[] = { 1.0, 1.0, 1.0, 1.0 };
   GLfloat mat_shininess[] = { 50.0 };
   GLfloat light_position[] = { 0.0, 1.0, 1.0, 0.0 };
   GLfloat light_ambient[] = { 0.0, 0.0, 1.0, 0.2 };
   glClearColor (0.0, 0.0, 0.0, 0.0);
   glShadeModel (GL_SMOOTH);

   glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
   glMaterialfv(GL_FRONT, GL_SHININESS, mat_shininess);
   glLightfv(GL_LIGHT0, GL_POSITION, light_position);
   glLightfv(GL_LIGHT0, GL_AMBIENT, light_ambient);

   glEnable(GL_LIGHTING);
   glEnable(GL_LIGHT0);
   glEnable(GL_DEPTH_TEST);
  
}

void display(void) {
  glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);


  drawCube();

//  setLightPosition();
  /*
  glPushMatrix();
  glTranslatef(loc_x, loc_y, loc_z);
  glRotatef(rot, 0.0, 1.0, 0.0);
  glutSolidTeapot(5.0);
  glPopMatrix();
  */
  drawFloor();
  drawSphere();

  glFlush();
}

void setLightPosition() {
//  GLfloat light_position[] = { 5.0, 0.0, 0.0, 10.0 };
//  GLfloat light_position[] = { 0.0, 8.0, 10.0, 10.0 };
  GLfloat light_ambient[] = { 1.0, 1.0, 1.0, 1.0 };
  GLfloat light_diffuse[] = { 1.0, 1.0, 1.0, 0.5 };
  GLfloat light_specular[] = {1.0, 1.0, 1.0, 1.0 };
  GLfloat light_position[] = { -5.0, 0.0, 0.0, 0.0 };
  GLfloat spot_direction[] = { 0.0, -1.0, 0.0, 0.0 };

  glLightfv(GL_LIGHT0, GL_AMBIENT, light_ambient); 
  glLightfv(GL_LIGHT0, GL_DIFFUSE, light_diffuse); 
  glLightfv(GL_LIGHT0, GL_SPECULAR, light_specular); 
  glLightfv(GL_LIGHT0, GL_POSITION, light_position); 
  glLightfv(GL_LIGHT0, GL_SPOT_DIRECTION, spot_direction); 

  glLightf(GL_LIGHT0, GL_SPOT_CUTOFF, 45.0);
  glLightf(GL_LIGHT0, GL_CONSTANT_ATTENUATION, 2.0);

}

void drawSphere() {
  glPushMatrix();
  glLoadIdentity();
  glTranslatef(-5.0, 2.0, 0.0);

  glutSolidSphere(2.0, 20, 16);

  glPopMatrix();
}

void drawCube() {

  glPushMatrix();
  glLoadIdentity();


  GLfloat mat_red[] = {1.0, 0.0, 0.0, 1.0 };
  GLfloat mat_orange[] = {1.0, 0.647, 0.0, 1.0 };
  GLfloat mat_yellow[] = {1.0, 1.0, 0.0, 1.0 };
  GLfloat mat_green[] = {0.0, 1.0, 0.0, 1.0 };
  GLfloat mat_blue[] = {0.0, 0.0, 1.0, 1.0 };
  GLfloat mat_white[] = {1.0, 1.0, 1.0, 1.0 };
  GLfloat mat_specular[] = {1.0, 1.0, 1.0, 1.0 };
  GLfloat mat_shininess[] = { 50.0 };

  glTranslatef(5.0, 0.0, 0.0);
  glTranslatef(loc_x, loc_y, loc_z);
  glRotatef(rot, 0, 1, 0);



  //top
  glBegin(GL_QUADS);
  glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_white);
  glNormal3f(0.0, 1.0, 0.0);
  glVertex3f(-2, 4,  2);
  glVertex3f( 2, 4,  2);
  glVertex3f( 2, 4, -2);
  glVertex3f(-2, 4, -2);
  glEnd();

  //front
  glBegin(GL_QUADS);
  glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_green);
  glNormal3f(0.0, 0.0, -1.0);
  glVertex3f(-2, 4, -2);
  glVertex3f( 2, 4, -2);
  glVertex3f( 2, 0, -2);
  glVertex3f(-2, 0, -2);
  glEnd();

  //back
  glBegin(GL_QUADS);
  glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_yellow);
  glNormal3f(0.0, 0.0, 1.0);
  glVertex3f(-2, 0, 2);
  glVertex3f( 2, 0, 2);
  glVertex3f( 2, 4, 2);
  glVertex3f(-2, 4, 2);
  glEnd();
  
  //left
  glBegin(GL_QUADS);
  glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_orange);
  glNormal3f(-1.0, 0.0, 0.0);
  glVertex3f(-2, 4,  2);
  glVertex3f(-2, 4, -2);
  glVertex3f(-2, 0, -2);
  glVertex3f(-2, 0,  2);
  glEnd();

  //right
  glBegin(GL_QUADS);
  glNormal3f(1.0, 0.0, 0.0);
  glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_red);
  glVertex3f(2,  4, -2);
  glVertex3f(2,  4,  2);
  glVertex3f(2, -0,  2);
  glVertex3f(2, -0, -2);
  glEnd();

  //bottom
  glBegin(GL_QUADS);
  glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_blue);
  glNormal3f(0.0, -1.0, 0.0);
  glVertex3f(-2, 0, -2);
  glVertex3f( 2, 0, -2);
  glVertex3f( 2, 0,  2);
  glVertex3f(-2, 0,  2);
  glEnd();

  glPopMatrix();


}

void drawFloor() {
  GLfloat mat_gray[] = {0.5, 0.5, 0.5, 1.0 };
  GLfloat mat_black[] = {0, 0, 0, 1.0 };
  GLfloat mat_white[] = {1.0, 1.0, 1.0, 1.0 };
  GLfloat mat_shininess[] = {50.0 };

  int i, j;
  glPushMatrix();
  glLoadIdentity();

  glBegin(GL_QUADS);

  for (i = -20; i < 20; i++) {
    for (j = -20; j < 20; j++) {
      glMaterialfv(GL_FRONT, GL_SHININESS, mat_shininess);
      if ((i + j) % 2 == 0) {
        glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_black);
      } else {
        glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_gray);
      }
      glNormal3f(0.0, 1.0, 0.0);
      glVertex3f((j * 4) - 2, 0, (i * 4) + 2);
      glVertex3f((j * 4) + 2, 0, (i * 4) + 2);
      glVertex3f((j * 4) + 2, 0, (i * 4) - 2);
      glVertex3f((j * 4) - 2, 0, (i * 4) - 2);


    }
  }

  glEnd();
  glPopMatrix();

  glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_white);

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
  glutInitDisplayMode(GLUT_SINGLE|GLUT_RGB|GLUT_DEPTH);

  glutInitWindowSize(1280, 720);
  glutInitWindowPosition(0, 0);
  glutCreateWindow("OpenGL Demo");
  init();

  glutDisplayFunc(display);
  glutKeyboardFunc(keyboardHandler);
  glutMainLoop();

  return 0;
}
