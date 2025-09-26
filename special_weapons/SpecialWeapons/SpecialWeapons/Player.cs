using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public const float WALK_ACCELERATION = Game1.BLOCK_SIZE * 8;
        public const float WALK_SPEED_MAX = Game1.BLOCK_SIZE * 8;
        public const float WALK_DECELERATION = Game1.BLOCK_SIZE * 16;
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
            x = 4 * Game1.BLOCK_SIZE;
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

            //check room bounds
            if (x < 0) {
                x = 0;
                vel_x = 0f;
            } else if (x + w > Game1.SCREEN_WIDTH) {
                x = Game1.SCREEN_WIDTH - w;
                vel_x = 0f;
            }

            //handle Y velocity
            if (jumpstate == JumpState.Rising) {
                //set velocity based on jump velocity
                vel_y = RISE_VELOCITY;
                fJumpButtonTime += deltaTime;
                if (fJumpButtonTime > fMaxJumpButtonTime) {
                    jumpstate = JumpState.Falling;
                }

                //check collision with overhead block
                b = null;
                b = checkBlockCollision(game.listBlocks, 0, (int)MathF.Ceiling(vel_y * deltaTime));
                if (b != null) {
                    vel_y = 0f;
                    y = b.y - h;
                    jumpstate = JumpState.Falling;
                    fJumpButtonTime = 0f;
                }

            } else if (jumpstate == JumpState.Falling) {
                //set velocity based on fall acceleration
                vel_y -= FALL_ACCELERATION;

                //check collision with overhead block ("falling" may still have upwards velocity)
                if (vel_y > 0f) {
                    b = null;
                    b = checkBlockCollision(game.listBlocks, 0, (int)MathF.Ceiling(vel_y * deltaTime));
                    if (b != null) {
                        vel_y = 0f;
                        y = b.y - h;
                        jumpstate = JumpState.Falling;
                        fJumpButtonTime = 0f;
                    }
                } else if (vel_y < 0f) {
                    //check collision with ground
                    b = null;
                    b = checkBlockCollision(game.listBlocks, 0, 0);
                    if (b != null) {
                        jumpstate = JumpState.Grounded;
                        vel_y = 0f;
                        y = b.y + b.h;
                    }
                }

            } else if (jumpstate == JumpState.Grounded) {
                //check if grounded player is now falling
                b = null;
                b = checkBlockCollision(game.listBlocks, 0, -1);
                if (b == null) {
                    jumpstate = JumpState.Falling;
                }
            }

            //handle X velocity
            if (walkstate == WalkState.Left) {
                //calculate new velocity
                vel_x -= WALK_ACCELERATION * deltaTime;
                if (vel_x < -WALK_SPEED_MAX) {
                    vel_x = -WALK_SPEED_MAX;
                }

            } else if (walkstate == WalkState.Right) {
                //calculate new velocity
                vel_x += WALK_ACCELERATION * deltaTime;
                if (vel_x > WALK_SPEED_MAX) {
                    vel_x = WALK_SPEED_MAX;
                }

            } else {
                if (vel_x > 0f) {
                    //calculate new velocity
                    vel_x -= WALK_DECELERATION * deltaTime;
                    if (vel_x < 0f) {
                        vel_x = 0f;
                    }

                } else if (vel_x < 0f) {
                    //calculate new velocity
                    vel_x += WALK_DECELERATION * deltaTime;
                    if (vel_x > 0f) {
                        vel_x = 0f;
                    }

                }
            }

            //check horizontal velocity collision
            if (vel_x > 0f) {
                //stop moving if collided into block
                b = null;
                b = checkBlockCollision(game.listBlocks, (int)MathF.Ceiling(vel_x * deltaTime), 0);
                if (b != null) {
                    x = b.x - w;
                    vel_x = 0f;
                    walkstate = WalkState.None;
                }

            } else if (vel_x < 0f) {
                //stop moving if collided into block
                b = null;
                b = checkBlockCollision(game.listBlocks, (int)MathF.Floor(vel_x * deltaTime), 0);
                if (b != null) {
                    x = b.x + b.w;
                    vel_x = 0f;
                    walkstate = WalkState.None;
                }

            }

            //move the player
            x += vel_x * deltaTime;
            y += vel_y * deltaTime;

            //update the sprite index
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

        public void charge(float fTime, Game1 game) {
            if (weapon != null && weapon is WeaponCharge) {
                ((WeaponCharge)weapon).charge(fTime, game);
            }
        }

        public void chargeRelease(Game1 game) {
            if (weapon != null && weapon is WeaponCharge) {
                ((WeaponCharge)weapon).chargeRelease(game);
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
