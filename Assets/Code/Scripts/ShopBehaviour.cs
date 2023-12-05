using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ShopBehaviour : MonoBehaviour, IInteractable
{
    public void Interact() {
        Time.timeScale = 0;
    }
}
