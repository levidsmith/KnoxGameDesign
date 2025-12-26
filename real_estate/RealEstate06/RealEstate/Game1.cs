using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace RealEstate { 
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int SCREEN_WIDTH = 1920;
        public const int SCREEN_HEIGHT = 1080;
        GameManager gamemanager;

        Dictionary<string, SpriteFont> fonts;
        Dictionary<string, Texture2D> sprites;

        KeyboardState keyboardCurrent;
        KeyboardState keyboardPrevious;

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
            gamemanager = new GameManager();

            using (Stream stream = TitleContainer.OpenStream("properties.txt")) {
                using (StreamReader reader = new StreamReader(stream)) {
                    int i = 0;
                    string strLine;
                    while ((strLine = reader.ReadLine()) != null) {
                        if (strLine != "") {
                            string[] strLineArray = strLine.Split(",");
                            switch(strLineArray[0]) {
                                case "R":
                                    gamemanager.addPropertyResidential(i, int.Parse(strLineArray[1]), strLineArray[2], int.Parse(strLineArray[3]), int.Parse(strLineArray[4]), int.Parse(strLineArray[5]), int.Parse(strLineArray[6]), int.Parse(strLineArray[7]), int.Parse(strLineArray[8]), int.Parse(strLineArray[9]), int.Parse(strLineArray[10]), int.Parse(strLineArray[11]));
                                    break;
                                case "P":
                                    gamemanager.addPropertyPark(i, strLineArray[1], int.Parse(strLineArray[2]));
                                    break;
                                case "D":
                                    gamemanager.addPropertyDam(i, strLineArray[1], int.Parse(strLineArray[2]));
                                    break;
                            }
                            
                        }

                        i++;
                    }
                }
            }


            

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            fonts = new Dictionary<string, SpriteFont>();
            fonts.Add("fontNormal", Content.Load<SpriteFont>("FontNormal"));
            fonts.Add("fontSmall", Content.Load<SpriteFont>("FontSmall"));

            sprites = new Dictionary<string, Texture2D>();
            sprites.Add("sprPlayerPiece", Content.Load<Texture2D>("player_piece"));
            sprites.Add("sprPropertySpace", Content.Load<Texture2D>("property_space"));
            sprites.Add("sprPropertySpaceColor", Content.Load<Texture2D>("property_space_color"));


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardCurrent = Keyboard.GetState();

            gamemanager.modeCurrent.Update(gameTime, keyboardCurrent, keyboardPrevious);

            // TODO: Add your update logic here

            base.Update(gameTime);

            keyboardPrevious = keyboardCurrent;
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();

            _spriteBatch.DrawString(fonts["fontNormal"], "Mode: " + gamemanager.modeCurrent.strName, new Vector2(800, 32), Color.Black);
            _spriteBatch.End();


            gamemanager.modeCurrent.Draw(gameTime, _spriteBatch, fonts, sprites);


            base.Draw(gameTime);
        }
    }
}
