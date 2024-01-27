using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    public Sprite emptyHeart;
    public Sprite filledHeart;
    
    private SpriteRenderer heartRender;

    public void Start() { heartRender = GetComponent<SpriteRenderer>(); }

    public void SwapMySprite(bool status)
    {
        if (status) heartRender.sprite = filledHeart;
        else heartRender.sprite = emptyHeart;
    }
}
