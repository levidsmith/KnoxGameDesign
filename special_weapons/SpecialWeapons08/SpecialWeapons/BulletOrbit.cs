using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {

    public class BulletOrbit : Bullet {

        float x_orig;
        float y_orig;
        float fSpeed;
        float fDistance;
        public BulletOrbit(int init_x, int init_y) : base(init_x, init_x) {

            x = init_x;
            y = init_y;
            x_orig = init_x;
            y_orig = init_y;

            w = 24;
            h = 24;

            isAlive = true;

            fSpeed = 1f; //orbits per second
            fDistance = Game1.BLOCK_SIZE * 2f;
        }

        public override void Update(float deltaTime, Game1 game) {
            if (!isAlive) {
                return;
            }


            
            x = x_orig + (MathF.Cos(fLifetime * (2f * MathF.PI) * fSpeed) * fDistance);
            y = y_orig + (MathF.Sin(fLifetime * (2f * MathF.PI) * fSpeed) * fDistance);


            Enemy e = checkEnemyCollision(game.listEnemies);
            if (e != null) {
                e.setDamage(1);
                isAlive = false;

            }

            fLifetime += deltaTime;
        }

        public void setOffset(float fPercent) {
            fLifetime = fPercent / fSpeed;
        }

        public void setOrbitPosition(int x, int y) {
            x_orig = x;
            y_orig = y;
        }



    }


}
