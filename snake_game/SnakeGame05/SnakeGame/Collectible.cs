using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame {
    internal class Collectible {
        public int iRow;
        public int iCol;
        public int iValue;

        public Collectible() {
            this.iValue = 1;
        }
    }
}
