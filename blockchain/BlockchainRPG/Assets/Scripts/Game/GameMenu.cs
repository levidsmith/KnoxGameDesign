//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {


    // Start is called before the first frame update



    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void doExplore() {
        SceneManager.LoadScene("battle");
    }

    public void doEquip() {
        SceneManager.LoadScene("equip");

    }

    public void doQuit() {
        SceneManager.LoadScene("title");

    }
}
