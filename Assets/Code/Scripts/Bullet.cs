using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float projectileSpeed = 0.5f;
    public bool direction = true;
    public bool targetPlayer = false;

    void FixedUpdate()
    {
        // Impulsa la bala hacia una direccion, se usa un FixedUpdate para que sus frames sean fijos en todo momento
        if (direction) gameObject.transform.Translate(Vector2.right * projectileSpeed, Space.World);
        else gameObject.transform.Translate(Vector2.left * projectileSpeed, Space.World);
    }

    // Borrar bala al salir de la pantalla (solo funciona en game/build mode)
    private void OnBecameInvisible() { Destroy(gameObject); }

    // Destroys itself upon touching anything and kills enemies!
    void OnTriggerEnter2D(Collider2D col) {
        // Ignore player shooting a fireball
        if (!targetPlayer && col.CompareTag("Player") || col.name == "Shield") return;

        // Kills an enemy
        if (col.CompareTag("Enemy")) {
            // GameManager.Instance.Kill(col.gameObject);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        // Destroy all bullets when colliding with an obstacle
        else if (col.CompareTag("Obstacle")) { Destroy(col.gameObject); Destroy(gameObject); }
        else { Destroy(gameObject); } // Destroy itself
    }
}
