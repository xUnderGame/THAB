using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Powerup
{
    void Start() { duration = 10f; }

    void FixedUpdate()
    {
        // Guard cases for the powerup & debug activate
        if (Input.GetKeyDown(KeyCode.P)) Activate();
        if (!PowerupTimer()) return;

        // Attracts all souls to your location
        foreach (GameObject soul in GameObject.FindGameObjectsWithTag("Soul"))
        {
            soul.transform.position += (GameManager.Instance.player.playerObject.transform.position - soul.transform.position) / 5;
            soul.layer = GameManager.Instance.player.playerObject.layer;
        }
    }
}
