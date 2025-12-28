using UnityEngine;

public class Phaser : Character
{
    [Header("Phasing")]
    [SerializeField] private float phaseDuration = 2f;
    [SerializeField] private float phaseCooldown = 3f;
    [SerializeField] private float phasedMoveMultiplier = 0.5f;

    private bool isPhasing = false;
    private bool canPhase = true;

    private int normalLayer;
    private int phasingLayer;
    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();

        normalLayer = LayerMask.NameToLayer("Player");
        phasingLayer = LayerMask.NameToLayer("PhasingPlayer");
    }

    public override void UseAbility()
    {
        if (canPhase)
        {
            StartCoroutine(PhaseRoutine());
        }
    }

    protected override void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float speed = isPhasing ? moveSpeed * phasedMoveMultiplier : moveSpeed;

        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);
    }

    private System.Collections.IEnumerator PhaseRoutine()
    {
        canPhase = false;
        isPhasing = true;

        gameObject.layer = phasingLayer;
        SetTransparency(0.4f);

        yield return new WaitForSeconds(phaseDuration);

        gameObject.layer = normalLayer;
        SetTransparency(1f);

        isPhasing = false;

        yield return new WaitForSeconds(phaseCooldown);
        canPhase = true;
    }

    private void SetTransparency(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
