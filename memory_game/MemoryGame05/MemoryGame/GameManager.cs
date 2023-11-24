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
        const int MAX_CARDS = 18;
        const int CARDS_PER_ROW = 6;
        const int CARD_SPACING = 16;

        private List<Card> cardsSelected;
        private List<Card> cardsMatched;

        float fResetDelay = 0f;


        public GameManager() {
            listAllCards = createCards();
            shuffleCards();
            setCardPositions();
            cardsSelected = new List<Card>();
            cardsMatched = new List<Card>();

        }

        private List<Card> createCards() {
            int id;
            int idCount;
            int iMaxSharedIDs = 2;

            List<Card> cards = new List<Card>();
            int i;
            id = 0;
            idCount = 0;
            for (i = 0; i < MAX_CARDS; i++) {
                Card card = new Card();
                card.setID(id);
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

        private void setCardPositions() {
            int i;

            i = 0;
            foreach (Card card in listAllCards) {
                card.setPosition((i % CARDS_PER_ROW) * (Card.CARD_WIDTH + CARD_SPACING),
                                 (i / CARDS_PER_ROW) * (Card.CARD_HEIGHT + CARD_SPACING)
                                 );
                i++;
            }


        }

        public void Draw(SpriteBatch spritebatch) {
            foreach (Card card in listAllCards) {
                card.Draw(spritebatch);
            }
        }

        public void checkPress(int in_x, int in_y) {

            if (fResetDelay <= 0f) {
                foreach (Card card in listAllCards) {
                    if (card.checkPress(in_x, in_y)) {
                        cardsSelected.Add(card);
                    }
                }

                if (cardsSelected.Count == 2) {
                    checkMatch();
                    
                }
            }

        }

        private void checkMatch() {
            if (cardsSelected[0].iTypeID == cardsSelected[1].iTypeID) {
                cardsMatched.Add(cardsSelected[0]);
                cardsMatched.Add(cardsSelected[1]);
                cardsSelected.Clear();
            } else {
                fResetDelay = 2f;
                
            }
            
        }

        public void Update(GameTime gameTime) {
            if (fResetDelay > 0f) {
                fResetDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (fResetDelay <= 0f) {
                    cardsSelected[0].setFaceDown();
                    cardsSelected[1].setFaceDown();
                    cardsSelected.Clear();
                }
            }
        }





    }

}
