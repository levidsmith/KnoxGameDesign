using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Xna.Framework;

namespace SnakeGame {
    internal class GameManager {

        public List<Snake> snakes;
        public Arena arena;
        int iLevel;
        public float fUpdateDelay;
        public float fMaxUpdateDelay;
        public Collectible collectible;

        public int POINTS_DEATH = -10;
        public int LENGTH_ADD_MULTIPLIER = 4;

        public GameManager() {
            iLevel = 1;
            setupGame();

            setupLevel(iLevel);
        }

        private void setupGame() {
            snakes = new List<Snake>();

            Snake snake;

            snake = new Snake();
            snake.id = 1;
            snake.strName = "SAMMY";
            snake.iHead = 1;
            snake.iLength = 2;
            snake.iLives = 5;
            snake.isAlive = true;
            snake.direction = Snake.Direction.EAST;
            snake.color = new Color(255, 255, 85);

            snakes.Add(snake);

            snake = new Snake();
            snake.id = 2;
            snake.strName = "JAKE";
            snake.iHead = 1;
            snake.iLength = 2;
            snake.iLives = 5;
            snake.isAlive = true;
            snake.direction = Snake.Direction.EAST;
            snake.color = new Color(255, 85, 255);

            snakes.Add(snake);

            arena = new Arena();
            arena.setup(iLevel);

            setupCollectible();

            fMaxUpdateDelay = 0.2f;
            fUpdateDelay = fMaxUpdateDelay;

        }

        private void setupLevel(int iLevel) {

            switch (iLevel) {
                case 1:
                    snakes[0].iRow = 25;
                    snakes[0].iCol = 50;
                    snakes[0].direction = Snake.Direction.EAST;

                    snakes[1].iRow = 25;
                    snakes[1].iCol = 30;
                    snakes[1].direction = Snake.Direction.WEST;

                    break;
            }

            arena.setup(iLevel);
        }

        private void resetLevel() {
            foreach (Snake snake in snakes) {
                snake.body.Clear();
                snake.iLength = 2;
                snake.isAlive = true;
            }
            setupLevel(iLevel);
        }

        private void setupCollectible() {
            if (collectible == null) {
                collectible = new Collectible();
            } else {
                collectible.iValue += 1;
            }

            Random rand = new Random();
            int iRandRow = rand.Next(1, Arena.ARENA_ROWS - 2);
            int iRandCol = rand.Next(1, Arena.ARENA_COLS - 1);

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
                    resetLevel();
                }

                if (snake.isAlive && 
                    (collectible.iRow == snake.iRow || collectible.iRow + 1 == snake.iRow) && 
                    collectible.iCol ==snake.iCol) {
                    snake.iLength += collectible.iValue * LENGTH_ADD_MULTIPLIER;
                    snake.iScore += collectible.iValue;
                    setupCollectible();
                }

            }
        }


    }
}
