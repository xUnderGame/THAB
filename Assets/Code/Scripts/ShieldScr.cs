using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int frameCounter;
    public GameObject refpoint;

    // Start is called before the first frame update
    private void Start()
    {
        frameCounter = 60;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (frameCounter > 0) frameCounter--;
        transform.position = refpoint.transform.position;

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Obstacle"))
        {
            Destroy(col.gameObject);
            if (frameCounter==0)
            {
                GameManager.Instance.DisableShield();
            }
        }
    }
}
