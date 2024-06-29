using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Jumping {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 720;

        public const int BLOCK_SIZE = 48;
        Texture2D imgBlock;
        Texture2D imgPlayer;
        Player player;
        KeyboardState keyboardStatePrevious;
        SpriteFont fontRegular;


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

            base.Initialize();
            player = new Player();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            imgBlock = Content.Load<Texture2D>("block");
            imgPlayer = Content.Load<Texture2D>("player");
            fontRegular = Content.Load<SpriteFont>("RegularFont");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            handleInput();
            player.Update((float) gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        private void handleInput() {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left)) {
                player.vel_x = BLOCK_SIZE * -4;
            } else if (keyboardState.IsKeyDown(Keys.Right)) {
                player.vel_x = BLOCK_SIZE * 4;
            } else {
                player.vel_x = 0f;
            }

            if (keyboardState.IsKeyDown(Keys.Space) && !keyboardStatePrevious.IsKeyDown(Keys.Space)) {
                player.startJump();
            }

            if (!keyboardState.IsKeyDown(Keys.Space) && keyboardStatePrevious.IsKeyDown(Keys.Space)) {
                player.stopJump();
            }


            keyboardStatePrevious = keyboardState;

        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            _spriteBatch.Begin();

            int i, j;
            for (i = 0; i < 2; i++) {
                for (j = 0; j < (SCREEN_WIDTH / BLOCK_SIZE) + 1; j++) {
                    _spriteBatch.Draw(imgBlock, new Rectangle(j * BLOCK_SIZE, SCREEN_HEIGHT - (BLOCK_SIZE * (i + 1)), BLOCK_SIZE, BLOCK_SIZE), Color.White);
                }
            }

            _spriteBatch.Draw(imgPlayer, new Rectangle((int)player.x, SCREEN_HEIGHT - (int)player.y - (int)player.h, BLOCK_SIZE, BLOCK_SIZE * 2), Color.White);

            int iTextX = 8;
            int iTextY = 32;
            _spriteBatch.DrawString(fontRegular, string.Format("Linear Jump", player.x, player.y), new Vector2(iTextX, iTextY), Color.White);

            iTextY += 40;
            _spriteBatch.DrawString(fontRegular, string.Format("Player location: {0:0.0}, {1:0.0} ", player.x, player.y), new Vector2(iTextX, iTextY), Color.White);


            iTextY += 40;
            _spriteBatch.DrawString(fontRegular, string.Format("Jump State: {0}", player.getJumpstateName()), new Vector2(iTextX, iTextY), Color.White);

            iTextY += 40;
            _spriteBatch.DrawString(fontRegular, string.Format("Jump Button Time: {0:0.00}", player.fJumpButtonTime), new Vector2(iTextX, iTextY), Color.White);

            iTextY += 40;
            _spriteBatch.DrawString(fontRegular, string.Format("Max Jump Button Time: {0:0.00}", player.fMaxJumpButtonTime), new Vector2(iTextX, iTextY), Color.White);

            iTextY += 40;
            _spriteBatch.DrawString(fontRegular, string.Format("Jump Height Ratio: {0:0.00}", (player.y - (BLOCK_SIZE * 2)) / player.h), new Vector2(iTextX, iTextY), Color.White);


            _spriteBatch.End();

        }
    }
}
