using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame {
    internal class SnakeBody {
        public int iRow;
        public int iCol;

        public SnakeBody(int iRow, int iCol) {
            this.iRow = iRow;
            this.iCol = iCol;
        }
    }
}
