using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class EventCard01 : EventCard {

        public EventCard01() {
            strText = "Get Out of Incarceration Free";
            eventcardtype = EventCardType.Happenstance;
            colorCard = Color.Orange;
        }
        public override void action() {
            gamemanager.playerCurrent.iGetOutFreeCards++;
            gamemanager.strMessage = "Happenstance: " + strText;

        }
    }
}
