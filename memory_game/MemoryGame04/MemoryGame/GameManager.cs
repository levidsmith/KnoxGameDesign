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
        public GameManager() {
            listAllCards = createCards();
            shuffleCards();
            setCardPositions();

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
            foreach (Card card in listAllCards) {
                card.checkPress(in_x, in_y);
            }

        }


    }

}
