using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class EventCard03 : EventCard {

        public EventCard03() {
            strText = "Go Back 3 Spaces";
            eventcardtype = EventCardType.Happenstance;
            colorCard = Color.Orange;
        }
        public override void action() {
            gamemanager.strMessage = "Happening: " + strText;

            Space space = gamemanager.playerCurrent.spaceCurrent;

            int iSpaceIndex = gamemanager.getSpaceIndex(gamemanager.playerCurrent.spaceCurrent);
            iSpaceIndex -= 3;
            if (iSpaceIndex < 0) {
                iSpaceIndex += gamemanager.spaces.Count;
            }
            gamemanager.playerCurrent.spaceCurrent = gamemanager.spaces[iSpaceIndex];



        }
    }
}
