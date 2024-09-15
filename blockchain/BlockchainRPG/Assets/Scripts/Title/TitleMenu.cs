//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void doNewGame() {
        SceneManager.LoadScene("overworld");

    }

    public void doQuit() {
        Application.Quit();
    }
}
