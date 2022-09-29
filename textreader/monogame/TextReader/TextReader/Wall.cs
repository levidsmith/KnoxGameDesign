//2022 Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TextReader {
    class Wall {
        int x, y;

        public Wall(int in_x, int in_y) {
            x = in_x;
            y = in_y;

        }

        public void Draw(SpriteBatch in_sb) {
            in_sb.Draw(Game1.textures["wall"], new Vector2(x, y), Color.White);

        }
    }
}
