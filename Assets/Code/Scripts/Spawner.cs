using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    [DoNotSerialize] public float frameCounter;
    [DoNotSerialize] public float defaultCounter;
    [DoNotSerialize] public List<GameObject> grids;

    public List<GameObject> platforms;
    private readonly int[] laneTags = { 8, 9 };

    void Awake() { platforms = Resources.LoadAll<GameObject>("Platforms").ToList(); }

    void Start()
    {
        // Add lanes where to spawn
        grids.Add(GameObject.Find("Spawns Top"));
        grids.Add(GameObject.Find("Spawns Bottom"));

        defaultCounter = 1.5f;
        frameCounter = 0;
        platforms.ForEach(platform => Debug.Log(platform.name));
    }

    // Spawns an obstacle/platform every x seconds
    void FixedUpdate()
    {
        // Spawns an obstacle
        if (frameCounter <= 0)
        {
            MakeObstacle();
            frameCounter = defaultCounter - (GameManager.Instance.gameSpeed * 1.5f) + Random.Range(0.2f, 1f);
        }
        frameCounter -= Time.deltaTime;
    }

    // Creates an obstacle on BOTH lanes
    private void MakeObstacle()
    {
        // Top platform
        int rand = Random.Range(0, platforms.Count - 1);
        GameObject tempPlatform = Instantiate(platforms[rand], new Vector3(30, 2, 2), Quaternion.identity, grids[0].transform);
        tempPlatform.GetComponent<TilemapRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        tempPlatform.layer = laneTags[0];

        // Bottom platform
        rand = Random.Range(0, platforms.Count - 1);
        tempPlatform = Instantiate(platforms[rand], new Vector3(30, -5, 0), Quaternion.identity, grids[1].transform);
        tempPlatform.layer = laneTags[1];
    }
}
