using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        float fSpeed = 2f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * fSpeed;
        transform.Translate(0f, 0f, z, Space.Self);

        float rot = Input.GetAxis("Horizontal") * Time.deltaTime * 90f;
        transform.Rotate(Vector3.up, rot, Space.World);
        
    }
}
