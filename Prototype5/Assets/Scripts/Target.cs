using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;

    private float minSpeed = 9;
    private float maxSpeed = 13;
    private float maxTorque = 2;
    private float xRange = 4;
    private float ySpawnPos = -1;

    public int pointValue;
    public ParticleSystem explosionParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(
            RandomTorque(), 
            RandomTorque(),
            RandomTorque(),
            ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    public void DestroySelf()
    {
        if(gameManager.lives > 0)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            if (pointValue < 0)
            {
                gameManager.UpdateLives(-1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (pointValue > 0 && gameManager.lives > 0) {
            gameManager.UpdateLives(-1);
        }
    }



    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
