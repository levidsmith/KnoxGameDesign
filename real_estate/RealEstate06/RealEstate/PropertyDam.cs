using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate {
    public class PropertyDam : Property {

        public override int calculateRent() {
            int iDamCount;
            int iMultiplier;
            iDamCount = getPropertyOwner().getOwnedDamCount();

            switch(iDamCount) {
                case 1:
                    iMultiplier = 4;
                    break;
                case 2:
                    iMultiplier = 10;
                    break;
                default:
                    return 0;
            }
            return (gamemanager.dice[0].iRolledValue + gamemanager.dice[1].iRolledValue) * iMultiplier;

        }

    }
}
