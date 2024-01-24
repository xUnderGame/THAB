using UnityEngine;

public class BonusScore : IngameItem
{
    public int minPrice = 5;
    public int maxPrice = 20;

    public override void Init()
    {
        powerupName = "+100m";
        price = Random.Range(minPrice, maxPrice);
    }

    public override void Effect()
    {
        GameManager.Instance.meters += 100;
    }
}
