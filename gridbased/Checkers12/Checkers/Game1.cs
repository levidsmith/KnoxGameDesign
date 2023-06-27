using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Checkers {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Piece[,] pieceBoard;
        Piece pieceSelected;
        Dictionary<string, Texture2D> sprites;

        MouseState statePrevious;
        SpriteFont myfont;

        int iSelectedRow = -1;
        int iSelectedCol = -1;

        int iCurrentPlayer;

        bool isGameOver = false;
        int iWinnerPlayer = -1;

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

            pieceBoard = new Piece[8, 8];
            int i, j;
            for (i = 0; i < 8; i++) {
                for (j = 0; j < 8; j++) {
                    Piece p;
                    if (i < 3 && (i + j) % 2 == 1) {
                        p = new Piece(Color.White, true, false, 0);
                        p.posCurrent = new Vector2(j * 64, i * 64);
                        pieceBoard[i, j] = p;
                    } else if (i >= 5 && (i + j) % 2 == 1) {
                        p = new Piece(Color.Red, false, true, 1);
                        p.posCurrent = new Vector2(j * 64, i * 64);
                        pieceBoard[i, j] = p;
                    }
                }
            }

            pieceSelected = null;
            iCurrentPlayer = 0;

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

            texture = Content.Load<Texture2D>("king");
            sprites["king"] = texture;

            myfont = Content.Load<SpriteFont>("myfont");

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed && statePrevious.LeftButton == ButtonState.Released) {
                getSelected(state.X, state.Y);

                if (iSelectedRow >= 0 && iSelectedRow < 8 && iSelectedCol >= 0 && iSelectedCol < 8 &&
                    pieceBoard[iSelectedRow, iSelectedCol] != null && 
                    pieceBoard[iSelectedRow, iSelectedCol].iPlayer == iCurrentPlayer
                    ) {
                    pieceSelected = pieceBoard[iSelectedRow, iSelectedCol];
                } else if (pieceSelected != null && pieceBoard[iSelectedRow, iSelectedCol] == null && (iSelectedRow + iSelectedCol) % 2 == 1) { 
                    int[] iRowAndCol = getPieceRowAndCol(pieceSelected);
                    bool isValidMove = false;
                    if (iSelectedRow == iRowAndCol[0] + 1 && 
                        pieceSelected.canMoveUpRow &&
                        (iSelectedCol == iRowAndCol[1] + 1 || iSelectedCol == iRowAndCol[1] - 1)
                        ) {
                        isValidMove = true;
                    } else if (iSelectedRow == iRowAndCol[0] - 1 &&
                        pieceSelected.canMoveDownRow &&
                        (iSelectedCol == iRowAndCol[1] + 1 || iSelectedCol == iRowAndCol[1] - 1)
                        ) {
                        isValidMove = true;
                    }


                    //check hop
                    Piece pieceHopped;
                    if (iSelectedRow == iRowAndCol[0] + 2 &&
                        pieceSelected.canMoveUpRow) {


                        if (iSelectedCol == iRowAndCol[1] - 2) {
                            pieceHopped = pieceBoard[iRowAndCol[0] + 1, iRowAndCol[1] - 1];
                            if (pieceHopped != null && pieceHopped.color != pieceSelected.color) {
                                isValidMove = true;
                                pieceBoard[iRowAndCol[0] + 1, iRowAndCol[1] - 1] = null;
                            }

                        } else if (iSelectedCol == iRowAndCol[1] + 2) {
                            pieceHopped = pieceBoard[iRowAndCol[0] + 1, iRowAndCol[1] + 1];
                            if (pieceHopped != null && pieceHopped.color != pieceSelected.color) {
                                isValidMove = true;
                                pieceBoard[iRowAndCol[0] + 1, iRowAndCol[1] + 1] = null;
                            }

                        }
                    } else if (iSelectedRow == iRowAndCol[0] - 2 &&
                        pieceSelected.canMoveDownRow) {


                        if (iSelectedCol == iRowAndCol[1] - 2) {
                            pieceHopped = pieceBoard[iRowAndCol[0] - 1, iRowAndCol[1] - 1];
                            if (pieceHopped != null && pieceHopped.color != pieceSelected.color) {
                                isValidMove = true;
                                pieceBoard[iRowAndCol[0] - 1, iRowAndCol[1] - 1] = null;
                            }

                        } else if (iSelectedCol == iRowAndCol[1] + 2) {
                            pieceHopped = pieceBoard[iRowAndCol[0] - 1, iRowAndCol[1] + 1];
                            if (pieceHopped != null && pieceHopped.color != pieceSelected.color) {
                                isValidMove = true;
                                pieceBoard[iRowAndCol[0] - 1, iRowAndCol[1] + 1] = null;
                            }

                        }
                    }


                    if (isValidMove) {
                        pieceBoard[iRowAndCol[0], iRowAndCol[1]] = null;
                        pieceBoard[iSelectedRow, iSelectedCol] = pieceSelected;
                        pieceSelected.posTarget = new Vector2(iSelectedCol * 64, iSelectedRow * 64);
                        pieceSelected.isMoving = true;
                        checkPromotion(pieceSelected);

                        pieceSelected = null;
                        checkGameOver();
                        nextPlayerTurn();
                    } else {
                        pieceSelected = null;
                    }
                } else {
                    pieceSelected = null;

                }

            }

            int i, j;
            for (i = 0; i < 8; i++) {
                for (j = 0; j < 8; j++) {
                    if (pieceBoard[i, j] != null) {

                        if (pieceBoard[i, j].isMoving && 
                            pieceBoard[i, j].posCurrent != pieceBoard[i, j].posTarget) {

                            pieceBoard[i, j].posCurrent = Vector2.Lerp(pieceBoard[i, j].posCurrent,
                                                                       pieceBoard[i, j].posTarget,
                                                                       16f * (float)gameTime.ElapsedGameTime.TotalSeconds);

                        }

                    }

                }
            }

            base.Update(gameTime);

            statePrevious = state;
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
                    if (pieceBoard[i, j] != null) {
                        Color c;
                        if (pieceBoard[i, j] == pieceSelected) {
                            c = Color.Yellow;
                        } else {
                            c = pieceBoard[i, j].color;

                        }

                        //                        _spriteBatch.Draw(sprites[pieceBoard[i, j].strSprite], new Rectangle(j * 64, i * 64, 64, 64), c);
                        _spriteBatch.Draw(sprites[pieceBoard[i, j].strSprite], new Rectangle( (int) pieceBoard[i, j].posCurrent.X, (int) pieceBoard[i, j].posCurrent.Y, 64, 64), c);
                    }

                }

            }

            _spriteBatch.DrawString(myfont, "Cell " + iSelectedRow + ", " + iSelectedCol, new Vector2(20, 20), Color.Blue);

            if (!isGameOver) {

                _spriteBatch.DrawString(myfont, "Player  " + iCurrentPlayer, new Vector2(20, 50), Color.Blue);
            } else { 

                _spriteBatch.DrawString(myfont, "Player  " + iWinnerPlayer + " WINS", new Vector2(20, 50), Color.Blue);
            }


            _spriteBatch.End();
        }

        private void getSelected(int x, int y) {
            iSelectedRow = y / 64;
            iSelectedCol = x / 64;

            if (iSelectedRow > 8 || iSelectedRow < 0) {
                iSelectedRow = -1;
                iSelectedCol = -1;
            }

            if (iSelectedCol > 8 || iSelectedCol < 0) {
                iSelectedRow = -1;
                iSelectedCol = -1;
            }

        }

        private int[] getPieceRowAndCol(Piece p) {
            int[] iRowAndCol = new int[2];
            int i, j;
            for (i = 0; i < 8; i++) {
                for (j = 0; j < 8; j++) {
                    if (pieceBoard[i, j] == p) {
                        iRowAndCol[0] = i;
                        iRowAndCol[1] = j;
                    }
                }
            }
            return iRowAndCol;
        }

        private void nextPlayerTurn() {
            iCurrentPlayer++;
            if (iCurrentPlayer > 1) {
                iCurrentPlayer = 0;
            }
        }

        public void checkGameOver() {
            int[] iPieceCount = new int[2];

            int i, j;
            for (i = 0; i < 8; i++) {
                for (j = 0; j < 8; j++) {
                    if (pieceBoard[i, j] != null) {
                        iPieceCount[pieceBoard[i, j].iPlayer]++;
                    }
                }
            }

            if (iPieceCount[0] == 0) {
                iWinnerPlayer = 1;
                isGameOver = true;
            } else if (iPieceCount[1] == 0) {
                iWinnerPlayer = 0;
                isGameOver = true;

            }

        }

        private void checkPromotion(Piece piece) {
            int[] iRowAndCol = getPieceRowAndCol(piece);

            if (piece != null) {
                if (!piece.canMoveDownRow && iRowAndCol[0] == 8 - 1) {
                    piece.canMoveDownRow = true;
                    piece.strSprite = "king";
                } else if (!piece.canMoveUpRow && iRowAndCol[0] == 0) {
                    piece.canMoveUpRow = true;
                    piece.strSprite = "king";
                }
            }

        }
    }
}