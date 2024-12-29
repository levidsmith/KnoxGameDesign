//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Stock : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
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
        int iPosX, iPosY;
        float fPosZ;

        iPosX = 0;
        iPosY = 0;
        fPosZ = 0f;
        Card[] cards = transform.GetComponentsInChildren<Card>();
        foreach (Card card in cards) {
            card.transform.localPosition = new Vector3(iPosX, iPosY, fPosZ);
            iPosX += 8;
            fPosZ -= 0.1f;
            card.transform.SetAsLastSibling();

        }
    }

    public void unselectAllCards() {
        Card[] cards = transform.GetComponentsInChildren<Card>();

        foreach (Card card in cards) {
            card.setSelected(false);
        }

    }

    public void showTopCard() {
        Card[] stockCards = GetComponentsInChildren<Card>();
        if (stockCards.Length > 0) {
            stockCards[stockCards.Length - 1].setFaceUp(true);
        }
    }



}
