//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public enum Suit {
        spade,
        club,
        diamond,
        heart
    };

    public Suit suit;
    public int iValue;
    public bool isFaceUp;
    public bool isSelected;

    public void printCard() {
        Debug.Log("Suit: " + suit + " Value: " + iValue);
    }
}
