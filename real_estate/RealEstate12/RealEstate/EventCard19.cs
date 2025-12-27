using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class EventCard19 : EventCard {

        public EventCard19() {
            strText = "Bank error in your favor";
            eventcardtype = EventCardType.MysteryVault;
            colorCard = Color.Yellow;
        }
        public override void action() {
            gamemanager.playerCurrent.iMoney += 200;
            gamemanager.strMessage = "Mystery: " + strText;

        }
    }
}
