using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate {
    public class Property {
        public string strName;
        int iPurchasePrice;
        int iRent;
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

        public bool isPurchased() {
            return false;
        }

        public Player getPropertyOwner() {
            return null;
        }

        public bool purchaseProperty(Player player) {
            return false;
        }

        public bool purchaseHouse() {
            return false;
        }

        public bool purchaseHotel() {
            return false;
        }




    }
}
