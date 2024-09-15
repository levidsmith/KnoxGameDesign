//2024 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour {
    public GameObject PlayerPrefab;


    // Start is called before the first frame update
    void Start() {

        if (GameObject.FindObjectOfType<Player>() == null) {
            Player player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        }


    }

    // Update is called once per frame
    void Update() {
        
    }
}
