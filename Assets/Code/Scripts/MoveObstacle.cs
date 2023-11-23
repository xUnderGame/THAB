using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    private float penalty = 0.5f;
    void FixedUpdate() { transform.Translate(-(GameManager.Instance.gameSpeed - penalty), 0, 0, Space.World); }

    // Destroys GameObject after it leaves the screen (only works in build/game mode)
    public void OnBecameInvisible() { if (!gameObject.CompareTag("Fire")) Destroy(gameObject); }
}
