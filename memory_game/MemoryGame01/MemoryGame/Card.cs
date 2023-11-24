using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MemoryGame {
    internal class Card {
        int iTypeID;
        bool isFaceUp;
        Vector2 position;

        public const int CARD_WIDTH = 64;
        public const int CARD_HEIGHT = 96;

        public void Draw(SpriteBatch spritebatch) {
            spritebatch.Draw(Game1.textures["CardFaceDown"], new Rectangle((int) position.X, (int) position.Y, CARD_WIDTH, CARD_HEIGHT), Color.White);
        }

        public void setPosition(int in_x, int in_y) {
            position = new Vector2(in_x, in_y);

        }
    }
}
