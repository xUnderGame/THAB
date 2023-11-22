using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    [DoNotSerialize] public List<GameObject> grids;
    [DoNotSerialize] public List<GameObject> markedPlatforms;

    public List<GameObject> platformPrefabs;
    private readonly int[] laneTags = { 8, 9 };

    void Awake() { platformPrefabs = Resources.LoadAll<GameObject>("Platforms").ToList(); }

    void Start()
    {
        // Add lanes where to spawn
        grids.Add(GameObject.Find("Spawns Top"));
        grids.Add(GameObject.Find("Spawns Bottom"));

        // Spawn two platforms
        MakePlatform(true);
        MakePlatform(false);
    }

    // Spawns a platform every fixed time
    void FixedUpdate()
    {
        // Spawns a platform
        int shouldSpawn = FindNextSpawn();
        if (shouldSpawn != 0) {
            if (shouldSpawn == 8) MakePlatform(true);
            else MakePlatform(false);
        }
        Debug.Log(shouldSpawn);
    }

    // Creates an obstacle
    private void MakePlatform(bool position = true)
    {
        GameObject tempPlatform;
        int rand;

        // Where to spawn the platform
        if (position) {
            // Top platform
            rand = Random.Range(0, platformPrefabs.Count - 1);
            tempPlatform = Instantiate(platformPrefabs[rand], new Vector3(30, 2, 2), Quaternion.identity, grids[0].transform);
            // tempPlatform.transform.localScale = new Vector3(0.8f, 0.8f, tempPlatform.transform.localScale.z);
            tempPlatform.GetComponent<TilemapRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            tempPlatform.layer = laneTags[0];
        } else {
            // Bottom platform
            rand = Random.Range(0, platformPrefabs.Count - 1);
            tempPlatform = Instantiate(platformPrefabs[rand], new Vector3(30, -5, 0), Quaternion.identity, grids[1].transform);
            tempPlatform.layer = laneTags[1];
        }

        // Adds the spawned platform to a list with all the currently spawned ones
        markedPlatforms.Add(tempPlatform);
    }

    public int FindNextSpawn() {
        GameObject foundPlatform = markedPlatforms.FirstOrDefault(platform => platform.transform.position.x + (platform.GetComponent<Renderer>().bounds.size.x / 2) < GameManager.Instance.spawningGap);

        // Remove found platform from the marked list and spawn a new platform
        if (foundPlatform != null)
        {
            markedPlatforms.Remove(foundPlatform);
            return foundPlatform.layer;
        }
        else return 0;
    }
}
