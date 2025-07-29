using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelletEatingDemo {
    public class Enemy : Actor {

        public Game1 game;
        public Color color;
        public Enemy(Game1 g) {
            game = g;
            w = 32;
            h = 32;
            iSpeed = 2;
            direction = Direction.RIGHT;

        }

        public override void move(float deltaTime) {
                if (direction == Direction.UP) {
                    y -= iSpeed;
                } else if (direction == Direction.DOWN) {
                    y += iSpeed;
                } else if (direction == Direction.LEFT) {
                    x -= iSpeed;
                } else if (direction == Direction.RIGHT) {
                    x += iSpeed;
                }
        }

        public void checkWallCollision() {
            foreach (Wall wall in game.walls) {
                if (collision(wall.x, wall.y, wall.w, wall.h)) {
                    if (direction == Direction.UP) {
                        y = wall.y + wall.h;
                        direction = Direction.DOWN;
                    } else if (direction == Direction.DOWN) {
                        y = wall.y - h;
                        direction = Direction.UP;
                    } else if (direction == Direction.LEFT) {
                        x = wall.x + wall.w;
                        direction = Direction.RIGHT;
                    } else if (direction == Direction.RIGHT) {
                        x = wall.x - w;
                        direction = Direction.LEFT;
                    }

                }
            }
        }


        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            sb.Draw(textures["enemy"], new Rectangle(x, y, w, h), color);
        }




    }
}
