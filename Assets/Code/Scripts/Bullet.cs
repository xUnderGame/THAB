using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fbSpeed = 0.5f;
    public bool direction = true;
    public bool targetPlayer = false;

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
    
    // Destroys itself upon touching an enemy/player, bullets cannot collide into eachother (for now)!
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && targetPlayer) GameManager.Instance.player.Kill();
        else if (col.CompareTag("Enemy")) GameManager.Instance.KillEnemy(col.gameObject);

        // Destroys itself
        Destroy(gameObject);
    }
}
