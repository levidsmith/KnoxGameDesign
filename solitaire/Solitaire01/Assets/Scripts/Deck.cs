//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public List<Card> cards;
    public GameObject CardPrefab;

    void Start() {
        setup();
        
    }

    private void setup() {
        cards = new List<Card>();

        int i, j;

        for (i = 0; i < 13; i++) {
            for (j = 0; j < 4; j++) {
                Card card = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity).GetComponent<Card>();
                card.iValue = i;
                card.suit = (Card.Suit) j;
                card.transform.SetParent(transform);
                cards.Add(card);
            }
        }

        printDeck();
    }

    private void printDeck() {
        foreach (Card card in cards) {
            card.printCard();
        }
    }
}
