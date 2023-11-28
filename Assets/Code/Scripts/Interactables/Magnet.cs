using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : Powerup, IInteractable
{
    void Start() { duration = 10f; }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P)) Activate();
        if (!active) return;

        GameObject[] souls = GameObject.FindGameObjectsWithTag("Soul");
        foreach (GameObject soul in souls)
        {
            Vector3 direction = GameManager.Instance.player.playerObject.transform.position - soul.transform.position;
            direction /= 10;
            soul.layer = GameManager.Instance.player.playerObject.layer;
            soul.transform.position = soul.transform.position + direction;
        }
        PowerupTimer();
    }
}
