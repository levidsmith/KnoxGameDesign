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


    }
}
