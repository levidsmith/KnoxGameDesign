using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {

    public class BulletTornado : Bullet {

        float x_orig;
        float y_orig;


        float y_accel;
        public BulletTornado(int init_x, int init_y) : base(init_x, init_x) {

            x = init_x;
            y = init_y;
            x_orig = init_x;
            y_orig = init_y;

            w = 24;
            h = 24;

            isAlive = true;

            fLifetime = 0f;
            fLifetimeMax = 2f;

            y_accel = Game1.BLOCK_SIZE * 4f;


        }

        public override void Update(float deltaTime, Game1 game) {
            if (!isAlive) {
                return;
            }

            vel_y += y_accel * deltaTime;


            x = x_orig + (fLifetime * vel_x);
            y = y_orig + (fLifetime * vel_y);


            Enemy e = checkEnemyCollision(game.listEnemies);
            if (e != null) {
                e.setDamage(1);

            }

            fLifetime += deltaTime;
            if (fLifetime > fLifetimeMax) {
                isAlive = false;
            }
        }


    }

}
