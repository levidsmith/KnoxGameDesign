using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelletEatingDemo {
    public abstract class Actor {
        public int x, y;
        public int w, h;

        public int xStart, yStart;

        public enum Direction { UP, DOWN, LEFT, RIGHT };
        protected Direction direction;
        protected int iSpeed;

        public abstract void move(float deltaTime);
        public bool collision(int other_x, int other_y, int other_w, int other_h) {
            if (x + w <= other_x ||
                x >= other_x + other_w ||
                y + h <= other_y ||
                y >= other_y + other_h) {
                return false;
            } else {
                return true;
            }
        }

        public bool checkWallCollision(int xDiff, int yDiff, List<Wall> walls) {
            int x1 = x + xDiff;
            int y1 = y + yDiff;
            

            foreach(Wall wall in walls) {
                if (!
                    (x1 + w <= wall.x ||
                    x1 >= wall.x + wall.w ||
                    y1 + h <= wall.y ||
                    y1 >= wall.y + wall.h)
                    ){
                    return true;
                }
            }
            return false;

        }

        public void resetPosition() {
            x = xStart;
            y = yStart;
        }


    }
}
