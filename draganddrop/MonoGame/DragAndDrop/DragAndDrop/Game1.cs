using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DragAndDrop {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static MouseState currentMouseState, previousMouseState;

        Card card;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            card = new Card();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            card.imgCard = Content.Load<Texture2D>("card");

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed) {
                card.pointerPressed(new Vector2(currentMouseState.Position.X, currentMouseState.Position.Y));
            } else if (currentMouseState.LeftButton == ButtonState.Pressed) {
                card.pointerDown(new Vector2(currentMouseState.Position.X, currentMouseState.Position.Y));
            } else if (currentMouseState.LeftButton != ButtonState.Pressed) {
                card.pointerUp(new Vector2(currentMouseState.Position.X, currentMouseState.Position.Y));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            float fScale = 2.5f;
            int w = (int) (250 / fScale);
            int h = (int)(350 / fScale);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            card.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
