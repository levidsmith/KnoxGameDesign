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

        public enum State { title, ready, running, complete};
        public State state;

        public SpriteFont fontLarge;
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

            resetLevel();


            state = State.title;
        }

        public void resetLevel() {
            player.setStartPosition();
            pellets.Clear();
            walls.Clear();

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

            fontLarge = Content.Load<SpriteFont>("FontLarge");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (state == State.title) {
                if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                    resetLevel();
                    fReadyCountDown = 3f;
                    state = State.ready;

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

                if (pellets.Count == 0) {
                    state = State.complete;
                }
            } else if (state == State.complete) {
                if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                    resetLevel();
                    fReadyCountDown = 3f;
                    state = State.ready;

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

                _spriteBatch.End();
            } else if (state == State.complete) {
                _spriteBatch.Begin();
                GraphicsDevice.Clear(Color.LightGray);

                foreach (Wall wall in walls) {
                    wall.Draw(_spriteBatch, textures);
                }
                _spriteBatch.DrawString(fontLarge, "LEVEL COMPLETE", new Vector2(240, 300), Color.Black);

                _spriteBatch.End();

            }


            base.Draw(gameTime);
        }
    }
}
