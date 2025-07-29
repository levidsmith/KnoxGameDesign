using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PelletEatingDemo.Actor;

namespace PelletEatingDemo {
    public class Pellet {
        public int x, y;
        public int w, h;

        public Game1 game;

        public Pellet(Game1 g) {
            game = g;
            w = 8;
            h = 8;
        }


        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            sb.Draw(textures["pellet"], new Rectangle(x, y, w, h), Color.White);

        }

    }
}
