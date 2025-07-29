using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PelletEatingDemo
{
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static int SCREEN_WIDTH = 1280;
        public static int SCREEN_HEIGHT = 720;

        private Dictionary<string, Texture2D> textures;

        public Player player;
        public List<Pellet> pellets;
        public List<Wall> walls;
        public List<Enemy> enemies;

        public enum State { title, ready, running, complete, gameover};
        public State state;

        public SpriteFont fontLarge;
        public SpriteFont fontNormal;
        float fTitleCountDown;
        float fReadyCountDown;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            player = new Player(this);
            pellets = new List<Pellet>();
            walls = new List<Wall>();
            enemies = new List<Enemy>();

            transitionStateTitle();
        }

        public void resetLevel() {
            player.resetPosition();
            pellets.Clear();
            walls.Clear();
            enemies.Clear();

            int iCellWidth = 32;

            int i, j;
            for (i = 0; i < SCREEN_HEIGHT / iCellWidth; i++) {
                for (j = 0; j < SCREEN_WIDTH / iCellWidth; j++) {
                    if (i == 0 ||
                        i == (SCREEN_HEIGHT / iCellWidth) - 1 ||
                        i == (SCREEN_HEIGHT / iCellWidth) - 2 ||
                        j == 0 ||
                        j == (SCREEN_WIDTH / iCellWidth - 1) ||
                        ((i % 2 == 0) && (j % 10 != 1) && (j != 38))
                        ) {

                        Wall w = new Wall(this);
                        w.x = j * iCellWidth;
                        w.y = i * iCellWidth;
                        walls.Add(w);

                    } else {

                        Pellet p = new Pellet(this);
                        p.x = (j * iCellWidth) + (iCellWidth / 2);
                        p.y = (i * iCellWidth) + (iCellWidth / 2);
                        pellets.Add(p);
                    }

                }
            }

            Enemy e;

            e = new Enemy(this);
            e.xStart = (1 * iCellWidth);
            e.yStart = (1 * iCellWidth);
            e.resetPosition();
            e.color = Color.Red;
            enemies.Add(e);

            e = new Enemy(this);
            e.xStart = (38 * iCellWidth);
            e.yStart = (1 * iCellWidth);
            e.resetPosition();
            e.color = Color.Yellow;
            enemies.Add(e);

            e = new Enemy(this);
            e.xStart = (1 * iCellWidth);
            e.yStart = (19 * iCellWidth);
            e.resetPosition();
            e.color = Color.Cyan;
            enemies.Add(e);

            e = new Enemy(this);
            e.xStart = (38 * iCellWidth);
            e.yStart = (19 * iCellWidth);
            e.resetPosition();
            e.color = Color.Magenta;
            enemies.Add(e);


        }

        public void resetActorPositions() {
            player.resetPosition();
            foreach(Enemy enemy in enemies) {
                enemy.resetPosition();
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textures = new Dictionary<string, Texture2D>();
            textures.Add("player", Content.Load<Texture2D>("player"));
            textures.Add("pellet", Content.Load<Texture2D>("pellet"));
            textures.Add("wall", Content.Load<Texture2D>("wall"));
            textures.Add("enemy", Content.Load<Texture2D>("enemy"));

            fontLarge = Content.Load<SpriteFont>("FontLarge");
            fontNormal = Content.Load<SpriteFont>("FontNormal");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (state == State.title) {

                if (fTitleCountDown > 0f) {
                    fTitleCountDown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else {

                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                        player.iLives = 3;
                        resetLevel();
                        transitionStateReady();

                    }
                }
            } else if (state == State.ready) {
                    fReadyCountDown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (fReadyCountDown <= 0f) {
                        state = State.running;
                    }
            } else if (state == State.running) {

                    checkInput();
                    player.move((float)gameTime.ElapsedGameTime.TotalSeconds);
                    player.checkPelletCollision();
                    player.checkWallCollision();
                    player.checkEnemyCollision();

                    foreach (Enemy enemy in enemies) {
                        enemy.move((float)gameTime.ElapsedGameTime.TotalSeconds);
                        enemy.checkWallCollision();
                    }

                    if (pellets.Count == 0) {
                        state = State.complete;
                    }
            } else if (state == State.complete) {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                        resetLevel();
                    transitionStateReady();

                    }
            } else if (state == State.gameover) {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                    transitionStateTitle();

                    }
            }

            base.Update(gameTime);
        }

        public void checkInput() {
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                player.inputUp();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                player.inputDown();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                player.inputLeft();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                player.inputRight();
            }

        }

        protected override void Draw(GameTime gameTime) {

            if (state == State.title) {
                _spriteBatch.Begin();
                GraphicsDevice.Clear(Color.LightGray);

                _spriteBatch.DrawString(fontLarge, "PELLET EATING", new Vector2(240, 200), Color.Black);
                _spriteBatch.DrawString(fontLarge, "MAZE GAME", new Vector2(240, 300), Color.Black);

                _spriteBatch.End();
            } else if (state == State.ready) {
                _spriteBatch.Begin();
                GraphicsDevice.Clear(Color.LightGray);

                foreach (Wall wall in walls) {
                    wall.Draw(_spriteBatch, textures);
                }

                foreach (Pellet pellet in pellets) {
                    pellet.Draw(_spriteBatch, textures);
                }

                _spriteBatch.DrawString(fontLarge, "READY", new Vector2(240, 300), Color.Black);

                _spriteBatch.End();

            } else if (state == State.running) {
                GraphicsDevice.Clear(Color.LightGray);

                _spriteBatch.Begin();
                player.Draw(_spriteBatch, textures);
                foreach (Pellet pellet in pellets) {
                    pellet.Draw(_spriteBatch, textures);
                }
                foreach (Wall wall in walls) {
                    wall.Draw(_spriteBatch, textures);
                }
                foreach (Enemy enemy in enemies) {
                    enemy.Draw(_spriteBatch, textures);
                }

                _spriteBatch.DrawString(fontNormal, string.Format("Lives {0}", player.iLives), new Vector2(16, 700), Color.Black);

                _spriteBatch.End();
            } else if (state == State.complete) {
                _spriteBatch.Begin();
                GraphicsDevice.Clear(Color.LightGray);

                foreach (Wall wall in walls) {
                    wall.Draw(_spriteBatch, textures);
                }
                _spriteBatch.DrawString(fontLarge, "LEVEL COMPLETE", new Vector2(240, 300), Color.Black);

                _spriteBatch.End();
            } else if (state == State.gameover) {
                _spriteBatch.Begin();
                GraphicsDevice.Clear(Color.LightGray);

                _spriteBatch.DrawString(fontLarge, "GAME OVER", new Vector2(240, 300), Color.Black);

                _spriteBatch.End();


            }


            base.Draw(gameTime);
        }

        public void transitionStateTitle() {
            fTitleCountDown = 2f;
            state = State.title;
        }

        public void transitionStateReady() {
            resetActorPositions();
            fReadyCountDown = 1f;
            state = State.ready;
        }

        public void transitionStateGameover() {
            state = State.gameover;
        }
    }
}
