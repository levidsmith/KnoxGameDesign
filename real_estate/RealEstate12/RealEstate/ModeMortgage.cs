using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RealEstate {
    public class ModeMortgage : Mode {
        public int iMortgageSelect;

        public override void Update(GameTime gameTime, KeyboardState keyboardCurrent, KeyboardState keyboardPrevious) {
                if (keyboardCurrent.IsKeyDown(Keys.Up) == true && keyboardPrevious.IsKeyDown(Keys.Up) == false) {
                    mortgageSelectPrevious();
                }

                if (keyboardCurrent.IsKeyDown(Keys.Down) == true && keyboardPrevious.IsKeyDown(Keys.Down) == false) {
                    mortgageSelectNext();
                }

                if (keyboardCurrent.IsKeyDown(Keys.M) == true && keyboardPrevious.IsKeyDown(Keys.M) == false) {
                    mortgageSelectMortgage();
                }

                if (keyboardCurrent.IsKeyDown(Keys.U) == true && keyboardPrevious.IsKeyDown(Keys.U) == false) {
                    mortgageSelectUnmortgage();
                }


                if (keyboardCurrent.IsKeyDown(Keys.Q) == true && keyboardPrevious.IsKeyDown(Keys.Q) == false) {
                    gamemanager.modeCurrent = gamemanager.modes["board"];
                }


            }


        public void mortgageSelectPrevious() {
            iMortgageSelect--;
            if (iMortgageSelect < 0) {
                iMortgageSelect = gamemanager.playerCurrent.properties.Count - 1;
            }

        }

        public void mortgageSelectNext() {
            iMortgageSelect++;
            if (iMortgageSelect >= gamemanager.playerCurrent.properties.Count) {
                iMortgageSelect = 0;
            }
        }

        public void mortgageSelectMortgage() {
            if (!gamemanager.playerCurrent.properties[iMortgageSelect].isMortgaged) {
                gamemanager.playerCurrent.iMoney += gamemanager.playerCurrent.properties[iMortgageSelect].iPurchasePrice / 2;
                gamemanager.playerCurrent.properties[iMortgageSelect].isMortgaged = true;
            }

        }

        public void mortgageSelectUnmortgage() {
            if (gamemanager.playerCurrent.properties[iMortgageSelect].isMortgaged) {
                gamemanager.playerCurrent.iMoney -= (int)((gamemanager.playerCurrent.properties[iMortgageSelect].iPurchasePrice / 2) * 1.1f);
                gamemanager.playerCurrent.properties[iMortgageSelect].isMortgaged = false;
            }

        }


    }
}

