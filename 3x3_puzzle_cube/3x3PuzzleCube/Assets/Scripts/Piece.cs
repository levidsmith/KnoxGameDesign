//2025 Levi D. Smith
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public GameObject model;

    public GameObject sideU;
    public GameObject sideD;
    public GameObject sideL;
    public GameObject sideR;
    public GameObject sideF;
    public GameObject sideB;

    public List<Material> materials;

    public int iValueU;
    public int iValueD;
    public int iValueL;
    public int iValueR;
    public int iValueF;
    public int iValueB;


    void Start() {
        iValueU = -1;
        iValueD = -1;
        iValueL = -1;
        iValueR = -1;
        iValueF = -1;
        iValueB = -1;

    }

    void Update() {
        
    }


}
