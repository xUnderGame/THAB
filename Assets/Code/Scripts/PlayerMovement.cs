using UnityEngine;

public class PlayerMovement : LaneBehaviour, IDamageable
{
    [HideInInspector] public float fallForce;
    [HideInInspector] public int force;
    public Vector2 boxSize;
    public float castDistance;

    private ShootingBehaviour gun;
    private BoxCollider2D boxCollider;
    private readonly float coyoteTime = 0.2f;
    private readonly float bufferTime = 0.2f;
    private float coyoteTimeCounter;
    private float bufferTimeCounter;
    private PutoSuelo putoSuelo;
    private LaneBehaviour lb;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        fallForce = 100f;
        force = 20;
        gun = GetComponent<ShootingBehaviour>();
        boxCollider = GetComponent<BoxCollider2D>();
        lb = GetComponent<LaneBehaviour>();
        gun.cooldown = Time.time;
        GameManager.Instance.player.playerRB = GetComponent<Rigidbody2D>();
        gun.projectile = Resources.Load<GameObject>("Projectiles/Fireball");
        putoSuelo = transform.Find("PutoSuelo").GetComponent<PutoSuelo>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump
        if (bufferTimeCounter >= 0f && coyoteTimeCounter > 0f && lb.temp == null)
        {
            GameManager.Instance.player.playerRB.velocity = Vector2.zero;
            GameManager.Instance.player.playerRB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            bufferTimeCounter = 0f;

            animator.SetBool("Jump", true);
        }

        // Makes the player "slide"
        if (Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded())
        {
            // Resizes hitbox to be lower
            boxCollider.offset = new Vector2(boxCollider.offset.x, -0.5f);
            boxCollider.size = new Vector2(boxCollider.size.x, 1);
        }

        // Fast fall
        if (Input.GetKey(KeyCode.DownArrow) && !IsGrounded() && boxCollider.enabled)
        {
            // Increase gravity while player is on air
            GameManager.Instance.player.playerRB.AddForce(fallForce * Time.deltaTime * Vector2.down, ForceMode2D.Impulse);
        }

        // Revert back from sliding
        if (!Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Resizes hitbox with the normal, default hitbox
            boxCollider.offset = new Vector2(boxCollider.offset.x, 0);
            boxCollider.size = new Vector2(boxCollider.size.x, 2);
        }

        // Switch lanes
        if (Input.GetKeyDown(KeyCode.V) && IsGrounded() && Time.timeScale != 0)
        {
            GameManager.Instance.currentLane = SwapLane(
            GameManager.Instance.currentLane,
            GameManager.Instance.player.playerRB,
            GameManager.Instance.player.playerObject
            );
            putoSuelo.gameObject.layer = gameObject.layer;
        }

        // Enable forcefield
        if (Input.GetKeyDown(KeyCode.U)) GameManager.Instance.player.EnableShield();

        // Shoot fireballs
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale != 0)
        {
            gun.cooldown = gun.Shoot(
            gun.cooldown,
            gun.projectile,
            gameObject.transform.GetChild(0).transform.position,
            gameObject);
        }

        // Coyote time
        if (IsGrounded()) 
        {
            animator.SetBool("Jump", false);

            coyoteTimeCounter = coyoteTime; 
            lb.temp = null;
        }
        else coyoteTimeCounter -= Time.deltaTime;

        // Buffer time
        if (Input.GetKeyDown(KeyCode.UpArrow)) { bufferTimeCounter = bufferTime; }
        else { bufferTimeCounter -= Time.deltaTime; }
    }

    // Checks if the player is grounded.
    public bool IsGrounded() { return GameManager.Instance.player.playerRB.velocity.y == 0; }
    // public bool IsGrounded()
    // {
    //    return putoSuelo.isGrounded;
    // }

    // Collision actions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.TryGetComponent(out IDamageable damageables)) damageables?.Kill(gameObject);
        if (collision.TryGetComponent(out IInteractable interactables)) interactables?.Interact();

        // Damage player (fire)
        if (collision.CompareTag("Fire")) { Kill(collision.gameObject); }
    }

    public void Kill(GameObject go)
    {
        GameManager.Instance.player.HurtPlayer(go);
    }
}
