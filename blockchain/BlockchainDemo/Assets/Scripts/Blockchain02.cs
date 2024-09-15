//2024 Levi D. Smith
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Linq;


public class Blockchain02 : MonoBehaviour {

    public string strBlockchain;

    // Start is called before the first frame update
    void Start() {
        displayBlockchain();

    }

    // Update is called once per frame
    void Update() {

    }

    public void displayBlockchain() {
        string strURL = "http://192.168.1.18/blockchain/data/blockchain.json";
        StartCoroutine(getBlockchain(strURL));
    }

    IEnumerator getBlockchain(string strURL) {

        UnityWebRequest www = UnityWebRequest.Get(strURL);
        yield return www.SendWebRequest();

        byte[] results = www.downloadHandler.data;

        string strBlockchain = www.downloadHandler.text;
        Debug.Log(strBlockchain);
        this.strBlockchain = strBlockchain;

      //  getMoney();
        //getItems();

    }

    public int getMoney() {
        int iMoneyReceived = 0;
        int iMoneySent = 0;

        string strPattern;
        MatchCollection matches;

        Debug.Log("MONEY RECEIVED");

        strPattern = @"""from_id"": ""\w+"",\s*""to_id"": ""ldsmith"",\s*""gold"": (\d+)";

        matches = Regex.Matches(strBlockchain, strPattern, RegexOptions.IgnoreCase);

        foreach (Match match in matches) {
            iMoneyReceived += int.Parse(match.Groups[1].Captures[0].Value);
            Debug.Log("Received: " + match.Groups[1].Captures[0].Value);
        }
        Debug.Log(string.Format("Total money received: {0}", iMoneyReceived));



        Debug.Log("MONEY SENT");

        strPattern = @"""from_id"": ""ldsmith"",\s*""to_id"": "".+"",\s*""gold"": (\d+)";

        matches = Regex.Matches(strBlockchain, strPattern, RegexOptions.IgnoreCase);

        foreach (Match match in matches) {
            iMoneySent += int.Parse(match.Groups[1].Captures[0].Value);
            Debug.Log("Sent: " + match.Groups[1].Captures[0].Value);
        }
        Debug.Log(string.Format("Total money sent: {0}", iMoneySent));

        return iMoneyReceived - iMoneySent;

    }

    public List<string> getItems() {
        List<string> strItems = new List<string>();

        string strPattern;
        MatchCollection matches;
        Debug.Log("PURCHASED ITEMS");

        strPattern = @"""from_id"": ""\w+"",\s*""to_id"": ""ldsmith"",\s*""item_id"": ""(\w+)""";
        strPattern = @"""from_id"": ""\w+"",\s*""to_id"": ""ldsmith"",\s*""item_id"":\s*""([A-Za-z0-9\-]+)""";

        matches = Regex.Matches(strBlockchain, strPattern, RegexOptions.IgnoreCase);

        foreach (Match match in matches) {
            strItems.Add(match.Groups[1].Captures[0].Value);
            Debug.Log("Owned Item: " + match.Groups[1].Captures[0].Value);
        }
        //        Debug.Log(string.Format("Total money received: {0}", iMoneyReceived));
        return strItems;


    }

    public string getItem(string strID) {
        string strData = "";
        Debug.Log("Matching on: " + strID);

        string strPattern;
        MatchCollection matches;

        //strPattern = @"""id"":\s*""([A-Za-z0-9\-]+)"",\s*""name"":\s*""(\w+)"",.*""attack"":\s*(\d+) ";
        //strPattern = @"""id"":\s*""([A-Za-z0-9\-]+)"",\s*""name"": ""(\w+)""";

        //        strPattern = @"""id"":\s*""(";
        //        strPattern += strID.Replace("-", "\\-");
        //        strPattern += @")"",\s*""name"": ""([a-zA-Z ]+)""";

                strPattern = @"""id"":\s*""(";
                strPattern += strID.Replace("-", "\\-");
                strPattern += @")"",\s*""name"": ""([a-zA-Z ]+)"",\s*";
                strPattern += @"""cost"":\s*.*,\s*";
                strPattern += @"""attack"":\s*(.*),\s";


        Debug.Log("Pattern: " + strPattern);

        matches = Regex.Matches(strBlockchain, strPattern, RegexOptions.IgnoreCase);
        
        foreach (Match match in matches) {
            //            strData = "name: " + match.Groups[1].Captures[0].Value;
            Debug.Log("Matched: " + match.Groups[2].Captures[0].Value);
            strData = "name: " + match.Groups[2].Captures[0].Value;
            strData = "attack: " + match.Groups[3].Captures[0].Value;

        }

        return strData;
    }


}

