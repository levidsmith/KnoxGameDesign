//2022 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReader : MonoBehaviour {

    public TextAsset textData;
    public GameObject objWall;

    void Start() {
        parseData();
        
    }

    void Update() {
        
    }

    private void parseData() {
        string strData = textData.text;
        int iRow;
        int iCol;

        iRow = 0;
        string[] strRowData = strData.Split('\n');

        foreach(string strLine in strRowData) {
            iCol = 0;
            foreach (char c in strLine) {
                if (c == '#') {
                    Vector3 pos = new Vector3(iCol, 0f, strRowData.Length - iRow);
                    Instantiate(objWall, pos, Quaternion.identity);
                }
                iCol++;
            }
            iRow++;
        }
    }
}