using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public  class Block {
        public int x, y;
        public int w, h;

        public Block(int init_x, int init_y, int init_w, int init_h) {
            x = init_x;
            y = init_y;
            w = init_w;
            h = init_h;

        }

        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            sb.Draw(textures["block"], new Rectangle(x, Game1.SCREEN_HEIGHT - y - h, w, h), new Rectangle(0, 0, 16, 16), Color.White);

        }

    }
}
