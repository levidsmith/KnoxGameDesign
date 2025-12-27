using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RealEstate.Display {
    public class DisplayManager {
        public GameManager gamemanager;
        public Dictionary<string, SpriteFont> fonts;
        public Dictionary<string, Texture2D> sprites;
        public SpriteBatch _spriteBatch;

        DisplayModeAuction displaymodeauction;
        DisplayModeBoard displaymodeboard;
        DisplayModeBuild displaymodebuild;
        DisplayModeMortgage displaymodemortgage;
        DisplayModeTrade displaymodetrade;

        public DisplayManager(GameManager gamemanager) {
            this.gamemanager = gamemanager;

            displaymodeauction = new DisplayModeAuction(gamemanager, this);
            displaymodeboard = new DisplayModeBoard(gamemanager, this);
            displaymodebuild = new DisplayModeBuild(gamemanager, this);
            displaymodemortgage = new DisplayModeMortgage(gamemanager, this);
            displaymodetrade = new DisplayModeTrade(gamemanager, this);

        }
        public void Draw(GameTime gameTime) {
            //gamemanager.modeCurrent.Draw(gameTime, _spriteBatch, fonts, sprites);
            if (gamemanager.modeCurrent is ModeAuction) {
                displaymodeauction.Draw(gameTime, _spriteBatch, fonts, sprites);
            } else if (gamemanager.modeCurrent is ModeBoard) {
                displaymodeboard.Draw(gameTime, _spriteBatch, fonts, sprites);
            } else if (gamemanager.modeCurrent is ModeBuild) {
                displaymodebuild.Draw(gameTime, _spriteBatch, fonts, sprites);
            } else if (gamemanager.modeCurrent is ModeMortgage) {
                displaymodemortgage.Draw(gameTime, _spriteBatch, fonts, sprites);
            } else if (gamemanager.modeCurrent is ModeTrade) {
                displaymodetrade.Draw(gameTime, _spriteBatch, fonts, sprites);

            }
        }

    }
}
