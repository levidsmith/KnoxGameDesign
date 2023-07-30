using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Parallax {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<string, Texture2D> sprites;

        Vector2 posPlayer;
        Vector2 posCamera;

        KeyboardState previousState;


        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            posPlayer = new Vector2(64 * 10, 64 * 9);
            posCamera = new Vector2();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            sprites = new Dictionary<string, Texture2D>();

            Texture2D texture;
            
            texture = Content.Load<Texture2D>("brick");
            sprites.Add("brick", texture);

            texture = Content.Load<Texture2D>("tree");
            sprites.Add("tree", texture);

            texture = Content.Load<Texture2D>("mountain");
            sprites.Add("mountain", texture);


            texture = Content.Load<Texture2D>("player");
            sprites.Add("player", texture);

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            posCamera = posPlayer;

            KeyboardState state = Keyboard.GetState();
            Keys key;

            key = Keys.Left;
            if (state.IsKeyDown(key)) {
                posPlayer.X = posPlayer.X - (5 * 64) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            key = Keys.Right;
            if (state.IsKeyDown(key)) {
                posPlayer.X = posPlayer.X + (5 * 64) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }



            base.Update(gameTime);
            previousState = state;


        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            int i, j;


            //mountains
            for (i = 0; i < 20; i++) {

                int x = i * 256;
                int y = 6 * 64;
                _spriteBatch.Draw(sprites["mountain"], new Rectangle(x, y, 256, 256), Color.White);
            }


            //trees
            for (i = 0; i < 20; i++) {
                for (j = 8; j < 9; j++) {
                    int x = i * 64;
                    int y = j * 64;
                    _spriteBatch.Draw(sprites["tree"], new Rectangle(x, y, 64, 128), Color.White);
                }
            }


            //bricks
            for (i = 0; i < 20; i++) {
                for (j = 10; j < 12; j++) {
                    int x = i * 64;
                    int y = j * 64;
                    _spriteBatch.Draw(sprites["brick"], new Rectangle(x, y, 64, 64), Color.White);
                }
            }

            //player
            _spriteBatch.Draw(sprites["player"], new Rectangle((int) posPlayer.X, (int) posPlayer.Y, 64, 64), Color.White);



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}