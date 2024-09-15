//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLog : MonoBehaviour {

    List<string> strBattleLog;
    public Text textLog;

    // Start is called before the first frame update
    void Start() {
        strBattleLog = new List<string>();
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void AddLog(string str) {
        strBattleLog.Add(str);

        textLog.text = "";

        int i;
        int iMaxLines = 5;

        i = 0;
        if (strBattleLog.Count > iMaxLines) {
            i = strBattleLog.Count - iMaxLines;
        }

        while (i < strBattleLog.Count)  {
                textLog.text += strBattleLog[i] + "\n";
            i++;
        }
    }
}
