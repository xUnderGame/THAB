using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour, IInteractable
{
    [DoNotSerialize] public float activationTime;
    [DoNotSerialize] public float duration;
    protected bool active = false;
    
    protected virtual void Activate() { active = true; }

    protected virtual void Deactivate() { active = false; }

    public virtual void Interact() { Activate(); activationTime = Time.time; }

    protected void PowerupTimer() { if (Time.time >= activationTime + duration) Deactivate(); }
}
