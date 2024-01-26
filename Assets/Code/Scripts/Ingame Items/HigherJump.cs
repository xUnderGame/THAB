using UnityEngine;

public class HigherJump : IngameItem
{
    public int minPrice = 25;
    public int maxPrice = 75;

    public override void Init()
    {
        powerupName = "Jump Boost";
        price = Random.Range(minPrice, maxPrice);
    }

    public override void Effect()
    {
        GameManager.Instance.pm.force += 4;
    }
}
