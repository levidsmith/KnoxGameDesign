﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FallingBlocks2 {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dictionary<string, Texture2D> sprites;
        int[,] board;
        int[,] currentPiece = { {1, 1, 1, 0 },
                                {0, 1, 0, 0 },
                                {0, 0, 0, 0 },
                                {0, 0, 0, 0 }};
        int iCurrentPieceRow = 20;
        int iCurrentPieceCol = 3;

        KeyboardState previousState;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 320;
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.ApplyChanges();

            base.Initialize();
            board = new int[20, 10];
            int i, j;
            for (i = 0; i < 20; i++) {
                for (j = 0; j < 10; j++) {
                    board[i, j] = 0;
                }
            }



        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            sprites = new Dictionary<string, Texture2D>();
            sprites["block_empty"] = Content.Load<Texture2D>("block_empty");
            sprites["block_filled"] = Content.Load<Texture2D>("block_filled");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState state = Keyboard.GetState();
            Keys key;

            key = Keys.Left;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                iCurrentPieceCol--;
            }

            key = Keys.Right;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                iCurrentPieceCol++;
            }



            base.Update(gameTime);
            previousState = state;
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            _spriteBatch.Begin();
            int i, j;
            for (i = 0; i < 20; i++) {
                for (j = 0; j < 10; j++) {
                    if (board[i, j] == 0) {
                        _spriteBatch.Draw(sprites["block_empty"], new Rectangle(j * 32, (20 - 1 - i) * 32, 32, 32), Color.White);
                    } else if (board[i, j] == 1) {
                        _spriteBatch.Draw(sprites["block_filled"], new Rectangle(j * 32, (20 - 1 - i) * 32, 32, 32), Color.White);
                    }
                }
            }

            for (i = 0; i < 4; i++) {
                for (j = 0; j < 4; j++) {
                    if (currentPiece[i, j] == 1) {
                        _spriteBatch.Draw(sprites["block_filled"], new Rectangle((j + iCurrentPieceCol) * 32, (i + iCurrentPieceRow - 20) * 32, 32, 32), Color.White);
                    }
                }
            }

            _spriteBatch.End();
        }
    }
}