using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBehaviour : MonoBehaviour {
    public bool SwapLane(bool currentLane, Rigidbody2D rb, GameObject character)
    {
        // Change to top lane
        if (!currentLane)
        {
            rb.mass = 0.6f;
            character.layer = 7; // Top layer
            character.transform.localScale = new Vector3(1f, 1f, 1f);
            character.transform.position = new Vector3(character.transform.position.x, 11f, 4f);
        }

        // Change to bottom lane
        else
        {
            rb.mass = 0.5f;
            character.layer = 6; // Bottom layer
            character.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            character.transform.position = new Vector3(character.transform.position.x, 11f, 0f);
        }

        return !currentLane;
    } 
}
