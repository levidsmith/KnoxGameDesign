//2022 Levi D. Smith
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace TextReader {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Dictionary<string, Texture2D> textures;
        List<Wall> walls;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            walls = new List<Wall>();

            using (Stream stream = TitleContainer.OpenStream("knox.txt")) {
                using (StreamReader reader = new StreamReader(stream)) {
                    string strLine;
                    int iRow;
                    int iCol;

                    iRow = 0;
                    while ((strLine = reader.ReadLine()) != null) {
                        iCol = 0;

                        foreach(char c in strLine) {
                            if (c == '#') {
                                Wall wall = new Wall(iCol * 32, iRow * 32);
                                walls.Add(wall);
                            }
                            iCol++;
                        }
                        iRow++;
                    }
                }
            }

                base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            textures = new Dictionary<string, Texture2D>();
            textures.Add("wall", Content.Load<Texture2D>("spr_wall"));
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.Draw(textures["wall"], new Vector2(256, 256), Color.White);
            foreach (Wall wall in walls) {
                wall.Draw(_spriteBatch);
            }
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
