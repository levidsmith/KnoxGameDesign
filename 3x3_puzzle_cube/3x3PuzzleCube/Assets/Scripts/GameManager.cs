using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject PiecePrefab;
    List<Piece> listRotatePieces;

    void Start() {
        listRotatePieces = new List<Piece>();
        createPieces();

    }

    void Update() {
       rotateLayerF();

    }

    public void createPieces() {
        int i, j;
        Vector3 pos;
        Piece p;


        for (i = -1; i <= 1; i++) {
            for (j = -1; j <= 1; j++) {
                pos = new Vector3(i, j, -1);
                p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
                listRotatePieces.Add(p);
                pos = new Vector3(i, j, 1);
                p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
            }
        }

        for (i = -1; i <= 1; i++) {
            pos = new Vector3(-1, i, 0);
            p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
            pos = new Vector3(1, i, 0);
            p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
        }

        pos = new Vector3(0, -1, 0);
        Instantiate(PiecePrefab, pos, Quaternion.identity);
        pos = new Vector3(0, 1, 0);
        Instantiate(PiecePrefab, pos, Quaternion.identity);

    }

    public void rotateLayerF() {

        foreach(Piece p in listRotatePieces) {
            p.transform.RotateAround(new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), 45f * Time.deltaTime);
        }

    }
}
