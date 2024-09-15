//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Blockchain02 blockchain;
    public Text textMoneyValue;
    public Text textItemsValue;

    // Start is called before the first frame update
    void Start() {
//        Debug.Log(blockchain.getMoney());
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void updateDisplay() {
        textMoneyValue.text = string.Format("Money: {0}", blockchain.getMoney());
        
        textItemsValue.text = "Items:\n";
        List<string> strItems = blockchain.getItems();
        foreach (string strID in strItems) {
            textItemsValue.text += strID + " > " + blockchain.getItem(strID) + "\n";
            //textItemsValue.text += "47b64d8b-01c0-474e-93ac-2be6e59affbc" + " > " + blockchain.getItem("47b64d8b-01c0-474e-93ac-2be6e59affbc") + "\n";
            //            textItemsValue.text += "c3aef170-6c83-47ff-8619-030175b6b09e" + " > " + blockchain.getItem("c3aef170-6c83-47ff-8619-030175b6b09e") + "\n";


        }
    }
}
