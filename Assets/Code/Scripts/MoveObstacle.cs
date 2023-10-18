using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.05f, 0, 0);
    }

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("hit");
        if (col.gameObject.tag == "DestroyObstacles")
        {
            Debug.Log("sunk");
            Destroy(gameObject);
        }
    }
}
