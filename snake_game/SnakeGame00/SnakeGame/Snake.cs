using Microsoft.Xna.Framework;

namespace SnakeGame {
    internal class Snake {

        public enum Direction { NORTH, SOUTH, EAST, WEST };


        public string strName;

        public int iHead;
        public int iLength;

        public int iRow;
        public int iCol;
        public Direction direction;
        public int iLives;
        public int iScore;
        public Color color;
        public bool isAlive;


    }
}
