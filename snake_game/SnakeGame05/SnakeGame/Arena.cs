using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame {
    internal class Arena {
        public const int ARENA_ROWS = 50;
        public const int ARENA_COLS = 80;
        public byte[,] cells;

        public const byte CELL_EMPTY = 0;
        public const byte CELL_WALL = 3;
        public const byte CELL_SNAKE1_BODY = 1;
        public const byte CELL_SNAKE2_BODY = 2;

        public Arena() {
            cells = new byte[ARENA_ROWS, ARENA_COLS];
            
        }

        public void setup(int iLevel) {
            int i, j;
            for (i = 0; i < ARENA_ROWS; i++) {
                for (j = 0; j < ARENA_COLS; j++) {
                    cells[i, j] = CELL_EMPTY;

                }
            }

            for (i = 0; i < ARENA_COLS; i++) {
                cells[0, i] = CELL_WALL;
                cells[ARENA_ROWS - 1, i] = CELL_WALL;
            }

            for (i = 0; i < ARENA_ROWS; i++) {
                cells[i, 0] = CELL_WALL;
                cells[i, ARENA_COLS - 1] = CELL_WALL;
            }


            switch(iLevel) {
                case 1:
                    break;
                case 2:
                    for (i = 20; i < 60; i++) {
                        cells[25, i] = CELL_WALL;
                    }
                    break;
                case 3:
                    for (i = 10; i < 40; i++) {
                        cells[i, 20] = CELL_WALL;
                        cells[i, 60] = CELL_WALL;
                    }
                    break;
                case 4:
                    for (i = 4; i < 30; i++) {
                        cells[i, 20] = CELL_WALL;
                        cells[53 - i, 60] = CELL_WALL;
                    }
                    for (i = 2; i < 40; i++) {
                        cells[38, i] = CELL_WALL;
                        cells[15, 81 - i] = CELL_WALL;
                    }
                    break;
                case 5:
                    for (i = 13; i < 39; i++) {
                        cells[i, 21] = CELL_WALL;
                        cells[i, 59] = CELL_WALL;
                    }
                    for (i = 23; i < 57; i++) {
                        cells[11, i] = CELL_WALL;
                        cells[41, i] = CELL_WALL;
                    }
                    break;

                case 6:
                    for (i = 4; i < 49; i++) {
                        if (i > 30 || i < 23) {
                            cells[i, 10] = CELL_WALL;
                            cells[i, 20] = CELL_WALL;
                            cells[i, 30] = CELL_WALL;
                            cells[i, 40] = CELL_WALL;
                            cells[i, 50] = CELL_WALL;
                            cells[i, 60] = CELL_WALL;
                            cells[i, 70] = CELL_WALL;
                        }
                    }
                    break;
                case 7:
                    for (i = 4; i < 49; i += 2) {
                        cells[i, 40] = CELL_WALL;
                    }
                    break;
                case 8:
                    for (i = 4; i < 40; i++) {
                        cells[i, 10] = CELL_WALL;
                        cells[53 - i, 20] = CELL_WALL;
                        cells[i, 30] = CELL_WALL;
                        cells[53 - i, 40] = CELL_WALL;
                        cells[i, 50] = CELL_WALL;
                        cells[53 - i, 60] = CELL_WALL;
                        cells[i, 70] = CELL_WALL;
                    }
                    break;
                case 9:
                    for (i = 6; i < 47; i++) {
                        cells[i, i] = CELL_WALL;
                        cells[i, i + 28] = CELL_WALL;
                    }
                    break;
                default:
                    for (i = 4; i < 49; i += 2) {
                        cells[i, 10] = CELL_WALL;
                        cells[i + 1, 20] = CELL_WALL;
                        cells[i, 30] = CELL_WALL;
                        cells[i + 1, 40] = CELL_WALL;
                        cells[i, 50] = CELL_WALL;
                        cells[i + 1, 60] = CELL_WALL;
                        cells[i, 70] = CELL_WALL;
                    }
                    break;


            }

        }
    }
}
