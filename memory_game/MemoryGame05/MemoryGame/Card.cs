using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MemoryGame {
    internal class Card {
        public int iTypeID;
        bool isFaceUp;
        Vector2 position;

        public const int CARD_WIDTH = 64;
        public const int CARD_HEIGHT = 96;

        public Card() {
            isFaceUp = false;
        }

        public void Draw(SpriteBatch spritebatch) {

            Texture2D spriteCard;
            if (isFaceUp) {
                spriteCard = Game1.textures["CardFaceUp"];
            } else {
                spriteCard = Game1.textures["CardFaceDown"];
            }


            spritebatch.Draw(spriteCard, new Rectangle((int) position.X, (int) position.Y, CARD_WIDTH, CARD_HEIGHT), Color.White);

            if (isFaceUp) {
                spritebatch.DrawString(Game1.fonts["CardFont"], string.Format("{0}", iTypeID), position, Color.Black);
            }
        }

        public void setID(int in_id) {
            iTypeID = in_id;
        }

        public void setPosition(int in_x, int in_y) {
            position = new Vector2(in_x, in_y);

        }

        public void setFaceDown() {
            isFaceUp = false;
        }

        public bool checkPress(int in_x, int in_y) {
            bool isPressed = false;
            Rectangle rect1 = new Rectangle((int) position.X, (int) position.Y, CARD_WIDTH, CARD_HEIGHT);
            if (!isFaceUp && rect1.Contains(in_x, in_y)) {
                isFaceUp = true;
                isPressed = true;

            }

            return isPressed;

        }
    }
}
