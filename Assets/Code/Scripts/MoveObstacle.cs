using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    void FixedUpdate() { transform.Translate(-GameManager.Instance.gameSpeed, 0, 0, Space.World); }

    // Destroys GameObject after it leaves the screen (only works in build/game mode)
    public void OnBecameInvisible() { Destroy(gameObject); }
}
