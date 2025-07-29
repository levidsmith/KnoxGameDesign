using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelletEatingDemo {
    public class Wall {
        public int x, y;
        public int w, h;

        public Game1 game;
        public Wall(Game1 g) {
            game = g;
            w = 32;
            h = 32;
        }

        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            float fRotation = 0f;
            sb.Draw(textures["wall"], new Rectangle((int)x, (int)y, (int)w, (int)h), Color.White);

        }
    }
}
