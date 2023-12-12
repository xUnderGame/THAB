using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IngameShopBehaviour : MonoBehaviour, IInteractable
{
    public void Interact() {
        Time.timeScale = 0;
        GameManager.Instance.currentShop = this;
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.EnableShopGUI();
    }

    public void BuyItem() { }
    public void RandomizeItems() { }
    public void LoadItems() { }
}
