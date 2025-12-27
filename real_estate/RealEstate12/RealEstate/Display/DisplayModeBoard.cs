using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace RealEstate.Display {
    public class DisplayModeBoard : DisplayMode {

        public const int SPACE_WIDTH = 88;
        public const int SPACE_HEIGHT = 144;

        public List<DisplayPlayerToken> displayplayertokens;

        public DisplayModeBoard(GameManager gamemanager, DisplayManager displaymanager) : base(gamemanager, displaymanager) {
            displayplayertokens = new List<DisplayPlayerToken>();
            int i = 0;
            foreach(Player player in gamemanager.players) {
                DisplayPlayerToken displayplayertoken = new DisplayPlayerToken();
                displayplayertoken.player = player;
                displayplayertoken.iMoveID = 0;
                displayplayertoken.iTokenID = i;


                displayplayertokens.Add(displayplayertoken);
                i++;
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> fonts, Dictionary<string, Texture2D> sprites) {

            _spriteBatch.Begin();
            _spriteBatch.Draw(sprites["sprBoardBackground"], new Vector2(0,0), null, Color.White, 0f, Vector2.Zero, new Vector2(1f, 1f), SpriteEffects.None, 0);


            int i;
            for (i = 0; i < gamemanager.spaces.Count; i++) {
                const int VERTICAL_SPACING = 20;
                int iVerticalSpace = VERTICAL_SPACING;

                _spriteBatch.Draw(getSpaceSprite(i), getSpacePosition(i), null, Color.White, getSpaceRotation(i), getSpaceRotationPoint(i), new Vector2(1f, 1f), SpriteEffects.None, 0);

                Property property = gamemanager.spaces[i].property;
                if (property != null) {
                    Color c = Color.Black;

                    Player propertyOwner = null;
                    if (property != null) {
                        propertyOwner = property.getPropertyOwner();
                        if (propertyOwner != null) {
                            c = Player.colors[propertyOwner.iColor];
                        }
                    }

                    if (property is PropertyResidential) {
                        //                        _spriteBatch.Draw(sprites["sprPropertySpaceColor"], gamemanager.spaces[i].position, PropertyResidential.PropertySetColors[((PropertyResidential)property).iPropertySet]);
                        _spriteBatch.Draw(displaymanager.sprites["sprSpacePropertyColor"], getSpacePosition(i), null, PropertyResidential.PropertySetColors[((PropertyResidential)property).iPropertySet], getSpaceRotation(i), getSpaceRotationPoint(i), new Vector2(1f, 1f), SpriteEffects.None, 0);


                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], property.strName, getSpacePosition(i) + getTextPosition(i), c);


                    if (!property.isOwned()) {
                        _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Buy ${0}", property.iPurchasePrice), getSpacePosition(i)  + getTextPosition(i) + new Vector2(0, iVerticalSpace), Color.Black);
                        iVerticalSpace += VERTICAL_SPACING;
                    } else {
                        if (property.isMortgaged) {
                            _spriteBatch.DrawString(fonts["fontSmall"], "Mortgaged", getSpacePosition(i) + getTextPosition(i) + new Vector2(0, iVerticalSpace), Color.Black);
                            iVerticalSpace += VERTICAL_SPACING;

                        } else {
                            _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Rent ${0}", property.calculateRent()), getSpacePosition(i) + getTextPosition(i) + new Vector2(0, iVerticalSpace), Color.Black);
                            iVerticalSpace += VERTICAL_SPACING;

                            if (property is PropertyResidential && propertyOwner != null && gamemanager.playerOwnsPropertySet(propertyOwner, (PropertyResidential)property)) {
                                _spriteBatch.DrawString(fonts["fontSmall"], "Set owned", getSpacePosition(i) + getTextPosition(i) + new Vector2(0, iVerticalSpace), c);
                                iVerticalSpace += VERTICAL_SPACING;
                            }

                            if (property is PropertyResidential) {
                                PropertyResidential propertyresidential = (PropertyResidential)property;
                                if (propertyresidential.iHouseCount > 0) {
                                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Houses: {0}", propertyresidential.iHouseCount), getSpacePosition(i) + getTextPosition(i) + new Vector2(0, iVerticalSpace), Color.Black);
                                    iVerticalSpace += VERTICAL_SPACING;
                                }
                                if (propertyresidential.iHotelCount > 0) {
                                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Hotels: {0}", propertyresidential.iHotelCount), getSpacePosition(i) + getTextPosition(i) + new Vector2(0, iVerticalSpace), Color.Black);
                                    iVerticalSpace += VERTICAL_SPACING;
                                }
                            }


                        }

                    }
                }

                if (gamemanager.spaces[i] is SpaceArrested) {
                    SpaceArrested spacearrested = (SpaceArrested)gamemanager.spaces[i];
                    _spriteBatch.DrawString(fonts["fontSmall"], spacearrested.strName, getSpacePosition(i) + getTextPosition(i), Color.Black);

                } else if (gamemanager.spaces[i] is SpaceIncarceration) {
                    SpaceIncarceration spaceincarceration = (SpaceIncarceration)gamemanager.spaces[i];
                    _spriteBatch.DrawString(fonts["fontSmall"], spaceincarceration.strName, getSpacePosition(i) + getTextPosition(i), Color.Black);
                    _spriteBatch.DrawString(fonts["fontSmall"], "Incarcerated", getSpacePosition(i) + getTextPosition(i) + new Vector2(40, 20), Color.Black);
                    _spriteBatch.DrawString(fonts["fontSmall"], "Just visiting", getSpacePosition(i) + getTextPosition(i) + new Vector2(4, 100), Color.Black);

                } else if (gamemanager.spaces[i] is SpaceEventCard) {
                    SpaceEventCard spaceeventcard = (SpaceEventCard)gamemanager.spaces[i];
                    _spriteBatch.DrawString(fonts["fontSmall"], spaceeventcard.getName(), getSpacePosition(i) + getTextPosition(i), Color.Black);
                } else if (gamemanager.spaces[i] is SpaceStart) {
                    SpaceStart spacestart = (SpaceStart)gamemanager.spaces[i];
                    _spriteBatch.DrawString(fonts["fontSmall"], spacestart.strName, getSpacePosition(i) + getTextPosition(i), Color.Black);
                } else if (gamemanager.spaces[i] is SpaceFree) {
                    SpaceFree spacefree = (SpaceFree)gamemanager.spaces[i];
                    _spriteBatch.DrawString(fonts["fontSmall"], spacefree.strName, getSpacePosition(i) + getTextPosition(i), Color.Black);
                } else if (gamemanager.spaces[i] is SpaceTaxes) {
                    SpaceTaxes spacetaxes = (SpaceTaxes)gamemanager.spaces[i];
                    _spriteBatch.DrawString(fonts["fontSmall"], spacetaxes.strName, getSpacePosition(i) + getTextPosition(i), Color.Black);


                }
            }



            /*
            for (i = 0; i < gamemanager.players.Count; i++) {
                Vector2 vectPosition = getSpacePosition(gamemanager.getSpaceIndex(gamemanager.players[i].spaceCurrent));
                vectPosition -= new Vector2(SPACE_WIDTH / 2, 0);

                if (gamemanager.isPlayerIncarcerated(gamemanager.players[i])) {
                    vectPosition.X = vectPosition.X + 50;
                    vectPosition.Y = vectPosition.Y - 50;
                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("{0}", gamemanager.getSpaceIncarceration().getIncarceratedRolls(gamemanager.players[i])), vectPosition + new Vector2(((i % 2) * 20) + 20, (i / 2) * 20), Color.Black);
                }

                Texture2D sprToken = null;
                switch(gamemanager.players[i].iToken) {
                    case 0:
                        sprToken = sprites["sprToken01"];
                        break;
                    case 1:
                        sprToken = sprites["sprToken02"];
                        break;
                    case 2:
                        sprToken = sprites["sprToken03"];
                        break;
                    case 3:
                        sprToken = sprites["sprToken04"];
                        break;
                    case 4:
                        sprToken = sprites["sprToken05"];
                        break;
                    case 5:
                        sprToken = sprites["sprToken06"];
                        break;
                }
                _spriteBatch.Draw(sprToken, vectPosition + new Vector2(((i / 2) * 20), (i % 2) * 20), null, Player.colors[gamemanager.players[i].iColor], 0f, new Vector2(16, 32), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0f);
            }
            */
            
            i = 0;
            foreach (DisplayPlayerToken displayplayertoken in displayplayertokens) {
                //_spriteBatch.Draw(displayplayertoken.getSpriteToken(displaymanager), displayplayertoken.position + new Vector2(((i / 2) * 20), (i % 2) * 20), null, Player.colors[gamemanager.players[i].iColor], 0f, new Vector2(16, 32), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0f);
                displayplayertoken.Update(gameTime, this);
                //_spriteBatch.Draw(displayplayertoken.getSpriteToken(displaymanager), displayplayertoken.position + new Vector2(((i / 2) * 20), (i % 2) * 20), null, displayplayertoken.getTokenColor(), 0f, new Vector2(16, 32), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0f);
                _spriteBatch.Draw(displayplayertoken.getSpriteToken(displaymanager), displayplayertoken.position + new Vector2(-20 + (i * 8), 0), null, displayplayertoken.getTokenColor(), 0f, new Vector2(16, 32), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0f);
                i++;

            }

            for (i = 0; i < gamemanager.players.Count; i++) {
                Vector2 vectPosition = new Vector2(600, 400 + (i * 50));
                _spriteBatch.DrawString(fonts["fontNormal"], gamemanager.players[i].strName, vectPosition, Player.colors[gamemanager.players[i].iColor]);
                _spriteBatch.DrawString(fonts["fontNormal"], " $" + gamemanager.players[i].iMoney, vectPosition + new Vector2(100, 0), Color.Black);
                _spriteBatch.DrawString(fonts["fontNormal"], "GOF " + gamemanager.players[i].iGetOutFreeCards, vectPosition + new Vector2(300, 0), Color.Black);
                if (gamemanager.isPlayerIncarcerated(gamemanager.players[i])) {
                    _spriteBatch.DrawString(fonts["fontNormal"], "Incarcerated", vectPosition + new Vector2(400, 0), Color.Black);
                }
            }


            Vector2 posText = new Vector2(32, 400);
            int iTextSpacing = 50;
            _spriteBatch.DrawString(fonts["fontNormal"], "Current player: " + gamemanager.playerCurrent.strName, posText, Color.Black);
            posText += new Vector2(0, iTextSpacing);

            switch (gamemanager.gamestate) {
                case GameManager.GameState.StartTurn:
                    _spriteBatch.DrawString(fonts["fontNormal"], "R: Roll", posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    break;
                case GameManager.GameState.LandOnSpace:
                    _spriteBatch.DrawString(fonts["fontNormal"], "Dice: " + gamemanager.dice[0].iRolledValue + ", " + gamemanager.dice[1].iRolledValue, posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    if (gamemanager.playerCurrent.spaceCurrent.property != null &&
                        !gamemanager.playerCurrent.spaceCurrent.property.isOwned()) {
                        _spriteBatch.DrawString(fonts["fontNormal"], "B: Buy Property", posText, Color.Black);
                        posText += new Vector2(0, iTextSpacing);

                        _spriteBatch.DrawString(fonts["fontNormal"], "A: Auction Property", posText, Color.Black);
                        posText += new Vector2(0, iTextSpacing);

                    }
                    _spriteBatch.DrawString(fonts["fontNormal"], "M: Mortgage", posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    _spriteBatch.DrawString(fonts["fontNormal"], "U: Build", posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    _spriteBatch.DrawString(fonts["fontNormal"], "T: Trade", posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    _spriteBatch.DrawString(fonts["fontNormal"], "E: End Turn", posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    break;
                case GameManager.GameState.Incarcerated:
                    _spriteBatch.DrawString(fonts["fontNormal"], "R: Roll for doubles", posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    _spriteBatch.DrawString(fonts["fontNormal"], "P: Pay $50", posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    _spriteBatch.DrawString(fonts["fontNormal"], "C: Use GetOut card", posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    break;

                case GameManager.GameState.GameOver:
                    _spriteBatch.DrawString(fonts["fontSmall"], "Game Over: Player " + gamemanager.playerCurrent.strName + " wins!", posText, Color.Black);
                    posText += new Vector2(0, iTextSpacing);

                    break;

            }

            _spriteBatch.DrawString(fonts["fontNormal"], gamemanager.strMessage, new Vector2(600, 750), Color.Black);


            _spriteBatch.End();


        }


        public Vector2 getSpacePosition(int iSpace) {
            int iBoardWidth = (SPACE_WIDTH * 9) + (SPACE_HEIGHT * 2);
            Vector2 vectBoardOffset = new Vector2((Game1.SCREEN_WIDTH - iBoardWidth) / 2, (Game1.SCREEN_HEIGHT - iBoardWidth) / 2);
            Vector2 pos = new Vector2((SPACE_WIDTH * 9) + (SPACE_HEIGHT * 1.5f), (SPACE_WIDTH * 9) + (SPACE_HEIGHT * 1.5f));

            int i = 0;

            while (i < iSpace) {
                if (i == 0) {
                    pos += new Vector2(-(SPACE_HEIGHT + SPACE_WIDTH) / 2, 0);
                } else if (i < 9) {
                    pos += new Vector2(-SPACE_WIDTH, 0);
                } else if (i == 9) {
                    pos += new Vector2(-(SPACE_HEIGHT + SPACE_WIDTH) / 2, 0);
                } else if (i == 10) {
                    pos += new Vector2(0, -(SPACE_HEIGHT + SPACE_WIDTH) / 2);
                } else if (i < 19) {
                    pos += new Vector2(0, -SPACE_WIDTH);
                } else if (i == 19) {
                    pos += new Vector2(0, -(SPACE_WIDTH + SPACE_HEIGHT) / 2);
                } else if (i == 20) {
                    pos += new Vector2((SPACE_HEIGHT + SPACE_WIDTH) / 2, 0);
                } else if (i < 29) {
                    pos += new Vector2(SPACE_WIDTH, 0);
                } else if (i == 29) {
                    pos += new Vector2((SPACE_HEIGHT + SPACE_WIDTH) / 2, 0);
                } else if (i == 30) {
                    pos += new Vector2(0, (SPACE_HEIGHT + SPACE_WIDTH) / 2);
                } else if (i < 39) {
                    pos += new Vector2(0, SPACE_WIDTH);
                }
                i++;
            }


            return pos + vectBoardOffset;
        }

        public Vector2 getSpacePosition(Space space) {
            return getSpacePosition(gamemanager.getSpaceIndex(space));
        }



        public float getSpaceRotation(int iSpace) {
            float fRot = 0f;
            if (iSpace < 10) {
                fRot = 0f;
            } else if (iSpace < 20) {
                fRot = MathF.PI / 2;
            } else if (iSpace < 30) {
                fRot = MathF.PI;
            } else if (iSpace < 40) {
                fRot = MathF.PI * 1.5f;
            }

            return fRot;
        }

        public Vector2 getSpaceRotationPoint(int iSpace) {
            if (iSpace % 10 == 0) {
                return new Vector2(SPACE_HEIGHT / 2, SPACE_HEIGHT / 2);
            } else {
                return new Vector2(SPACE_WIDTH / 2, SPACE_HEIGHT / 2);
            }
        }


        public Texture2D getSpaceSprite(int iSpace) {
            if (iSpace % 10 == 0) {
                return displaymanager.sprites["sprSpaceCorner"];
            } else {
                return displaymanager.sprites["sprSpaceRegular"];
            }
        }

        public Vector2 getTextPosition(int iSpace) {
            if (iSpace % 10 == 0) {
                return new Vector2(-SPACE_HEIGHT / 2, -SPACE_HEIGHT / 2);
            } else {
                return new Vector2(-(SPACE_WIDTH / 2) + 4, -(SPACE_HEIGHT / 2) + 32);
            }

        }





    }

}
