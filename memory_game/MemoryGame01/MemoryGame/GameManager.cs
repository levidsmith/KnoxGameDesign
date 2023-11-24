using Microsoft.Xna.Framework;
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

        }

        private List<Card> createCards() {
            List<Card> cards = new List<Card>();
            int i;
            for (i = 0; i < MAX_CARDS; i++) {
                Card card = new Card();
                card.setPosition((i % CARDS_PER_ROW) * (Card.CARD_WIDTH + CARD_SPACING), 
                                 (i / CARDS_PER_ROW) * (Card.CARD_HEIGHT + CARD_SPACING)
                                 );
                cards.Add(card);
            }

            return cards;
        }

        public void Draw(SpriteBatch spritebatch) {
            foreach (Card card in listAllCards) {
                card.Draw(spritebatch);
            }
        }


    }

}
