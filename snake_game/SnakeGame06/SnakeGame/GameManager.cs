using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

namespace SnakeGame {
    internal class GameManager {

        public List<Snake> snakes;
        public Arena arena;
        public int iLevel;
        public float fUpdateDelay;
        public float fMaxUpdateDelay;
        public Collectible collectible;

        const int POINTS_DEATH = -10;
        const int LENGTH_ADD_MULTIPLIER = 4;
        const int COLLECTIBLE_VALUE_FOR_NEXT_LEVEL = 9;
        const int START_SNAKE_LENGTH = 2;
        const int START_SNAKE_LIVES = 5;

        public enum GameState { TITLE, OPTIONS, PLAYING, PRE_LEVEL, PLAYER_DEAD, GAME_OVER };
        public GameState gamestate;

        Random rand;

        public GameManager() {
            iLevel = 1;
            gamestate = GameState.PRE_LEVEL;
             rand = new Random();
            setupGame();

        }

        public void setupGame() {
            snakes = new List<Snake>();

            Snake snake;

            snake = new Snake();
            snake.id = 1;
            snake.strName = "Sammy";
            snake.iHead = 1;
            snake.iLength = START_SNAKE_LENGTH;
            snake.iLives = 5;
            snake.isAlive = true;
            snake.direction = Snake.Direction.EAST;
            snake.color = new Color(255, 255, 85);

            snakes.Add(snake);

            snake = new Snake();
            snake.id = 2;
            snake.strName = "Jake";
            snake.iHead = 1;
            snake.iLength = START_SNAKE_LENGTH;
            snake.iLives = 5;
            snake.isAlive = true;
            snake.direction = Snake.Direction.EAST;
            snake.color = new Color(255, 85, 255);

            snakes.Add(snake);

            arena = new Arena();

            setupPlayers(iLevel);
            arena.setup(iLevel);

            setupNextCollectible(true);

            fMaxUpdateDelay = 0.1f;
            fUpdateDelay = fMaxUpdateDelay;

        }

        private void setupPlayers(int iLevel) {

            switch (iLevel) {
                case 1:
                    snakes[0].iRow = 25;
                    snakes[0].iCol = 50;
                    snakes[0].direction = Snake.Direction.EAST;

                    snakes[1].iRow = 25;
                    snakes[1].iCol = 30;
                    snakes[1].direction = Snake.Direction.WEST;

                    break;
                case 2:
                    snakes[0].iRow = 7;
                    snakes[0].iCol = 60;
                    snakes[0].direction = Snake.Direction.WEST;

                    snakes[1].iRow = 43;
                    snakes[1].iCol = 20;
                    snakes[1].direction = Snake.Direction.EAST;

                    break;
                case 3:
                    snakes[0].iRow = 25;
                    snakes[0].iCol = 50;
                    snakes[0].direction = Snake.Direction.NORTH;

                    snakes[1].iRow = 25;
                    snakes[1].iCol = 30;
                    snakes[1].direction = Snake.Direction.SOUTH;

                    break;
                case 4:
                    snakes[0].iRow = 7;
                    snakes[0].iCol = 60;
                    snakes[0].direction = Snake.Direction.WEST;

                    snakes[1].iRow = 43;
                    snakes[1].iCol = 20;
                    snakes[1].direction = Snake.Direction.EAST;

                    break;
                case 5:
                    snakes[0].iRow = 25;
                    snakes[0].iCol = 50;
                    snakes[0].direction = Snake.Direction.NORTH;

                    snakes[1].iRow = 25;
                    snakes[1].iCol = 30;
                    snakes[1].direction = Snake.Direction.SOUTH;

                    break;
                case 6:
                    snakes[0].iRow = 7;
                    snakes[0].iCol = 65;
                    snakes[0].direction = Snake.Direction.SOUTH;

                    snakes[1].iRow = 43;
                    snakes[1].iCol = 15;
                    snakes[1].direction = Snake.Direction.NORTH;

                    break;
                case 7:
                    snakes[0].iRow = 7;
                    snakes[0].iCol = 65;
                    snakes[0].direction = Snake.Direction.SOUTH;

                    snakes[1].iRow = 43;
                    snakes[1].iCol = 15;
                    snakes[1].direction = Snake.Direction.NORTH;

                    break;
                case 8:
                    snakes[0].iRow = 7;
                    snakes[0].iCol = 65;
                    snakes[0].direction = Snake.Direction.SOUTH;

                    snakes[1].iRow = 43;
                    snakes[1].iCol = 15;
                    snakes[1].direction = Snake.Direction.NORTH;

                    break;

                case 9:
                    snakes[0].iRow = 40;
                    snakes[0].iCol = 75;
                    snakes[0].direction = Snake.Direction.NORTH;

                    snakes[1].iRow = 15;
                    snakes[1].iCol = 5;
                    snakes[1].direction = Snake.Direction.SOUTH;

                    break;

                default:
                    snakes[0].iRow = 7;
                    snakes[0].iCol = 65;
                    snakes[0].direction = Snake.Direction.SOUTH;

                    snakes[1].iRow = 43;
                    snakes[1].iCol = 15;
                    snakes[1].direction = Snake.Direction.NORTH;

                    break;
            }

        }

        public void resetLevel() {
            foreach (Snake snake in snakes) {
                snake.body.Clear();
                snake.iLength = START_SNAKE_LENGTH;
                snake.isAlive = true;
            }
            setupPlayers(iLevel);
            arena.setup(iLevel);
            setupNextCollectible(true);
        }

        public void resetGame() {
            gamestate = GameManager.GameState.PRE_LEVEL;

            iLevel = 1;

            foreach (Snake snake in snakes) {
                snake.iLives = START_SNAKE_LIVES;
                snake.iScore = 0;
            }

            collectible.iValue = 1;
            resetLevel();
        }

        private void setupNextLevel() {
            gamestate = GameState.PRE_LEVEL;
            iLevel += 1;
            collectible.iValue = 1;
            resetLevel();
        }

        private void setupNextCollectible(bool isReset) {
            if (collectible == null) {
                collectible = new Collectible();
            }

            if (isReset) {
                collectible.iValue = 1;
            } else {
                collectible.iValue++;
            }


                int iRandRow = -1;
            int iRandCol = -1;

            bool keepLooping = true;
            while (keepLooping) {
                iRandRow = rand.Next(1, Arena.ARENA_ROWS - 2);
                iRandCol = rand.Next(1, Arena.ARENA_COLS - 1);

                if (arena.cells[iRandRow, iRandCol] == Arena.CELL_EMPTY &&
                    arena.cells[iRandRow + 1, iRandCol] == Arena.CELL_EMPTY) {
                    keepLooping = false;
                }
            }

            collectible.iRow = iRandRow;
            collectible.iCol = iRandCol;
        }

        public void Update(float deltaTime) {
            fUpdateDelay -= deltaTime;

            if (fUpdateDelay <= 0f) {
                foreach (Snake snake in snakes) {
                    if (snake.isAlive) {
                        snake.body.Add(new SnakeBody(snake.iRow, snake.iCol));

                        switch (snake.direction) {
                            case Snake.Direction.NORTH:
                                arena.cells[snake.iRow, snake.iCol] = snake.id;
                                snake.iRow -= 1;
                                break;
                            case Snake.Direction.SOUTH:
                                arena.cells[snake.iRow, snake.iCol] = snake.id;
                                snake.iRow += 1;
                                break;
                            case Snake.Direction.WEST:
                                arena.cells[snake.iRow, snake.iCol] = snake.id;
                                snake.iCol -= 1;
                                break;
                            case Snake.Direction.EAST:
                                arena.cells[snake.iRow, snake.iCol] = snake.id;
                                snake.iCol += 1;
                                break;

                        }
                    }

                    if (snake.body.Count > snake.iLength) {
                        SnakeBody snakebody = snake.body[0];
                        arena.cells[snakebody.iRow, snakebody.iCol] = 0;
                        snake.body.Remove(snakebody);
                    }

                }
                checkCollision();


                fUpdateDelay = fMaxUpdateDelay;
            }

        }

        private void checkCollision() {
            foreach (Snake snake in snakes) {


                if (snake.isAlive && arena.cells[snake.iRow, snake.iCol] != 0) {
                    snake.iLives -= 1;
                    snake.isAlive = false;
                    snake.iScore += POINTS_DEATH;
                    checkGameOver();
                    if (gamestate != GameState.GAME_OVER) {
                        doPlayerDead();
                    }
                }

                if (snake.isAlive && 
                    (collectible.iRow == snake.iRow || collectible.iRow + 1 == snake.iRow) && 
                    collectible.iCol == snake.iCol) {
                    snake.iLength += collectible.iValue * LENGTH_ADD_MULTIPLIER;
                    snake.iScore += collectible.iValue;

                    if (collectible.iValue == COLLECTIBLE_VALUE_FOR_NEXT_LEVEL) {
                        setupNextLevel();
                    } else {
                      setupNextCollectible(false);
                    }

                    Game1.sounds["sound_pickup"].Play();
                }

            }

            if (snakes[0].iRow == snakes[1].iRow &&
                snakes[0].iCol == snakes[1].iCol) {

                snakes[0].iLives -= 1;
                snakes[0].isAlive = false;
                snakes[0].iScore += POINTS_DEATH;
                snakes[1].iLives -= 1;
                snakes[1].isAlive = false;
                snakes[1].iScore += POINTS_DEATH;
                checkGameOver();
                if (gamestate != GameState.GAME_OVER) {
                    doPlayerDead();
                }

            }

        }

        private void checkGameOver() {
            if (snakes[0].iLives < 0 || snakes[1].iLives < 0) {
                gamestate = GameState.GAME_OVER;
            }

        }

        private void doPlayerDead() {
            gamestate = GameState.PLAYER_DEAD;
            Game1.sounds["sound_dead"].Play();
        }



    }
}
