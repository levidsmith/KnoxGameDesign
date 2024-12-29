//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Pile : MonoBehaviour {
    public Vector2 position;
    

    void Start() {
        
        
    }
    
    public bool addCard(Card card, bool checkAllowed) {
        card.transform.SetParent(transform);
        return true;

    }

    public void setCardPositions() {
        int iPosY = 0;
        float fPosZ = 0f;
        int iSpacing = 32;

        Card[] cards = transform.GetComponentsInChildren<Card>();
        foreach (Card card in cards) {
            card.transform.localPosition = new Vector3(0f, iPosY, fPosZ);
            iPosY -= iSpacing;
            fPosZ -= 0.1f;
        }

    }

    public Card getTopCard() {
        Card[] cards = transform.GetComponentsInChildren<Card>();
        if (cards.Length > 0) {
            return cards[cards.Length - 1];
        } else {
            return null;
        }
    }

    public void unselectAllCards() {
        Card[] cards = transform.GetComponentsInChildren<Card>();

        foreach (Card card in cards) {
            card.setSelected(false);
        }

    }


}
