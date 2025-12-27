using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Display {
    public abstract class DisplayMode {
        protected GameManager gamemanager;
        protected DisplayManager displaymanager;

        public DisplayMode(GameManager gamemanager, DisplayManager displaymanager) {
            this.gamemanager = gamemanager;
            this.displaymanager = displaymanager;

        }

        public abstract void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> fonts, Dictionary<string, Texture2D> sprites);

    }
}
