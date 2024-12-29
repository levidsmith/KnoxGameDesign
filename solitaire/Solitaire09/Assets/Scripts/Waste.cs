//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void unselectAllCards() {
        Card[] cards = transform.GetComponentsInChildren<Card>();

        foreach (Card card in cards) {
            card.setSelected(false);
        }

    }

}
