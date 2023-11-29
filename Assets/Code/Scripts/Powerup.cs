using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour, IInteractable
{
    [DoNotSerialize] public float activationTime;
    [DoNotSerialize] public float duration;

    public enum PowerupTypes { Magnet, Shield };
    public PowerupTypes selectedPowerup;
    
    // Activate the powerup
    protected virtual void Activate() { activationTime = Time.time; }

    // Deactivate the powerup
    protected virtual void Deactivate() {  }

    // Interact with the object
    public virtual void Interact() {
        var thing = (Powerup)GameManager.Instance.player.playerObject.GetComponent(selectedPowerup.ToString());
        thing.Activate();
        Destroy(gameObject);
    }

    // Check for the remaining powerup time
    protected bool PowerupTimer() { return Time.time <= activationTime + duration && activationTime != 0f; }
}
