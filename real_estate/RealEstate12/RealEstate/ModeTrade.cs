using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RealEstate {
    public class ModeTrade : Mode {

        public Player playerTrade1;
        public Player playerTrade2;
        public List<Property> playerTrade1SelectedProperties;
        public List<Property> playerTrade2SelectedProperties;
        public int playerTrade1Cash;
        public int playerTrade2Cash;
        public bool playerTrade1ConfirmYes;
        public bool playerTrade1ConfirmNo;
        public bool playerTrade2ConfirmYes;
        public bool playerTrade2ConfirmNo;

        public int iSelectedPlayer;

        public Player playerSelected;
        public int iSelectedProperty;

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