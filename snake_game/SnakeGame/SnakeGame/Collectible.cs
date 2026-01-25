//2026 - Levi D. Smith <developer@levidsmith.com>
//for KnoxGameDesign www.knoxgamedesign.org
using Microsoft.Xna.Framework;

namespace SnakeGame {
    public class Collectible {
        public int iRow;
        public int iCol;
        public int iValue;
        public Color color;

        public Collectible() {
            this.iValue = 1;
            color = new Color(255, 255, 85);
        }
    }
}
