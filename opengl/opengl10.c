//2024 Levi D. Smith
#include <stdio.h>
#include <GL/glut.h>
#include <math.h>

struct Player {
  float x, y, z;
  float rot;
  void *draw;
}
typedef Player;

struct Bullet {
  float x, y, z;
  int isAlive;
}
typedef Bullet;

struct Enemy {
  float x, y, z;
  int isAlive;
}
typedef Enemy;


//function prototypes
void draw_player();
void draw_enemies();
void draw_bullets();
int check_collision(float, float, float, float, float, float);

void drawFloor();
void setLightPosition();
void init_lights();

//game objects
Player player;
Bullet bullets[3];
Enemy enemies[8];

//materials
GLfloat mat_red[] = {1.0, 0.0, 0.0, 1.0 };
GLfloat mat_orange[] = {1.0, 0.647, 0.0, 1.0 };
GLfloat mat_yellow[] = {1.0, 1.0, 0.0, 1.0 };
GLfloat mat_green[] = {0.0, 1.0, 0.0, 1.0 };
GLfloat mat_blue[] = {0.0, 0.0, 1.0, 1.0 };
GLfloat mat_white[] = {1.0, 1.0, 1.0, 1.0 };
GLfloat mat_specular[] = {1.0, 1.0, 1.0, 1.0 };
GLfloat mat_gray[] = {0.5, 0.5, 0.5, 1.0 };
GLfloat mat_black[] = {0, 0, 0, 1.0 };

void init(void) {
  glClearColor(0.0, 0.0, 0.0, 0.0);
  glViewport(0, 0, 1280 / 2, 720 / 2);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();

  gluPerspective(60, 1.7778, 0.1, 50); 
  gluLookAt(0, 20, 20, 
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

void init_game() {

  //init player
  player.x = 0;
  player.y = 0;
  player.z = 10;

  //init enemies
  int i;
  for (i = 0; i < 8; i++) {
    enemies[i].x = (-4 * 5) + i * 5;
    enemies[i].y = 0;
    enemies[i].z = -10;
    enemies[i].isAlive = 1;
  }
}

void create_bullet() {
  int i;
  int iBullet = -1;


  for (i = 0; i < 3; i++) {
    if (!bullets[i].isAlive) {
      iBullet = i;	      
      break;
    }
  }
  //create bullet
  if (iBullet > -1) {
    bullets[iBullet].x = player.x;
    bullets[iBullet].y = player.y;
    bullets[iBullet].z = player.z;
    bullets[iBullet].isAlive = 1;
  }
}

void update_bullets() {
  int i, j;

  for (i = 0; i < 3; i++) {
    if (bullets[i].isAlive) {
      bullets[i].z -= 0.5 * 0.01667;


      for (j = 0; j < 8; j++) {

        if (enemies[j].isAlive &&
            check_collision(bullets[i].x, bullets[i].z, 1,
            enemies[j].x, enemies[j].z, 2) ) {
          bullets[i].isAlive = 0;
          enemies[j].isAlive = 0;
	}

      } 
    }

    if (bullets[i].z < -20) {
      bullets[i].isAlive = 0;  
    }
  }
}

int check_collision(float x1, float y1, float r1, float x2, float y2, float r2) {
  //use circle collision
  if ( sqrt(pow(x1 - x2, 2) + pow(y1 - y2, 2)) < r1 + r2) {
    return 1;  
  } else {
    return 0;
  }
}

void init_lights() {
   GLfloat mat_specular[] = { 1.0, 1.0, 1.0, 1.0 };
   GLfloat mat_shininess[] = { 50.0 };
   GLfloat light_position[] = { 0.0, 1.0, 1.0, 0.0 };
   GLfloat light_ambient[] = { 1, 1, 1, 0.2 };
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
  int i;
  glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);

//draw game objects
  draw_player();
  draw_enemies();
  draw_bullets();

  drawFloor();

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

void draw_player() {

  glPushMatrix();
  glLoadIdentity();

  glTranslatef(player.x, player.y, player.z);
  glRotatef(player.rot, 0, 1, 0);

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


void draw_enemies() {
  int i;
  for (i = 0; i < 8; i++) {
    if (enemies[i].isAlive) {
      glPushMatrix();
      glLoadIdentity();
      glTranslatef(enemies[i].x, enemies[i].y, enemies[i].z);

      glutSolidSphere(2.0, 20, 16);

      glPopMatrix();
    }
  }
}

void draw_bullets() {
  int i;
  for (i = 0; i < 3; i++) {
    if (bullets[i].isAlive) {
      glPushMatrix();
      glLoadIdentity();
      glTranslatef(bullets[i].x, bullets[i].y, bullets[i].z);

      glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_red);
      glutSolidSphere(1.0, 10, 8);

      glPopMatrix();
    }
  }
}


void drawFloor() {
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
  float fSpeed = 0.5;
  float fRotSpeed = 10;

  switch(key) {
    case 'q':
      printf("rotate left\n");    
      player.rot -= fRotSpeed;
      break;   
    case 's':
      printf("move back\n");    
      player.z -= fSpeed;
      break;   
    case 'e':
      player.rot += fRotSpeed;
      printf("rotate right\n");    
      break;   
    case 'w':
      printf("move forward\n");    
      player.z += fSpeed;
      break;   
    case 'a':
      printf("move left\n");    
      player.x -= fSpeed;
      break;   
    case 'd':
      printf("move right\n");    
      player.x += fSpeed;
      break;   
    case 32:
      printf("shoot\n");    
      create_bullet();
      break;   
    case 27:
      exit(1);
      break; 
  }

}

void update() {
  update_bullets();
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

  init_game();

  glutIdleFunc(update);
  glutDisplayFunc(display);
  glutKeyboardFunc(keyboardHandler);
  glutMainLoop();

  return 0;
}
