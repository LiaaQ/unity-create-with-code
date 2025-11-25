using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    public float border = 5f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Color[] colorPalette;

    private int direction;
    private Rigidbody rb;
    private ScoreManager scoreManager;

    private bool hasLanded = false;
    public bool hasSettled = false;
    private bool isFalling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreManager = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
        ApplyRandomColor();
        direction = Random.value < 0.5f ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreManager.isGameOver) return;
        if (!isFalling) MoveCube();

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isFalling)
        {
            isFalling = true;
            rb.useGravity = true;
            rb.isKinematic = false;
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

    private void ApplyRandomColor()
    {
        var rend = GetComponent<Renderer>();
        if (rend == null)
            return;

        // Choose color
        Color chosen;
        if (colorPalette != null && colorPalette.Length > 0)
            chosen = colorPalette[Random.Range(0, colorPalette.Length)];
        else
            chosen = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.6f, 1f);

        // Determine shader color property
        Material mat = rend.sharedMaterial;
        string colorProp = null;
        if (mat != null)
        {
            if (mat.HasProperty("_BaseColor")) colorProp = "_BaseColor";
            else if (mat.HasProperty("_Color")) colorProp = "_Color";
            else if (mat.HasProperty("_TintColor")) colorProp = "_TintColor";
            else if (mat.HasProperty("_MainTex")) colorProp = "_MainTex"; // not a color, kept for completeness
        }

        // Apply via MaterialPropertyBlock
        if (!string.IsNullOrEmpty(colorProp) && colorProp != "_MainTex")
        {
            var mpb = new MaterialPropertyBlock();
            rend.GetPropertyBlock(mpb);
            mpb.SetColor(colorProp, chosen);
            rend.SetPropertyBlock(mpb);
            return;
        }

        // Fallback: create an instance and set .color (less efficient)
        if (rend.material != null)
        {
            if (rend.material.HasProperty("_Color"))
                rend.material.color = chosen;
            else if (rend.material.HasProperty("_BaseColor"))
                rend.material.SetColor("_BaseColor", chosen);
            else if (rend.material.HasProperty("_TintColor"))
                rend.material.SetColor("_TintColor", chosen);
            else
                Debug.LogWarning($"No known color property found on material of {name}. Cannot tint.");
        }
    }
}
