using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class SpaceStart : Space {
        public string strName;

        public SpaceStart() {
            strName = "Start";

        }

        public void action() {
            gamemanager.playerCurrent.iMoney += 200;

        }
    }
}
