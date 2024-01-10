using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Powerup
{
    public override void Activate()
    {
        GameManager.Instance.player.playerObject.transform.Find("Shield").gameObject.layer = GameManager.Instance.player.playerObject.layer;
        GameManager.Instance.player.EnableShield();
    }
    
    public override void Deactivate() { GameManager.Instance.player.DisableShield(); }

    void FixedUpdate()
    {
        // Debug toggle
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (GameManager.Instance.player.isShieldEnabled) Deactivate();
            else Activate();
        }
    }
}
