using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace IFrames {
    public class GameManager : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont myfont;

        public List<GameObject> gameobjects;
        public Dictionary<string, Texture2D> textures;

        Player player;
        InputHandler inputhandler;


        



        public GameManager() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            gameobjects = new List<GameObject>();
            textures = new Dictionary<string, Texture2D>();

            

        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here


            base.Initialize();

            GameObject obj;
            obj = new Player("Player", textures["player"], this);
            obj.setPosition(200, 200);
            gameobjects.Add(obj);
            player = (Player) obj;
            
            obj = new Enemy("Enemy", textures["enemy"], this);
            obj.setPosition(600, 200);
            gameobjects.Add(obj);

            inputhandler = new InputHandler(player);

        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            textures["player"] = this.Content.Load<Texture2D>("player");
            textures["player_dead"] = this.Content.Load<Texture2D>("player_dead");
            textures["enemy"] = this.Content.Load<Texture2D>("enemy");

            myfont = this.Content.Load<SpriteFont>("myfont");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            foreach (GameObject obj in gameobjects) {
                obj.Update(gameTime);
            }

            inputhandler.handleInputKeyboard();
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (GameObject obj in gameobjects) {
                obj.Draw(_spriteBatch);
            }

            _spriteBatch.DrawString(myfont, "HP: " + player.iHP, new Vector2(32, 32), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
