using System.Collections;
using UnityEngine;

public class Dasher : Character
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 1f;

    private bool isDashing;
    private bool canDash = true;

    // Update is called once per frame
    protected override void Update()
    {
        if (!isDashing)
        {
            base.Update();
        }

        if(Input.GetKeyDown(KeyCode.E) && canDash)
        {
            UseAbility();
        }
    }

    public override void UseAbility()
    {
        if(!canDash || isDashing) return;

        StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        float direction = Input.GetAxisRaw("Horizontal");
        if (direction == 0) direction = 1;

        rb.linearVelocity = new Vector2(direction * dashSpeed, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    protected override void HandleMovement()
    {
        if (isDashing) return;
        base.HandleMovement();
    }
}
