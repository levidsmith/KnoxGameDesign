//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EquipMenu : MonoBehaviour {
    public Text TextWalletID;
    public Text TextEquipment;

    public Text TextEquipIdentifier;
    public Text TextEquipContract;

    public Text TextEquipResult;


    public NetworkManager networkmanager;
    // Start is called before the first frame update
    void Start() {
        TextWalletID.text = Wallet.strWalletID;
        //        TextEquipment.text = networkmanager.getEquipment();

        
    }

    // Update is called once per frame
    void Update() {
        TextEquipment.text = "My equipment";
        TextEquipment.text = networkmanager.strEquipment;

        TextEquipResult.text = networkmanager.strEquipResult;


    }

    public void doReturn() {
        SceneManager.LoadScene("overworld");
    }

    public void doEquip() {
        int iIdentifier;
        string strContract;

        iIdentifier = int.Parse(TextEquipIdentifier.text);
        strContract = TextEquipContract.text;
        networkmanager.doEquip(iIdentifier, strContract);
    }
}
