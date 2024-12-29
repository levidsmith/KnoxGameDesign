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

    void Start() {
        setup();
 
    }

    private void setup() {
        piles = new List<Pile>();
        foundations = new List<Foundation>();

        int i;
        for (i = 0; i < 7; i++) {
            Pile pile = Instantiate(PilePrefab, Vector3.zero, Quaternion.identity).GetComponent<Pile>();
            piles.Add(pile);
        }

        for (i = 0; i < 4; i++) {
            Foundation foundation = Instantiate(FoundationPrefab, Vector3.zero, Quaternion.identity).GetComponent<Foundation>();
            foundations.Add(foundation);
        }

    }

    private void Update() {

        if (Input.GetMouseButtonDown(0)) {

            if (getSelectedCard() != null) {
                Debug.Log("Current selected: " + getSelectedCard().getDisplayValue());
            }

            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
            if (hit) {
                Debug.Log("hit: " + hit.collider.name);
                Card card = hit.collider.GetComponent<Card>();
                if (card != null) {
                    Debug.Log(card.getDisplayValue());
                    deck.unselectAllCards();
                    card.setSelected(true);

                }
            }

        }


    }

    public Card getSelectedCard() {
        Card cardSelected = null;

        foreach (Card card in deck.cards) {
            if (card.isSelected) {
                cardSelected = card;
            }
        }

        return cardSelected;
    }
}
