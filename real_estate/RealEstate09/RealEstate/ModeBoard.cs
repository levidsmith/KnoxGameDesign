using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RealEstate {
    public class ModeBoard : Mode {

        public override void Update(GameTime gameTime, KeyboardState keyboardCurrent, KeyboardState keyboardPrevious) {
            switch (gamemanager.gamestate) {
                case GameManager.GameState.StartTurn:
                    if (keyboardCurrent.IsKeyDown(Keys.R) == true && keyboardPrevious.IsKeyDown(Keys.R) == false) {
                        
                        gamemanager.dice[0].roll();
                        gamemanager.dice[1].roll();
                        
                        /*
                        gamemanager.dice[0].iRolledValue = 2;
                        gamemanager.dice[1].iRolledValue = 2;
                        */

                        gamemanager.moveSpaces();
                    }
                    break;
                case GameManager.GameState.Incarcerated:
                    if (keyboardCurrent.IsKeyDown(Keys.R) == true && keyboardPrevious.IsKeyDown(Keys.R) == false) {
                        gamemanager.dice[0].roll();
                        gamemanager.dice[1].roll();
                        gamemanager.incarceratedRoll();
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.P) == true && keyboardPrevious.IsKeyDown(Keys.P) == false) {
                        gamemanager.incarceratedPay();
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.C) == true && keyboardPrevious.IsKeyDown(Keys.C) == false) {
                        gamemanager.incarceratedUseCard();
                    }


                    break;
                case GameManager.GameState.LandOnSpace:
                    if (keyboardCurrent.IsKeyDown(Keys.E) == true && keyboardPrevious.IsKeyDown(Keys.E) == false) {
                        gamemanager.endTurn();
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.B) == true && keyboardPrevious.IsKeyDown(Keys.B) == false) {
                        gamemanager.purchaseProperty(gamemanager.playerCurrent, gamemanager.playerCurrent.spaceCurrent.property);
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.A) == true && keyboardPrevious.IsKeyDown(Keys.A) == false) {
                        gamemanager.modeCurrent = gamemanager.modes["auction"];
                        ((ModeAuction)gamemanager.modeCurrent).setAuction(gamemanager.playerCurrent.spaceCurrent.property);
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.M) == true && keyboardPrevious.IsKeyDown(Keys.M) == false) {
                        gamemanager.modeCurrent = gamemanager.modes["mortgage"];
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.T) == true && keyboardPrevious.IsKeyDown(Keys.T) == false) {
                        gamemanager.modeCurrent = gamemanager.modes["trade"];
                        ((ModeTrade)gamemanager.modeCurrent).reset();
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.U) == true && keyboardPrevious.IsKeyDown(Keys.U) == false) {
                        gamemanager.modeCurrent = gamemanager.modes["build"];
                    }

                    break;


            }


        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> fonts, Dictionary<string, Texture2D> sprites) {

            _spriteBatch.Begin();
            int i;
            for (i = 0; i < gamemanager.spaces.Count; i++) {
                _spriteBatch.Draw(sprites["sprPropertySpace"], gamemanager.spaces[i].position, Color.White);

                Property property = gamemanager.spaces[i].property;
                if (property != null) {
                    Color c = Color.Black;

                    Player propertyOwner = null;
                    if (property != null) {
                        propertyOwner = property.getPropertyOwner();
                        if (propertyOwner != null) {
                            c = Player.colors[gamemanager.getPlayerIndex(propertyOwner)];
                        }
                    }

                    if (property is PropertyResidential) {
                        _spriteBatch.Draw(sprites["sprPropertySpaceColor"], gamemanager.spaces[i].position, PropertyResidential.PropertySetColors[((PropertyResidential)property).iPropertySet]);
                    }
                    _spriteBatch.DrawString(fonts["fontNormal"], property.strName, gamemanager.spaces[i].position + new Vector2(48, 0), c);

                    if (!property.isOwned()) {
                        _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Buy ${0}", property.iPurchasePrice), gamemanager.spaces[i].position + new Vector2(48, 32), Color.Black);
                    } else {
                        if (property.isMortgaged) {
                            _spriteBatch.DrawString(fonts["fontSmall"], "Mortgaged", gamemanager.spaces[i].position + new Vector2(48, 32), Color.Black);

                        } else {
                            _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Rent ${0}", property.calculateRent()), gamemanager.spaces[i].position + new Vector2(48, 32), Color.Black);

                            if (property is PropertyResidential && propertyOwner != null && gamemanager.playerOwnsPropertySet(propertyOwner, (PropertyResidential)property)) {
                                _spriteBatch.DrawString(fonts["fontSmall"], "Set owned", gamemanager.spaces[i].position + new Vector2(250, 32), c);
                            }

                            if (property is PropertyResidential) {
                                PropertyResidential propertyresidential = (PropertyResidential)property;
                                if (propertyresidential.iHouseCount > 0) {
                                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Houses: {0}", propertyresidential.iHouseCount), gamemanager.spaces[i].position + new Vector2(150, 32), Color.Black);
                                }
                                if (propertyresidential.iHotelCount > 0) {
                                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Hotels: {0}", propertyresidential.iHotelCount), gamemanager.spaces[i].position + new Vector2(150, 32), Color.Black);
                                }
                            }


                        }

                    }
                }

                if (gamemanager.spaces[i] is SpaceArrested) {
                    SpaceArrested spacearrested = (SpaceArrested)gamemanager.spaces[i];
                    _spriteBatch.DrawString(fonts["fontNormal"], spacearrested.strName, gamemanager.spaces[i].position + new Vector2(48, 0), Color.Black);

                } else if (gamemanager.spaces[i] is SpaceIncarceration) {
                    SpaceIncarceration spaceincarceration = (SpaceIncarceration)gamemanager.spaces[i];
                    _spriteBatch.DrawString(fonts["fontNormal"], spaceincarceration.strName, gamemanager.spaces[i].position + new Vector2(48, 0), Color.Black);
                    _spriteBatch.DrawString(fonts["fontSmall"], "Incarcerated", gamemanager.spaces[i].position + new Vector2(48, 40), Color.Black);
                    _spriteBatch.DrawString(fonts["fontSmall"], "Just visiting", gamemanager.spaces[i].position + new Vector2(248, 40), Color.Black);

                }

            }


            for (i = 0; i < gamemanager.players.Count; i++) {
                Vector2 vectPosition = gamemanager.players[i].spaceCurrent.position;

                if (gamemanager.isPlayerIncarcerated(gamemanager.players[i])) {
                    vectPosition.X = vectPosition.X - 340;
                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("{0}", gamemanager.getSpaceIncarceration().getIncarceratedRolls(gamemanager.players[i])), vectPosition + new Vector2(((i % 2) * 20) + 340 + 20, (i / 2) * 20), Color.Black);
                }

                _spriteBatch.Draw(sprites["sprPlayerPiece"], vectPosition + new Vector2(((i % 2) * 20) + 340, (i / 2) * 20), Player.colors[i]);
                _spriteBatch.DrawString(fonts["fontSmall"], gamemanager.players[i].strName, vectPosition + new Vector2(((i % 2) * 20) + 340, (i / 2) * 20), Color.Black);
                
            }

            for (i = 0; i < gamemanager.players.Count; i++) {
                Vector2 vectPosition = new Vector2(800, 700 + (i * 50));
                _spriteBatch.DrawString(fonts["fontNormal"], gamemanager.players[i].strName, vectPosition, Player.colors[i]);
                _spriteBatch.DrawString(fonts["fontNormal"], " $" + gamemanager.players[i].iMoney, vectPosition + new Vector2(100, 0), Color.Black);
            }


            _spriteBatch.DrawString(fonts["fontNormal"], "Current player: " + gamemanager.playerCurrent.strName, new Vector2(32, 700), Color.Black);

            switch (gamemanager.gamestate) {
                case GameManager.GameState.StartTurn:
                    _spriteBatch.DrawString(fonts["fontNormal"], "R: Roll", new Vector2(32, 750), Color.Black);
                    break;
                case GameManager.GameState.LandOnSpace:
                    _spriteBatch.DrawString(fonts["fontNormal"], "Dice: " + gamemanager.dice[0].iRolledValue + ", " + gamemanager.dice[1].iRolledValue, new Vector2(32, 750), Color.Black);
                    if (gamemanager.playerCurrent.spaceCurrent.property != null &&
                        !gamemanager.playerCurrent.spaceCurrent.property.isOwned()) {
                        _spriteBatch.DrawString(fonts["fontNormal"], "B: Buy Property", new Vector2(32, 800), Color.Black);
                        _spriteBatch.DrawString(fonts["fontNormal"], "A: Auction Property", new Vector2(332, 800), Color.Black);
                    }
                    _spriteBatch.DrawString(fonts["fontNormal"], "M: Mortgage", new Vector2(32, 850), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "U: Build", new Vector2(32, 900), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "T: Trade", new Vector2(32, 950), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "E: End Turn", new Vector2(32, 1000), Color.Black);
                    break;
                case GameManager.GameState.Incarcerated:
                    _spriteBatch.DrawString(fonts["fontNormal"], "R: Roll for doubles", new Vector2(32, 750), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "P: Pay $50", new Vector2(32, 800), Color.Black);
                    _spriteBatch.DrawString(fonts["fontNormal"], "C: Use GetOut card", new Vector2(32, 850), Color.Black);
                    break;

                case GameManager.GameState.GameOver:
                    _spriteBatch.DrawString(fonts["fontSmall"], "Game Over: Player " + gamemanager.playerCurrent.strName + " wins!", new Vector2(1200, 800), Color.Black);
                    break;

            }

            _spriteBatch.DrawString(fonts["fontSmall"], gamemanager.strMessage, new Vector2(1200, 750), Color.Black);


            _spriteBatch.End();


        }

    }
}
