using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumping {
    public class Player {
        public float x, y, w, h;
        public float vel_x, vel_y;

        public float fJumpButtonTime;
        public float fMaxJumpButtonTime;

        public enum JumpState { Grounded, Rising, Falling };
        public JumpState jumpstate;

        public Player() {
            w = Game1.BLOCK_SIZE;
            h = Game1.BLOCK_SIZE * 2;
            x = 5 * Game1.BLOCK_SIZE;
            y = 2 * Game1.BLOCK_SIZE;

            vel_x = 0f;
            vel_y = 0f;
            jumpstate = JumpState.Grounded;

            fJumpButtonTime = 0f;
            fMaxJumpButtonTime = 0.5f;


        }

        public void Update(float deltaTime) {
            x += vel_x * deltaTime;
            y += vel_y * deltaTime;

            if (x < 0) {
                x = 0;
            } else if (x + w > Game1.SCREEN_WIDTH) {
                x = Game1.SCREEN_WIDTH - w;
            }

            if (jumpstate == JumpState.Rising) {
                vel_y = Game1.BLOCK_SIZE * 8;
                fJumpButtonTime += deltaTime;
                if (fJumpButtonTime > fMaxJumpButtonTime) {
                    jumpstate = JumpState.Falling;
                }
            } else if (jumpstate == JumpState.Falling) {
                vel_y = -Game1.BLOCK_SIZE * 4;

                if (y < Game1.BLOCK_SIZE * 2) {
                    y = Game1.BLOCK_SIZE * 2;
                    jumpstate = JumpState.Grounded;
                    vel_y = 0f;
                }
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
