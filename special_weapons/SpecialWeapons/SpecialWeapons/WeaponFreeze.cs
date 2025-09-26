using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public class WeaponFreeze : Weapon {
        public WeaponFreeze() {
            fShootDelay = 0f;
            fShootDelayMax = 0.25f;
            strName = "Freeze";

        }

        public override void Update(float deltaTime, Game1 game) {
            if (fShootDelay > 0f) {
                fShootDelay -= deltaTime;
            }
        }

        public override void shoot(Game1 game) {

            fShootDelay = fShootDelayMax;

            foreach(Enemy e in game.listEnemies) {
                e.startFreeze();
            }

        }

    }
}
