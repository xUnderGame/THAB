using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Translate(-GameManager.Instance.gameSpeed, 0, 0);
    }

    // Destroys GameObject after it leaves the screen (only works in build/game mode)
    public void OnBecameInvisible() {
        Destroy(gameObject);
    }

    // Kill the player on collision
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && !gameObject.CompareTag("Coin") && !gameObject.CompareTag("Powerup")) GameManager.Instance.player.Kill();
    }
}
