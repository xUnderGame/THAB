
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
        int obs = Random.Range(0, 401);
        if (obs <= 100)
        { 
            GameObject lan = Instantiate(lane, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            lan.transform.localScale = new Vector3(4f, 6f, 1f);
        }
        else if (obs <= 200)
        {
            GameObject jum = Instantiate(jump, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
            jum.transform.localScale = new Vector3(1f, 2f, 1f);
        }
        else if (obs <= 300)
        {
            GameObject duc = Instantiate(duck, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            duc.transform.localScale = new Vector3(3f, 3f, 1f);
        }
        else if (obs <= 400)
        {
            GameObject sol = Instantiate(soul, transform.position + new Vector3(0, 0, 0), Quaternion.identity, soul.transform.parent);
            sol.transform.localScale = new Vector3(3f, 3f, 1f);
            //sol.soulsDisplay = score;
        }

    }
}
