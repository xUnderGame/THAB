using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    public SpriteRenderer heartRender;
    public Sprite emptyHeart;
    public Sprite filledHeart;

    public void SwapMySprite(bool status)
    {
        if (status) heartRender.sprite = filledHeart;
        else heartRender.sprite = emptyHeart;
    }
}
