using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : LaneBehaviour
{
    public int force = 20;
    public float fallForce = 0.5f;
    public Vector2 boxSize;
    public float castDistance;

    private BoxCollider2D boxCollider;
    private readonly float coyoteTime = 0.2f;
    private readonly float bufferTime = 0.2f;
    private float coyoteTimeCounter;
    private float bufferTimeCounter;
    private ShootingBehaviour gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<ShootingBehaviour>();
        gun.cooldown = Time.time;
        boxCollider = GetComponent<BoxCollider2D>();
        GameManager.Instance.player.playerRB = GetComponent<Rigidbody2D>();
        gun.projectile = Resources.Load<GameObject>("Projectiles/Fireball");
    }

    // Update is called once per frame
    void Update()
    {
        // Jump
        if (bufferTimeCounter >= 0f && coyoteTimeCounter > 0f) {
            GameManager.Instance.player.playerRB.velocity = Vector2.zero;
            GameManager.Instance.player.playerRB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            bufferTimeCounter = 0f;
        }

        // Makes the player "slide"
        if (Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded()) {
            // Resizes hitbox to be lower
            boxCollider.offset = new Vector2(boxCollider.offset.x, -0.5f);
            boxCollider.size = new Vector2(boxCollider.size.x, 1);
        }

        // Fast fall
        if (Input.GetKey(KeyCode.DownArrow) && !IsGrounded())
        {
            // Increase gravity while player is on air
            GameManager.Instance.player.playerRB.AddForce(Vector2.down * fallForce, ForceMode2D.Impulse);
        }
         

        // Revert back from sliding
        if (!Input.GetKeyDown(KeyCode.DownArrow)) {
            // Resizes hitbox with the normal, default hitbox
            boxCollider.offset = new Vector2(boxCollider.offset.x, 0);
            boxCollider.size = new Vector2(boxCollider.size.x, 2);
        }

        // Switch lanes
        if (Input.GetKeyDown(KeyCode.V) && IsGrounded()) GameManager.Instance.currentLane = SwapLane(
            GameManager.Instance.currentLane,
            GameManager.Instance.player.playerRB, 
            GameManager.Instance.player.playerObject
        );

        // Enable forcefield
        if (Input.GetKeyDown(KeyCode.U)) GameManager.Instance.player.EnableShield();

        // Shoot fireballs
        if (Input.GetKeyDown(KeyCode.Space)) {
            gun.cooldown = gun.Shoot(
            gun.cooldown,
            gun.projectile,
            gameObject.transform.GetChild(0).transform.position,
            gameObject); 
        }

        // Coyote time
        if (IsGrounded()) coyoteTimeCounter = coyoteTime;
        else coyoteTimeCounter -= Time.deltaTime;

        // Buffer time
        if (Input.GetKeyDown(KeyCode.UpArrow)) { bufferTimeCounter = bufferTime; }
        else { bufferTimeCounter -= Time.deltaTime; }
    }

    // Checks if the player is grounded.
    public bool IsGrounded() { return GameManager.Instance.player.playerRB.velocity.y == 0; } // Should make a better grounded check in the future
    //public bool IsGrounded()
    //{
    //    return Physics2D.OverlapCircle(GameManager.Instance.putoSuelo.position, 0.6f, 9);
    //}

    // Collision actions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageables)) damageables?.Kill(gameObject);
        if (collision.TryGetComponent(out IInteractable interactables)) interactables?.Interact();
    }
}
