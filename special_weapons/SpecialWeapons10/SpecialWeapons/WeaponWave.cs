using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public class WeaponWave : Weapon {
        public WeaponWave() {
            fShootDelay = 0f;
            fShootDelayMax = 0.25f;
            strName = "Wave";

        }

        public override void Update(float deltaTime, Game1 game) {
            if (fShootDelay > 0f) {
                fShootDelay -= deltaTime;
            }
        }

        public override void shoot(Game1 game) {
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

            BulletWave b = new BulletWave(bullet_x, bullet_y);
            b.vel_x = p.iXFacing;
            game.listBullets.Add(b);
            fShootDelay = fShootDelayMax;



        }

    }
}
