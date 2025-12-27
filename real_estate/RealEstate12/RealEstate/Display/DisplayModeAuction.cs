using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RealEstate.Display {
    public class DisplayModeAuction : DisplayMode {
        ModeAuction modeauction;


        public DisplayModeAuction(GameManager gamemanager, DisplayManager displaymanager) : base(gamemanager, displaymanager) {
            modeauction = (ModeAuction) gamemanager.modes["auction"];

        }


        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> fonts, Dictionary<string, Texture2D> sprites) {
            int i;
            Color c;


            _spriteBatch.Begin();



            _spriteBatch.DrawString(fonts["fontNormal"], "Current player: " + gamemanager.playerCurrent.strName, new Vector2(32, 700), Color.Black);

            _spriteBatch.DrawString(fonts["fontNormal"], "B: Bid", new Vector2(32, 750), Color.Black);
            _spriteBatch.DrawString(fonts["fontNormal"], "Up/Down Select Player", new Vector2(32, 800), Color.Black);
            _spriteBatch.DrawString(fonts["fontNormal"], "Q: Return", new Vector2(32, 950), Color.Black);


            _spriteBatch.DrawString(fonts["fontSmall"], "Auction: " + modeauction.propertyToAuction.strName, new Vector2(200, 200), Color.Black);
            _spriteBatch.DrawString(fonts["fontSmall"], "Next Bid: " + modeauction.iNextBid, new Vector2(200, 240), Color.Black);

            _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Countdown: {0:0}", modeauction.fCountdown), new Vector2(400, 240), Color.Black);



            c = Color.Black;
            for (i = 0; i < gamemanager.players.Count; i++) {
                Player player = gamemanager.players[i];
                c = Color.Black;

                if (i == modeauction.iSelectedPlayer) {
                    c = Color.Blue;
                }
                _spriteBatch.DrawString(fonts["fontSmall"], player.strName, new Vector2(200, 280 + (i * 20)), c);
            }


            for (i = 0; i < gamemanager.players.Count; i++) {
                c = Color.Black;

                if (i == modeauction.iSelectedPlayer) {
                    c = Color.Blue;
                }
                _spriteBatch.DrawString(fonts["fontSmall"], string.Format("{0}", modeauction.playerBids[i]), new Vector2(300, 280 + (i * 20)), c);
            }


            _spriteBatch.End();
        }



    }
}

