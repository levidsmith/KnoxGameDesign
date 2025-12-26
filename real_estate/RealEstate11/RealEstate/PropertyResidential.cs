using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class PropertyResidential : Property {
        public static Color[] PropertySetColors = { Color.Purple, Color.LightBlue, Color.Magenta, Color.Orange, Color.Red, Color.Yellow, Color.Green, Color.Blue };

        public int iPropertySet;
        public int iRent1House;
        public int iRent2Houses;
        public int iRent3Houses;
        public int iRent4Houses;
        public int iRent1Hotel;
        public int iHouseCost;
        public int iHotelCost;
        public int iHouseCount;
        public int iHotelCount;

        public override int calculateRent() {
            switch (iHouseCount) {
                case 1:
                    return iRent1House;
                case 2:
                    return iRent2Houses;
                case 3:
                    return iRent3Houses;
                case 4:
                    return iRent4Houses;
            }

            switch(iHotelCount) {
                case 1:
                    return iRent1Hotel;
            }

            Player playerOwner = getPropertyOwner();
            if (playerOwner != null && gamemanager.playerOwnsPropertySet(playerOwner, this)) {
                return 2 * iRent;
            }


            return iRent;
        }


    }
}
