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


