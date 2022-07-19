//2022 LD Smith - levidsmith.com
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Curves {
    class InputHandler {
        KeyboardState previousState;
        public GameManager gamemanager;

        public InputHandler(GameManager in_gamemanager) {
            gamemanager = in_gamemanager;
        }
        public void handleInputKeyboard() {


            KeyboardState state = Keyboard.GetState();
            Keys key;

            key = Keys.Space;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                gamemanager.doNextCurve();
            }


            key = Keys.Enter;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                gamemanager.doNextScreen();
            }


            /*
            //player controls
            key = Keys.Left;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                player.moveLeft();
                
            } else if (!state.IsKeyDown(key) && previousState.IsKeyDown(key)) {
                player.stopMovingLeft();
            }

            key = Keys.Right;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                player.moveRight();
            } else if (!state.IsKeyDown(key) && previousState.IsKeyDown(key)) {
                player.stopMovingRight();
            }

            key = Keys.Up;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                player.moveUp();
            } else if (!state.IsKeyDown(key) && previousState.IsKeyDown(key)) {
                player.stopMovingUp();
            }

            key = Keys.Down;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                player.moveDown();
            } else if (!state.IsKeyDown(key) && previousState.IsKeyDown(key)) {
                player.stopMovingDown();
            }
            */


            previousState = state;

        }

    }
}
