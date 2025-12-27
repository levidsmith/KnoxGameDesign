using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RealEstate.Display {
    public class DisplayModeBuild : DisplayMode {

        ModeBuild modebuild;

        public DisplayModeBuild(GameManager gamemanager, DisplayManager displaymanager) : base(gamemanager, displaymanager) {
            modebuild = (ModeBuild) gamemanager.modes["build"];

        }


        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> fonts, Dictionary<string, Texture2D> sprites) {

            _spriteBatch.Begin();



            _spriteBatch.DrawString(fonts["fontNormal"], "Current player: " + gamemanager.playerCurrent.strName, new Vector2(32, 700), Color.Black);

            _spriteBatch.DrawString(fonts["fontNormal"], "H: Build House", new Vector2(32, 750), Color.Black);
            _spriteBatch.DrawString(fonts["fontNormal"], "T: Build Hotel", new Vector2(32, 800), Color.Black);
            _spriteBatch.DrawString(fonts["fontNormal"], "S: Sell House", new Vector2(32, 850), Color.Black);
            _spriteBatch.DrawString(fonts["fontNormal"], "E: Sell Hotel", new Vector2(32, 900), Color.Black);
            _spriteBatch.DrawString(fonts["fontNormal"], "Q: Return", new Vector2(32, 950), Color.Black);

            _spriteBatch.DrawString(fonts["fontSmall"], "Select property to build", new Vector2(200, 200), Color.Black);

            int i = 0;
            foreach (Property p in gamemanager.playerCurrent.properties) {
                Color c = Color.Black;
                if (modebuild.iPropertySelect == i) {
                    c = Color.Blue;
                }

                _spriteBatch.DrawString(fonts["fontSmall"], p.strName, new Vector2(200, 250 + (i * 20)), c);
                if (p is PropertyResidential) {
                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Houses: {0}", ((PropertyResidential)p).iHouseCount), new Vector2(400, 250 + (i * 20)), c);
                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Hotels: {0}", ((PropertyResidential)p).iHotelCount), new Vector2(600, 250 + (i * 20)), c);
                }
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
