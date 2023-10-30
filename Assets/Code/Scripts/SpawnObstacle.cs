using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject soulPrefab;
    public GameObject duckPrefab;
    public GameObject jumpPrefab;
    public GameObject lanePrefab;
    [DoNotSerialize] public float frameCounter;
    [DoNotSerialize] public float defaultCounter;

    void Start()
    {
        defaultCounter = 3.0f;
        frameCounter = defaultCounter;
    }

    void FixedUpdate()
    {
        if (frameCounter <= 0)
        {
            MakeObstacle();
            frameCounter = defaultCounter + Random.Range(0, 1);
        }
        frameCounter -= Time.deltaTime;
    }

    // Creates an obstacle on a randomly chosen lane
    private void MakeObstacle()
    {
        int obstacle = Random.Range(0, 4);
        switch(obstacle) {
            // Wall
            case 0:
                GameObject tempLane = Instantiate(lanePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                tempLane.transform.localScale = new Vector3(4f, 6f, 1f);
                break;

            // Object to jump
            case 1:
                GameObject tempJump = Instantiate(jumpPrefab, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
                tempJump.transform.localScale = new Vector3(1f, 2f, 1f);
                break;

            // Object to duck
            case 2:
                GameObject tempDuck = Instantiate(duckPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                tempDuck.transform.localScale = new Vector3(3f, 3f, 1f);
                break;

            // Spawns a soul
            case 3:
                GameObject tempSoul = Instantiate(soulPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity, soul.transform.parent);
                tempSoul.transform.localScale = new Vector3(3f, 3f, 1f);
                //tempSoul.soulsDisplay = score;
                break;
        }
    }
}
