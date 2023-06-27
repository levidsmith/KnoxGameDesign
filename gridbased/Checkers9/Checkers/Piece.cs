using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Checkers {
    internal class Piece {
        public Color color;
        public bool canMoveUpRow;
        public bool canMoveDownRow;
        public int iPlayer;

        public Piece(Color in_c, bool in_b1, bool in_b2, int in_iPlayer) {
            color = in_c;
            canMoveUpRow = in_b1;
            canMoveDownRow = in_b2;
            iPlayer = in_iPlayer;
        }
    }
}
