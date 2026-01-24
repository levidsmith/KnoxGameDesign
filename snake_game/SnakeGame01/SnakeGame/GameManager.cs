using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeGame {
    internal class GameManager {

        public List<Snake> snakes;
        public Arena arena;
        int iLevel;
        public float fUpdateDelay;
        public float fMaxUpdateDelay;

        public GameManager() {
            setupGame();

            iLevel = 1;
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
            snake.isAlive = true;
            snake.direction = Snake.Direction.EAST;
            snake.color = new Color(255, 255, 85);

            snakes.Add(snake);

            snake = new Snake();
            snake.id = 2;
            snake.strName = "JAKE";
            snake.iHead = 1;
            snake.iLength = 2;
            snake.isAlive = true;
            snake.direction = Snake.Direction.EAST;
            snake.color = new Color(255, 85, 255);

            snakes.Add(snake);


            arena = new Arena();

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
        }

        public void Update(float deltaTime) {
            fUpdateDelay -= deltaTime;

            if (fUpdateDelay <= 0f) {
                foreach (Snake snake in snakes) {
                    if (snake.isAlive) {
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

                }
                checkCollision();

                fUpdateDelay = fMaxUpdateDelay;
            }

        }

        private void checkCollision() {
            foreach (Snake snake in snakes) {
                if (snake.isAlive && arena.cells[snake.iRow, snake.iCol] != 0) {
                    snake.isAlive = false;
                }
            }
        }


    }
}
