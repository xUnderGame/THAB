using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    [HideInInspector] public List<GameObject> grids;
    [HideInInspector] public List<GameObject> markedPlatforms;
    [HideInInspector] public List<GameObject> platformPrefabs;
    [HideInInspector] public List<GameObject> powerupPrefabs;

    private int nextPowerup = 0;
    private readonly float minimumSpawnPosition = 25f;
    private readonly int[] laneTags = { 8, 9 };

    void Awake()
    {
        platformPrefabs = Resources.LoadAll<GameObject>("Platforms").ToList();
        powerupPrefabs = Resources.LoadAll<GameObject>("Powerups").ToList();
    }

    void Start()
    {
        // Add lanes where to spawn
        grids.Add(GameObject.Find("Spawns Top"));
        grids.Add(GameObject.Find("Spawns Bottom"));

        // Compresses the bounds of the tilemaps
        platformPrefabs.ForEach(platform => platform.GetComponent<Tilemap>().CompressBounds());

        // Spawn two platforms
        StartCoroutine(SelectPowerup());
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
    }

    // Creates an obstacle
    private void MakePlatform(bool position)
    {
        GameObject tempPlatform;
        int rand;

        // Where to spawn the platform
        if (position) {
            // Top platform
            rand = Random.Range(0, platformPrefabs.Count - 1);
            tempPlatform = Instantiate(platformPrefabs[rand], new Vector3(100, -3, 2), Quaternion.identity, grids[0].transform);
            tempPlatform.GetComponent<TilemapRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            // tempPlatform.transform.localScale = new Vector3(0.8f, 0.8f, tempPlatform.transform.localScale.z);
            // tempPlatform.GetComponent<TilemapCollider2D>().ProcessTilemapChanges();
            // tempPlatform.GetComponent<CompositeCollider2D>().GenerateGeometry();
            tempPlatform.layer = laneTags[0];
        } else {
            // Bottom platform
            rand = Random.Range(0, platformPrefabs.Count - 1);
            tempPlatform = Instantiate(platformPrefabs[rand], new Vector3(100, -5, 0), Quaternion.identity, grids[1].transform);
            tempPlatform.GetComponent<TilemapRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
            tempPlatform.layer = laneTags[1];
        }

        // Adds a powerup to one of the valid powerup locations
        if (nextPowerup != 0) {
            List<GameObject> powerupPositions = tempPlatform.transform.GetComponentsInChildren<Transform>()
            .Where(isValid => isValid.CompareTag("PowerupPosition"))
            .Select(position => position.gameObject).ToList();

            // At least one powerup position available
            if (powerupPositions.Count != 0) {
                // Instantiates it
                GameObject tempPowerup = Instantiate(powerupPrefabs[nextPowerup - 1], 
                powerupPositions[Random.Range(0, powerupPositions.Count - 1)].transform.position,
                Quaternion.identity,
                tempPlatform.transform);

                // Sets the correct layer material
                tempPowerup.GetComponent<Renderer>().material.color = tempPlatform.GetComponent<TilemapRenderer>().material.color;

                // Marks the next platform, now it MUST spawn a powerup in one of the available locations.
                StartCoroutine(SelectPowerup());
                nextPowerup = 0;
            }
        }

        // Changes layer of all children
        foreach (var child in tempPlatform.GetComponentsInChildren<Transform>()) { child.transform.gameObject.layer = tempPlatform.layer; }

        // Adds the spawned platform to a list with all the currently spawned ones and moves it a bit
        tempPlatform.transform.position = new Vector3(minimumSpawnPosition + tempPlatform.GetComponent<Renderer>().bounds.size.x / 2, tempPlatform.transform.position.y, tempPlatform.transform.position.z); 
        markedPlatforms.Add(tempPlatform);
    }

    // Finds the next platform where one should spawn
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

    public IEnumerator SelectPowerup() {
        yield return new WaitForSeconds(10f);
        nextPowerup = Random.Range(1, 3);
    }
}
