using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int force = 20;
    public float fallForce = 0.5f;

    private BoxCollider2D boxCollider;
    private int magnetTimer = 0;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        GameManager.Instance.player.playerRB = GetComponent<Rigidbody2D>();
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
        if (Input.GetKeyDown(KeyCode.V)) GameManager.Instance.SwapLane();

        // Temporary magnet powerup test (Should move this to fixedUpdate!)
        if (Input.GetKeyDown(KeyCode.P)) { magnetTimer = 720; }
        if (magnetTimer > 0)
        {
            magnetTimer--;
            GameObject[] souls = GameObject.FindGameObjectsWithTag("Coin");
            foreach (GameObject soul in souls)
            {
                Vector3 direction = transform.position - soul.transform.position;
                direction /= 10;
                soul.layer = GameManager.Instance.player.playerObject.layer;
                soul.transform.position = soul.transform.position + direction;
            }
        }

        // Enable forcefield
        if (Input.GetKeyDown(KeyCode.U)) {
            GameManager.Instance.EnableShield();
        }

        // Shoot fireballs
        if (Input.GetKeyDown(KeyCode.Space)) { 
            GameManager.Instance.player.fireballCD = GameManager.Instance.SpawnBullet(GameManager.Instance.player.fireballCD, GameManager.Instance.player.fireballPrefab, gameObject.transform.GetChild(0).transform.position);
        }   
        
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

    }

    // Checks if the player is grounded.
    bool IsGrounded() { return GameManager.Instance.player.playerRB.velocity.y == 0; }

    // Collision actions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Powerups
        if (collision.CompareTag("Powerup")) {
            magnetTimer = 720;
            Destroy(collision.gameObject);
        }

        // Bullets
        else if (collision.CompareTag("Bullet")) {
            GameManager.Instance.player.HurtPlayer(collision, "Fireball");
        }

        // Obstacles
        else if (collision.CompareTag("Obstacle")) {
            GameManager.Instance.player.HurtPlayer(collision);
        }
    }
}
