using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RealEstate {
    public class ModeAuction : Mode {
        public int iSelectedPlayer;
        public Property propertyToAuction;

        public int iNextBid;
        List<int> playerBids;

        float fCountdown;
        const float MAX_BID_TIME = 10f;

        public override void Update(GameTime gameTime, KeyboardState keyboardCurrent, KeyboardState keyboardPrevious) {
            if (keyboardCurrent.IsKeyDown(Keys.Down) == true && keyboardPrevious.IsKeyDown(Keys.Down) == false) {
                selectNextPlayer();
            }

            if (keyboardCurrent.IsKeyDown(Keys.Up) == true && keyboardPrevious.IsKeyDown(Keys.Up) == false) {
                selectPreviousPlayer();
            }

            if (keyboardCurrent.IsKeyDown(Keys.B) == true && keyboardPrevious.IsKeyDown(Keys.B) == false) {
                playerBids[iSelectedPlayer] = iNextBid;
                iNextBid = (int)(iNextBid * 1.20f);
                fCountdown = MAX_BID_TIME;
            }


            fCountdown -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (fCountdown <= 0f) {
                completeAuction();
            }



        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> fonts, Dictionary<string, Texture2D> sprites) {
            int i;
            Color c;


            _spriteBatch.Begin();



            _spriteBatch.DrawString(fonts["fontNormal"], "Current player: " + gamemanager.playerCurrent.strName, new Vector2(32, 700), Color.Black);

            _spriteBatch.DrawString(fonts["fontNormal"], "B: Bid", new Vector2(32, 750), Color.Black);
            _spriteBatch.DrawString(fonts["fontNormal"], "Up/Down Select Player", new Vector2(32, 800), Color.Black);
            _spriteBatch.DrawString(fonts["fontNormal"], "Q: Return", new Vector2(32, 950), Color.Black);


            _spriteBatch.DrawString(fonts["fontSmall"], "Auction: " + propertyToAuction.strName, new Vector2(200, 200), Color.Black);
            _spriteBatch.DrawString(fonts["fontSmall"], "Next Bid: " + iNextBid, new Vector2(200, 240), Color.Black);

            _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Countdown: {0:0}", fCountdown), new Vector2(400, 240), Color.Black);



            c = Color.Black;
            for (i = 0; i < gamemanager.players.Count; i++) {
                Player player = gamemanager.players[i];
                c = Color.Black;

                if (i == iSelectedPlayer) {
                    c = Color.Blue;
                }
                _spriteBatch.DrawString(fonts["fontSmall"], player.strName, new Vector2(200, 280 + (i * 20)), c);
            }


            for (i = 0; i < gamemanager.players.Count; i++) {
                c = Color.Black;

                if (i == iSelectedPlayer) {
                    c = Color.Blue;
                }
                _spriteBatch.DrawString(fonts["fontSmall"], string.Format("{0}", playerBids[i]), new Vector2(300, 280 + (i * 20)), c);
            }


            _spriteBatch.End();
        }

        public void setAuction(Property property) {
            propertyToAuction = property;
            iNextBid = (int) (property.iPurchasePrice * 0.1f);
            playerBids = new List<int>();

            foreach(Player player in gamemanager.players) {
                playerBids.Add(0);
            }

            fCountdown = MAX_BID_TIME;
        }

        public void selectNextPlayer() {
            iSelectedPlayer++;
            if (iSelectedPlayer >= gamemanager.players.Count) {
                iSelectedPlayer = 0;

            }
        }

        public void selectPreviousPlayer() {
            iSelectedPlayer--;
            if (iSelectedPlayer < 0) {
                iSelectedPlayer = gamemanager.players.Count - 1;
            }
        }

        private void completeAuction() {
            int iHighestBidder = 0;
            int i = 0;
            foreach (int iBid in playerBids) {
                if (iBid > playerBids[iHighestBidder]) {
                    iHighestBidder = i;
                }
                i++;

            }
            gamemanager.players[iHighestBidder].properties.Add(propertyToAuction);
            gamemanager.players[iHighestBidder].iMoney -= playerBids[iHighestBidder];
            gamemanager.modeCurrent = gamemanager.modes["board"];
           
        }



    }
}



