using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SnakeGame {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int CELL_SIZE = 20;

        public const int SCREEN_WIDTH = CELL_SIZE * Arena.ARENA_COLS;
        public const int SCREEN_HEIGHT = CELL_SIZE * (Arena.ARENA_ROWS + 2);

        GameManager gamemanager;

        SpriteFont sprFont;
        Texture2D sprCell;

        public static Dictionary<string, SoundEffect> sounds;
        public KeyboardState keyboardStateCurrent;
        public KeyboardState keyboardStatePrevious;
      

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
            gamemanager.doTitleScreen();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            sprCell = Content.Load<Texture2D>("cell");
            sprFont = Content.Load<SpriteFont>("DosFont");

            sounds = new Dictionary<string, SoundEffect>();
            sounds.Add("sound_intro", Content.Load<SoundEffect>("sound_intro"));
            sounds.Add("sound_level_start", Content.Load<SoundEffect>("sound_level_start"));
            sounds.Add("sound_dead", Content.Load<SoundEffect>("sound_dead"));
            sounds.Add("sound_pickup", Content.Load<SoundEffect>("sound_pickup"));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardStateCurrent = Keyboard.GetState();

            switch (gamemanager.gamestate) {
                case GameManager.GameState.TITLE:
                    if (keyboardStateCurrent.IsKeyDown(Keys.Space) && !keyboardStatePrevious.IsKeyDown(Keys.Space)) {
                        gamemanager.gamestate = GameManager.GameState.OPTIONS;
                    }
                    break;

                case GameManager.GameState.OPTIONS:

                    switch (gamemanager.options.optionsstate) {
                        case Options.OptionsState.GET_PLAYERS:
                            if (keyboardStateCurrent.IsKeyDown(Keys.D1) && !keyboardStatePrevious.IsKeyDown(Keys.D1)) {
                                gamemanager.options.iPlayers = 1;
                                gamemanager.options.optionsstate = Options.OptionsState.GET_SPEED;
                            }

                            if (keyboardStateCurrent.IsKeyDown(Keys.D2) && !keyboardStatePrevious.IsKeyDown(Keys.D2)) {
                                gamemanager.options.iPlayers = 2;
                                gamemanager.options.optionsstate = Options.OptionsState.GET_SPEED;
                            }
                            break;
                        case Options.OptionsState.GET_SPEED:
                            if (keyboardStateCurrent.IsKeyDown(Keys.D1) && !keyboardStatePrevious.IsKeyDown(Keys.D1)) {
                                gamemanager.options.setSpeed(1);
                                gamemanager.options.optionsstate = Options.OptionsState.GET_SPEED_INCREASE;

                            }
                            if (keyboardStateCurrent.IsKeyDown(Keys.D2) && !keyboardStatePrevious.IsKeyDown(Keys.D2)) {
                                gamemanager.options.setSpeed(2);
                                gamemanager.options.optionsstate = Options.OptionsState.GET_SPEED_INCREASE;
                            }
                            if (keyboardStateCurrent.IsKeyDown(Keys.D3) && !keyboardStatePrevious.IsKeyDown(Keys.D3)) {
                                gamemanager.options.setSpeed(3);
                                gamemanager.options.optionsstate = Options.OptionsState.GET_SPEED_INCREASE;
                            }
                            break;
                        case Options.OptionsState.GET_SPEED_INCREASE:
                            if (keyboardStateCurrent.IsKeyDown(Keys.Y) && !keyboardStatePrevious.IsKeyDown(Keys.Y)) {
                                gamemanager.options.isSpeedIncreased = true;
                                gamemanager.setupGame();
                                gamemanager.gamestate = GameManager.GameState.PRE_LEVEL;
                            }
                            if (keyboardStateCurrent.IsKeyDown(Keys.N) && !keyboardStatePrevious.IsKeyDown(Keys.N)) {
                                gamemanager.options.isSpeedIncreased = false;
                                gamemanager.setupGame();
                                gamemanager.gamestate = GameManager.GameState.PRE_LEVEL;
                            }

                            break;
                    }

                    break;

                case GameManager.GameState.PRE_LEVEL:
                    if (keyboardStateCurrent.IsKeyDown(Keys.Space) && !keyboardStatePrevious.IsKeyDown(Keys.Space)) {
                        gamemanager.gamestate = GameManager.GameState.PLAYING;
                        sounds["sound_level_start"].Play();
                    }
                    break;
                case GameManager.GameState.PLAYER_DEAD:
                    if (keyboardStateCurrent.IsKeyDown(Keys.Space) && !keyboardStatePrevious.IsKeyDown(Keys.Space)) {
                        gamemanager.resetLevel();
                        gamemanager.gamestate = GameManager.GameState.PLAYING;
                        sounds["sound_level_start"].Play();
                    }
                    break;
                case GameManager.GameState.GAME_OVER:
                    if (Keyboard.GetState().IsKeyDown(Keys.Y)) {
                        gamemanager.resetGame();
                    } else if (Keyboard.GetState().IsKeyDown(Keys.N)) {
                        Exit();
                    }
                    break;
                case GameManager.GameState.PLAYING:
                    if (gamemanager.options.iPlayers > 0) {
                        if (keyboardStateCurrent.IsKeyDown(Keys.Up) && !keyboardStatePrevious.IsKeyDown(Keys.Up) && gamemanager.snakes[0].direction != Snake.Direction.SOUTH) {
                            gamemanager.snakes[0].direction = Snake.Direction.NORTH;
                        } else if (keyboardStateCurrent.IsKeyDown(Keys.Down) && !keyboardStatePrevious.IsKeyDown(Keys.Down) && gamemanager.snakes[0].direction != Snake.Direction.NORTH) {
                            gamemanager.snakes[0].direction = Snake.Direction.SOUTH;
                        } else if (keyboardStateCurrent.IsKeyDown(Keys.Right) && !keyboardStatePrevious.IsKeyDown(Keys.Right) && gamemanager.snakes[0].direction != Snake.Direction.WEST) {
                            gamemanager.snakes[0].direction = Snake.Direction.EAST;
                        } else if (keyboardStateCurrent.IsKeyDown(Keys.Left) && !keyboardStatePrevious.IsKeyDown(Keys.Left) && gamemanager.snakes[0].direction != Snake.Direction.EAST) {
                            gamemanager.snakes[0].direction = Snake.Direction.WEST;
                        }
                    }

                    if (gamemanager.options.iPlayers > 1) {
                        if (keyboardStateCurrent.IsKeyDown(Keys.W) && !keyboardStatePrevious.IsKeyDown(Keys.W) && gamemanager.snakes[1].direction != Snake.Direction.SOUTH) {
                            gamemanager.snakes[1].direction = Snake.Direction.NORTH;
                        } else if (keyboardStateCurrent.IsKeyDown(Keys.S) && !keyboardStatePrevious.IsKeyDown(Keys.S) && gamemanager.snakes[1].direction != Snake.Direction.NORTH) {
                            gamemanager.snakes[1].direction = Snake.Direction.SOUTH;
                        } else if (keyboardStateCurrent.IsKeyDown(Keys.D) && !keyboardStatePrevious.IsKeyDown(Keys.D) && gamemanager.snakes[1].direction != Snake.Direction.WEST) {
                            gamemanager.snakes[1].direction = Snake.Direction.EAST;
                        } else if (keyboardStateCurrent.IsKeyDown(Keys.A) && !keyboardStatePrevious.IsKeyDown(Keys.A) && gamemanager.snakes[1].direction != Snake.Direction.EAST) {
                            gamemanager.snakes[1].direction = Snake.Direction.WEST;
                        }
                    }

                    float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    gamemanager.Update(deltaTime);
                    break;
            }

            base.Update(gameTime);

            keyboardStatePrevious = keyboardStateCurrent;

        }

        protected override void Draw(GameTime gameTime) {
            Color colorQBasicBlue = new Color(0, 0, 170);
            GraphicsDevice.Clear(colorQBasicBlue);


            // TODO: Add your drawing code here
            switch (gamemanager.gamestate) {
                case GameManager.GameState.TITLE:
                    DrawTitle(gameTime);
                    break;
                case GameManager.GameState.OPTIONS:
                    DrawOptions();
                    break;
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

            string strText = string.Format("Level {0},   Push Space", gamemanager.iLevel);
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

        private void DrawTitle(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            int x, y;
            Color colorTextBackground = new Color(170, 0, 0);
            Color colorQBasicGray = new Color(170, 170, 170);

            _spriteBatch.Begin();

            int iBorderWidth = 32;
            int iRowOffset = 2;
            int iColOffset = (Arena.ARENA_COLS - iBorderWidth) / 2;

            string strText = "";


            strText = "K n o x G a m e D e s i g n    S n a k e s";
            DrawTextCentered(strText, 6 - iRowOffset, Color.White);

            strText = "created in MonoGame - 2026";
            DrawTextCentered(strText, 8 - iRowOffset, colorQBasicGray);

            strText = "based on QBasic Nibbles by Rick Raddatz at Microsoft 1990";
            DrawTextCentered(strText, 12 - iRowOffset, colorQBasicGray);

            strText = "Nibbles is a game for one or two players.  Navigate your snakes";
            DrawTextCentered(strText, 16 - iRowOffset, colorQBasicGray);

            strText = "around the game board trying to eat up numbers while avoiding";
            DrawTextCentered(strText, 18 - iRowOffset, colorQBasicGray);

            strText = "running into walls or other snakes.  The more numbers you eat up,";
            DrawTextCentered(strText, 20 - iRowOffset, colorQBasicGray);

            strText = "the more points you gain and the longer your snake becomes.";
            DrawTextCentered(strText, 22 - iRowOffset, colorQBasicGray);

            strText = " Game Controls ";
            DrawTextCentered(strText, 26 - iRowOffset, colorQBasicGray);

            strText = "  General             Player 1               Player 2    ";
            DrawTextCentered(strText, 30 - iRowOffset, colorQBasicGray);

            strText = "                        (Up)                   (Up)      ";
            DrawTextCentered(strText, 32 - iRowOffset, colorQBasicGray);

            strText = "P - Pause                " + (char) 0x3F + "                      W       ";
            DrawTextCentered(strText, 34 - iRowOffset, colorQBasicGray);

            strText = "                     (Left) " + (char)0x3F + "   " + (char)0x3F + " (Right)   (Left) A   D (Right)  ";
            DrawTextCentered(strText, 36 - iRowOffset, colorQBasicGray);

            strText = "                         " + (char)0x3F + "                      S       ";
            DrawTextCentered(strText, 38 - iRowOffset, colorQBasicGray);

            strText = "                       (Down)                 (Down)     ";
            DrawTextCentered(strText, 40 - iRowOffset, colorQBasicGray);

            strText = "Press Space to Continue";
            DrawTextCentered(strText, 48 - iRowOffset, colorQBasicGray);


            _spriteBatch.End();
            DrawTitleSparkle(gameTime);
        }

        private void DrawTitleSparkle(GameTime gameTime) {
            //there's probably a better ways to do this
            //but I didn't want to spend all of my time on the
            //title screen
            Color colorQBasicRed = new Color(170, 0, 0);
            string strSparkle = "*    *    *    *    *    *    *    *    *    *    *    *    *    *    *    *    *    *";
            _spriteBatch.Begin();

            float fSparkleSpeed = 20; //moves 20 times per second
            int iStringOffset = (int) (gameTime.TotalGameTime.TotalSeconds * fSparkleSpeed) % 5;
            int x, y;

            x = 0 * CELL_SIZE;
            y = 0 * CELL_SIZE;
            _spriteBatch.DrawString(sprFont, strSparkle.Substring(iStringOffset), new Vector2(x, y), colorQBasicRed);

            x = 0 * CELL_SIZE;
            y = 42 * CELL_SIZE;
            _spriteBatch.DrawString(sprFont, new string(' ', iStringOffset) + strSparkle, new Vector2(x, y), colorQBasicRed);

            int i;
            strSparkle = "*";
            for (i = 0; i < 4; i++) {
                x = 0 * CELL_SIZE;
                y = (( (i * 5) + iStringOffset) * 2 ) * CELL_SIZE;
                _spriteBatch.DrawString(sprFont, strSparkle, new Vector2(x, y), colorQBasicRed);

                x = (80 - 1) * CELL_SIZE;
                y = (((i * 5) + (5 - iStringOffset)) * 2) * CELL_SIZE;
                _spriteBatch.DrawString(sprFont, strSparkle, new Vector2(x, y), colorQBasicRed);

            }


            _spriteBatch.End();

        }

        private void DrawOptions() {
            Color colorQBasicGray = new Color(170, 170, 170);

            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            if (gamemanager.options.optionsstate >= Options.OptionsState.GET_PLAYERS) {
                DrawTextAt("How many players (1 or 2)?", 10, 20, colorQBasicGray);
            }

            if (gamemanager.options.optionsstate >= Options.OptionsState.GET_SPEED) {
                DrawTextAt(string.Format("{0}", gamemanager.options.iPlayers), 10, 20 + 26, colorQBasicGray);

                DrawTextAt("Skill level?", 16, 21, colorQBasicGray);
                DrawTextAt("1   = Novice", 18, 22, colorQBasicGray);
                DrawTextAt("2   = Expert", 20, 22, colorQBasicGray);
                DrawTextAt("3   = Twiddle Fingers", 22, 22, colorQBasicGray);
            }

            if (gamemanager.options.optionsstate >= Options.OptionsState.GET_SPEED_INCREASE) {
                DrawTextAt(string.Format("{0}", gamemanager.options.iPlayers), 10, 20 + 26, colorQBasicGray);
                DrawTextAt(string.Format("{0}", gamemanager.options.iSkillLevel), 16, 21 + 13, colorQBasicGray);

                DrawTextAt("Increase game speed during play (Y or N)", 30, 15, colorQBasicGray);
            }

            _spriteBatch.End();


        }


        private void DrawPlaying() {
            Color colorWalls = new Color(255, 85, 85);
            int iRowOffset = 2;
            int x, y;

            _spriteBatch.Begin();
            if (gamemanager.snakes.Count > 0 && gamemanager.snakes[0].isAlive) {
                _spriteBatch.Draw(sprCell, new Rectangle(gamemanager.snakes[0].iCol * CELL_SIZE, (gamemanager.snakes[0].iRow + iRowOffset) * CELL_SIZE, CELL_SIZE, CELL_SIZE), gamemanager.snakes[0].color);
            }

            if (gamemanager.snakes.Count > 1 && gamemanager.snakes[1].isAlive) {
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
                _spriteBatch.DrawString(sprFont, string.Format("{0}", gamemanager.collectible.iValue), new Vector2(x, y), gamemanager.collectible.color);
            }

            if (gamemanager.options.iPlayers > 0) {
                x = 50 * CELL_SIZE; ;
                y = 0;
                _spriteBatch.DrawString(sprFont, string.Format("{2}-->  Lives: {1}     {0:#,###,##0}", gamemanager.snakes[0].iScore * 100, gamemanager.snakes[0].iLives, gamemanager.snakes[0].strName.ToUpper()), new Vector2(x, y), Color.White);
            }

            if (gamemanager.options.iPlayers > 1) {
                x = 0;
                y = 0;
                _spriteBatch.DrawString(sprFont, string.Format("{0:#,###,##0} Lives: {1}  <--{2}", gamemanager.snakes[1].iScore * 100, gamemanager.snakes[1].iLives, gamemanager.snakes[1].strName.ToUpper()), new Vector2(x, y), Color.White);
            }

//            _spriteBatch.DrawString(sprFont, string.Format("Delay: {0:0.000} Max Delay: {1:0.000}", gamemanager.fUpdateDelay, gamemanager.fMaxUpdateDelay - gamemanager.fUpdateDelayReduction), new Vector2(0, 2 * CELL_SIZE), Color.White);

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

            _spriteBatch.DrawString(sprFont, strText, new Vector2(x, y), color);

        }

        private void DrawTextAt(string strText, int iRow, int iCol, Color color) {
            int x, y;

            x = iCol * CELL_SIZE;
            y = iRow * CELL_SIZE;

            _spriteBatch.DrawString(sprFont, strText, new Vector2(x, y), color);

        }


    }
}
