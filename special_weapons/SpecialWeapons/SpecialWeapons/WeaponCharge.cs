using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public class WeaponCharge : Weapon {

        public float fChargeTime;

        public WeaponCharge() {
            fShootDelay = 0f;
            fShootDelayMax = 0.25f;
            strName = "Charge X";

        }

        public override void Update(float deltaTime, Game1 game) {
            if (fShootDelay > 0f) {
                fShootDelay -= deltaTime;
            }

            strName = "Charge " + getChargeLevel();
        }

        public override void shoot(Game1 game) {

        }

        public void charge(float fTime, Game1 game) {
            fChargeTime += fTime;

        }
        public void chargeRelease(Game1 game) {
            int bullet_x;
            int bullet_y;
            int bullet_direction;

            Player p = game.player;

            if (fShootDelay > 0f) {
                return;
            }

            if (p.iXFacing == 1) {
                bullet_x = (int)p.x + (int)p.w;
            } else if (game.player.iXFacing == -1) {
                bullet_x = (int)p.x - 24;
            } else {
                bullet_x = (int)p.x;
            }

            bullet_y = (int)(p.y + 32);

            bullet_direction = p.iXFacing;

            BulletCharge b = new BulletCharge(bullet_x, bullet_y);


            if (fChargeTime < 1f) {
                b.setPower(0);
            } else if (fChargeTime < 2f) {
                b.setPower(1);
            } else {
                b.setPower(2);
            }


            b.vel_x = p.iXFacing;
            game.listBullets.Add(b);
            fShootDelay = fShootDelayMax;
            fChargeTime = 0f;
        }

        public int getChargeLevel() {

            if (fChargeTime < 1f) {
                return 0;
            } else if (fChargeTime < 2f) {
                return 1;
            } else {
                return 2;
            }

        }

    }

}
