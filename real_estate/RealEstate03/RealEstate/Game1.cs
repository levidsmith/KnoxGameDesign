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
                            string[] strLineArray = strLine.Split(",");
                            gamemanager.addProperty(strLineArray[0], int.Parse(strLineArray[1]), int.Parse(strLineArray[2]), i);
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

            switch (gamemanager.gamestate) {
                case GameManager.GameState.StartTurn:
                    if (keyboardCurrent.IsKeyDown(Keys.R) == true && keyboardPrevious.IsKeyDown(Keys.R) == false) {
                        gamemanager.dice[0].roll();
                        gamemanager.dice[1].roll();
                        gamemanager.moveSpaces();
                    }
                    break;
                case GameManager.GameState.LandOnSpace:
                    if (keyboardCurrent.IsKeyDown(Keys.E) == true && keyboardPrevious.IsKeyDown(Keys.E) == false) {
                        gamemanager.endTurn();
                    }

                    if (keyboardCurrent.IsKeyDown(Keys.B) == true && keyboardPrevious.IsKeyDown(Keys.B) == false) {
                        gamemanager.purchaseProperty(gamemanager.playerCurrent, gamemanager.playerCurrent.spaceCurrent.property);
                    }

                    break;

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
                    Color c = Color.Black;
                    Property property = gamemanager.spaces[i].property;
                    if (property != null) {
                        Player propertyOwner = property.getPropertyOwner();
                        if (propertyOwner != null) {
                            c = Player.colors[gamemanager.getPlayerIndex(propertyOwner)];
                        }
                    }
                    
                    _spriteBatch.DrawString(fontNormal, gamemanager.spaces[i].property.strName, gamemanager.spaces[i].position + new Vector2(128 + 48, 0), c);
                    _spriteBatch.DrawString(fontSmall, string.Format("${0}", gamemanager.spaces[i].property.iPurchasePrice), gamemanager.spaces[i].position + new Vector2(128 + 48, 32), Color.Black);
                }
            }

            for (i = 0; i < gamemanager.players.Count; i++) {
                Vector2 vectPosition = gamemanager.players[i].spaceCurrent.position;
                _spriteBatch.Draw(sprPlayerPiece, vectPosition + new Vector2((i * 20), 8), Player.colors[i]);
                _spriteBatch.DrawString(fontSmall, gamemanager.players[i].strName, vectPosition + new Vector2((i * 20), 8), Color.Black);
            }

            for (i = 0; i < gamemanager.players.Count; i++) {
                Vector2 vectPosition = new Vector2(400, 700 + (i * 50));
                _spriteBatch.DrawString(fontNormal, gamemanager.players[i].strName, vectPosition, Player.colors[i]);
                _spriteBatch.DrawString(fontNormal, " $" + gamemanager.players[i].iMoney, vectPosition + new Vector2(100, 0), Color.Black);
            }


            _spriteBatch.DrawString(fontNormal, "Current player: " + gamemanager.playerCurrent.strName, new Vector2(32, 700), Color.Black);

            switch(gamemanager.gamestate) {
                case GameManager.GameState.StartTurn:
                    _spriteBatch.DrawString(fontNormal, "R: Roll", new Vector2(32, 750), Color.Black);
                    break;
                case GameManager.GameState.LandOnSpace:
                    _spriteBatch.DrawString(fontNormal, "Dice: " + gamemanager.dice[0].iRolledValue + ", " + gamemanager.dice[1].iRolledValue, new Vector2(32, 750), Color.Black);
                    if (gamemanager.playerCurrent.spaceCurrent.property != null &&
                        !gamemanager.playerCurrent.spaceCurrent.property.isOwned()) {
                        _spriteBatch.DrawString(fontNormal, "B: Buy Property", new Vector2(32, 800), Color.Black);
                    }
                    _spriteBatch.DrawString(fontNormal, "E: End Turn", new Vector2(32, 850), Color.Black);
                    break;
                case GameManager.GameState.GameOver:
                    _spriteBatch.DrawString(fontSmall, "Game Over: Player " + gamemanager.playerCurrent.strName + " wins!", new Vector2(800, 750), Color.Black);
                    break;

            }

            _spriteBatch.DrawString(fontSmall, gamemanager.strMessage, new Vector2(800, 700), Color.Black);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
