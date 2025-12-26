using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class EventCard17 : EventCard {

        public EventCard17() {
            strText = "Get Out of Incarceration Free";
            eventcardtype = EventCardType.MysteryVault;
            colorCard = Color.Yellow;
        }
        public override void action() {
            gamemanager.playerCurrent.iGetOutFreeCards++;
            gamemanager.strMessage = "Mystery Vault: " + strText;

        }
    }
}