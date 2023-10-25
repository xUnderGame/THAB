
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{

    public GameObject duck;
    public GameObject jump;
    public GameObject lane;
    public GameObject soul;
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
        int obs = Random.Range(0, 301);
        if (obs == 0)
            Instantiate(lane, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        else if (obs <= 100)
            Instantiate(jump, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
        else if (obs <= 200)
            Instantiate(duck, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        else if (obs <= 300)
        {
            GameObject sol = Instantiate(soul, transform.position + new Vector3(0, 2.3f, 0), Quaternion.identity, soul.transform.parent);
            sol.transform.localScale = new Vector3(3f, 3f, 3f);
            //sol.soulsDisplay = score;
        }

    }
}
