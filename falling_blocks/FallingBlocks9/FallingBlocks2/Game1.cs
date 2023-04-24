using Microsoft.Xna.Framework;
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
        int[,] currentPiece = { {0, 0, 0, 0, 0},
                                {0, 0, 0, 0, 0},
                                {0, 1, 1, 1, 0 },
                                {0, 0, 1, 0, 0 },
                                {0, 0, 0, 0, 0 }};
        int iCurrentPieceRow = 20;
        int iCurrentPieceCol = 2;

        float fDropCountdown;
        float fMaxDropCountdown = .5f;

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

            fDropCountdown = fMaxDropCountdown;



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

            key = Keys.Up;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                //rotate
                int[,] rotatedPiece = new int[5,5];
                int i, j;
                for (i = 0; i < 5; i++) {
                    for (j = 0; j < 5; j++) {
                        rotatedPiece[i, j] = currentPiece[j, 4 - i ];
                    }
                }
                currentPiece = rotatedPiece;

            }


            fDropCountdown -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (fDropCountdown <= 0) {
                iCurrentPieceRow--;
                fDropCountdown += fMaxDropCountdown;

                int i, j;
                for (i = 0; i < 5; i++) {
                    for (j = 0; j < 5; j++) {
                        if (currentPiece[i, j] == 1 && (iCurrentPieceRow + i) == 0) {
                            addPieceToBoard();
                        } else if (currentPiece[i, j] == 1 && 
                            (iCurrentPieceRow + i - 1) >= 0 && (iCurrentPieceRow + i - 1) < 20 && 
                            (board[iCurrentPieceRow + i - 1, iCurrentPieceCol + j]) == 1) {
                            addPieceToBoard();
                        }
                    }
                }
            }

            base.Update(gameTime);
            previousState = state;
        }

        private void addPieceToBoard() {

            int i, j;
            for (i = 0; i < 5; i++) {
                for (j = 0; j < 5; j++) {
                    if (currentPiece[i, j] == 1) {
                        board[iCurrentPieceRow + i, iCurrentPieceCol + j] = 1;
                    }
                }
            }

            iCurrentPieceRow = 20;
            iCurrentPieceCol = 3;

            checkClearedRows();

        }

        private void checkClearedRows() {
            int i, j;

            i = 0;
            while (i < 20) { 
                bool rowCleared = true;
                for (j = 0; j < 10; j++) {
                    if (board[i, j] == 0) {
                        rowCleared = false;
                    }
                }

                if (rowCleared) {
                    for (j = 0; j < 10; j++) {
                        board[i, j] = 0;
                    }

                    int i1, j1;
                    for (i1 = i; i1 < 19; i1++) {
                        for (j1 = 0; j1 < 10; j1++) {
                            board[i1, j1] = board[i1 + 1, j1];
                        }
                    }

                } else {
                    i++;
                }
            }

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

            for (i = 0; i < 5; i++) {
                for (j = 0; j < 5; j++) {
                    if (currentPiece[i, j] == 1) {
                        _spriteBatch.Draw(sprites["block_filled"], new Rectangle((j + iCurrentPieceCol) * 32, (20 - iCurrentPieceRow - i) * 32, 32, 32), Color.White);
                    }
                }
            }

            _spriteBatch.End();
        }
    }
}