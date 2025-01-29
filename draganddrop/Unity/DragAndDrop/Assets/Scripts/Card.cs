//2025 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public int w, h;

    public bool isDragged;
    public Vector2 dragOffset;

    void Start() {
        w = (int)((RectTransform)transform).sizeDelta.x;
        h = (int)((RectTransform)transform).sizeDelta.y;
        isDragged = false;

    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            pointerPressed(Input.mousePosition);
        } else if (Input.GetMouseButton(0)) {
            pointerDown(Input.mousePosition);
        } else if (!Input.GetMouseButton(0)) {
            pointerUp(Input.mousePosition);
        }
    }
    public void pointerPressed(Vector2 pointerPosition) {
        checkPressed(Input.mousePosition);
    }
    public void pointerUp(Vector2 pointerPosition) {
        stopDrag();
    }
    public void pointerDown(Vector2 pointerPosition) {
        if (isDragged) {
            transform.position = Input.mousePosition + (Vector3)dragOffset;
        }
    }
    private void checkPressed(Vector3 pointerPosition) {
        Debug.Log("Pointer Pressed: " + pointerPosition);
        Debug.Log("Card Position: " + transform.position);
        if (pointerPosition.x >= transform.position.x &&
            pointerPosition.x < transform.position.x + w &&
            pointerPosition.y >= transform.position.y &&
            pointerPosition.y < transform.position.y + h) {
            startDrag((Vector2)transform.position - (Vector2)pointerPosition);
        }
    }
    private void startDrag(Vector2 offset) {
        isDragged = true;
        dragOffset = offset;
    }
    private void stopDrag() {
        isDragged = false;
    }
}
