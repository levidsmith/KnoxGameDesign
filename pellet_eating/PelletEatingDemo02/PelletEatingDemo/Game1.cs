using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PelletEatingDemo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static int SCREEN_WIDTH = 1280;
        public static int SCREEN_HEIGHT = 720;

        private Dictionary<string, Texture2D> textures;

        Player player;
        List<Pellet> pellets;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            player = new Player();
            pellets = new List<Pellet>();

            int i, j;
            for (i = 0; i < SCREEN_HEIGHT / 32; i++) {
                for (j = 0; j < SCREEN_WIDTH / 32; j++) {
                    int iCellWidth = 32;
                    Pellet p = new Pellet();
                    p.x = (j * iCellWidth) + ( iCellWidth / 2);
                    p.y = (i * iCellWidth) + ( iCellWidth / 2);
                    pellets.Add(p);

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
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            checkInput();
            player.move((float) gameTime.ElapsedGameTime.TotalSeconds);

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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();
            player.Draw(_spriteBatch, textures);
            foreach (Pellet pellet in pellets) {
                pellet.Draw(_spriteBatch, textures);
            }
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
