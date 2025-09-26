using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SpecialWeapons {
    public class Player {
        public float x, y, w, h;
        public float vel_x, vel_y;
        

        public float fJumpButtonTime;
        public float fMaxJumpButtonTime;

        public const float RISE_VELOCITY = Game1.BLOCK_SIZE * 8;
        public const float FALL_ACCELERATION = Game1.BLOCK_SIZE / 2;

        public enum JumpState { Grounded, Rising, Falling };
        public JumpState jumpstate;
        public enum WalkState { Left, Right, None };
        public WalkState walkstate;


        public int iXFacing;
        float fWalkFrameTime;
        float fWalkFrameTimeMax = 0.2f;
        int iWalkSpriteIndex;

        public Weapon weapon;
        public int iCurrentWeapon;

        public float fInputDirectionX;
        public float fInputDirectionY;




        public Player() {
            w = Game1.BLOCK_SIZE;
            h = Game1.BLOCK_SIZE * 2;
            x = 2 * Game1.BLOCK_SIZE;
            y = Game1.BLOCK_SIZE;

            vel_x = 0f;
            vel_y = 0f;
            jumpstate = JumpState.Grounded;

            fJumpButtonTime = 0f;
            fMaxJumpButtonTime = 0.4f;
            iWalkSpriteIndex = 0;
            iXFacing = 1;

        }

        public void Update(float deltaTime, Game1 game) {
            Block b;


            if (x < 0) {
                x = 0;
                vel_x = 0f;
            } else if (x + w > Game1.SCREEN_WIDTH) {
                x = Game1.SCREEN_WIDTH - w;
                vel_x = 0f;
            }



            //set jump velocity based on acceleration
            if (jumpstate == JumpState.Rising) {
                vel_y = RISE_VELOCITY;
                fJumpButtonTime += deltaTime;
                if (fJumpButtonTime > fMaxJumpButtonTime) {
                    jumpstate = JumpState.Falling;
                }
            } else if (jumpstate == JumpState.Falling) {
                vel_y -= FALL_ACCELERATION;

                b = null;
                b = checkBlockCollision(game.listBlocks, 0, 0);
                if (b != null) {
                    jumpstate = JumpState.Grounded;
                    vel_y = 0f;
                    y = b.y + b.h;
                }

            } else if (jumpstate == JumpState.Grounded) {
                b = null;
                b = checkBlockCollision(game.listBlocks, 0, -1);
                if (b == null) {
                    jumpstate = JumpState.Falling;
                }
            }



            //set walk velocity based on acceleration
            float accel_x = Game1.BLOCK_SIZE * 8;
            if (walkstate == WalkState.Left) {
                vel_x -= accel_x * deltaTime;
                if (vel_x < -Game1.BLOCK_SIZE * 8) {
                    vel_x = -Game1.BLOCK_SIZE * 8;
                }


            } else if (walkstate == WalkState.Right) {
                vel_x += accel_x * deltaTime;
                if (vel_x > Game1.BLOCK_SIZE * 8) {
                    vel_x = Game1.BLOCK_SIZE * 8;
                }
            } else {
                float decelerate_x = Game1.BLOCK_SIZE * 16;
                if (vel_x > 0f) {
                    vel_x -= decelerate_x * deltaTime;
                    if (vel_x < 0f) {
                        vel_x = 0f;
                    }
                } else if (vel_x < 0f) {
                    vel_x += decelerate_x * deltaTime;
                    if (vel_x > 0f) {
                        vel_x = 0f;
                    }
                }



            }


            x += vel_x * deltaTime;
            y += vel_y * deltaTime;

            if (vel_x != 0f && vel_y == 0) {
                fWalkFrameTime += deltaTime;
                if (fWalkFrameTime > fWalkFrameTimeMax) {
                    iWalkSpriteIndex++;
                    if (iWalkSpriteIndex > 3) {
                        iWalkSpriteIndex = 1;
                    }
                    fWalkFrameTime = 0f;
                }
            } else if (vel_x == 0f) {
                iWalkSpriteIndex = 0;
            }


        }

        public void startJump() {
            if (jumpstate == JumpState.Grounded) {
                jumpstate = JumpState.Rising;

            }
        }

        public void stopJump() {
            jumpstate = JumpState.Falling;
            fJumpButtonTime = 0f;

        }

        public void walkLeft() {
            walkstate = WalkState.Left;
            iXFacing = -1;
        }

        public void walkRight() {
            walkstate = WalkState.Right;
            iXFacing = 1;

        }

        public void walkNone() {
            walkstate = WalkState.None;

        }

        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            
            if (iXFacing == 1) {
                sb.Draw(textures["player"], new Rectangle((int)x, Game1.SCREEN_HEIGHT - (int)y - (int)h, (int)w, (int)h), new Rectangle(0 + (16 * iWalkSpriteIndex), 0, 16, 32), Color.White);
            } else if (iXFacing == -1) {
                sb.Draw(textures["player"], new Rectangle((int)x, Game1.SCREEN_HEIGHT - (int)y - (int)h, (int)w, (int)h), new Rectangle(0 + (16 * iWalkSpriteIndex), 0, 16, 32), Color.White,
                    0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }

        }

        private Block checkBlockCollision(List<Block> listBlocks, int diff_x, int diff_y) {
            foreach (Block b in listBlocks) {
                if (collided(b, (int) x + diff_x, (int) y + diff_y)) {
                    return b;
                }

            }
            return null;
        }
        private bool collided(Block b, int x1, int y1) {
            if (x1 + w <= b.x ||
                x1 >= b.x + b.w ||
                y1 + h <= b.y ||
                y1 >= b.y + b.h) {
                return false;
            } else {
                return true;
            }

        }

        public string getJumpstateName() {
            switch (jumpstate) {
                case JumpState.Grounded:
                    return "grounded";
                case JumpState.Rising:
                    return "rising";
                case JumpState.Falling:
                    return "falling";
                default:
                    return "error";
            }
        }

        public void shoot(Game1 game) {
            if (weapon != null) {
                weapon.shoot(game);
            }
        }

        public void selectNextWeapon(Game1 game) {
            iCurrentWeapon++;
            if (iCurrentWeapon >= game.listWeapons.Count) {
                iCurrentWeapon = 0;
            }

            weapon = game.listWeapons[iCurrentWeapon];

        }

        public void setInputDirection(bool isInputUp, bool isInputDown, bool isInputLeft, bool isInputRight) {
            float fInputX = 0f;
            float fInputY = 0f;

            if (isInputUp) {
                fInputY = 1f;
            } else if (isInputDown) {
                fInputY = -1f;
            }

            if (isInputLeft) {
                fInputX = -1f;
            } else if (isInputRight) {
                fInputX = 1f;
            }

            fInputDirectionX = 1f;

            float fMag = MathF.Sqrt(MathF.Pow(fInputX, 2) + MathF.Pow(fInputY, 2));

            if (fMag > 0f) {
                fInputDirectionX = fInputX / fMag;
                fInputDirectionY = fInputY / fMag;
            } else {
                fInputDirectionX = 0f;
                fInputDirectionY = 0f;

            }


        }



    }



}
