using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MemoryGame {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameManager gamemanager;
        public static Dictionary<string, Texture2D> textures;
        public static Dictionary<string, SpriteFont> fonts;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            gamemanager = new GameManager();
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textures.Add("CardFaceUp", Content.Load<Texture2D>("card_face_up"));
            textures.Add("CardFaceDown", Content.Load<Texture2D>("card_face_down"));

            fonts.Add("CardFont", Content.Load<SpriteFont>("CardFont"));
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
            _spriteBatch.Begin();
            gamemanager.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}