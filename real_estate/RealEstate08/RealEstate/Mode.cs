using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RealEstate {
    public abstract class Mode {

        public GameManager gamemanager;
        public string strName;

        public abstract void Update(GameTime gameTime, KeyboardState keyboardCurrent, KeyboardState keyboardPrevious);
        public abstract void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> sprites, Dictionary<string, Texture2D> fonts);
    }
}
