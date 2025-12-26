using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RealEstate {
    public class ModeTrade : Mode {

        Player playerTrade1;
        Player playerTrade2;
        List<Property> playerTrade1SelectedProperties;
        List<Property> playerTrade2SelectedProperties;
        int playerTrade1Cash;
        int playerTrade2Cash;
        bool playerTrade1ConfirmYes;
        bool playerTrade1ConfirmNo;
        bool playerTrade2ConfirmYes;
        bool playerTrade2ConfirmNo;

        int iSelectedPlayer;

        Player playerSelected;
        int iSelectedProperty;

        public enum TradeState { StateSelectPlayer, StateSelectProperty };
        public TradeState state;

        public ModeTrade() {
            iSelectedPlayer = 0;
            state = TradeState.StateSelectPlayer;
        }

        public void reset() {
            iSelectedPlayer = 0;
            playerTrade1 = gamemanager.playerCurrent;
            playerTrade2 = null;

            playerTrade1ConfirmYes = false;
            playerTrade1ConfirmNo = false;

            playerTrade2ConfirmYes = false;
            playerTrade2ConfirmNo = false;

            playerTrade1Cash = 0;
            playerTrade2Cash = 0;

        }

        public override void Update(GameTime gameTime, KeyboardState keyboardCurrent, KeyboardState keyboardPrevious) {

            switch(state) {
                case TradeState.StateSelectPlayer:
                    if (keyboardCurrent.IsKeyDown(Keys.Down) == true && keyboardPrevious.IsKeyDown(Keys.Down) == false) {
                        selectNextPlayer();
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.Up) == true && keyboardPrevious.IsKeyDown(Keys.Up) == false) {
                        selectPreviousPlayer();
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.Space) == true && keyboardPrevious.IsKeyDown(Keys.Space) == false) {
                        playerTrade2 = gamemanager.players[iSelectedPlayer];

                        playerTrade1SelectedProperties = new List<Property>();
                        playerTrade2SelectedProperties = new List<Property>();

                        playerSelected = playerTrade1;
                        iSelectedProperty = 0;
                        state = TradeState.StateSelectProperty;
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.Q) == true && keyboardPrevious.IsKeyDown(Keys.Q) == false) {
                        gamemanager.modeCurrent = gamemanager.modes["board"];
                    }

                    break;

                case TradeState.StateSelectProperty:
                    if (keyboardCurrent.IsKeyDown(Keys.Left) == true && keyboardPrevious.IsKeyDown(Keys.Left) == false) {
                        playerSelected = playerTrade1;
                        iSelectedProperty = 0;
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.Right) == true && keyboardPrevious.IsKeyDown(Keys.Right) == false) {
                        playerSelected = playerTrade2;
                    }


                    if (keyboardCurrent.IsKeyDown(Keys.Down) == true && keyboardPrevious.IsKeyDown(Keys.Down) == false) {
                        selectNextProperty();
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.Up) == true && keyboardPrevious.IsKeyDown(Keys.Up) == false) {
                        selectPreviousProperty();
                    }


                    if (keyboardCurrent.IsKeyDown(Keys.T) == true && keyboardPrevious.IsKeyDown(Keys.T) == false) {
                        if (playerSelected == playerTrade1) {
                            if (!playerTrade1SelectedProperties.Contains(playerTrade1.properties[iSelectedProperty])) {
                                playerTrade1SelectedProperties.Add(playerTrade1.properties[iSelectedProperty]);
                            } else {
                                playerTrade1SelectedProperties.Remove(playerTrade1.properties[iSelectedProperty]);
                            }
                        }

                        if (playerSelected == playerTrade2) {
                            if (!playerTrade2SelectedProperties.Contains(playerTrade2.properties[iSelectedProperty])) {
                                playerTrade2SelectedProperties.Add(playerTrade2.properties[iSelectedProperty]);
                            } else {
                                playerTrade2SelectedProperties.Remove(playerTrade2.properties[iSelectedProperty]);
                            }
                        }

                    }

                    if (keyboardCurrent.IsKeyDown(Keys.D1) == true && keyboardPrevious.IsKeyDown(Keys.D1) == false) {
                        if (playerSelected == playerTrade1) {
                            playerTrade1Cash += 100;
                            if (playerTrade1Cash > playerTrade1.iMoney) {
                                playerTrade1Cash = playerTrade1.iMoney;
                            }
                        }

                        if (playerSelected == playerTrade2) {
                            playerTrade2Cash += 100;
                            if (playerTrade2Cash > playerTrade2.iMoney) {
                                playerTrade2Cash = playerTrade2.iMoney;
                            }
                        }
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.D2) == true && keyboardPrevious.IsKeyDown(Keys.D2) == false) {
                        if (playerSelected == playerTrade1) {
                            playerTrade1Cash += 10;
                            if (playerTrade1Cash > playerTrade1.iMoney) {
                                playerTrade1Cash = playerTrade1.iMoney;
                            }
                        }

                        if (playerSelected == playerTrade2) {
                            playerTrade2Cash += 10;
                            if (playerTrade2Cash > playerTrade2.iMoney) {
                                playerTrade2Cash = playerTrade2.iMoney;
                            }
                        }
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.D3) == true && keyboardPrevious.IsKeyDown(Keys.D3) == false) {
                        if (playerSelected == playerTrade1) {
                            playerTrade1Cash += 1;
                            if (playerTrade1Cash > playerTrade1.iMoney) {
                                playerTrade1Cash = playerTrade1.iMoney;
                            }
                        }

                        if (playerSelected == playerTrade2) {
                            playerTrade2Cash += 1;
                            if (playerTrade2Cash > playerTrade2.iMoney) {
                                playerTrade2Cash = playerTrade2.iMoney;
                            }
                        }
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.D7) == true && keyboardPrevious.IsKeyDown(Keys.D7) == false) {
                        if (playerSelected == playerTrade1) {
                            playerTrade1Cash -= 100;
                            if (playerTrade1Cash < 0) {
                                playerTrade1Cash = 0;
                            }
                        }

                        if (playerSelected == playerTrade2) {
                            playerTrade2Cash -= 100;
                            if (playerTrade2Cash < 0) {
                                playerTrade2Cash = 0;
                            }
                        }
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.D8) == true && keyboardPrevious.IsKeyDown(Keys.D8) == false) {
                        if (playerSelected == playerTrade1) {
                            playerTrade1Cash -= 10;
                            if (playerTrade1Cash < 0) {
                                playerTrade1Cash = 0;
                            }
                        }

                        if (playerSelected == playerTrade2) {
                            playerTrade2Cash -= 10;
                            if (playerTrade2Cash < 0) {
                                playerTrade2Cash = 0;
                            }
                        }
                    }
                    if (keyboardCurrent.IsKeyDown(Keys.D9) == true && keyboardPrevious.IsKeyDown(Keys.D9) == false) {
                        if (playerSelected == playerTrade1) {
                            playerTrade1Cash -= 1;
                            if (playerTrade1Cash < 0) {
                                playerTrade1Cash = 0;
                            }
                        }

                        if (playerSelected == playerTrade2) {
                            playerTrade2Cash -= 1;
                            if (playerTrade2Cash < 0) {
                                playerTrade2Cash = 0;
                            }
                        }
                    }




                    if (keyboardCurrent.IsKeyDown(Keys.Y) == true && keyboardPrevious.IsKeyDown(Keys.Y) == false) {
                        if (playerSelected == playerTrade1) {
                            playerTrade1ConfirmYes = true;
                            playerTrade1ConfirmNo = false;
                        }
                        if (playerSelected == playerTrade2) {
                            playerTrade2ConfirmYes = true;
                            playerTrade2ConfirmNo = false;
                        }

                        if (playerTrade1ConfirmYes && playerTrade2ConfirmYes) {
                            completeTrade();
                        }
                    }


                    if (keyboardCurrent.IsKeyDown(Keys.N) == true && keyboardPrevious.IsKeyDown(Keys.N) == false) {
                        if (playerSelected == playerTrade1) {
                            playerTrade1ConfirmYes = false;
                            playerTrade1ConfirmNo = true;
                        }
                        if (playerSelected == playerTrade2) {
                            playerTrade2ConfirmYes = false;
                            playerTrade2ConfirmNo = true;
                        }
                    }



                    if (keyboardCurrent.IsKeyDown(Keys.Q) == true && keyboardPrevious.IsKeyDown(Keys.Q) == false) {
                        state = TradeState.StateSelectPlayer;
                    }

                    break;

            }


        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> fonts, Dictionary<string, Texture2D> sprites) {
            Color c = Color.Black;

            _spriteBatch.Begin();



            _spriteBatch.DrawString(fonts["fontNormal"], "Current player: " + gamemanager.playerCurrent.strName, new Vector2(32, 700), Color.Black);

            _spriteBatch.DrawString(fonts["fontNormal"], "Q: Return", new Vector2(32, 950), Color.Black);

            int i;
            switch (state) {
                case TradeState.StateSelectPlayer:
                    _spriteBatch.DrawString(fonts["fontNormal"], "Space: Select trade with player", new Vector2(32, 750), Color.Black);

                    _spriteBatch.DrawString(fonts["fontSmall"], "Select player to trade", new Vector2(200, 200), Color.Black);

                    for (i = 0; i < gamemanager.players.Count; i++) {
                        Player player = gamemanager.players[i];
                        c = Color.Black;
                        if (player == gamemanager.playerCurrent) {
                            c = Color.Gray;
                        }

                        if (i == iSelectedPlayer) {
                            c = Color.Blue;
                        }
                        _spriteBatch.DrawString(fonts["fontSmall"], player.strName, new Vector2(200, 220 + (i * 20)), c);
                    }

                    break;
                case TradeState.StateSelectProperty:

                    _spriteBatch.DrawString(fonts["fontNormal"], "Left / Right: Switch selected side", new Vector2(32, 750), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "T: Add / Remove property", new Vector2(32, 800), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "1,2,3: Add Cash", new Vector2(32, 850), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "7,8,9: Remove Cash", new Vector2(32, 900), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "Y: Confirm trade - Yes", new Vector2(632, 750), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "N: Confirm trade - No", new Vector2(632, 800), Color.Black);

                    c = Color.Black;
                    if (playerSelected == playerTrade1) {
                        c = Color.Blue;
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], playerTrade1.strName, new Vector2(200, 200), c);

                    for (i = 0; i < playerTrade1.properties.Count; i++) {
                        Property property = playerTrade1.properties[i];

                        c = Color.Black;
                        if (playerSelected == playerTrade1 && i == iSelectedProperty) {
                            c = Color.Blue;
                        }
                        if (playerTrade1SelectedProperties.Contains(property)) {
                            _spriteBatch.DrawString(fonts["fontSmall"], "T", new Vector2(200, 220 + (i * 20)), c);

                        }
                        _spriteBatch.DrawString(fonts["fontSmall"], property.strName, new Vector2(220, 220 + (i * 20)), c);
                    }
                    
                    c = Color.Black;
                    i++;
                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Cash ${0}", playerTrade1Cash), new Vector2(220, 220 + (i * 20)), c);

                    i++;
                    if (playerTrade1ConfirmYes) {
                        _spriteBatch.DrawString(fonts["fontSmall"], "X", new Vector2(200, 220 + (i * 20)), c);
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], "Yes", new Vector2(220, 220 + (i * 20)), c);

                    i++;
                    if (playerTrade1ConfirmNo) {
                        _spriteBatch.DrawString(fonts["fontSmall"], "X", new Vector2(200, 220 + (i * 20)), c);
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], "No", new Vector2(220, 220 + (i * 20)), c);




                    c = Color.Black;
                    if (playerSelected == playerTrade2) {
                        c = Color.Blue;
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], playerTrade2.strName, new Vector2(600, 200), c);

                    for (i = 0; i < playerTrade2.properties.Count; i++) {
                        Property property = playerTrade2.properties[i];

                        c = Color.Black;
                        if (playerSelected == playerTrade2 && i == iSelectedProperty) {
                            c = Color.Blue;
                        }
                        if (playerTrade2SelectedProperties.Contains(property)) {
                            _spriteBatch.DrawString(fonts["fontSmall"], "T", new Vector2(600, 220 + (i * 20)), c);

                        }
                        _spriteBatch.DrawString(fonts["fontSmall"], property.strName, new Vector2(620, 220 + (i * 20)), c);
                    }

                    c = Color.Black;
                    i++;
                    i++;
                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Cash ${0}", playerTrade2Cash), new Vector2(620, 220 + (i * 20)), c);


                    i++;
                    if (playerTrade2ConfirmYes) {
                        _spriteBatch.DrawString(fonts["fontSmall"], "X", new Vector2(600, 220 + (i * 20)), c);
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], "Yes", new Vector2(620, 220 + (i * 20)), c);

                    i++;
                    if (playerTrade2ConfirmNo) {
                        _spriteBatch.DrawString(fonts["fontSmall"], "X", new Vector2(600, 220 + (i * 20)), c);
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], "No", new Vector2(620, 220 + (i * 20)), c);




                    break;

            }

            _spriteBatch.End();
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

        public void selectNextProperty() {
            iSelectedProperty++;
            if (iSelectedProperty >= playerSelected.properties.Count) {
                iSelectedProperty = 0;

            }
        }
        public void selectPreviousProperty() {
            iSelectedProperty--;
            if (iSelectedProperty < 0) {
                iSelectedProperty = playerSelected.properties.Count - 1;

            }
        }

        public void completeTrade() {
            foreach(Property property in playerTrade1SelectedProperties) {
                playerTrade2.properties.Add(property);
                playerTrade1.properties.Remove(property);
            }

            foreach (Property property in playerTrade2SelectedProperties) {
                playerTrade1.properties.Add(property);
                playerTrade2.properties.Remove(property);
            }

            playerTrade1.iMoney += playerTrade2Cash;
            playerTrade1.iMoney -= playerTrade1Cash;

            playerTrade2.iMoney += playerTrade1Cash;
            playerTrade2.iMoney -= playerTrade2Cash;

            playerTrade1Cash = 0;
            playerTrade2Cash = 0;

        }


    }
}