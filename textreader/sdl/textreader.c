#include <stdio.h>
#include <SDL2/SDL.h>
#define CHARS_PER_LINE 32 
#define WALL_WIDTH 32
#define WALL_HEIGHT 32

SDL_Window *window = NULL;
SDL_Surface *screenSurface = NULL;

void parse_file() {
  SDL_Surface *sprWall;
  SDL_Rect rect;
  sprWall = SDL_LoadBMP("spr_wall.bmp");
  rect.w = WALL_WIDTH;
  rect.h = WALL_HEIGHT;

  FILE *file = fopen("knox.txt", "r");
  char strLine[CHARS_PER_LINE];

  int iRow = 0;
  int iCol = 0;
  while (fgets(strLine, CHARS_PER_LINE, file) != NULL) {
    while (iCol < CHARS_PER_LINE) {
      if (strLine[iCol] == '#') {
        rect.x = iCol * WALL_WIDTH;
        rect.y = iRow * WALL_HEIGHT;
        SDL_BlitSurface(sprWall, NULL, screenSurface, &rect);
      }
      iCol++;
    }
    iRow++;
    iCol = 0;
  }

  fclose(file);
  SDL_FreeSurface(sprWall);
}

int main(int argc, char *args[]) {
  SDL_Init(SDL_INIT_VIDEO); 

  window = SDL_CreateWindow("SDL Text Reader", SDL_WINDOWPOS_UNDEFINED, 
		  SDL_WINDOWPOS_UNDEFINED, 1024, 768, SDL_WINDOW_SHOWN);

  screenSurface = SDL_GetWindowSurface(window);
  SDL_FillRect(screenSurface, NULL, SDL_MapRGB(screenSurface->format, 
			  0x00, 0x00, 0xFF));

  parse_file();

  SDL_UpdateWindowSurface(window);
  SDL_Delay(60000);
  SDL_DestroyWindow(window);
  SDL_Quit();
  return 0;
}
