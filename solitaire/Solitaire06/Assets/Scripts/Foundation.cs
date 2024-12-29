//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour {
    public List<Card> cards;

    void Start() {
        cards = new List<Card>();
        
    }

    public bool addCard(Card card) {
        cards.Add(card);
        return true;

    }

    public void unselectAllCards() {
        foreach (Card card in cards) {
            card.setSelected(false);
        }

    }

}
