using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Powerup, IInteractable
{
    private float frameCounter;

    // Resets the timer after its enabled
    void OnEnable() { frameCounter = 240f; }

    void FixedUpdate()
    {
        // Disable shield after x "frames"
        if (frameCounter > 0) frameCounter--;
        else GameManager.Instance.player.DisableShield();
    }
}
