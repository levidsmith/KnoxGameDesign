//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {

//    public Player player;
    public Text textPlayerHealth;
    public Text textPlayerMagic;
    public Text textPlayerTurnDelay;
    Player player;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if (player == null) {
            player = GameObject.FindObjectOfType<Player>();
        }

        if (player != null) {
            textPlayerHealth.text = player.iHealth + " / " + player.iMaxHealth;
            textPlayerMagic.text = player.iMagic + " / " + player.iMaxMagic;
            textPlayerTurnDelay.text = string.Format("{0:0.0}", player.fTurnDelay);
        }
        
    }
}
