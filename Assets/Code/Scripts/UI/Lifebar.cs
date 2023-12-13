using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{
    public GameObject[] Hearts;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Lives()
    {
        Hearts[GameManager.Instance.lives].GetComponent<HeartDisplay>().SwapMySprite();
    }

}
