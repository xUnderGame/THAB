using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour, IInteractable
{
    [DoNotSerialize] public float activationTime;
    [DoNotSerialize] public float duration;
    
    protected virtual void Activate() { activationTime = Time.time; Debug.Log(activationTime); }

    protected virtual void Deactivate() {  }

    public virtual void Interact() { Activate(); }

    protected bool PowerupTimer() { Debug.Log($"{activationTime}, {Time.time}, {activationTime + duration}"); return Time.time <= activationTime + duration && activationTime != 0f; }
}
