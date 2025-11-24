using UnityEngine;

public class Flipper : MonoBehaviour {

    public float restPosition = 0f;
    public float pressedPosition;
    public bool isPressed;
    float flipperStrength = 100f;
    float flipperDamper = 1f;

    public HingeJoint hingejoint;
    JointSpring spring;
    JointLimits jl;

    void Start() {
        isPressed = false;
        spring = new JointSpring();


        jl = new JointLimits();

        jl.min = restPosition;
        jl.max = pressedPosition;
        hingejoint.limits = jl;

    }

    void Update() {

        spring.spring = flipperStrength;
        spring.damper = flipperDamper;

        if (isPressed) {
            spring.targetPosition = pressedPosition;
        } else {
            spring.targetPosition = restPosition;
        }

        hingejoint.spring = spring;
    }
}
