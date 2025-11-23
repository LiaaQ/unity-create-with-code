using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    [SerializeField] private float border = 7f;
    [SerializeField] private float speed = 2f;
    private int direction = 1;
    private Rigidbody rb;
    private ScoreManager scoreManager;

    private bool hasLanded = false;
    public bool hasSettled = false;
    private bool isFalling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFalling) MoveCube();

        if(Input.GetKeyDown(KeyCode.Space) && !isFalling)
        {
            isFalling = true;
            rb.useGravity = true;
        }
    }

    void MoveCube()
    {
        float move = speed * direction * Time.deltaTime;
        transform.position += new Vector3(move, 0, 0);


        if (transform.position.x > border) direction = -1;
        else if (transform.position.x < -border) direction = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isFalling) return;

        if (collision.collider.CompareTag("Ground")) scoreManager.GameOver();

        if (!hasLanded) StartCoroutine(TestSettled());
    }

    IEnumerator TestSettled()
    {
        hasLanded = true;

        while (rb.angularVelocity.magnitude > 0.05f)
        {
            Debug.Log(rb.angularVelocity.magnitude);
            yield return null;
        }
            

        Debug.Log(rb.angularVelocity.magnitude);

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            if (hit.collider.CompareTag("Cube"))
                scoreManager.AddScore();
        }

        hasSettled = true;
    }
}
