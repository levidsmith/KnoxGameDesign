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
        public List<int> playerBids;

        public float fCountdown;
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



