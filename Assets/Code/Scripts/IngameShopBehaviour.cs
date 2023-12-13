using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IngameShopBehaviour : MonoBehaviour, IInteractable
{
    public List<IngameItem> items;
    public void Awake() { items = Resources.LoadAll<IngameItem>("Items").ToList(); }

    public void Interact() {
        Time.timeScale = 0;
        GameManager.Instance.currentShop = this;
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.EnableShopGUI();
    }

    public void RandomizeItems()
    {

    }
}
