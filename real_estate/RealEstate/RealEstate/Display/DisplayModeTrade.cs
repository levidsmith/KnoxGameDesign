using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static RealEstate.ModeTrade;

namespace RealEstate.Display {
    public class DisplayModeTrade : DisplayMode {
        ModeTrade modetrade;

        public DisplayModeTrade(GameManager gamemanager, DisplayManager displaymanager) : base(gamemanager, displaymanager) {
            modetrade = (ModeTrade)gamemanager.modes["trade"];

        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Dictionary<string, SpriteFont> fonts, Dictionary<string, Texture2D> sprites) {
            Color c = Color.Black;

            _spriteBatch.Begin();



            _spriteBatch.DrawString(fonts["fontNormal"], "Current player: " + gamemanager.playerCurrent.strName, new Vector2(32, 700), Color.Black);

            _spriteBatch.DrawString(fonts["fontNormal"], "Q: Return", new Vector2(32, 950), Color.Black);

            int i;
            switch (modetrade.state) {
                case TradeState.StateSelectPlayer:
                    _spriteBatch.DrawString(fonts["fontNormal"], "Space: Select trade with player", new Vector2(32, 750), Color.Black);

                    _spriteBatch.DrawString(fonts["fontSmall"], "Select player to trade", new Vector2(200, 200), Color.Black);

                    for (i = 0; i < gamemanager.players.Count; i++) {
                        Player player = gamemanager.players[i];
                        c = Color.Black;
                        if (player == gamemanager.playerCurrent) {
                            c = Color.Gray;
                        }

                        if (i == modetrade.iSelectedPlayer) {
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
                    if (modetrade.playerSelected == modetrade.playerTrade1) {
                        c = Color.Blue;
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], modetrade.playerTrade1.strName, new Vector2(200, 200), c);

                    for (i = 0; i < modetrade.playerTrade1.properties.Count; i++) {
                        Property property = modetrade.playerTrade1.properties[i];

                        c = Color.Black;
                        if (modetrade.playerSelected == modetrade.playerTrade1 && i == modetrade.iSelectedProperty) {
                            c = Color.Blue;
                        }
                        if (modetrade.playerTrade1SelectedProperties.Contains(property)) {
                            _spriteBatch.DrawString(fonts["fontSmall"], "T", new Vector2(200, 220 + (i * 20)), c);

                        }
                        _spriteBatch.DrawString(fonts["fontSmall"], property.strName, new Vector2(220, 220 + (i * 20)), c);
                    }

                    c = Color.Black;
                    i++;
                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Cash ${0}", modetrade.playerTrade1Cash), new Vector2(220, 220 + (i * 20)), c);

                    i++;
                    if (modetrade.playerTrade1ConfirmYes) {
                        _spriteBatch.DrawString(fonts["fontSmall"], "X", new Vector2(200, 220 + (i * 20)), c);
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], "Yes", new Vector2(220, 220 + (i * 20)), c);

                    i++;
                    if (modetrade.playerTrade1ConfirmNo) {
                        _spriteBatch.DrawString(fonts["fontSmall"], "X", new Vector2(200, 220 + (i * 20)), c);
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], "No", new Vector2(220, 220 + (i * 20)), c);




                    c = Color.Black;
                    if (modetrade.playerSelected == modetrade.playerTrade2) {
                        c = Color.Blue;
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], modetrade.playerTrade2.strName, new Vector2(600, 200), c);

                    for (i = 0; i < modetrade.playerTrade2.properties.Count; i++) {
                        Property property = modetrade.playerTrade2.properties[i];

                        c = Color.Black;
                        if (modetrade.playerSelected == modetrade.playerTrade2 && i == modetrade.iSelectedProperty) {
                            c = Color.Blue;
                        }
                        if (modetrade.playerTrade2SelectedProperties.Contains(property)) {
                            _spriteBatch.DrawString(fonts["fontSmall"], "T", new Vector2(600, 220 + (i * 20)), c);

                        }
                        _spriteBatch.DrawString(fonts["fontSmall"], property.strName, new Vector2(620, 220 + (i * 20)), c);
                    }

                    c = Color.Black;
                    i++;
                    i++;
                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Cash ${0}", modetrade.playerTrade2Cash), new Vector2(620, 220 + (i * 20)), c);


                    i++;
                    if (modetrade.playerTrade2ConfirmYes) {
                        _spriteBatch.DrawString(fonts["fontSmall"], "X", new Vector2(600, 220 + (i * 20)), c);
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], "Yes", new Vector2(620, 220 + (i * 20)), c);

                    i++;
                    if (modetrade.playerTrade2ConfirmNo) {
                        _spriteBatch.DrawString(fonts["fontSmall"], "X", new Vector2(600, 220 + (i * 20)), c);
                    }
                    _spriteBatch.DrawString(fonts["fontSmall"], "No", new Vector2(620, 220 + (i * 20)), c);




                    break;

            }

            _spriteBatch.End();
        }


    }
}