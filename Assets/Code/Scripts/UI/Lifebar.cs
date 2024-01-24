using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{
    private Vector3 heartPos;
    public GameObject heartPref;
    private List<GameObject> hearts = new();

    // Start is called before the first frame update
    void Start()
    {
        heartPos = GameManager.Instance.livesDisplay.transform.position;

        // Creates the objects
        for (int i = 0; i < GameManager.Instance.lives; i++)
        {
            hearts.Add(Instantiate(heartPref, heartPos, Quaternion.identity, GameManager.Instance.livesDisplay.transform));
            heartPos += new Vector3(2, 0, 0);
        }
    }

    public void Lives(bool status)
    {
        if (status) hearts[GameManager.Instance.lives].GetComponent<HeartDisplay>().SwapMySprite(true);
        else hearts[GameManager.Instance.lives].GetComponent<HeartDisplay>().SwapMySprite(false);
    }
}
