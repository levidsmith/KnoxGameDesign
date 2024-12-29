//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour {

    void Start() {
        
    }

    public bool addCard(Card card) {
        //        cards.Add(card);
        //        return true;
        card.transform.SetParent(transform);
        card.transform.localPosition = Vector2.zero;

        return true;

    }

    public void unselectAllCards() {
        Card[] cards = transform.GetComponentsInChildren<Card>();

        foreach (Card card in cards) {
            card.setSelected(false);
        }

    }

    public Card getTopCard() {
        Card card = null;

        Card[] cards = transform.GetComponentsInChildren<Card>();
        if (cards.Length > 0) {
            card = cards[cards.Length - 1];
        }


        return card;
    }


    public void setCardPositions() {
        float fPosZ = -0.1f;

        Card[] cards = transform.GetComponentsInChildren<Card>();
        foreach (Card card in cards) {
            card.transform.localPosition = new Vector3(0f, 0f, fPosZ);
            fPosZ -= 0.1f;
        }

    }

    public bool isCompleted() {
        Card[] cards = transform.GetComponentsInChildren<Card>();

        //check option 1 (full check)
        int iCardsToCheck = 3;
        if (cards.Length < iCardsToCheck) {
            return false;
        }
        int i;
        for (i = 0; i < iCardsToCheck; i++) {
            Debug.Log("Checking: " + cards[i].iValue + " = " + (i + 1));
            if (cards[i].iValue != i + 1) {
                return false;
            }
        }
        return true;

        /*
        //check option 2 (check top card value is 13)
        if (cards[cards.Length - 1].iValue == 13) {
            return true;
        }

        //check option 3 (card count is 13)
        if (cards.Length == 13) {
            return true;
        }
        */


    }



}
