//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public int iHealth;
    public int iMaxHealth;
    public int iMagic;
    public int iMaxMagic;
    public int iStrength;

    public float fTurnDelay;
    public float fMaxTurnDelay;

    // Start is called before the first frame update
    void Start() {
        iMaxHealth = 20;
        iMaxMagic = 8;

        iHealth = iMaxHealth;
        iMagic = iMaxMagic;

        iStrength = 1;

        fMaxTurnDelay = 5f;
        fTurnDelay = fMaxTurnDelay;

        DontDestroyOnLoad(this.gameObject);
        
    }

    // Update is called once per frame
    void Update() {
        fTurnDelay -= Time.deltaTime;
        if (fTurnDelay < 0f) {
            fTurnDelay = 0f;
        }

    }
}
