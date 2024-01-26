using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutoSuelo : MonoBehaviour
{
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), transform.root.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), transform.root.Find("Shield").GetComponent<Collider2D>());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
