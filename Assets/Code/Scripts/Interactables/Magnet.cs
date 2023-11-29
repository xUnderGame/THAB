using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Powerup, IInteractable
{
    void Start() { duration = 10f; }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P)) Activate();
        if (!PowerupTimer()) return;

        foreach (GameObject soul in GameObject.FindGameObjectsWithTag("Soul"))
        {
            Vector3 direction = GameManager.Instance.player.playerObject.transform.position - soul.transform.position;
            direction /= 10;
            soul.layer = GameManager.Instance.player.playerObject.layer;
            soul.transform.position = soul.transform.position + direction;
        }
    }
}
