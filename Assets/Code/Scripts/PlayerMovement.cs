using System.Collections;
using System.Collections.Generic;
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
        // "Slide"
        if (Input.GetKey(KeyCode.V) && IsGrounded()) {
            boxCollider.offset = new Vector2(boxCollider.offset.x, -0.5f);
            boxCollider.size = new Vector2(boxCollider.size.x, 1);
        }
    }

    bool IsGrounded()
    {
        return rb.velocity.y == 0;
    }
}
