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
    [DoNotSerialize] public float obstaclesFrameCounter;
    [DoNotSerialize] public float platformsFrameCounter;
    [DoNotSerialize] public float defaultCounter;

    private List<GameObject> lanes = new();
    private readonly int[] laneTag = { 8, 9 };

    void Start()
    {
        // Add lanes where to spawn
        lanes.Add(GameObject.Find("Spawns Top"));
        lanes.Add(GameObject.Find("Spawns Bottom"));
        
        // Other vars
        defaultCounter = 3.0f;
        obstaclesFrameCounter = defaultCounter;
        platformsFrameCounter = defaultCounter;
    }

    // Spawns an obstacle/platform every x seconds
    void FixedUpdate()
    {
        // Spawns an obstacle
        if (obstaclesFrameCounter <= 0)
        {
            MakeObstacle();
            obstaclesFrameCounter = defaultCounter - (GameManager.Instance.gameSpeed * 2) + Random.Range(0.2f, 1.5f);
        }
        obstaclesFrameCounter -= Time.deltaTime;
    }

    // Creates an obstacle on a randomly chosen lane
    private void MakeObstacle()
    {
        int obstacle = Random.Range(0, 4);
        int laneNum = Random.Range(0, 2);

        switch(obstacle) {
            // Wall
            case 0:
                GameObject tempLane = Instantiate(lanePrefab, lanes[laneNum].transform.position, Quaternion.identity, lanes[laneNum].transform);
                tempLane.transform.localScale = new Vector3(4f, 6f, 1f);
                tempLane.layer = laneTag[laneNum];
                break;

            // Object to jump
            case 1:
                GameObject tempJump = Instantiate(jumpPrefab, lanes[laneNum].transform.position, Quaternion.identity, lanes[laneNum].transform);
                tempJump.transform.localScale = new Vector3(1f, 2f, 1f);
                tempJump.layer = laneTag[laneNum];
                break;

            // Object to duck
            case 2:
                GameObject tempDuck = Instantiate(duckPrefab, lanes[laneNum].transform.position, Quaternion.identity, lanes[laneNum].transform);
                tempDuck.transform.localScale = new Vector3(3f, 3f, 1f);
                tempDuck.layer = laneTag[laneNum];
                break;

            // Spawns a soul
            case 3:
                GameObject tempSoul = Instantiate(soulPrefab, lanes[laneNum].transform.position, Quaternion.identity, lanes[laneNum].transform);
                tempSoul.transform.localScale = new Vector3(1f, 1f, 1f);
                tempSoul.layer = laneTag[laneNum];
                //tempSoul.soulsDisplay = score;
                break;
        }
    }
}
