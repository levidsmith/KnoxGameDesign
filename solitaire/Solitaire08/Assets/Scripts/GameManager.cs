//2024 Levi D. Smith
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
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
        deck.stock.GetComponent<Stock>().setCardPositions();



        //create Foundations
        foundations = new List<Foundation>();
        iPosX = 0;
        iPosY = 320;
        iSpacingX = 200;
        for (i = 0; i < 4; i++) {
            Foundation foundation = Instantiate(FoundationPrefab, Vector3.zero, Quaternion.identity).GetComponent<Foundation>();
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

                Foundation foundation = hit.collider.GetComponent<Foundation>();
                if (foundation != null) {
                    selectFoundation(foundation);
                }

                Waste waste = hit.collider.GetComponent<Waste>();
                if (waste != null) {
                    addStockToWaste();
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
                if (selectRemaining) {
                    selectedStack.Add(card);
                } else if (card.isSelected) {
                    selectedStack.Add(card);
                    selectRemaining = true;
                }

            }
        }


        return selectedStack;
    }

    private void unselectAllCards() {
        deck.unselectAllCards();
        deck.stock.GetComponent<Stock>().unselectAllCards();
        deck.waste.GetComponent<Waste>().unselectAllCards();

        foreach (Pile pile in piles) {
            pile.unselectAllCards();
        }

        foreach (Foundation foundation in foundations) {
            foundation.unselectAllCards();
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

        foreach (Foundation foundation in foundations) {
            Card[] foundationCards = foundation.GetComponentsInChildren<Card>();
            if (foundationCards.Length > 0) {
                Card foundationTopCard = foundation.getTopCard();
                Debug.Log("Foundation Top card: " + foundation.getTopCard().getDisplayValue());
                if (cardSelected == foundationTopCard &&
                    previousSelected != null) {
                    Debug.Log("Previous selected: " + previousSelected.getDisplayValue());
                    if (previousSelected.suit == foundationTopCard.suit &&
                        previousSelected.iValue == foundationTopCard.iValue + 1) {
                        previousSelected.transform.SetParent(foundation.transform);
                        previousSelected.transform.localPosition = Vector2.zero;
                        foundation.setCardPositions();

                    }
                }
            }

        }

        Card[] wasteCards = deck.waste.GetComponentsInChildren<Card>();
        foreach (Card card in wasteCards) {
            if (cardSelected == card) {
                addStockToWaste();
            }
        }

    }

    public void selectFoundation(Foundation foundationSelected) {
        Debug.Log("foundation clicked");

        Debug.Log("selectedStack count: " + getSelectedStack().Count);

        if (getSelectedStack().Count == 1) {
            Card cardSelected = getSelectedStack()[0];

            Card[] foundationCards = foundationSelected.GetComponentsInChildren<Card>();
            if (foundationCards.Length == 0) {
                if (cardSelected.iValue == 1) {
                    cardSelected.transform.SetParent(foundationSelected.transform);
                    cardSelected.transform.localPosition = Vector2.zero;
                }
            } 
        }

    }

    private void addStockToWaste() {
        Stock stock = deck.stock.GetComponent<Stock>();
        Waste waste = deck.waste.GetComponent<Waste>();

        Card stockTopCard = stock.getTopCard();
        if (stockTopCard != null) {
            stockTopCard.transform.SetParent(waste.transform);
            stockTopCard.transform.localPosition = Vector2.zero;
        } else {
            Card[] wasteCards = deck.waste.GetComponentsInChildren<Card>();
            foreach (Card card in wasteCards) {
                card.transform.SetParent(stock.transform);
                
            }
            stock.setCardPositions();
        }

        unselectAllCards();
    }
}
