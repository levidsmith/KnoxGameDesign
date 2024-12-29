//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

//    public List<Card> cards;
    public GameObject CardPrefab;
    public GameObject stock;
    public GameObject waste;
    public GameObject shuffle;

    void Start() {
        
    }

    public void createCards() {
        int i, j;

        for (i = 1; i < 14; i++) {
            for (j = 0; j < 4; j++) {
                Card card = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity).GetComponent<Card>();
                card.setCardValue((Card.Suit)j, i);
                card.transform.SetParent(transform);
            }
        }

        shuffleCards();
    }

    public void shuffleCards() {
        Card[] cards = GetComponentsInChildren<Card>();
  
        foreach (Card card in cards) {
            int iRand = Random.Range(0, cards.Length);
            //card.transform.SetParent(shuffle.transform);
            card.transform.SetParent(stock.transform);
            card.transform.SetSiblingIndex(iRand);
        }

    }

    public void unselectAllCards() {
        foreach (Card card in stock.GetComponentsInChildren<Card>()) {
            card.setSelected(false);
        }

    }

    private void printDeck() {
        foreach (Card card in stock.GetComponentsInChildren<Card>()) {
            card.printCard();
        }
        foreach (Card card in waste.GetComponentsInChildren<Card>()) {
            card.printCard();
        }
    }

    
}
