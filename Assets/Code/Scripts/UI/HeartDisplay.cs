using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    public SpriteRenderer heartRender;
    public Sprite emptyHeart;

    void Start() {
    }
    public void SwapMySprite()
    {
        heartRender.sprite = emptyHeart;
    }
}
