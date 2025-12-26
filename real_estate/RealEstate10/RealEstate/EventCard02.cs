using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class EventCard02 : EventCard {

        public EventCard02() {
            strText = "Advance to Nearest Park";
            eventcardtype = EventCardType.Happenstance;
            colorCard = Color.Orange;
        }
        public override void action() {
            gamemanager.strMessage = "Happenstance: " + strText;

            Space space = gamemanager.playerCurrent.spaceCurrent;
            Space spaceTarget = null;
            int i = 0;
            while (i < gamemanager.spaces.Count) {
                space = space.spaceNext;

                if (space.property != null && space.property is PropertyPark) {
                    spaceTarget = space;
                    break;
                }
                i++;
            }

            if (spaceTarget != null) {
                gamemanager.moveToSpace(spaceTarget);
            }
            

        }
    }
}
