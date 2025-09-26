using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Jumping {
    public class Player {
        public float x, y, w, h;
        public float vel_x, vel_y;

        public float fJumpButtonTime;
        public float fMaxJumpButtonTime;

        public const float RISE_VELOCITY = Game1.BLOCK_SIZE * 8;
        public const float FALL_ACCELERATION = Game1.BLOCK_SIZE / 2;

        public float fDebugJumpTime;
        public float fDebugJumpHeight;

        //        public float accel_x;
        //        public float accel_y;

        public enum JumpState { Grounded, Rising, Falling };
        public JumpState jumpstate;

        public enum WalkState { Left, Right, None };
        public WalkState walkstate;

        public Player() {
            w = Game1.BLOCK_SIZE;
            h = Game1.BLOCK_SIZE * 2;
            x = 5 * Game1.BLOCK_SIZE;
            y = 2 * Game1.BLOCK_SIZE;

            vel_x = 0f;
            vel_y = 0f;
            jumpstate = JumpState.Grounded;

            fJumpButtonTime = 0f;
            fMaxJumpButtonTime = 0.4f;

            fDebugJumpTime = 0f;



        }

        public void Update(float deltaTime) {
            x += vel_x * deltaTime;
            y += vel_y * deltaTime;

            if (x < 0) {
                x = 0;
                vel_x = 0f;
            } else if (x + w > Game1.SCREEN_WIDTH) {
                x = Game1.SCREEN_WIDTH - w;
                vel_x = 0f;
            }

            //set jump velocity based on acceleration
            if (jumpstate == JumpState.Rising) {
                fDebugJumpTime += deltaTime;
                fDebugJumpHeight = y - (Game1.BLOCK_SIZE * 2);

                //vel_y = Game1.BLOCK_SIZE * 8;
                vel_y = RISE_VELOCITY;
                fJumpButtonTime += deltaTime;
                if (fJumpButtonTime > fMaxJumpButtonTime) {
                    jumpstate = JumpState.Falling;
                }
            } else if (jumpstate == JumpState.Falling) {
                fDebugJumpTime += deltaTime;
                if (y - (Game1.BLOCK_SIZE * 2) > fDebugJumpHeight) {
                    fDebugJumpHeight = y - (Game1.BLOCK_SIZE * 2);
                }

                vel_y -= FALL_ACCELERATION;

                if (y < Game1.BLOCK_SIZE * 2) {
                    y = Game1.BLOCK_SIZE * 2;
                    jumpstate = JumpState.Grounded;
                    vel_y = 0f;
                }
            }

            //set walk velocity based on acceleration
            float accel_x = Game1.BLOCK_SIZE * 8;
            if (walkstate == WalkState.Left) {
                //vel_x = -Game1.BLOCK_SIZE * 4;
                vel_x -= accel_x * deltaTime;
                if (vel_x < -Game1.BLOCK_SIZE * 8) {
                    vel_x = -Game1.BLOCK_SIZE * 8;
                }
            } else if (walkstate == WalkState.Right) {
                //vel_x = Game1.BLOCK_SIZE * 4;
                vel_x += accel_x * deltaTime;
                if (vel_x > Game1.BLOCK_SIZE * 8) {
                    vel_x = Game1.BLOCK_SIZE * 8;
                }
            } else {
                //vel_x = 0f;
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
        }

        public void startJump() {
            if (jumpstate == JumpState.Grounded) {
                jumpstate = JumpState.Rising;

                fDebugJumpTime = 0f;
                fDebugJumpHeight = 0f;
            }
        }

        public void stopJump() {
            jumpstate = JumpState.Falling;
            fJumpButtonTime = 0f;

        }

        public void walkLeft() {
            walkstate = WalkState.Left;
        }

        public void walkRight() {
            walkstate = WalkState.Right;

        }

        public void walkNone() {
            walkstate = WalkState.None;

        }

        public string getJumpstateName() {
            switch(jumpstate) {
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

    }
}
