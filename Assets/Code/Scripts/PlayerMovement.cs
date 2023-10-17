using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int force = 20;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump
        if (Input.GetKey(KeyCode.UpArrow) && IsGrounded()) {
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
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
    }

    // Checks if the player is grounded.
    bool IsGrounded() { return rb.velocity.y == 0; }
}
