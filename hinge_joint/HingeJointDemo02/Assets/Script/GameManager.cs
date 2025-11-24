using UnityEngine;

public class GameManager : MonoBehaviour {

    public Flipper leftFlipper;
    public Flipper rightFlipper;

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        handleInput();
    }

    private void handleInput() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            leftFlipper.isPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            leftFlipper.isPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rightFlipper.isPressed = true;

        }

        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            rightFlipper.isPressed = false;
        }

    }
}
