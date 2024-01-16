using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneEnemy : LaneBehaviour, IDamageable
{
    private bool hasSwapped = false;

    // Update is called once per frame
    void Update()
    {
        // Swaps lanes when position passes a threshold
        if (transform.position.x <= 4 && !hasSwapped)
        {
            hasSwapped = true;
            SwapLane(gameObject.layer == 7,
                GetComponent<Rigidbody2D>(),
                gameObject);
        }

        // Destroys enemy after a position threshold
        if (transform.position.x <= -20) Destroy(gameObject);
    }

    // Collision actions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable test)) test?.Kill(gameObject);
    }

    public void Kill(GameObject go)
    {
        Debug.Log($"{gameObject.name} was killed!");
        Destroy(gameObject);
    }
}
