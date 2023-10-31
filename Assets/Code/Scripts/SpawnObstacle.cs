using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{

    public GameObject duck;
    public GameObject jump;
    public GameObject lane;
    private int frameCounter;
    // Start is called before the first frame update
    void Start()
    {
        frameCounter = 300;
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter--;
        if (frameCounter <= 0)
        {
            int tim = Random.Range(0,271);
            makeObstacle();
            frameCounter = 210 + tim;
        }
    }
    private void makeObstacle()
    {
        int obs = Random.Range(0, 3);
        switch (obs)
        {
            case 0:
                Instantiate(jump, new Vector3(20, 0.5f, 0), Quaternion.identity);
                break;
            case 1:
                Instantiate(duck, new Vector3(20, 4, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(lane, new Vector3(20, 3, 0), Quaternion.identity);
                break;
        }
    }
}
