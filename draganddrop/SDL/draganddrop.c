/* 2025 Levi D. Smith <developer@levidsmith.com> */
#include <SDL.h>
#include <stdio.h>
#include <stdlib.h>

#define SCREEN_WIDTH 1280
#define SCREEN_HEIGHT 720
#define TRUE 1
#define FALSE 0


typedef struct {
  SDL_Surface *imgCard;
  int x, y;
  int w, h;
  int isDragged;
} Card;

int drag_offset_x;
int drag_offset_y;

void setupCard(Card *card);
void destroyCard(Card *card);
int checkCardPressed(int pointer_x, int pointer_y, Card *card);

int main(int argc, char *args[]) {
  SDL_Window *window = NULL;
  SDL_Surface *screenSurface = NULL;
  SDL_Init(SDL_INIT_VIDEO);
  window = SDL_CreateWindow("Drag and Drop SDL", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, SCREEN_WIDTH, SCREEN_HEIGHT, SDL_WINDOW_SHOWN);
  screenSurface = SDL_GetWindowSurface(window);
  SDL_UpdateWindowSurface(window);

  Card *card;
  card = malloc(sizeof(Card));
  setupCard(card);

  SDL_Rect rectCardBlit;
  int pointer_x;
  int pointer_y;

  SDL_Event theEvent;

  while (TRUE) {
    SDL_PollEvent(&theEvent);
    if (theEvent.type == SDL_QUIT) {
      break;
    }

    SDL_GetMouseState(&pointer_x, &pointer_y);

    if (theEvent.type == SDL_MOUSEBUTTONDOWN) {
      if (checkCardPressed(pointer_x, pointer_y, card)) { 
        drag_offset_x = card->x - pointer_x;
	drag_offset_y = card->y - pointer_y;
        card->isDragged = TRUE;
      }

    } else if (theEvent.type == SDL_MOUSEBUTTONUP) {
      card->isDragged = FALSE;

    } else if (theEvent.type == SDL_MOUSEMOTION) {
      if (card->isDragged) {
	card->x = pointer_x + drag_offset_x;
	card->y = pointer_y + drag_offset_y;
      }
    }

    SDL_FillRect(screenSurface, NULL, SDL_MapRGB(screenSurface->format, 0x64, 0x95, 0xED));

    rectCardBlit.x = card->x;
    rectCardBlit.y = card->y;
    rectCardBlit.w = card->w;
    rectCardBlit.h = card->h;
    SDL_BlitSurface(card->imgCard, NULL, screenSurface, &rectCardBlit);

    SDL_UpdateWindowSurface(window);
  }

  destroyCard(card);

  SDL_DestroyWindow(window);
  SDL_Quit();
  return 0;
}

void setupCard(Card *card) {
  card->imgCard = SDL_LoadBMP("card.bmp");
  SDL_SetColorKey(card->imgCard, SDL_TRUE, SDL_MapRGB(card->imgCard->format, 0xFF, 0x00, 0xFF));

  card->x = 32;
  card->y = 16;
  card->w = 250;
  card->h = 350;
  card->isDragged = FALSE;
}

void destroyCard(Card *card) {
  SDL_FreeSurface(card->imgCard);

}

int checkCardPressed(int pointer_x, int pointer_y, Card *card) {
  if (pointer_x >= card->x &&
      pointer_x < card->x + card->w &&
      pointer_y >= card->y &&
      pointer_y < card->y + card->h) {
    return TRUE;
  }
  return FALSE;
}

