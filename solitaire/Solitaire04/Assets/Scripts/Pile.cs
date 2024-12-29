//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour {
    List<Card> cards;

    void Start() {
        cards = new List<Card>();
        
    }
    
    public bool addCard(Card card, bool checkAllowed) {

        if (checkAllowed) {
            Card topCard = cards[cards.Count - 1];
            if (card.iValue == topCard.iValue - 1 &&
                Card.getCardColor(card.suit) != Card.getCardColor(topCard.suit)
                ) {
                cards.Add(card);
                return true;
            }

        } else {
            cards.Add(card);
            return true;
        }

        return false;
    }

}
