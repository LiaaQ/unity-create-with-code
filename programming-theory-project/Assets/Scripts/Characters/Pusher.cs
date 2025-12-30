using UnityEngine;

public class Pusher : Character
{
    [Header("Push Assist")]
    [SerializeField] private float rayDistance = 1.2f;
    [SerializeField] private float lightMass = 0.5f;

    private Rigidbody2D currentTarget;
    private float originalMass;

    protected override void UseAbility()
    {
        TryAssistPush();
    }

    private void TryAssistPush()
    {
        Vector2 direction = Input.GetAxis("Horizontal") >= 0 ? Vector2.right : Vector2.left;
        Vector2 origin = rb.position;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction,
            rayDistance,
            LayerMask.GetMask("Pushable")
        );

        if (hit.collider == null)
        {
            ResetTarget();
            return;
        }

        Rigidbody2D body = hit.collider.attachedRigidbody;
        if (body == null)
        {
            ResetTarget();
            return;
        }

        if (currentTarget != body)
        {
            ResetTarget();
            currentTarget = body;
            originalMass = body.mass;
            body.mass = lightMass;
        }
    }

    private void ResetTarget()
    {
        if (currentTarget != null)
        {
            currentTarget.mass = originalMass;
            currentTarget = null;
        }
    }

    public override void CancelAbility()
    {
        base.CancelAbility();
        ResetTarget();
    }
}
