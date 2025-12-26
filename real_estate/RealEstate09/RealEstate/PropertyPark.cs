using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class PropertyPark : Property {

        public override int calculateRent() {
            int iParkCount;
            iParkCount = getPropertyOwner().getOwnedParkCount();
            return 25 * ((int) MathF.Pow(2, iParkCount - 1));
        }

    }
}
