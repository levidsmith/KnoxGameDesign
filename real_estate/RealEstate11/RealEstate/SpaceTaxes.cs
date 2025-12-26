using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class SpaceTaxes : Space {
        public string strName;
        public int iTaxAmount;

        public SpaceTaxes(int iAmount) {
            iTaxAmount = iAmount;
            strName = "Taxes $" + iTaxAmount;

        }

        public void action() {
            gamemanager.playerCurrent.iMoney -= iTaxAmount;
            gamemanager.strMessage = gamemanager.playerCurrent.strName + " paid $" + iTaxAmount + " in taxes";

        }
    }
}
