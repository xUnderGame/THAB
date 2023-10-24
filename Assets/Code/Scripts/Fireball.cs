using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float fbSpeed = 1f;
    private Rigidbody2D fireballRB;

    // Start is called before the first frame update
    void Start()
    {
        fireballRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Impulsa la bala hacia la derecha, se usa un FixedUpdate para que sus
        //frames sean fijos en todo momento

        //fireballRB.AddForce(Vector2.right * fbSpeed, ForceMode2D.Impulse);
        gameObject.transform.Translate(Vector2.right * fbSpeed);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
