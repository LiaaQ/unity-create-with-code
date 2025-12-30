using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D rb;
    protected bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleMovement();

        if(Input.GetKeyDown(KeyCode.E))
        {
            UseAbility();
        }
    }

    protected virtual void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(h * moveSpeed, rb.linearVelocity.y);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // 🔹 Polymorphic ability
    protected abstract void UseAbility();

    public virtual void CancelAbility()
    {
        StopAllCoroutines();
    }
}