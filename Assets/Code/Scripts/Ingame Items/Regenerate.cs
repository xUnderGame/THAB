using UnityEngine;

public class Regenerate : IngameItem
{
    public int minPrice = 15;
    public int maxPrice = 55;

    public override void Init()
    {
        powerupName = "Heal 1hp";
        price = Random.Range(minPrice, maxPrice);
    }

    public override void Effect()
    {
        if (GameManager.Instance.lives >= GameManager.Instance.maxLives - 1) { GameManager.Instance.souls += price; return; }
        GameManager.Instance.livesDisplay.GetComponent<Lifebar>().Lives(true);
        GameManager.Instance.lives++;
    }
}
