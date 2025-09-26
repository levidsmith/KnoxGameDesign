using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public class BulletWave : Bullet {

        float x_orig;
        float y_orig;
        float fSpeed;
        public BulletWave(int init_x, int init_y) : base(init_x, init_x) {

            x = init_x;
            y = init_y;
            x_orig = init_x;
            y_orig = init_y;

            w = 24;
            h = 24;

            isAlive = true;

            fLifetime = 0f;
            fLifetimeMax = 1f;

            fSpeed = Game1.BLOCK_SIZE * 16;
        }

        public override void Update(float deltaTime, Game1 game) {
            if (!isAlive) {
                return;
            }


            x = x_orig + (fLifetime * fSpeed * vel_x);
            y = y_orig + (MathF.Sin(fLifetime * (2f * MathF.PI) * 2f) * (Game1.BLOCK_SIZE * 2f));


            Enemy e = checkEnemyCollision(game.listEnemies);
            if (e != null) {
                e.setDamage(1);
                isAlive = false;

            }

            fLifetime += deltaTime;
            if (fLifetime > fLifetimeMax) {
                isAlive = false;
            }
        }


    }
}
