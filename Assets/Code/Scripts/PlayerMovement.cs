using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : LaneBehaviour, IDamageable
{
    [HideInInspector] public float force = 20f;
    [HideInInspector] public float fallForce = 0.5f;
    [HideInInspector] public float coyoteTime = 0.2f;

    private ShootingBehaviour gun;
    private BoxCollider2D boxCollider;
    private float coyoteTimeCounter;

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
        if (Input.GetKey(KeyCode.UpArrow) && coyoteTimeCounter > 0f) {
            GameManager.Instance.player.playerRB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
        }

        // Makes the player "slide"
        if (Input.GetKey(KeyCode.DownArrow) && IsGrounded()) {
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
        if (!Input.GetKey(KeyCode.DownArrow)) {
            // Resizes hitbox with the normal, default hitbox
            boxCollider.offset = new Vector2(boxCollider.offset.x, 0);
            boxCollider.size = new Vector2(boxCollider.size.x, 2);
        }

        // Switch lanes
        if (Input.GetKeyDown(KeyCode.V) && Time.timeScale != 0)
        {
            GameManager.Instance.currentLane = SwapLane(
            GameManager.Instance.currentLane,
            GameManager.Instance.player.playerRB,
            GameManager.Instance.player.playerObject);
            GameManager.Instance.player.playerObject.transform.Find("Shield").gameObject.layer =GameManager.Instance.player.playerObject.layer;
        }

        // Shoot fireballs
        if (Input.GetKeyDown(KeyCode.Space)  && Time.timeScale != 0) {
            gun.cooldown = gun.Shoot(
            gun.cooldown,
            gun.projectile,
            gameObject.transform.GetChild(0).transform.position,
            gameObject); 
        }

        // Coyote time
        if (IsGrounded()) coyoteTimeCounter = coyoteTime;
        else coyoteTimeCounter -= Time.deltaTime;
    }

    // Checks if the player is grounded.
    bool IsGrounded() { return GameManager.Instance.player.playerRB.velocity.y == 0; } // Should make a better grounded check in the future

    // Collision actions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageables)) damageables?.Kill(gameObject);
        if (collision.TryGetComponent(out IInteractable interactables)) interactables?.Interact();
    }

    public void Kill(GameObject go)
    {
        Debug.Log($"{gameObject.name} was killed!");
        if (GameManager.Instance.lives > 1)
        {
            GameManager.Instance.lives--;
            // Respawn on the current lane
            if (GameManager.Instance.currentLane)
            {
                transform.position = new Vector3(-8f, 5f, 4f);
            }

            else
            {
                transform.position = new Vector3(-8f, 4f, 0f);
            }

            //change number of remaining lives
            GameObject.Find("LivesDisplay").GetComponent<Lifebar>().Lives();
        }
        else
        {
            GameManager.Instance.LoadScene("Game Over");
        }


    }
}
