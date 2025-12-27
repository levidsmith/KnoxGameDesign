using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class Player {
        public string strName;
        public int iMoney;
        public Space spaceCurrent;
        public Player playerNext;
        public List<Property> properties;
        public GameManager gamemanager;
        public int iGetOutFreeCards;
        public int iDoublesCount;
        public int iToken;
        public int iColor;
        public List<Space> spacesMoved;

        public static Color[] colors = { Color.LawnGreen, Color.CornflowerBlue, Color.Red, Color.MediumPurple, Color.Gold, Color.Cyan };

        public Player() {
            properties = new List<Property>();
            iGetOutFreeCards = 0;
            iDoublesCount = 0;
            spacesMoved = new List<Space>();
        }

        public int getOwnedParkCount() {
            int iCount = 0;

            foreach (Property p in properties) {
                if (p is PropertyPark) {
                    iCount++;
                }
            }

            return iCount;
        }


        public int getOwnedDamCount() {
            int iCount = 0;

            foreach (Property p in properties) {
                if (p is PropertyDam) {
                    iCount++;
                }
            }

            return iCount;
        }

        public bool ownsPropertySet(PropertyResidential p) {
            return gamemanager.playerOwnsPropertySet(this, p);

        }



    }
}
