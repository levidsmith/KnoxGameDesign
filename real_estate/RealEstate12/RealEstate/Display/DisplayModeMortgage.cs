using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RealEstate.Display {
    public class DisplayModeMortgage : DisplayMode {
        ModeMortgage modemortgage;

        public DisplayModeMortgage(GameManager gamemanager, DisplayManager displaymanager) : base(gamemanager, displaymanager) {
            modemortgage = (ModeMortgage) gamemanager.modes["mortgage"];

        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> fonts, Dictionary<string, Texture2D> sprites) {

            _spriteBatch.Begin();



            _spriteBatch.DrawString(fonts["fontNormal"], "Current player: " + gamemanager.playerCurrent.strName, new Vector2(32, 700), Color.Black);

            _spriteBatch.DrawString(fonts["fontSmall"], "M: Mortgage", new Vector2(32, 750), Color.Black);
            _spriteBatch.DrawString(fonts["fontSmall"], "U: Unmortgage", new Vector2(32, 800), Color.Black);
            _spriteBatch.DrawString(fonts["fontSmall"], "Q: Return", new Vector2(32, 900), Color.Black);

            _spriteBatch.DrawString(fonts["fontSmall"], "Select property to mortgage", new Vector2(200, 200), Color.Black);

            int i = 0;
            foreach (Property p in gamemanager.playerCurrent.properties) {
                Color c = Color.Black;
                if (modemortgage.iMortgageSelect == i) {
                    c = Color.Blue;
                }
                if (p.isMortgaged) {
                    _spriteBatch.DrawString(fonts["fontSmall"], "M", new Vector2(200, 220 + (i * 20)), c);
                }
                _spriteBatch.DrawString(fonts["fontSmall"], p.strName, new Vector2(250, 220 + (i * 20)), c);
                i++;

            }

            for (i = 0; i < gamemanager.players.Count; i++) {
                Vector2 vectPosition = new Vector2(400, 700 + (i * 50));
                _spriteBatch.DrawString(fonts["fontNormal"], gamemanager.players[i].strName, vectPosition, Player.colors[i]);
                _spriteBatch.DrawString(fonts["fontNormal"], " $" + gamemanager.players[i].iMoney, vectPosition + new Vector2(100, 0), Color.Black);
            }


            _spriteBatch.End();
        }

    }
}

