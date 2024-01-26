using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IInteractable
{
    [HideInInspector] public float activationTime;
    [HideInInspector] public float duration;

    public enum PowerupTypes { Magnet, Shield };
    public PowerupTypes selectedPowerup;
    
    // Activate the powerup
    public virtual void Activate() { activationTime = Time.time; }

    // Deactivate the powerup
    public virtual void Deactivate() {  }

    // Interact with the object
    public virtual void Interact() {
        ((Powerup)GameManager.Instance.player.playerObject.GetComponent(selectedPowerup.ToString())).Activate();
        GameManager.Instance.ChangeScore(3);
        Destroy(gameObject);
    }

    // Check for the remaining powerup time
    protected bool PowerupTimer() { return Time.time <= activationTime + duration && activationTime != 0f; }
}
