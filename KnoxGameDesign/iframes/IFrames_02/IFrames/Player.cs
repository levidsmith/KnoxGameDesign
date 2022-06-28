using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IFrames {
    class Player : GameObject {
        public int iHP;
        bool isAlive;
        float vel_x, vel_y;
        float fInvincibilityTime;

        public Player(string in_strName, Texture2D in_img, GameManager in_gamemanager) : base(in_strName, in_img, in_gamemanager) {
            iHP = 20;
            fInvincibilityTime = 0f;
            isAlive = true;
            

        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isAlive) {
                //move
                x += vel_x * deltaTime;
                y += vel_y * deltaTime;
                checkBounds();

                //collision
                if (fInvincibilityTime <= 0f) {
                    foreach (GameObject obj in gamemanager.gameobjects) {
                        if (obj is Enemy) {
                            if (hasCollided(obj)) {
                                iHP -= 1;
                                fInvincibilityTime = 2f;
                            }
                        }
                    }

                    if (iHP <= 0) {
                        isAlive = false;
                        img = gamemanager.textures["player_dead"];
                    }
                } else {
                    fInvincibilityTime -= deltaTime;

                }
            }


        }

        
        public override void Draw(SpriteBatch sb) {
            float fAlpha = 1f;
            if (fInvincibilityTime > 0f) {
                fAlpha = 0.2f;
            }
            sb.Draw(img, new Rectangle((int)x, (int)y, w, h), new Color(1f, 1f, 1f, fAlpha));
        }

        private void checkBounds() {
            if (x < 0) {
                x = 0;
            } else if (x + w > 640) {
                x = 640 - w;
            }

            if (y < 0) {
                y = 0;
            } else if (y + h > 480) {
                y = 480 - h;
            }
        }

        public void moveLeft() {
            vel_x = -2f * UNIT_SIZE;
        }

        public void stopMovingLeft() {
            if (vel_x < 0f) {
                vel_x = 0f;
            }
        }

        public void moveRight() {
            vel_x = 2f * UNIT_SIZE;


        }
        public void stopMovingRight() {
            if (vel_x > 0f) {
                vel_x = 0f;
            }


        }

        public void moveUp() {
            vel_y = -2f * UNIT_SIZE;

        }
        public void stopMovingUp() {
            if (vel_y < 0f) {
                vel_y = 0f;
            }

        }

        public void moveDown() {
            vel_y = 2f * UNIT_SIZE;

        }

        public void stopMovingDown() {
            if (vel_y > 0f) {
                vel_y = 0f;
            }

        }

    }
}
