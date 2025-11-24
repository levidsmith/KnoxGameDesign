using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public GameObject ballPrefab;
    float fBallSpawnCountdown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        fBallSpawnCountdown = 0f;
        
    }

    // Update is called once per frame
    void Update() {
        fBallSpawnCountdown -= Time.deltaTime;
        if (fBallSpawnCountdown <= 0f) {
            //float x = Random.Range(-0.25f, 0.25f);
            float x = Random.Range(-0.25f, 0f);
            float y = 0.14f;
            float z = 0.56f;
            Instantiate(ballPrefab, new Vector3(x, y, z), Quaternion.identity);
            fBallSpawnCountdown = 2f;
        }
        
    }
}
