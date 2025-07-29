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
            direction = Direction.NONE;

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
                        direction = Direction.NONE;
                    } else if (direction == Direction.DOWN) {
                        y = wall.y - h;
                        direction = Direction.NONE;
                    } else if (direction == Direction.LEFT) {
                        x = wall.x + wall.w;
                        direction = Direction.NONE;
                    } else if (direction == Direction.RIGHT) {
                        x = wall.x - w;
                        direction = Direction.NONE;
                    }

                }
            }
        }

        public void checkChangeDirection() {
            bool canMoveUp;
            bool canMoveDown;
            bool canMoveLeft;
            bool canMoveRight;

            canMoveUp = canMoveDirection(Direction.UP, game.walls);
            canMoveDown = canMoveDirection(Direction.DOWN, game.walls);
            canMoveLeft = canMoveDirection(Direction.LEFT, game.walls);
            canMoveRight = canMoveDirection(Direction.RIGHT, game.walls);

            List<Direction> validMoves = new List<Direction>();
            Random rand = new Random();
            int iRand;


            if (direction == Direction.LEFT) {
                if (canMoveLeft) {
                    validMoves.Add(Direction.LEFT);
                }
                if (canMoveUp) {
                    validMoves.Add(Direction.UP);
                }
                if (canMoveDown) {
                    validMoves.Add(Direction.DOWN);
                }
            } else if (direction == Direction.RIGHT) {
                if (canMoveRight) {
                    validMoves.Add(Direction.RIGHT);
                }
                if (canMoveUp) {
                    validMoves.Add(Direction.UP);
                }
                if (canMoveDown) {
                    validMoves.Add(Direction.DOWN);
                }
            } else if (direction == Direction.UP) {
                if (canMoveUp) {
                    validMoves.Add(Direction.UP);
                }
                if (canMoveLeft) {
                    validMoves.Add(Direction.LEFT);
                }
                if (canMoveRight) {
                    validMoves.Add(Direction.RIGHT);
                }
            } else if (direction == Direction.DOWN) {
                if (canMoveDown) {
                    validMoves.Add(Direction.DOWN);
                }
                if (canMoveLeft) {
                    validMoves.Add(Direction.LEFT);
                }
                if (canMoveRight) {
                    validMoves.Add(Direction.RIGHT);
                }
            }

            if (validMoves.Count > 0) {
                direction = validMoves[rand.Next(0, validMoves.Count)];
            }


        }


        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            sb.Draw(textures["enemy"], new Rectangle(x, y, w, h), color);
        }




    }
}
