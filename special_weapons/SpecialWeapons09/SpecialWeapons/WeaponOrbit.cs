using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {

    public class WeaponOrbit : Weapon {

        public List<BulletOrbit> bullets;

        public WeaponOrbit() {
            fShootDelay = 0f;
            fShootDelayMax = 0.25f;
            strName = "Orbit";
            bullets = new List<BulletOrbit>();

        }

        public override void Update(float deltaTime, Game1 game) {
            if (fShootDelay > 0f) {
                fShootDelay -= deltaTime;
            }

            foreach (BulletOrbit b in bullets) {
                b.setOrbitPosition((int)(game.player.x + game.player.w / 2), (int)(game.player.y + game.player.h / 2));
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

            foreach (BulletOrbit b1 in bullets) {
                b1.destroy();
            }
            bullets.Clear();


            bullet_x = (int)p.x + (int)p.w / 2;
            bullet_y = (int)(game.player.y + game.player.h / 2);

            bullet_direction = p.iXFacing;


            BulletOrbit b;
            b = new BulletOrbit(bullet_x, bullet_y);
            b.setOffset(0f);
            game.listBullets.Add(b);
            bullets.Add(b);

            b = new BulletOrbit(bullet_x, bullet_y);
            b.setOffset(0.25f);
            game.listBullets.Add(b);
            bullets.Add(b);

            b = new BulletOrbit(bullet_x, bullet_y);
            b.setOffset(0.5f);
            game.listBullets.Add(b);
            bullets.Add(b);

            b = new BulletOrbit(bullet_x, bullet_y);
            b.setOffset(0.75f);
            game.listBullets.Add(b);
            bullets.Add(b);


            fShootDelay = fShootDelayMax;

        }


    }
}
