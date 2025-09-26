using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace SpecialWeapons {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 720;

        public const int BLOCK_SIZE = 48;
        Dictionary<string, Texture2D> textures;

        KeyboardState keyboardStatePrevious;
        SpriteFont fontRegular;

        public Player player;
        public List<Block> listBlocks;

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

            player = new Player();

            listBlocks = new List<Block>();

            LevelReader lr = new LevelReader();
            lr.readLevel(listBlocks);


            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textures = new Dictionary<string, Texture2D>();

            // TODO: use this.Content to load your game content here
            textures.Add("block", Content.Load<Texture2D>("block"));
            textures.Add("player", Content.Load<Texture2D>("player"));

            fontRegular = Content.Load<SpriteFont>("fontRegular");

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            handleInput();
            
            player.Update((float)gameTime.ElapsedGameTime.TotalSeconds, this);


            base.Update(gameTime);
        }

        private void handleInput() {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left)) {
                player.walkLeft();
            } else if (keyboardState.IsKeyDown(Keys.Right)) {
                player.walkRight();
            } else {
                player.walkNone();
            }

            if (keyboardState.IsKeyDown(Keys.Z) && !keyboardStatePrevious.IsKeyDown(Keys.Z)) {
                player.startJump();
            }

            if (!keyboardState.IsKeyDown(Keys.Z) && keyboardStatePrevious.IsKeyDown(Keys.Z)) {
                player.stopJump();
            }


            keyboardStatePrevious = keyboardState;

        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            _spriteBatch.Begin();


            foreach (Block b in listBlocks) {
                b.Draw(_spriteBatch, textures);

            }

            player.Draw(_spriteBatch, textures);

            _spriteBatch.DrawString(fontRegular, "pos: " + player.x + ", " + player.y, new Vector2(32, 32), Color.White);
            _spriteBatch.DrawString(fontRegular, "jumpstate: " + player.getJumpstateName(), new Vector2(32, 64), Color.White);

            _spriteBatch.End();
        }



    }
}
