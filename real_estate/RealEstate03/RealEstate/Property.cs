using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate {
    public class Property {
        public string strName;
        public int iPurchasePrice;
        public int iRent;
        int iRent1House;
        int iRent2Houses;
        int iRent3Houses;
        int iRent4Houses;
        int iRentHotel;
        int iHousePrice;
        int iHotelPrice;
        int iMortgageValue;
        int iHouseCount;
        int iHotelCount;

        public GameManager gamemanager;

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





    }
}
