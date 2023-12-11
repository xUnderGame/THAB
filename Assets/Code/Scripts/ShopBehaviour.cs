using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ShopBehaviour : MonoBehaviour, IInteractable
{
    public void Interact() {
        Time.timeScale = 0;
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.EnableShopGUI();
    }
}
