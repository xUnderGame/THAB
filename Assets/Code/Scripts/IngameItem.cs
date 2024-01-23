using UnityEngine;

public abstract class IngameItem
{
    [HideInInspector] public string powerupName;
    [HideInInspector] public int price;
    public abstract void Effect();
    public abstract void Init();
    public void Click()
    {
        // if (price > GameManager.Instance.souls) return;
        GameManager.Instance.souls -= price;
        Effect();
    }
}
