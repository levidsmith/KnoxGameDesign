using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RealEstate {
    public class ModeBuild : Mode {
        public int iPropertySelect;
        public const int HOUSE_COUNT_MAX = 4;
        public const int HOTEL_COUNT_MAX = 1;

        public override void Update(GameTime gameTime, KeyboardState keyboardCurrent, KeyboardState keyboardPrevious) {
            if (keyboardCurrent.IsKeyDown(Keys.Up) == true && keyboardPrevious.IsKeyDown(Keys.Up) == false) {
                propertySelectPrevious();
            }

            if (keyboardCurrent.IsKeyDown(Keys.Down) == true && keyboardPrevious.IsKeyDown(Keys.Down) == false) {
                propertySelectNext();
            }

            if (keyboardCurrent.IsKeyDown(Keys.H) == true && keyboardPrevious.IsKeyDown(Keys.H) == false) {
                propertySelectBuyHouse();
            }

            if (keyboardCurrent.IsKeyDown(Keys.T) == true && keyboardPrevious.IsKeyDown(Keys.T) == false) {
                propertySelectBuyHotel();
            }

            if (keyboardCurrent.IsKeyDown(Keys.S) == true && keyboardPrevious.IsKeyDown(Keys.S) == false) {
                propertySelectSellHouse();
            }

            if (keyboardCurrent.IsKeyDown(Keys.E) == true && keyboardPrevious.IsKeyDown(Keys.E) == false) {
                propertySelectSellHotel();
            }


            if (keyboardCurrent.IsKeyDown(Keys.Q) == true && keyboardPrevious.IsKeyDown(Keys.Q) == false) {
                gamemanager.modeCurrent = gamemanager.modes["board"];
            }


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
                if (iPropertySelect == i) {
                    c = Color.Blue;
                }

                _spriteBatch.DrawString(fonts["fontSmall"], p.strName, new Vector2(200, 250 + (i * 20)), c);
                if (p is PropertyResidential) {
                    _spriteBatch.DrawString(fonts["fontSmall"], string.Format("Houses: {0}", ((PropertyResidential) p).iHouseCount), new Vector2(400, 250 + (i * 20)), c);
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

        public void propertySelectPrevious() {
            iPropertySelect--;
            if (iPropertySelect < 0) {
                iPropertySelect = gamemanager.playerCurrent.properties.Count - 1;
            }

        }

        public void propertySelectNext() {
            iPropertySelect++;
            if (iPropertySelect >= gamemanager.playerCurrent.properties.Count) {
                iPropertySelect = 0;
            }
        }

        public void propertySelectBuyHouse() {
            if (gamemanager.playerCurrent.properties.Count == 0) {
                return;
            }

            if (gamemanager.playerCurrent.properties[iPropertySelect] is PropertyResidential) {
                PropertyResidential propertyresidential = (PropertyResidential)gamemanager.playerCurrent.properties[iPropertySelect];
                if (gamemanager.playerCurrent.iMoney >= propertyresidential.iHouseCost &&
                    propertyresidential.iHouseCount < HOUSE_COUNT_MAX &&
                    propertyresidential.iHotelCount == 0 &&
                    gamemanager.playerOwnsPropertySet(gamemanager.playerCurrent, propertyresidential)) {

                    gamemanager.playerCurrent.iMoney -= propertyresidential.iHouseCost;
                    propertyresidential.iHouseCount++;
                }
            }

        }

        public void propertySelectBuyHotel() {
            if (gamemanager.playerCurrent.properties.Count == 0) {
                return;
            }

            if (gamemanager.playerCurrent.properties[iPropertySelect] is PropertyResidential) {
                PropertyResidential propertyresidential = (PropertyResidential)gamemanager.playerCurrent.properties[iPropertySelect];
                if (gamemanager.playerCurrent.iMoney >= propertyresidential.iHotelCost &&
                    propertyresidential.iHouseCount == HOUSE_COUNT_MAX &&
                    propertyresidential.iHotelCount < HOTEL_COUNT_MAX) {


                    gamemanager.playerCurrent.iMoney -= propertyresidential.iHotelCost;
                    propertyresidential.iHouseCount = 0;
                    propertyresidential.iHotelCount++;
                }
            }

        }


        public void propertySelectSellHouse() {
            if (gamemanager.playerCurrent.properties.Count == 0) {
                return;
            }

            if (gamemanager.playerCurrent.properties[iPropertySelect] is PropertyResidential) {
                PropertyResidential propertyresidential = (PropertyResidential)gamemanager.playerCurrent.properties[iPropertySelect];
                if (propertyresidential.iHouseCount > 0) {
                    gamemanager.playerCurrent.iMoney += propertyresidential.iHouseCost / 2;
                    propertyresidential.iHouseCount--;
                }
            }

        }

        public void propertySelectSellHotel() {
            if (gamemanager.playerCurrent.properties.Count == 0) {
                return;
            }

            if (gamemanager.playerCurrent.properties[iPropertySelect] is PropertyResidential) {
                PropertyResidential propertyresidential = (PropertyResidential)gamemanager.playerCurrent.properties[iPropertySelect];
                if (propertyresidential.iHotelCount > 0) {
                    gamemanager.playerCurrent.iMoney += propertyresidential.iHotelCost / 2;
                    propertyresidential.iHouseCount = 4;
                    propertyresidential.iHotelCount--;
                }
            }

        }



    }
}


