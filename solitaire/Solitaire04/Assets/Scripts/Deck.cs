//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public List<Card> cards;
    public GameObject CardPrefab;

    void Start() {
        setup();
        shuffleCards();

        updateCardPositions();
        
    }

    private void setup() {
        cards = new List<Card>();

        int i, j;

        for (i = 0; i < 13; i++) {
            for (j = 0; j < 4; j++) {
                Card card = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity).GetComponent<Card>();
                card.setCardValue((Card.Suit)j, i);
                card.transform.SetParent(transform);
                cards.Add(card);
            }
        }
    }

    public void shuffleCards() {
        List<Card> shuffledCards = new List<Card>();

        while (cards.Count > 0) {
            int iRand = Random.Range(0, cards.Count);
            shuffledCards.Add(cards[iRand]);
            cards.RemoveAt(iRand);
        }

        cards = shuffledCards;

    }

    private void updateCardPositions() {
        int iPosX, iPosY;

        iPosX = -1800;
        iPosY = -200;
        foreach (Card card in cards) {
            card.transform.localPosition = new Vector2(iPosX, iPosY);
            iPosX += 64;

            if (iPosX > -32) {
                iPosX = -1800;
                iPosY -= 350;
            }

            card.transform.SetAsLastSibling();

        }
    }


    private void printDeck() {
        foreach (Card card in cards) {
            card.printCard();
        }
    }
}
