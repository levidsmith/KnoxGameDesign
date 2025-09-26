using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public abstract class Weapon {
        protected float fShootDelay;
        protected float fShootDelayMax;
        public string strName;

        public Weapon() {
            fShootDelay = 0f;
            fShootDelayMax = 0.25f;

        }

        public abstract void Update(float deltaTime, Game1 game);
        public abstract void shoot(Game1 game);


        }
    }
