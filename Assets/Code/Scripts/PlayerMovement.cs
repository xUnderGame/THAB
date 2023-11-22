using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int force = 20;

    private BoxCollider2D boxCollider;
    private int magnetTimer = 0;
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
        if (Input.GetKey(KeyCode.UpArrow) && IsGrounded()) {
            GameManager.Instance.player.playerRB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

        // Makes the player "slide"
        if (Input.GetKey(KeyCode.DownArrow) && IsGrounded()) {
            // Resizes hitbox to be lower
            boxCollider.offset = new Vector2(boxCollider.offset.x, -0.5f);
            boxCollider.size = new Vector2(boxCollider.size.x, 1);
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
            GameManager.Instance.player.EnableShield();
        }

        // Shoot fireballs
        if (Input.GetKeyDown(KeyCode.Space)) { 
            gun.cooldown = gun.Shoot(gun.cooldown, gun.projectile, gameObject.transform.GetChild(0).transform.position);
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
            GameManager.Instance.player.HurtPlayer(collision);
        }

        // Obstacles
        else if (collision.CompareTag("Obstacle")) {
            GameManager.Instance.player.HurtPlayer(collision);
        }

        // Fire
        else if (collision.CompareTag("Fire")) {
            GameManager.Instance.player.Kill();
        }
    }
}
