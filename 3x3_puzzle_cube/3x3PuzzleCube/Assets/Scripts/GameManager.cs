//2025 Levi D. Smith

using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject PiecePrefab;
    List<Piece> listRotatePieces;

    Vector3? vectRotationPoint = null;
    float fRotateDegrees;
    const float ROTATE_SPEED = 360f;

    void Start() {
        listRotatePieces = new List<Piece>();
        createPieces();

    }

    void Update() {

        getInput();


        if (listRotatePieces.Count > 0) {
            rotateLayer();
        }
    }

    private void getInput() {
        if (listRotatePieces.Count > 0) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            Debug.Log("U pressed");
            listRotatePieces.Clear();
            Piece[] pieces = transform.GetComponentsInChildren<Piece>();
            vectRotationPoint = new Vector3(0f, 1f, 0f);
            fRotateDegrees = 0f;

            foreach (Piece p in pieces) {
                if (Mathf.RoundToInt(p.transform.position.y) == 1) {
                    listRotatePieces.Add(p);

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.Log("D pressed");
            listRotatePieces.Clear();
            Piece[] pieces = transform.GetComponentsInChildren<Piece>();
            foreach (Piece p in pieces) {
                if (Mathf.RoundToInt(p.transform.position.y) == -1) {
                    listRotatePieces.Add(p);
                }
            }

            vectRotationPoint = new Vector3(0f, -1f, 0f);
            fRotateDegrees = 0f;
        }


        if (Input.GetKeyDown(KeyCode.L)) {
            Debug.Log("L pressed");
            listRotatePieces.Clear();
            Piece[] pieces = transform.GetComponentsInChildren<Piece>();
            foreach (Piece p in pieces) {
                if (Mathf.RoundToInt(p.transform.position.x) == -1f) {
                    listRotatePieces.Add(p);

                }
            }
            vectRotationPoint = new Vector3(-1f, 0f, 0f);
            fRotateDegrees = 0f;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("R pressed");
            listRotatePieces.Clear();
            Piece[] pieces = transform.GetComponentsInChildren<Piece>();
            foreach (Piece p in pieces) {
                if (Mathf.RoundToInt(p.transform.position.x) == 1) {
                    listRotatePieces.Add(p);

                }
            }
            vectRotationPoint = new Vector3(1f, 0f, 0f);
            fRotateDegrees = 0f;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            Debug.Log("F pressed");
            listRotatePieces.Clear();
            Piece[] pieces = transform.GetComponentsInChildren<Piece>();
            foreach (Piece p in pieces) {
                if (Mathf.RoundToInt(p.transform.position.z) == -1) {
                    listRotatePieces.Add(p);

                }
            }
            vectRotationPoint = new Vector3(0f, 0f, -1f);
            fRotateDegrees = 0f;
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            Debug.Log("F pressed");
            listRotatePieces.Clear();
            Piece[] pieces = transform.GetComponentsInChildren<Piece>();
            foreach (Piece p in pieces) {
                if (Mathf.RoundToInt(p.transform.position.z) == 1f) {
                    listRotatePieces.Add(p);

                }
            }
            vectRotationPoint = new Vector3(0f, 0f, 1f);
            fRotateDegrees = 0f;
        }
    }

    public void createPieces() {
        int i, j;
        Vector3 pos;
        Piece p;

        for (i = -1; i <= 1; i++) {
            for (j = -1; j <= 1; j++) {
                pos = new Vector3(i, j, -1);
                p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
                p.transform.SetParent(this.transform);
                pos = new Vector3(i, j, 1);
                p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
                p.transform.SetParent(this.transform);

            }
        }

        for (i = -1; i <= 1; i++) {
            pos = new Vector3(-1, i, 0);
            p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
            p.transform.SetParent(this.transform);
            pos = new Vector3(1, i, 0);
            p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
            p.transform.SetParent(this.transform);
        }

        pos = new Vector3(0, -1, 0);
        p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
        p.transform.SetParent(this.transform);
        pos = new Vector3(0, 1, 0);
        p = Instantiate(PiecePrefab, pos, Quaternion.identity).GetComponent<Piece>();
        p.transform.SetParent(this.transform);
    }

    public void rotateLayer() {

        if (listRotatePieces.Count > 0) {
            float fRotate = ROTATE_SPEED * Time.deltaTime;
            if (fRotateDegrees + fRotate > 90f) {
                fRotate = 90f - fRotateDegrees;
                fRotateDegrees = 90f;
            } else {
                fRotateDegrees += fRotate;
            }

            foreach (Piece p in listRotatePieces) {
                p.transform.RotateAround((Vector3)vectRotationPoint, (Vector3)vectRotationPoint, fRotate);

            }

            if (fRotateDegrees >= 90f) {
                foreach (Piece p in listRotatePieces) {
                    p.transform.position = new Vector3(Mathf.RoundToInt(p.transform.position.x),
                        Mathf.RoundToInt(p.transform.position.y),
                        Mathf.RoundToInt(p.transform.position.z)
                        );
                }

                listRotatePieces.Clear();
            }
        }
    }
}
