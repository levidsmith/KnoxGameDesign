using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PelletEatingDemo.Actor;

namespace PelletEatingDemo {
    internal class Pellet {
        public float x, y;
        public float w, h;

        public Pellet() {
            w = 8;
            h = 8;
        }


        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            float fRotation = 0f;
            sb.Draw(textures["pellet"], new Rectangle((int)x, (int)y, (int)w, (int)h), new Rectangle(0, 0, 8, 8), Color.White, fRotation, new Vector2(4, 4), SpriteEffects.None, 0f);

        }

    }
}
