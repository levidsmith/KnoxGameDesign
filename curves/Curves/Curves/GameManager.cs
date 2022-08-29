//2022 LD Smith - levidsmith.com
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Curves {
    public class GameManager : Game {
        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 720;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Screen> screens;
        int iCurrentScreen;
        public Dictionary<string, Texture2D> sprites;
        public Dictionary<string, SpriteFont> fonts;
        InputHandler inputhandler;

        public GameManager() {
            _graphics = new GraphicsDeviceManager(this);


            Content.RootDirectory = "Content";
            IsMouseVisible = true;



            screens = new List<Screen>();
            sprites = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();

            screens.Add(new ScreenBasicParabola(this));
            screens.Add(new ScreenSineWave(this));
            screens.Add(new ScreenPhysics(this));
            screens.Add(new ScreenBezier(this));
            iCurrentScreen = 0;

            inputhandler = new InputHandler(this);

        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            sprites.Add("player", Content.Load<Texture2D>("player"));

            fonts.Add("gridfont", Content.Load<SpriteFont>("gridfont"));
            fonts.Add("largefont", Content.Load<SpriteFont>("largefont"));
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            inputhandler.handleInputKeyboard();

            // TODO: Add your update logic here

            base.Update(gameTime);

            screens[iCurrentScreen].Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            screens[iCurrentScreen].Draw(_spriteBatch);

        }

        public void doNextCurve() {
            screens[iCurrentScreen].doNext();
        }

        public void doNextScreen() {
            iCurrentScreen++;
            if (iCurrentScreen >= 4) {
                iCurrentScreen = 0;
            }
            
        }


    }
}
