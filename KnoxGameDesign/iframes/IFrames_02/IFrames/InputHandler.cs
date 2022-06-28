using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace IFrames {
    class InputHandler {
        KeyboardState previousState;
        public Player player;

        public InputHandler(Player in_player) {
            player = in_player;
        }
        public void handleInputKeyboard() {


            KeyboardState state = Keyboard.GetState();
            Keys key;

            key = Keys.Space;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                //nextScreen();
            }

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



            previousState = state;

        }

    }
}
