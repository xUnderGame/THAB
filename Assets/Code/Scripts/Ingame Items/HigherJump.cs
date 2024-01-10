using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherJump : IngameItem
{
    public int minPrice = 25;
    public int maxPrice = 150;
    
    public override void Effect()
    {
        GameManager.Instance.pm.force += 4;
    }
}
