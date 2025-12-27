using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class EventCard18 : EventCard {

        public EventCard18() {
            strText = "Doctor's fee pay $50";
            eventcardtype = EventCardType.MysteryVault;
            colorCard = Color.Yellow;
        }
        public override void action() {
            gamemanager.playerCurrent.iMoney -= 50;
            gamemanager.strMessage = "Mystery: " + strText;

        }
    }
}
