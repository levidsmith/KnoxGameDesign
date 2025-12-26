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
        Texture2D sprPropertySpace;
        Texture2D sprPropertySpaceColor;

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
            fontNormal = Content.Load<SpriteFont>("FontNormal");
            fontSmall = Content.Load<SpriteFont>("FontSmall");
            sprPlayerPiece = Content.Load<Texture2D>("player_piece");
            sprPropertySpace = Content.Load<Texture2D>("property_space");
            sprPropertySpaceColor = Content.Load<Texture2D>("property_space_color");


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

//                        gamemanager.dice[0].iRolledValue = 2;
//                        gamemanager.dice[1].iRolledValue = 3;
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
            GraphicsDevice.Clear(Color.LightGray);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            int i;
            for (i = 0; i < gamemanager.spaces.Count; i++) {
                _spriteBatch.Draw(sprPropertySpace, gamemanager.spaces[i].position, Color.White);

                //_spriteBatch.DrawString(fontNormal, i + ":", gamemanager.spaces[i].position + new Vector2(128, 0), Color.Black);
                Property property = gamemanager.spaces[i].property;
                if (property != null) {
                    Color c = Color.Black;

                    Player propertyOwner = null ;                    
                    if (property != null) {
                        propertyOwner = property.getPropertyOwner();
                        if (propertyOwner != null) {
                            c = Player.colors[gamemanager.getPlayerIndex(propertyOwner)];
                        }
                    }

                    if (property is PropertyResidential) {
                        _spriteBatch.Draw(sprPropertySpaceColor, gamemanager.spaces[i].position, PropertyResidential.PropertySetColors[((PropertyResidential) property).iPropertySet]);
                    }
                    _spriteBatch.DrawString(fontNormal, property.strName, gamemanager.spaces[i].position + new Vector2(48, 0), c);

                    if (!property.isOwned()) {
                        _spriteBatch.DrawString(fontSmall, string.Format("Buy ${0}", property.iPurchasePrice), gamemanager.spaces[i].position + new Vector2(48, 32), Color.Black);
                    } else {
                        _spriteBatch.DrawString(fontSmall, string.Format("Rent ${0}", property.calculateRent()), gamemanager.spaces[i].position + new Vector2(48, 32), Color.Black);
                        if (property is PropertyResidential && propertyOwner != null && gamemanager.playerOwnsPropertySet(propertyOwner, (PropertyResidential)property)) {
                            _spriteBatch.DrawString(fontSmall, "Set owned", gamemanager.spaces[i].position + new Vector2(200, 32), c);
                        }

                    }
                }
            }

            for (i = 0; i < gamemanager.players.Count; i++) {
                Vector2 vectPosition = gamemanager.players[i].spaceCurrent.position;
                _spriteBatch.Draw(sprPlayerPiece, vectPosition + new Vector2(( (i%2) * 20) + 340, (i/2) * 20), Player.colors[i]);
                _spriteBatch.DrawString(fontSmall, gamemanager.players[i].strName, vectPosition + new Vector2(( (i%2) * 20) + 340, (i/2) * 20), Color.Black);
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
