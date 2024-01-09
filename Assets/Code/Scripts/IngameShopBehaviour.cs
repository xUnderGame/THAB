using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IngameShopBehaviour : MonoBehaviour, IInteractable
{
    public List<IngameItem> itemPool;
    public List<IngameItem> shopItems = new(capacity: 4);
    public void Awake() { itemPool = Resources.LoadAll<IngameItem>("Items").ToList(); }

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
