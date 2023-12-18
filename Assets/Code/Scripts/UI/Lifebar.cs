using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{
    private Vector3 heartPos;
    public GameObject heartPref;
    private GameObject[] hearts;
    // Start is called before the first frame update
    void Start()
    {
        hearts = new GameObject[GameManager.Instance.lives];
        Debug.Log(hearts.Length);
        heartPos = transform.position;
        for(int i=0;i< GameManager.Instance.lives; i++)
        {

            hearts[i] = Instantiate(heartPref, heartPos, Quaternion.identity);
            
            heartPos += new Vector3(2, 0, 0);
            Debug.Log(hearts[i].transform.position);
        }
    }
    public void Lives()
    {

        try
        {
            hearts[GameManager.Instance.lives].GetComponent<HeartDisplay>().SwapMySprite();
        }catch { Debug.Log("cant find heart"); }
    }

}
