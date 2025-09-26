using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public  class WeaponEightWay : Weapon {
        public WeaponEightWay() {
            fShootDelay = 0f;
            fShootDelayMax = 0.25f;
            strName = "Eight Way";

        }

        public override void Update(float deltaTime, Game1 game) {
            if (fShootDelay > 0f) {
                fShootDelay -= deltaTime;
            }
        }

        public override void shoot(Game1 game) {
            int bullet_x;
            int bullet_y;
            

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

            
            Bullet b = new Bullet(bullet_x, bullet_y);

            if (game.player.fInputDirectionX != 0f || game.player.fInputDirectionY != 0f) {
                b.setVelocity(game.player.fInputDirectionX, game.player.fInputDirectionY);
            } else {
                b.setVelocity(p.iXFacing, 0f);
            }
                game.listBullets.Add(b);
            fShootDelay = fShootDelayMax;

        }

    }
}
