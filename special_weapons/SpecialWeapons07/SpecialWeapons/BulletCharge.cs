using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public class BulletCharge : Bullet {

        float x_orig;
        float y_orig;
        float fSpeed;
        public int iPower;
        public BulletCharge(int init_x, int init_y) : base(init_x, init_x) {

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


            x = x_orig + fLifetime * fSpeed * vel_x;
            y = y_orig;


            Enemy e = checkEnemyCollision(game.listEnemies);
            if (e != null) {
                int iDamage = 0;
                switch (iPower) {
                    case 0:
                        iDamage = 1;
                        break;
                    case 1:
                        iDamage = 2;
                        break;
                    case 2:
                        iDamage = 5;
                        break;
                }

                e.setDamage(iDamage);
                isAlive = false;

            }

            fLifetime += deltaTime;
            if (fLifetime > fLifetimeMax) {
                isAlive = false;
            }
        }

        public void setPower(int iValue) {
            iPower = iValue;
            w = 24 * (iValue + 1);
            h = 24 * (iValue + 1);
        }


        public override void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            if (!isAlive) {
                return;
            }

            Color c = Color.Red;
            switch(iPower) {
                case 0:
                    c = Color.Green;
                    break;
                case 1:
                    c = Color.Blue;
                    break;
                case 2:
                    c = Color.Red;
                    break;
            }

            sb.Draw(textures["bullet"], new Rectangle((int)x, (int)Game1.SCREEN_HEIGHT - (int)y - h, w, h), new Rectangle(0, 0, 8, 8), c);


        }



    }
}
