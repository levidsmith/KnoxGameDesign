using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RealEstate.Display;
using System.Collections.Generic;
using System.IO;


namespace RealEstate { 
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;

        public const int SCREEN_WIDTH = 1920;
        public const int SCREEN_HEIGHT = 1080;
        GameManager gamemanager;
        DisplayManager displaymanager;


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

            displaymanager = new DisplayManager(gamemanager);



            

            base.Initialize();
        }

        protected override void LoadContent() {
            SpriteBatch _spriteBatch;
            Dictionary<string, SpriteFont> fonts;
            Dictionary<string, Texture2D> sprites;

        _spriteBatch = new SpriteBatch(GraphicsDevice);
            fonts = new Dictionary<string, SpriteFont>();
            fonts.Add("fontNormal", Content.Load<SpriteFont>("FontNormal"));
            fonts.Add("fontSmall", Content.Load<SpriteFont>("FontSmall"));

            sprites = new Dictionary<string, Texture2D>();
            sprites.Add("sprPlayerPiece", Content.Load<Texture2D>("player_piece"));
//            sprites.Add("sprPropertySpace", Content.Load<Texture2D>("property_space"));
//            sprites.Add("sprPropertySpaceColor", Content.Load<Texture2D>("property_space_color"));
            sprites.Add("sprSpaceRegular", Content.Load<Texture2D>("space_regular"));
            sprites.Add("sprSpaceCorner", Content.Load<Texture2D>("space_corner"));
            sprites.Add("sprSpacePropertyColor", Content.Load<Texture2D>("space_property_color"));
            sprites.Add("sprBoardBackground", Content.Load<Texture2D>("board_background"));
            sprites.Add("sprToken01", Content.Load<Texture2D>("player_token_01"));
            sprites.Add("sprToken02", Content.Load<Texture2D>("player_token_02"));
            sprites.Add("sprToken03", Content.Load<Texture2D>("player_token_03"));
            sprites.Add("sprToken04", Content.Load<Texture2D>("player_token_04"));
            sprites.Add("sprToken05", Content.Load<Texture2D>("player_token_05"));
            sprites.Add("sprToken06", Content.Load<Texture2D>("player_token_06"));

            displaymanager._spriteBatch = _spriteBatch;
            displaymanager.fonts = fonts;
            displaymanager.sprites = sprites;
            
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

            displaymanager.Draw(gameTime);


            base.Draw(gameTime);
        }
    }
}
