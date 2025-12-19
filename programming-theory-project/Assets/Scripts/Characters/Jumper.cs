using UnityEngine;

public class Jumper : Character
{
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private int maxJumps = 2;

    private int jumpsRemaining;

    protected override void Awake()
    {
        base.Awake();
        jumpsRemaining = maxJumps;
    }

    protected override void Update()
    {
        base.Update();

        if (IsGrounded())
        {
            jumpsRemaining = maxJumps;
        }
    }

    public override void UseAbility()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpsRemaining--;
    }

    private bool IsGrounded()
    {
        // Simple & reliable for a prototype
        return Mathf.Abs(rb.linearVelocity.y) < 0.01f;
    }
}