//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text textValue;

    public void printCard() {
        Debug.Log("Suit: " + suit + " Value: " + iValue);
    }

    public void setCardValue(Suit in_suit, int in_iValue) {
        suit = in_suit;
        iValue = in_iValue;

        string strDisplayValue = "";
        switch(iValue) {
            case 0:
                strDisplayValue = "A";
                break;
            case 10:
                strDisplayValue = "J";
                break;
            case 11:
                strDisplayValue = "Q";
                break;
            case 12:
                strDisplayValue = "K";
                break;
            default:
                strDisplayValue = string.Format("{0}", iValue + 1);
                break;

        }

        string strDisplaySuit = "";
        switch(suit) {
            case Suit.spade:
                strDisplaySuit = "S";
                textValue.color = Color.black;
                break;
            case Suit.club:
                strDisplaySuit = "C";
                textValue.color = Color.black;
                break;
            case Suit.diamond:
                strDisplaySuit = "D";
                textValue.color = Color.red;
                break;
            case Suit.heart:
                strDisplaySuit = "H";
                textValue.color = Color.red;
                break;
        }

        textValue.text = string.Format("{0}{1}", strDisplayValue, strDisplaySuit);

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
