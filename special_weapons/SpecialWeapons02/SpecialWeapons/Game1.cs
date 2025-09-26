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
        public List<Bullet> listBullets;
        public List<Enemy> listEnemies;
        public List<Weapon> listWeapons;

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
            listBullets = new List<Bullet>();
            listEnemies = new List<Enemy>();
            listWeapons = new List<Weapon>();

            LevelReader lr = new LevelReader();
            lr.readLevel(listBlocks, listEnemies);

            listWeapons.Add(new WeaponBuster());
            listWeapons.Add(new WeaponEightWay());
            player.weapon = listWeapons[listWeapons.Count - 1];





            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textures = new Dictionary<string, Texture2D>();

            // TODO: use this.Content to load your game content here
            textures.Add("block", Content.Load<Texture2D>("block"));
            textures.Add("player", Content.Load<Texture2D>("player"));
            textures.Add("bullet", Content.Load<Texture2D>("bullet"));
            textures.Add("enemy", Content.Load<Texture2D>("enemy"));

            fontRegular = Content.Load<SpriteFont>("fontRegular");

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            handleInput();
            
            player.Update((float)gameTime.ElapsedGameTime.TotalSeconds, this);

            foreach(Bullet b in listBullets) {
                b.Update((float)gameTime.ElapsedGameTime.TotalSeconds, this);
            }

            foreach (Enemy e in listEnemies) {
                e.Update((float)gameTime.ElapsedGameTime.TotalSeconds, this);
            }

            foreach (Weapon w in listWeapons) {
                w.Update((float)gameTime.ElapsedGameTime.TotalSeconds, this);
            }

            base.Update(gameTime);
        }

        private void handleInput() {
            KeyboardState keyboardState = Keyboard.GetState();

            player.setInputDirection(keyboardState.IsKeyDown(Keys.Up), keyboardState.IsKeyDown(Keys.Down), keyboardState.IsKeyDown(Keys.Left), keyboardState.IsKeyDown(Keys.Right));



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

            if (keyboardState.IsKeyDown(Keys.Space) && !keyboardStatePrevious.IsKeyDown(Keys.Space)) {

                player.shoot(this);
            }

            if (!keyboardState.IsKeyDown(Keys.Tab) && keyboardStatePrevious.IsKeyDown(Keys.Tab)) {
                player.selectNextWeapon(this);
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

            foreach (Bullet b in listBullets) {
                b.Draw(_spriteBatch, textures);

            }

            foreach (Enemy e in listEnemies) {
                e.Draw(_spriteBatch, textures);

            }


            player.Draw(_spriteBatch, textures);

            _spriteBatch.DrawString(fontRegular, "Weapon: " + player.weapon.strName, new Vector2(32, 64), Color.White);

            _spriteBatch.End();
        }



    }
}
