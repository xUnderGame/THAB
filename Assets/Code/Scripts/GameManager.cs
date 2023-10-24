using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject topPlayer;
    public GameObject bottomPlayer;
    public GameObject fireballPrefab;

    private bool currentLane;
    public int souls;

    void Awake()
    {
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }

        // Setting stuff up
        currentLane = false; // (Starts as bottom lane)
        topPlayer.SetActive(false);
        souls = 0;
    }

    // Swap current lanes
    public void SwapLane() 
    {
        bottomPlayer.SetActive(currentLane);
        topPlayer.SetActive(!currentLane);
        currentLane = !currentLane;
    }

    // Adds to the player amount of souls
    public void ChangeSouls(int amount)
    {
        souls += amount;
    }

    public void ShootFireball()
    {
        Instantiate(fireballPrefab, GameObject.Find("ShootingPoint").transform.position, Quaternion.identity);
    }
}
