using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class IngameShopBehaviour : MonoBehaviour, IInteractable
{
    public List<IngameItem> itemPool;
    public List<IngameItem> shopItems = new(capacity: 4);
    public void Awake() { itemPool = new() { new HigherJump(), new Regenerate() }; }

    public void Interact()
    {
        Time.timeScale = 0;
        GameManager.Instance.currentShop = this;
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.EnableShopGUI();
        RandomizeItems();
    }

    // Randomizes the items in the current active shop
    public void RandomizeItems()
    {
        for (int i = 1; i < 5; i++)
        {
            IngameItem randomItem = itemPool[Random.Range(0, itemPool.Count)];
            randomItem.Init();

            GameObject mainItem = GameManager.Instance.shopUI.transform.Find($"Item {i}").gameObject;
            mainItem.transform.Find("Panel").Find("Button").GetComponent<Button>().onClick.AddListener(() => randomItem.Click());
            mainItem.transform.Find("Item Text").GetComponent<Text>().text = randomItem.powerupName;
            mainItem.transform.Find("Price").GetComponent<Text>().text = randomItem.price.ToString();
        }
    }
}
