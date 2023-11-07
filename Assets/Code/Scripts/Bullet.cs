using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Sprite bulletSprite;
    public float fbSpeed = 0.5f;
    public bool direction = true;

    // void Start()
    // {
    //     gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprite;
    // }

    void FixedUpdate()
    {
        // Impulsa la bala hacia una direccion, se usa un FixedUpdate para que sus frames sean fijos en todo momento
        if (direction) gameObject.transform.Translate(Vector2.right * fbSpeed);
        else gameObject.transform.Translate(Vector2.left * fbSpeed);
    }

    // Borrar bala al salir de la pantalla (solo funciona en game/build mode)
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    // Deletes the GameObject it collides with, bullets can collide into eachother!
    void OnTriggerEnter2D(Collider2D col) {
        if (!col.CompareTag("DestroyObstacles")) {
            Destroy(col.attachedRigidbody.gameObject);
            Destroy(gameObject);
        }
    }
}
