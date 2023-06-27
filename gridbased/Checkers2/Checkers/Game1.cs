﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Checkers {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int[,] iBoard;
        Dictionary<string, Texture2D> sprites;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 512;
            _graphics.PreferredBackBufferHeight = 512;
            _graphics.ApplyChanges();

            base.Initialize();

            iBoard = new int[8, 8];
            int i, j;
            for (i = 0; i < 8; i++) {
                for (j = 0; j < 8; j++) {
                    if (i < 3 && (i + j) % 2 == 1) {
                        iBoard[i, j] = 1;
                    } else if (i >= 5 && (i + j) % 2 == 1) {
                        iBoard[i, j] = 2;
                    }


                }
            }

        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            sprites = new Dictionary<string, Texture2D>();

            // TODO: use this.Content to load your game content here
            Texture2D texture;
            
            texture = Content.Load<Texture2D>("space");
            sprites["space"] = texture;

            texture = Content.Load<Texture2D>("checker");
            sprites["checker"] = texture;


        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            _spriteBatch.Begin();
            int i, j;
            for (i = 0; i < 8; i++) {
                for (j = 0; j < 8; j++) {
                    Color c;
                    if ((i + j) % 2 == 0) {
                        c = Color.Red;
                    } else if ((i + j) % 2 == 1) {
                        c = Color.Black;
                    } else {
                        c = Color.Red;
                    }
                    _spriteBatch.Draw(sprites["space"], new Rectangle(j * 64, i * 64, 64, 64), c);
                }
            }


            for (i = 0; i < 8; i++) {
                for (j = 0; j < 8; j++) {
                    if (iBoard[i, j] == 1) {
                        _spriteBatch.Draw(sprites["checker"], new Rectangle(j * 64, i * 64, 64, 64), Color.White);
                    } else if (iBoard[i, j] == 2) {
                            _spriteBatch.Draw(sprites["checker"], new Rectangle(j * 64, i * 64, 64, 64), Color.Red);

                        }
                    }
            }

            _spriteBatch.End();
        }
    }
}