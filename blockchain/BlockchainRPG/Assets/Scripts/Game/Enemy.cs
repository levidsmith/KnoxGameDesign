//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public string strName;
    public int iHealth;
    public int iMaxHealth;
    public int iAttack;
    public int iDefense;
    public int iExp;
    public int iGold;
    public float fTurnDelay;
    public float fMaxTurnDelay;

    public Text textName;
    public Text textHealth;
    public Text textTurnDelay;

    public List<Image> images;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        textName.text = strName;
        textHealth.text = iHealth + " / " + iMaxHealth;
        textTurnDelay.text = string.Format("{0: 0.0}", fTurnDelay);

        fTurnDelay -= Time.deltaTime;
        if (fTurnDelay <= 0f) {
            doAttack();
        }
        
    }

    private void doAttack() {
        Player player;
        player = GameObject.FindObjectOfType<Player>();
        player.iHealth -= iAttack;
        fTurnDelay = fMaxTurnDelay;

        BattleLog battlelog;
        battlelog = GameObject.FindObjectOfType<BattleLog>();
        battlelog.AddLog(strName + " attacks Player for " + iAttack + " damage");

        BattleManager battlemanager = GameObject.FindObjectOfType<BattleManager>();
        battlemanager.checkPlayerDefeated();
    }


}
