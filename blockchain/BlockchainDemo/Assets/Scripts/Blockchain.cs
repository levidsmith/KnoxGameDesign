//2024 Levi D. Smith
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Blockchain : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        displayBlockchain();
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void displayBlockchain() {
        string strURL = "http://192.168.1.18/blockchain/blockchain03.php";
        StartCoroutine(getBlockchain(strURL));
    }

    IEnumerator getBlockchain(string strURL) {

        UnityWebRequest www = UnityWebRequest.Get(strURL);
        yield return www.SendWebRequest();

        byte[] results = www.downloadHandler.data;

        string strBlockchain = www.downloadHandler.text;
        Debug.Log(strBlockchain);

    }
}
