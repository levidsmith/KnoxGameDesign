//2024 Levi D. Smith
using System;
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
    public Image imgCardFaceUp;
    public Image imgCardFaceDown;

    public void printCard() {
        Debug.Log("Suit: " + suit + " Value: " + iValue);
    }

    public void setCardValue(Suit in_suit, int in_iValue) {
        suit = in_suit;
        iValue = in_iValue;

        textValue.text = getDisplayValue();



    }

    public string getDisplayValue() {

        string strDisplayValue = "";
        switch (iValue) {
            case 1:
                strDisplayValue = "A";
                break;
            case 11:
                strDisplayValue = "J";
                break;
            case 12:
                strDisplayValue = "Q";
                break;
            case 13:
                strDisplayValue = "K";
                break;
            default:
                strDisplayValue = string.Format("{0}", iValue);
                break;

        }

        string strDisplaySuit = "";
        switch (suit) {
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

        return string.Format("{0}{1}", strDisplayValue, strDisplaySuit);

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

    public void setSelected(bool in_isSelected) {
        isSelected = in_isSelected;

        if (isSelected) {
            imgCardFaceUp.color = Color.yellow;
        } else {
            imgCardFaceUp.color = Color.white;
        }

    }

    public void setFaceUp(Boolean b) {
        isFaceUp = b;

        if (isFaceUp) {
            imgCardFaceUp.gameObject.SetActive(true);
            imgCardFaceDown.gameObject.SetActive(false);

        } else {
            imgCardFaceUp.gameObject.SetActive(false);
            imgCardFaceDown.gameObject.SetActive(true);

        }
    }


}
