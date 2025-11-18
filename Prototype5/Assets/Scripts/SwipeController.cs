using UnityEngine;
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class SwipeController : MonoBehaviour
{
    private Camera cam;
    public Vector3 mousePos;
    private GameManager gameManager;
    private TrailRenderer trail;
    private BoxCollider col;
    private bool swiping = false;

    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.lives > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping) UpdateMousePos();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroySelf();
        }
    }

    void UpdateMousePos()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }
}
