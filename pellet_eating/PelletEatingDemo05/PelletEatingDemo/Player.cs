using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;

namespace PelletEatingDemo {
    public class Player : Actor {

        bool isMoving;
        public Game1 game;
        public Player(Game1 g) {
            game = g;
            w = 32;
            h = 32;
            iSpeed = 8;
            direction = Direction.LEFT;
            isMoving = true;

            setStartPosition();
        }

        public void setStartPosition() {
            x = 20 * 32;
            y = 11 * 32;

        }

        public override void move(float deltaTime) {
            if (isMoving) {
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
        }

        public void inputUp() {
            if (!checkWallCollision(0, -iSpeed, game.walls)) {
                direction = Direction.UP;
                isMoving = true;
            }
        }

        public void inputDown() {
            if (!checkWallCollision(0, iSpeed, game.walls)) {
                direction = Direction.DOWN;
                isMoving = true;
            }
        }

        public void inputLeft() {
            if (!checkWallCollision(-iSpeed, 0, game.walls)) {
                direction = Direction.LEFT;
                isMoving = true;
            }
        }
        public void inputRight() {
            if (!checkWallCollision(iSpeed, 0, game.walls)) {
                direction = Direction.RIGHT;
                isMoving = true;
            }
        }

        public void checkPelletCollision() {
            List<Pellet> pellets = game.pellets;
            int i;

            i = pellets.Count - 1;
            while (i >= 0) {
                if (collision(pellets[i].x, pellets[i].y, pellets[i].w, pellets[i].h)) {
                    pellets.RemoveAt(i);
                }
                i--;
            }

        }

        public void checkWallCollision() {
            foreach (Wall wall in game.walls) {
                if (collision(wall.x, wall.y, wall.w, wall.h)) {
                    isMoving = false;
                    if (direction == Direction.UP) {
                        y = wall.y + wall.h;
                    } else if (direction == Direction.DOWN) {
                        y = wall.y - h;
                    } else if (direction == Direction.LEFT) {
                        x = wall.x + wall.w;
                    } else if (direction == Direction.RIGHT) {
                        x = wall.x - w;
                    }

                }
            }
        }

        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            float fRotation;
            if (direction == Direction.RIGHT) {
                fRotation = 0f;
            } else if (direction == Direction.DOWN) {
                fRotation = 0.5f * (float) Math.PI;
            } else if (direction == Direction.LEFT) {
                fRotation = 1.0f * (float)Math.PI;
            } else if (direction == Direction.UP) {
                fRotation = 1.5f * (float)Math.PI;
            } else {
                fRotation = 0f;
            }

            sb.Draw(textures["player"], new Rectangle((int)(x + (w / 2)), (int)(y + (h/ 2)), (int)w, (int)h), new Rectangle(0, 0, 32, 32), Color.White, fRotation, new Vector2(16, 16), SpriteEffects.None, 0f);
        }

    }
}
