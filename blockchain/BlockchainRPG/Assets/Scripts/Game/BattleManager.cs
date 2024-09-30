//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

    public Player player;
    public List<Enemy> enemies;
    public GameObject EnemyPrefab;
    public GameObject canvas;
    public GameObject panelBattleWin;
    public GameObject panelBattleLose;
    public List<Texture2D> enemyImages;

    public Button buttonAttack;
    public Button buttonMagic;
    public Button buttonItem;
    public Button buttonDefend;

    // Start is called before the first frame update
    void Start() {
        setupEnemies();
        player = GameObject.FindObjectOfType<Player>();
        player.fTurnDelay = player.fMaxTurnDelay;
    }

    // Update is called once per frame
    void Update() {
        if (player == null) {
            player = GameObject.FindObjectOfType<Player>();
        }
        if (player.fTurnDelay <= 0f) {
            disablePlayerCommands();
            
        } else {
            enablePlayerCommands();
        }
        
    }

    public void setupEnemies() {
        Enemy enemy;

        int iRand;

        iRand = Random.Range(0, 3);


        enemy = Instantiate(EnemyPrefab, Vector3.zero, Quaternion.identity).GetComponent<Enemy>();

        switch (iRand) {
            case 0:
                enemy.strName = "Mosquito";
                enemy.iMaxHealth = 3;
                enemy.iHealth = enemy.iMaxHealth;
                enemy.iAttack = 1;
                enemy.iDefense = 0;
                enemy.iExp = 1;
                enemy.iGold = 1;
                enemy.fMaxTurnDelay = 7;

                break;
            case 1:
                enemy.strName = "Wasp";
                enemy.iMaxHealth = 5;
                enemy.iHealth = enemy.iMaxHealth;
                enemy.iAttack = 2;
                enemy.iDefense = 0;
                enemy.iExp = 1;
                enemy.iGold = 1;
                enemy.fMaxTurnDelay = 7;

                break;

            case 2:
                enemy.strName = "Snake";
                enemy.iMaxHealth = 8;
                enemy.iHealth = enemy.iMaxHealth;
                enemy.iAttack = 5;
                enemy.iDefense = 0;
                enemy.iExp = 5;
                enemy.iGold = 1;
                enemy.fMaxTurnDelay = 7;

                break;
        }

        enemy.images[iRand].gameObject.SetActive(true);
        enemy.fTurnDelay = enemy.fMaxTurnDelay;
        enemy.transform.SetParent(canvas.transform);
        enemy.transform.localPosition = Vector3.zero;
        
        enemies.Add(enemy);


    }

    public void doPlayerAttack() {
        Player player;
        Enemy enemy;
        BattleLog battlelog = GameObject.FindObjectOfType<BattleLog>();

        player = GameObject.FindObjectOfType<Player>();
        enemy = GameObject.FindObjectOfType<Enemy>();
        if (player.fTurnDelay <= 0f) {
            //int iDamage = player.iStrength;
            int iDamage = player.iWeaponAttack;
            enemy.iHealth -= iDamage;
            battlelog.AddLog("Player attacks " + enemy.strName + " with " + player.strWeaponName + " for " + iDamage + " damage");

            player.fTurnDelay = player.fMaxTurnDelay;
            if (enemy.iHealth <= 0) {
                DestroyImmediate(enemy.gameObject);

                if (GameObject.FindObjectsOfType<Enemy>().Length == 0) {
                    battlelog.AddLog("Player has defeated all enemies");
                    battleWin();
                }
            }

        }


    }

    public void doPlayerRun() {
        SceneManager.LoadScene("overworld");


    }

    public void checkPlayerDefeated() {
        Player player;
        player = GameObject.FindObjectOfType<Player>();

        if (player.iHealth <= 0) {
            battleLose();
        }

    }


    public void battleWin() {
        panelBattleWin.SetActive(true);

    }

    public void battleLose() {
        panelBattleLose.SetActive(true);

    }

    public void doBattleWinContinue() {
        SceneManager.LoadScene("overworld");
    }

    public void doBattleLoseContinue() {
        SceneManager.LoadScene("title");
    }

    public void enablePlayerCommands() {
        buttonAttack.interactable = false;
        buttonMagic.interactable = false;
        buttonItem.interactable = false;
        buttonDefend.interactable = false;

    }

    public void disablePlayerCommands() {
        buttonAttack.interactable = true;
        buttonMagic.interactable = true;
        buttonItem.interactable = true;
        buttonDefend.interactable = true;
    }


}


