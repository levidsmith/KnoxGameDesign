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

        }
    }
}
