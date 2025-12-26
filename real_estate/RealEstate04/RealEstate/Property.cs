using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate {
    public abstract class Property {
        public string strName;
        public int iPurchasePrice;
        public int iRent;
        bool isMortgaged;

        public GameManager gamemanager;

        public Property() {
            isMortgaged = false;
        }

        public bool isOwned() {
            if (getPropertyOwner() != null) {
                return true;
            } else {
                return false;
            }
        }

        public bool purchaseHouse() {
            return false;
        }

        public bool purchaseHotel() {
            return false;
        }

        public Player getPropertyOwner() {
            foreach (Player p1 in gamemanager.players) {
                foreach (Property p2 in p1.properties) {
                    if (p2 == this) {
                        return p1;
                    }
                }
            }

            return null;
        }

        public abstract int calculateRent();







    }
}
