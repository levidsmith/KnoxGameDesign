//2024 Levi D. Smith
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour {
    // Start is called before the first frame update

    public string strEquipment;
    public string strEquipResult;

    public string strContract;

    public string strEquipName;
    public string iEquipAttack;

    void Start() {
        pullEquipment();
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void pullEquipment() {
        string strURL = "https://api.opensea.io/api/v2/chain/ethereum/account/" + Wallet.strWalletID + "/nfts";
        StartCoroutine(getEquipmentValues(strURL));
    }

    IEnumerator getEquipmentValues(string strURL) {
        UnityWebRequest www = UnityWebRequest.Get(strURL);
        www.SetRequestHeader("accept", "application/json");
        www.SetRequestHeader("x-api-key", Wallet.API_KEY);
        yield return www.SendWebRequest();

        byte[] results = www.downloadHandler.data;
        Debug.Log(www.downloadHandler.text);
        string strResultJSON = www.downloadHandler.text;

        string strPattern;
        MatchCollection matches;

        strPattern = @"""identifier"": ""(\d+)"",";
        strPattern += @".*?";
        strPattern += @"""contract"": ""([a-zA-Z0-9]+)"",";
        strPattern += @".*?";
        strPattern += @"""name"": ""([a-zA-Z0-9 ]+)"",";
        strPattern += @".*?";
        strPattern += @"""description"": ""(.+?)"",";

        matches = Regex.Matches(strResultJSON, strPattern, RegexOptions.IgnoreCase);

        strEquipment = "Equipment (identifier, name, description, contract):\n";
        int i = 1;
        foreach (Match match in matches) {
            strEquipment += match.Groups[1].Captures[0].Value + ": ";
            strEquipment += match.Groups[3].Captures[0].Value + "\n";
            strEquipment += match.Groups[4].Captures[0].Value + "\n";
            strEquipment += match.Groups[2].Captures[0].Value + "\n";
            strEquipment += "\n";

            strContract = match.Groups[2].Captures[0].Value;

            i++;
        }
    }


    public void doEquip(int identifier, string strContract) {
        string strURL = "https://api.opensea.io/api/v2/chain/ethereum/contract/" + strContract + "/nfts/" + identifier;
        StartCoroutine(getEquipmentTraits(strURL));

    }
    IEnumerator getEquipmentTraits(string strURL) {
        string strSelectedWeaponName = "Nothing";
        int iSelectedWeaponAttack = 0;


        UnityWebRequest www = UnityWebRequest.Get(strURL);
        www.SetRequestHeader("accept", "application/json");
        www.SetRequestHeader("x-api-key", Wallet.API_KEY);
        yield return www.SendWebRequest();

        byte[] results = www.downloadHandler.data;
        Debug.Log(www.downloadHandler.text);
        string strResultJSON = www.downloadHandler.text;

        string strPattern;
        MatchCollection matches;

        //Get Name
        strPattern = @"""name"": ""([a-zA-Z ]+)"",";
        strPattern += @".*?";

        matches = Regex.Matches(strResultJSON, strPattern, RegexOptions.IgnoreCase);

        strEquipResult = "Equipment name:\n";
        foreach (Match match in matches) {
            strSelectedWeaponName = match.Groups[1].Captures[0].Value;
            strEquipResult += match.Groups[1].Captures[0].Value + "\n";
            strEquipResult += "\n";

        }


        //Get Traits
        strPattern = @"""trait_type"": ""([a-zA-Z]+)"",";
        strPattern += @".*?";
        strPattern += @"""value"": ""(\d+)""";
        
        matches = Regex.Matches(strResultJSON, strPattern, RegexOptions.IgnoreCase);

        strEquipResult += "Equip Traits (type, value):\n";
        foreach (Match match in matches) {

            if (match.Groups[1].Captures[0].Value == "ATK") {
                iSelectedWeaponAttack = int.Parse(match.Groups[2].Captures[0].Value);
            }

            strEquipResult += match.Groups[1].Captures[0].Value + "\n";
            strEquipResult += match.Groups[2].Captures[0].Value + "\n";
            strEquipResult += "\n";

        }

        //Get Owners
        strPattern = @"""owners"":";
        strPattern += @".*?";
        strPattern += @"""address"": ""([a-zA-Z0-9]+)"",";
        strPattern += @".*?";
        strPattern += @"""quantity"": (\d+)";

        matches = Regex.Matches(strResultJSON, strPattern, RegexOptions.IgnoreCase);

        strEquipResult += "Owners (address, quantity):\n";
        
        foreach (Match match in matches) {
            strEquipResult += match.Groups[1].Captures[0].Value + "\n";
            strEquipResult += match.Groups[2].Captures[0].Value + "\n";

        }

        Player player = GameObject.FindObjectOfType<Player>();
        player.strWeaponName = strSelectedWeaponName;
        player.iWeaponAttack = iSelectedWeaponAttack;

    }


}
