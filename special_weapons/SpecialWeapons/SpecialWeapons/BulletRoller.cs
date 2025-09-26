using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpecialWeapons.Player;

namespace SpecialWeapons {

    public class BulletRoller : Bullet {

        public enum RollState { Grounded, Falling };
        public const float FALL_ACCELERATION = Game1.BLOCK_SIZE / 2;



        float x_orig;
        float y_orig;
        float fSpeed;
        RollState rollstate;
        public BulletRoller(int init_x, int init_y) : base(init_x, init_x) {

            x = init_x;
            y = init_y;
            x_orig = init_x;
            y_orig = init_y;

            w = 48;
            h = 48;

            isAlive = true;

            fLifetime = 0f;
            fLifetimeMax = 1f;

            fSpeed = Game1.BLOCK_SIZE * 8;

            vel_y = 8f * Game1.BLOCK_SIZE;
            rollstate = RollState.Falling;
            
        }

        public override void Update(float deltaTime, Game1 game) {
            if (!isAlive) {
                return;
            }

            Block b = null;

            x += vel_x * fSpeed * deltaTime;

            if (rollstate == RollState.Falling) {
                vel_y -= FALL_ACCELERATION;
                y += vel_y * deltaTime;
            } else if (rollstate == RollState.Grounded) {
                b = null;
                b = checkBlockCollision(game.listBlocks, 0, -1);
                if (b == null) {
                    rollstate = RollState.Falling;
                }
            }



            
            b = checkBlockCollision(game.listBlocks, 0, 0);
            if (b != null) {
                rollstate = RollState.Grounded;
                vel_y = 0f;
                y = b.y + b.h;
            }





            Enemy e = checkEnemyCollision(game.listEnemies);
            if (e != null) {
                e.setDamage(1);
                isAlive = false;
            }
        }


    }

}
