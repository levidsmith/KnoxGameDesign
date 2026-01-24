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

        public const byte CELL_WALL = 3;

        public Arena() {
            cells = new byte[ARENA_ROWS, ARENA_COLS];
            setup();
        }

        private void setup() {
            int i;
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
