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

    public enum CardColor {
        red,
        black
    };

    public Suit suit;
    public int iValue;
    public bool isFaceUp;
    public bool isSelected;

    public void printCard() {
        Debug.Log("Suit: " + suit + " Value: " + iValue);
    }

    public static CardColor getCardColor(Suit suit) {
        switch(suit) {
            case Suit.spade:
                return CardColor.black;
            case Suit.club:
                return CardColor.black;
            case Suit.diamond:
                return CardColor.red;
            case Suit.heart:
                return CardColor.red;
            default:
                return CardColor.black;
        }
    }
}
