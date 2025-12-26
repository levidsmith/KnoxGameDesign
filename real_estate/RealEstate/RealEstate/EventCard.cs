using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public abstract class EventCard {
        public Color colorCard;
        public string strText;
        public GameManager gamemanager;
        public enum EventCardType { Happenstance, MysteryVault };
        public EventCardType eventcardtype;

        public abstract void action();
    }
}
