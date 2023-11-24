using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame {
    internal class GameManager {
        List<Card> listAllCards;
        public const int MAX_CARDS = 18;
        public const int CARDS_PER_ROW = 6;
        public const int CARD_SPACING = 16;

        private List<Card> cardsSelected;
        private List<Card> cardsMatched;

        float fResetDelay = 0f;
        int iTurns;
        float fGameTime;

        enum GameState { STATE_TITLE, STATE_PLAYING, STATE_WIN };
        GameState state;


        public GameManager() {

            state = GameState.STATE_TITLE;

        }

        public void resetGame() {
            listAllCards = createCards();
            shuffleCards();
            setCardPositions();
            cardsSelected = new List<Card>();
            cardsMatched = new List<Card>();
            iTurns = 0;
            fGameTime = 0f;


        }

        private List<Card> createCards() {
            int i;
            int id;
            int idCount;
            int iMaxSharedIDs = 2;

            List<Color> colors = createRandomColorList(MAX_CARDS / iMaxSharedIDs);

            List<Card> cards = new List<Card>();
            id = 0;
            idCount = 0;
            for (i = 0; i < MAX_CARDS; i++) {
                Card card = new Card();
                card.setID(id);
                card.setColor(colors[id]);
                idCount++;
                if (idCount >= iMaxSharedIDs) {
                    id++;
                    idCount = 0;
                }
                cards.Add(card);
            }

            return cards;
        }

        private void shuffleCards() {
            List<Card> shuffledCards = new List<Card>();
            Random r = new Random();

            while(listAllCards.Count > 0) {
                int iRand = r.Next(0, listAllCards.Count);
                Card card = listAllCards.ElementAt(iRand);
                shuffledCards.Add(card);
                listAllCards.RemoveAt(iRand);
            }

            listAllCards = shuffledCards;

        }

        private List<Color> createRandomColorList(int iSize) {
            int i;

            List<Color> colors = new List<Color>();
            for (i = 0; i < iSize; i++) {
                colors.Add(getHSVColor(360f * ((float)i / 9), 1f, 1f));
            }

            Random r = new Random();

            List<Color> colorsShuffled = new List<Color>();
            while (colors.Count > 0) {
                int iRand = r.Next(0, colors.Count);
                Color c = colors.ElementAt(iRand);
                colorsShuffled.Add(c);
                colors.RemoveAt(iRand);
            }

            return colorsShuffled;


        }

        private void setCardPositions() {
            int i;

            i = 0;
            foreach (Card card in listAllCards) {
                int pos_x = (i % CARDS_PER_ROW) * (Card.CARD_WIDTH + CARD_SPACING) + (GameManager.CARD_SPACING / 2);
                int pos_y = (i / CARDS_PER_ROW) * (Card.CARD_HEIGHT + CARD_SPACING) + (GameManager.CARD_SPACING / 2);
                card.setPosition(pos_x, pos_y);
                i++;
            }


        }

        public void Draw(SpriteBatch spritebatch, Vector2 screenSize) {
            string strText;
            int pos_x;
            int pos_y;
            spritebatch.Draw(Game1.textures["Table"], new Rectangle(0, 0, (int) screenSize.X, (int) screenSize.Y), Color.White);
            //spritebatch.DrawString(Game1.fonts["GameFont"], "Memory Game", new Vector2(50, 50), Color.Cyan);

            switch (state) {
                case GameState.STATE_TITLE:
                    strText = "Memory Game";
                    pos_x = ((int)screenSize.X - (int)Game1.fonts["GameFontTitle"].MeasureString(strText).X) / 2;
                    pos_y = ((int)screenSize.Y - (int)Game1.fonts["GameFontTitle"].MeasureString(strText).Y) / 2;
                    spritebatch.DrawString(Game1.fonts["GameFontTitle"], strText, new Vector2(pos_x - 4, pos_y + 4), Color.Black);
                    spritebatch.DrawString(Game1.fonts["GameFontTitle"], strText, new Vector2(pos_x, pos_y), Color.White);

                    strText = "2023 LD Smith";
                    pos_x = ((int)screenSize.X - (int)Game1.fonts["GameFontRegular"].MeasureString(strText).X) / 2;
                    pos_y = (int)screenSize.Y - 64;
                    spritebatch.DrawString(Game1.fonts["GameFontRegular"], strText, new Vector2(pos_x - 2, pos_y + 2), Color.Black);
                    spritebatch.DrawString(Game1.fonts["GameFontRegular"], strText, new Vector2(pos_x, pos_y), Color.White);


                    break;
                case GameState.STATE_PLAYING:
                    DrawCards(spritebatch);
                    break;
                case GameState.STATE_WIN:
                    DrawCards(spritebatch);
                    spritebatch.Draw(Game1.textures["Fade"], new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y), Color.White);
                    strText = "YOU WIN";
                    pos_x = ((int)screenSize.X - (int)Game1.fonts["GameFontTitle"].MeasureString(strText).X) / 2;
                    pos_y = ((int)screenSize.Y - (int)Game1.fonts["GameFontTitle"].MeasureString(strText).Y) / 2;
                    spritebatch.DrawString(Game1.fonts["GameFontTitle"], strText, new Vector2(pos_x - 4, pos_y + 4), Color.Black);
                    spritebatch.DrawString(Game1.fonts["GameFontTitle"], strText, new Vector2(pos_x, pos_y), Color.White);

                    pos_y += 64;
                    strText = string.Format("Turns - {0}", iTurns);
                    spritebatch.DrawString(Game1.fonts["GameFontRegular"], strText, new Vector2(pos_x - 2, pos_y + 2), Color.Black);
                    spritebatch.DrawString(Game1.fonts["GameFontRegular"], strText, new Vector2(pos_x, pos_y), Color.White);

                    pos_y += 32;
                    strText = string.Format("Time - {0:0.#} seconds", fGameTime / 1000f);
                    spritebatch.DrawString(Game1.fonts["GameFontRegular"], strText, new Vector2(pos_x - 2, pos_y + 2), Color.Black);
                    spritebatch.DrawString(Game1.fonts["GameFontRegular"], strText, new Vector2(pos_x, pos_y), Color.White);


                    break;
            }


            
        }

        private void DrawCards(SpriteBatch spritebatch) {
            foreach (Card card in listAllCards) {
                card.Draw(spritebatch);
            }

        }

        public void checkPress(int in_x, int in_y) {

            switch(state) {
                case GameState.STATE_TITLE:
                    state = GameState.STATE_PLAYING;
                    resetGame();
                    break;
                case GameState.STATE_PLAYING:
                    if (fResetDelay <= 0f) {
                        foreach (Card card in listAllCards) {
                            if (card.checkPress(in_x, in_y)) {
                                cardsSelected.Add(card);
                            }
                        }

                        if (cardsSelected.Count == 2) {
                            iTurns++;
                            checkMatch();

                        }
                    }
                    break;
                case GameState.STATE_WIN:
                    state = GameState.STATE_TITLE;
                    break;
            }


        }

        private void checkMatch() {
            if (cardsSelected[0].iTypeID == cardsSelected[1].iTypeID) {
                cardsMatched.Add(cardsSelected[0]);
                cardsMatched.Add(cardsSelected[1]);
                cardsSelected.Clear();
                Game1.soundeffects["Ching"].Play();
                checkWin();
            } else {
                fResetDelay = 2f;
                
            }
            
        }

        private void checkWin() {
            if (cardsMatched.Count == listAllCards.Count) {
                state = GameState.STATE_WIN;
                Game1.soundeffects["Cheer"].Play();

            }
        }

        public void Update(GameTime gameTime) {

            switch(state) {
                case GameState.STATE_PLAYING:
                    fGameTime += ((float)gameTime.ElapsedGameTime.TotalMilliseconds);
                    
                    break;
            }

            if (fResetDelay > 0f) {
                fResetDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (fResetDelay <= 0f) {
                    cardsSelected[0].setFaceDown();
                    cardsSelected[1].setFaceDown();
                    cardsSelected.Clear();
                }
            }
        }


        public Color getHSVColor(float h, float s, float v) {
            Color c;
            int i;
            float f, p, q, t;
            float r, g, b;

            h /= 60f;
            i = (int) MathF.Floor(h);
            f = h - i;
            p = v * (1 - s);
            q = v * (1 - s * f);
            t = v * (1 - s * (1 - f));
            switch(i) {
                case 0:
                    r = v;
                    g = t;
                    b = p;
                    break;
                case 1:
                    r = q;
                    g = v;
                    b = p;
                    break;
                case 2:
                    r = p;
                    g = v;
                    b = t;
                    break;
                case 3:
                    r = p;
                    g = q;
                    b = v;
                    break;
                case 4:
                    r = t;
                    g = p;
                    b = v;
                    break;
                default:
                    r = v;
                    g = p;
                    b = q;
                    break;

            }


            return new Color(r, g, b);
        }





    }

}
