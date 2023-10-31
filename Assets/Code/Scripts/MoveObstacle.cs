using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public float speed = 0.5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(-speed, 0, 0);
    }

    // Destroys GameObject after it leaves the screen (only works in build/game mode)
    public void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
