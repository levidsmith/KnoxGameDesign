using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate {
    public class Die {
        public int iRolledValue = 0;
        public void roll() {
            Random rand = new Random();
            iRolledValue = (rand.Next() % 6) + 1;

        }

    }

}
