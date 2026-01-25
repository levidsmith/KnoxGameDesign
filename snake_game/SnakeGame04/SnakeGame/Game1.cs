using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SnakeGame {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int SCREEN_WIDTH = 1600;
        public const int SCREEN_HEIGHT = 1040;
        public const int CELL_SIZE = 20;

        Texture2D sprCell;
        GameManager gamemanager;
        SpriteFont sprFont;
      

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize() {
            // TODO: Add your initialization logic here
            gamemanager = new GameManager();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            sprCell = Content.Load<Texture2D>("cell");
            sprFont = Content.Load<SpriteFont>("DosFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            switch (gamemanager.gamestate) {
                case GameManager.GameState.PRE_LEVEL:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                        gamemanager.gamestate = GameManager.GameState.PLAYING;
                    }
                    break;
                case GameManager.GameState.PLAYER_DEAD:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                        gamemanager.resetLevel();
                        gamemanager.gamestate = GameManager.GameState.PLAYING;                    }
                    break;
                case GameManager.GameState.GAME_OVER:
                    if (Keyboard.GetState().IsKeyDown(Keys.Y)) {
                        gamemanager.resetGame();
                    } else if (Keyboard.GetState().IsKeyDown(Keys.N)) {
                        Exit();
                    }
                    break;
                case GameManager.GameState.PLAYING:
                    if (Keyboard.GetState().IsKeyDown(Keys.Up) && gamemanager.snakes[0].direction != Snake.Direction.SOUTH) {
                        gamemanager.snakes[0].direction = Snake.Direction.NORTH;
                    } else if (Keyboard.GetState().IsKeyDown(Keys.Down) && gamemanager.snakes[0].direction != Snake.Direction.NORTH) {
                        gamemanager.snakes[0].direction = Snake.Direction.SOUTH;
                    } else if (Keyboard.GetState().IsKeyDown(Keys.Right) && gamemanager.snakes[0].direction != Snake.Direction.WEST) {
                        gamemanager.snakes[0].direction = Snake.Direction.EAST;
                    } else if (Keyboard.GetState().IsKeyDown(Keys.Left) && gamemanager.snakes[0].direction != Snake.Direction.EAST) {
                        gamemanager.snakes[0].direction = Snake.Direction.WEST;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.W) && gamemanager.snakes[1].direction != Snake.Direction.SOUTH) {
                        gamemanager.snakes[1].direction = Snake.Direction.NORTH;
                    } else if (Keyboard.GetState().IsKeyDown(Keys.S) && gamemanager.snakes[1].direction != Snake.Direction.NORTH) {
                        gamemanager.snakes[1].direction = Snake.Direction.SOUTH;
                    } else if (Keyboard.GetState().IsKeyDown(Keys.D) && gamemanager.snakes[1].direction != Snake.Direction.WEST) {
                        gamemanager.snakes[1].direction = Snake.Direction.EAST;
                    } else if (Keyboard.GetState().IsKeyDown(Keys.A) && gamemanager.snakes[1].direction != Snake.Direction.EAST) {
                        gamemanager.snakes[1].direction = Snake.Direction.WEST;
                    }

                    float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    gamemanager.Update(deltaTime);
                    break;
            }
            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            Color colorQBasicBlue = new Color(0, 0, 170);
            GraphicsDevice.Clear(colorQBasicBlue);


            // TODO: Add your drawing code here
            switch (gamemanager.gamestate) {
                case GameManager.GameState.PRE_LEVEL:
                    DrawPreLevel();
                    break;
                case GameManager.GameState.PLAYER_DEAD:
                    DrawPlayerDead();
                    break;
                case GameManager.GameState.GAME_OVER:
                    DrawGameOver();
                    break;
                case GameManager.GameState.PLAYING:
                    DrawPlaying();
                    break;

            }


            base.Draw(gameTime);
        }

        private void DrawPreLevel() {
            int x, y;
            Color colorTextBackground = new Color(170, 0, 0);

            _spriteBatch.Begin();

            int iBorderWidth = 32;
            int iBorderHeight = 6;
            int iRowOffset;
            int iColOffset = (Arena.ARENA_COLS - iBorderWidth) / 2;

            iRowOffset = (Arena.ARENA_ROWS - 3) / 2;

            DrawFillCells(iRowOffset, iColOffset, iBorderHeight, iBorderWidth, Color.White);
            DrawFillCells(iRowOffset + 1, iColOffset + 1, iBorderHeight - 2, iBorderWidth - 2, colorTextBackground);

            string strText = "Level {0},   Push Space";
            DrawTextCentered(strText, iRowOffset + 2, Color.White);

            _spriteBatch.End();
        }

        private void DrawPlayerDead() {
            int x, y;
            Color colorTextBackground = new Color(170, 0, 0);

            _spriteBatch.Begin();

            int iBorderWidth = 34;
            int iBorderHeight = 6;
            int iRowOffset;
            int iColOffset = (Arena.ARENA_COLS - iBorderWidth) / 2;

            iRowOffset = (Arena.ARENA_ROWS - 3) / 2;

            DrawFillCells(iRowOffset, iColOffset, iBorderHeight, iBorderWidth, Color.White);
            DrawFillCells(iRowOffset + 1, iColOffset + 1, iBorderHeight - 2, iBorderWidth - 2, colorTextBackground);

            string strText = "";
            if (!gamemanager.snakes[0].isAlive) {
                strText = gamemanager.snakes[0].strName + " Dies!  Push Space!  --->";
            } else if (!gamemanager.snakes[1].isAlive) {
                strText = "<---- " + gamemanager.snakes[1].strName + " Dies!  Push Space!";
            }

            DrawTextCentered(strText, iRowOffset + 2, Color.White);

            _spriteBatch.End();
        }

        private void DrawGameOver() {
            int x, y;
            Color colorTextBackground = new Color(170, 0, 0);

            _spriteBatch.Begin();


            int iBorderWidth = 32;
            int iBorderHeight = 10;
            int iRowOffset;
            int iColOffset = (Arena.ARENA_COLS - iBorderWidth) / 2;

            iRowOffset = (Arena.ARENA_ROWS - 3) / 2;

            DrawFillCells(iRowOffset, iColOffset, iBorderHeight, iBorderWidth, Color.White);
            DrawFillCells(iRowOffset + 1, iColOffset + 1, iBorderHeight - 2, iBorderWidth - 2, colorTextBackground);

            string strText;
            strText = "        G A M E   O V E R       ";
            DrawTextCentered(strText, iRowOffset + 2, Color.White);

            strText = "       Play Again?    (Y/N)       ";
            DrawTextCentered(strText, iRowOffset + 6, Color.White);

            _spriteBatch.End();
        }

        private void DrawPlaying() {
            Color colorWalls = new Color(255, 85, 85);
            int iRowOffset = 2;
            int x, y;

            _spriteBatch.Begin();
            if (gamemanager.snakes[0].isAlive) {
                _spriteBatch.Draw(sprCell, new Rectangle(gamemanager.snakes[0].iCol * CELL_SIZE, (gamemanager.snakes[0].iRow + iRowOffset) * CELL_SIZE, CELL_SIZE, CELL_SIZE), gamemanager.snakes[0].color);
            }

            if (gamemanager.snakes[1].isAlive) {
                _spriteBatch.Draw(sprCell, new Rectangle(gamemanager.snakes[1].iCol * CELL_SIZE, (gamemanager.snakes[1].iRow + iRowOffset) * CELL_SIZE, CELL_SIZE, CELL_SIZE), gamemanager.snakes[1].color);
            }

            int i, j;
            for (i = 0; i < Arena.ARENA_ROWS; i++) {
                for (j = 0; j < Arena.ARENA_COLS; j++) {
                    if (gamemanager.arena.cells[i, j] == Arena.CELL_WALL) {
                        _spriteBatch.Draw(sprCell, new Rectangle(j * CELL_SIZE, (i + iRowOffset) * CELL_SIZE, CELL_SIZE, CELL_SIZE), colorWalls);
                    }

                    if (gamemanager.arena.cells[i, j] == Arena.CELL_SNAKE1_BODY) {
                        _spriteBatch.Draw(sprCell, new Rectangle(j * CELL_SIZE, (i + iRowOffset) * CELL_SIZE, CELL_SIZE, CELL_SIZE), gamemanager.snakes[0].color);
                    }

                    if (gamemanager.arena.cells[i, j] == Arena.CELL_SNAKE2_BODY) {
                        _spriteBatch.Draw(sprCell, new Rectangle(j * CELL_SIZE, (i + iRowOffset) * CELL_SIZE, CELL_SIZE, CELL_SIZE), gamemanager.snakes[1].color);
                    }
                }
            }

            if (gamemanager.collectible != null) {
                x = gamemanager.collectible.iCol * CELL_SIZE;
                y = (gamemanager.collectible.iRow + iRowOffset) * CELL_SIZE;
                _spriteBatch.DrawString(sprFont, string.Format("{0}", gamemanager.collectible.iValue), new Vector2(x, y), gamemanager.snakes[0].color);
            }

            x = 0;
            y = 0;
            _spriteBatch.DrawString(sprFont, string.Format("{0:#,###,##0} Lives: {1}  <--{2}", gamemanager.snakes[1].iScore * 100, gamemanager.snakes[1].iLives, gamemanager.snakes[1].strName.ToUpper()), new Vector2(x, y), Color.White);

            x = 50 * CELL_SIZE; ;
            y = 0;
            _spriteBatch.DrawString(sprFont, string.Format("{2}-->  Lives: {1}     {0:#,###,##0}", gamemanager.snakes[0].iScore * 100, gamemanager.snakes[0].iLives, gamemanager.snakes[0].strName.ToUpper()), new Vector2(x, y), Color.White);


            _spriteBatch.End();
        }

        private void DrawFillCells(int iFromRow, int iFromCol, int iRowCount, int iColCount, Color color) {
            int x, y, w, h;

            x = iFromCol * CELL_SIZE;
            y = iFromRow * CELL_SIZE;
            w = iColCount * CELL_SIZE;
            h = iRowCount * CELL_SIZE;

            _spriteBatch.Draw(sprCell, new Rectangle(x, y, w, h), color);

        }

        private void DrawTextCentered(string strText, int iRow, Color color) {
            int x, y;

            x = (Arena.ARENA_COLS - strText.Length) / 2 * CELL_SIZE;
            y = iRow * CELL_SIZE;

            _spriteBatch.DrawString(sprFont, string.Format(strText, gamemanager.iLevel), new Vector2(x, y), color);

        }

    }
}
