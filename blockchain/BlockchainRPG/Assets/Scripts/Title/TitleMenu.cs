//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {
    public Text TextWalletID;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void doNewGame() {
        Wallet.strWalletID = TextWalletID.text;
        SceneManager.LoadScene("overworld");

    }

    public void doQuit() {
        Application.Quit();
    }
}
