//2026 - Levi D. Smith <developer@levidsmith.com>
//for KnoxGameDesign www.knoxgamedesign.org
namespace SnakeGame {
    public class Arena {
        public const int ARENA_ROWS = 48;
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
                    for (i = 20; i <= 60; i++) {
                        setCellWall(25, i);
                    }
                    break;
                case 3:
                    for (i = 10; i <= 40; i++) {
                        setCellWall(i, 20);
                        setCellWall(i, 60);
                    }
                    break;
                case 4:
                    for (i = 4; i <= 30; i++) {
                        setCellWall(i, 20);
                        setCellWall(53 - i, 60);
                    }
                    for (i = 2; i <= 40; i++) {
                        setCellWall(38, i);
                        setCellWall(15, 81 - i);
                    }

                    break;
                case 5:
                    for (i = 13; i <= 39; i++) {
                        setCellWall(i, 21);
                        setCellWall(i, 59);
                    }
                    for (i = 23; i <= 57; i++) {
                        setCellWall(11, i);
                        setCellWall(41, i);
                    }
                    break;

                case 6:
                    for (i = 4; i <= 49; i++) {
                        if (i > 30 || i < 23) {
                            setCellWall(i, 10);
                            setCellWall(i, 20);
                            setCellWall(i, 30);
                            setCellWall(i, 40);
                            setCellWall(i, 50);
                            setCellWall(i, 60);
                            setCellWall(i, 70);
                        }
                    }
                    break;
                case 7:
                    for (i = 4; i <= 49; i += 2) {
                        setCellWall(i, 40);
                    }
                    break;
                case 8:
                    for (i = 4; i <= 40; i++) {
                        setCellWall(i, 10);
                        setCellWall(53 - i, 20);
                        setCellWall(i, 30);
                        setCellWall(53 - i, 40);
                        setCellWall(i, 50);
                        setCellWall(53 -i, 60);
                        setCellWall(i, 70);
                    }
                    break;
                case 9:
                    for (i = 6; i <= 47; i++) {
                        setCellWall(i, i);
                        setCellWall(i, i + 28);
                    }
                    break;
                default:
                    for (i = 4; i <= 49; i += 2) {
                        setCellWall(i, 10);
                        setCellWall(i + 1, 20);
                        setCellWall(i, 30);
                        setCellWall(i + 1, 40);
                        setCellWall(i, 50);
                        setCellWall(i + 1, 60);
                        setCellWall(i, 70);
                    }
                    break;


            }

        }

        private void setCellWall(int iRow, int iCol) {
            //Converts from QBasic index format starting at 1 and
            //subtracts off the top two rows in QBasic for displaying score
            int iRowOffset = 3;
            int iColOffset = 1;
            cells[iRow - iRowOffset, iCol - iColOffset] = CELL_WALL;
        }
    }
}
