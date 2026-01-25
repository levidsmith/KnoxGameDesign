using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SnakeGame {
    public class Snake {

        public enum Direction { NORTH, SOUTH, WEST, EAST };

        public byte id;
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

        public List<SnakeBody> body;

        public Snake() {
            body = new List<SnakeBody>();
        }


    }
}
