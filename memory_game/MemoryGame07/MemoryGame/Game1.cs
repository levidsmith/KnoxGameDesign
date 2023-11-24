using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace MemoryGame {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameManager gamemanager;
        public static Dictionary<string, Texture2D> textures;
        public static Dictionary<string, SpriteFont> fonts;
        public static Dictionary<string, SoundEffect> soundeffects;
        public static Dictionary<string, Song> songs;

        MouseState statePrevious;

        public Game1() {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            textures = new Dictionary<string, Texture2D>();
            soundeffects = new Dictionary<string, SoundEffect>();
            fonts = new Dictionary<string, SpriteFont>();
            songs = new Dictionary<string, Song>();
            gamemanager = new GameManager();
        }

        protected override void Initialize() {
            _graphics.PreferredBackBufferWidth = GameManager.CARDS_PER_ROW * (Card.CARD_WIDTH + GameManager.CARD_SPACING);
            _graphics.PreferredBackBufferHeight = (GameManager.MAX_CARDS / GameManager.CARDS_PER_ROW) * (Card.CARD_HEIGHT + GameManager.CARD_SPACING);
            _graphics.ApplyChanges();

            Window.Title = "Memory Game Demo - 2023 Levi D. Smith";
            base.Initialize();

            MediaPlayer.Play(songs["MusicGame"]);
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;

            
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textures.Add("CardFaceUp", Content.Load<Texture2D>("card_face_up"));
            textures.Add("CardFaceDown", Content.Load<Texture2D>("card_face_down"));
            textures.Add("Table", Content.Load<Texture2D>("table"));
            textures.Add("Fade", Content.Load<Texture2D>("fade"));
            textures.Add("CardType01", Content.Load<Texture2D>("card_type_01"));
            textures.Add("CardType02", Content.Load<Texture2D>("card_type_02"));
            textures.Add("CardType03", Content.Load<Texture2D>("card_type_03"));
            textures.Add("CardType04", Content.Load<Texture2D>("card_type_04"));
            textures.Add("CardType05", Content.Load<Texture2D>("card_type_05"));
            textures.Add("CardType06", Content.Load<Texture2D>("card_type_06"));
            textures.Add("CardType07", Content.Load<Texture2D>("card_type_07"));
            textures.Add("CardType08", Content.Load<Texture2D>("card_type_08"));
            textures.Add("CardType09", Content.Load<Texture2D>("card_type_09"));

            soundeffects.Add("CardFlip", Content.Load<SoundEffect>("card_flip"));
            soundeffects.Add("Horn", Content.Load<SoundEffect>("horn"));
            soundeffects.Add("Cheer", Content.Load<SoundEffect>("cheer"));
            soundeffects.Add("Ching", Content.Load<SoundEffect>("ching"));

            songs.Add("MusicGame", Content.Load<Song>("music_game"));

            fonts.Add("CardFont", Content.Load<SpriteFont>("CardFont"));
            fonts.Add("GameFontTitle", Content.Load<SpriteFont>("GameFontTitle"));
            fonts.Add("GameFontRegular", Content.Load<SpriteFont>("GameFontRegular"));



        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed &&
                statePrevious.LeftButton == ButtonState.Released) {
                gamemanager.checkPress(state.X, state.Y);
            }
            statePrevious = state;

            gamemanager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
            _spriteBatch.Begin();
            gamemanager.Draw(_spriteBatch, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
            _spriteBatch.End();
        }


    }
}