using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace RealEstate { 
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int SCREEN_WIDTH = 1920;
        public const int SCREEN_HEIGHT = 1080;
        GameManager gamemanager;

        SpriteFont fontNormal;
        SpriteFont fontSmall;
        Texture2D sprPlayerPiece;

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
                            gamemanager.addProperty(strLine, i);
                        }

                        i++;
                    }
                }
            }


            

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            fontNormal = Content.Load<SpriteFont>("FontNormal");
            fontSmall = Content.Load<SpriteFont>("FontSmall");
            sprPlayerPiece = Content.Load<Texture2D>("player_piece");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardCurrent = Keyboard.GetState();
            if (keyboardCurrent.IsKeyDown(Keys.R) == true && keyboardPrevious.IsKeyDown(Keys.R) == false) {
                
                gamemanager.dice[0].roll();
                gamemanager.dice[1].roll();
                gamemanager.moveSpaces();
                gamemanager.endTurn();

            }
            // TODO: Add your update logic here

            base.Update(gameTime);

            keyboardPrevious = keyboardCurrent;
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            int i;
            for (i = 0; i < gamemanager.spaces.Count; i++) {
                _spriteBatch.DrawString(fontNormal, i + ":", gamemanager.spaces[i].position + new Vector2(128, 0), Color.Black);
                if (gamemanager.spaces[i].property != null) {
                    _spriteBatch.DrawString(fontNormal, gamemanager.spaces[i].property.strName, gamemanager.spaces[i].position + new Vector2(128 + 48, 0), Color.Black);
                }
            }

            for (i = 0; i < gamemanager.players.Count; i++) {
                Vector2 vectPosition = gamemanager.players[i].spaceCurrent.position;
                _spriteBatch.Draw(sprPlayerPiece, vectPosition + new Vector2((i * 20), 8), Player.colors[i]);
                _spriteBatch.DrawString(fontSmall, gamemanager.players[i].strName, vectPosition + new Vector2((i * 20), 8), Color.Black);
            }

            _spriteBatch.DrawString(fontNormal, "Current player: " + gamemanager.playerCurrent.strName, new Vector2(32, 700), Color.Black);
            _spriteBatch.DrawString(fontNormal, "R: Roll", new Vector2(32, 750), Color.Black);
            _spriteBatch.DrawString(fontNormal, "Dice: " + gamemanager.dice[0].iRolledValue + ", " + gamemanager.dice[1].iRolledValue, new Vector2(32, 800), Color.Black);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
