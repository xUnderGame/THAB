using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject topPlayer;
    public GameObject bottomPlayer;
    public GameObject fireballPrefab;
    public TMP_Text soulsDisplay;


    private readonly float fireballCD = 0.75f;
    private float cdTime;
    private bool currentLane;
    public int souls;

    void Awake()
    {
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }

        // Setting stuff up
        soulsDisplay = GameObject.Find("SoulsDisplay").GetComponent<TMP_Text>();
        currentLane = false; // (Starts as bottom lane)
        topPlayer.SetActive(false);
        cdTime = Time.time;
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

    // Shoots a fireball to the right
    public void ShootFireball()
    {
        // Cooldown before shooting
        if (cdTime <= Time.time)
        {
            Instantiate(fireballPrefab, GameObject.Find("ShootingPoint").transform.position, Quaternion.identity);
            cdTime = Time.time + fireballCD;
        }
    }
}
