
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
        int obs = Random.Range(0, 4);
        switch(obs) {
            // Wall
            case 0:
                Instantiate(lanePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                break;

            // Object to jump
            case 1:
                Instantiate(jumpPrefab, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
                break;

            // Object to duck
            case 2:
                Instantiate(duckPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                break;

            // Spawns a soul
            case 3:
                GameObject soulObject = Instantiate(soulPrefab, transform.position + new Vector3(0, 2.3f, 0), Quaternion.identity, soulPrefab.transform.parent);
                soulObject.transform.localScale = new Vector3(3f, 3f, 3f);
                //soulObject.soulsDisplay = score;
                break;
        }
    }
}
