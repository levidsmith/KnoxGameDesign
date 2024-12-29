//2024 Levi D. Smith
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    public Deck deck;
    public List<Pile> piles;
    public List<Foundation> foundations;


    public GameObject PilePrefab;
    public GameObject FoundationPrefab;
    public GameObject canvas;

    void Start() {
        setup();
 
    }

    private void setup() {
        int i, j;
        int iPosX = -600;
        int iPosY = 1000;
        int iSpacingX = 200;


        //create Deck stock
        deck.createCards();
        deck.shuffleCards();
        
        

        //create Piles
        piles = new List<Pile>();

        iPosX = -600;
        iPosY = 100; 
        iSpacingX = 200;
        for (i = 0; i < 7; i++) {
            Pile pile = Instantiate(PilePrefab, Vector3.zero, Quaternion.identity).GetComponent<Pile>();
            //pile.cards = new List<Card>();
            piles.Add(pile);
            pile.transform.SetParent(canvas.transform);

            for (j = 0; j < i + 1; j++) {
                pile.addCard(deck.stock.GetComponentsInChildren<Card>()[0], false);
            }

            pile.transform.localPosition = new Vector2(iPosX, iPosY);
            pile.setCardPositions();
            iPosX += iSpacingX;

        }

        //create Foundations
        foundations = new List<Foundation>();
        iPosX = 0;
        iPosY = 320;
        iSpacingX = 200;
        for (i = 0; i < 4; i++) {
            Foundation foundation = Instantiate(FoundationPrefab, Vector3.zero, Quaternion.identity).GetComponent<Foundation>();
            foundation.cards = new List<Card>();
            foundations.Add(foundation);
            foundation.transform.SetParent(canvas.transform);
            foundation.transform.localPosition = new Vector2(iPosX, iPosY);
            iPosX += iSpacingX;
        }



    }

    private void Update() {

        if (Input.GetMouseButtonDown(0)) {


            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
            if (hit) {
                Debug.Log("hit: " + hit.collider.name);
                Card card = hit.collider.GetComponent<Card>();
                if (card != null) {
                    selectCard(card);

                }
            }

        }


    }

    public List<Card> getSelectedStack() {
        //Card cardSelected = null;
        List<Card> selectedStack = new List<Card>();

        foreach (Card card in deck.stock.GetComponentsInChildren<Card>()) {
            if (card.isSelected) {
                selectedStack.Add(card);
            }
        }

        bool selectRemaining = false;
        foreach (Pile pile in piles) {
            selectRemaining = false;
            foreach (Card card in pile.transform.GetComponentsInChildren<Card>()) {
                if (card.isSelected) {
                    selectedStack.Add(card);
                    selectRemaining = true;
                }

                if (selectRemaining) {
                    selectedStack.Add(card);
                }
            }
        }


        return selectedStack;
    }

    private void unselectAllCards() {
        deck.unselectAllCards();

        foreach (Pile pile in piles) {
            pile.unselectAllCards();
        }

    }

    public void selectCard(Card cardSelected) {
        Card previousSelected = null;
        List<Card> previousSelectedStack = null;

        if (getSelectedStack().Count > 0) {
            previousSelected = getSelectedStack()[0];
            previousSelectedStack = getSelectedStack();

        }
        
        Debug.Log(cardSelected.getDisplayValue());
        unselectAllCards();
        cardSelected.setSelected(true);

        foreach (Pile pile in piles) {
            if (pile.getTopCard() == cardSelected) {
                Debug.Log("*** IS TOP CARD " + cardSelected.getDisplayValue());
                if (previousSelected != null) {
                    Debug.Log("*** previousSelected " + previousSelected.getDisplayValue());
                    if (previousSelected.iValue == cardSelected.iValue - 1) {
                        Debug.Log("*** MOVING " + previousSelected.getDisplayValue() + " to " + cardSelected.getDisplayValue());
                        foreach (Card card in previousSelectedStack) {
                            pile.addCard(card, false);
                        }
                        pile.setCardPositions();
                    }
                }

            }
        }



    }
}
