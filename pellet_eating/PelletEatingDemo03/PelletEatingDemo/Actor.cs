using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelletEatingDemo {
    internal abstract class Actor {
        public float x, y;
        public float w, h;
        public enum Direction { UP, DOWN, LEFT, RIGHT };
        protected Direction direction;
        protected float fSpeed;

        public abstract void move(float deltaTime);
        public bool collision(float other_x, float other_y, float other_w, float other_h) {
            if (x + w < other_x ||
                x > other_x + other_w ||
                y + h < other_y ||
                y > other_y + other_h) {
                return false;
            } else {
                return true;
            }
        }


    }
}
