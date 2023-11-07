using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerScriptable player;

    public GameObject topPlayer;
    public GameObject bottomPlayer;
    public GameObject topShield;
    public GameObject bottomShield;
    public GameObject fireballPrefab;
    public TMP_Text soulsDisplay;

    private readonly float shootingCD = 0.75f;
    private readonly float fireballCD = 0.75f;
    private float cdTime;
    private bool currentLane;
    public int souls;

    void Awake()
    {
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }

        // Scriptables
        player.top = GameObject.Find("Player 2");
        player.bottom = GameObject.Find("Player 1");
        player.fireballCD = Time.time;

        // Setting stuff up
        soulsDisplay = GameObject.Find("SoulsDisplay").GetComponent<TMP_Text>();
        currentLane = false; // (Starts as bottom lane)
        player.top.SetActive(false);
        souls = 0;
    }

    // Swap current lanes
    public void SwapLane() 
    {
        player.bottom.SetActive(currentLane);
        player.top.SetActive(!currentLane);
        if (topShield.activeSelf || bottomShield.activeSelf)
        {
            bottomShield.SetActive(currentLane);
            topShield.SetActive(!currentLane);
        }
        /*
        if (topShield.activeSelf)
        {
            //topShield.frameCounter = bottomShield.frameCounter;
        }
        else if (bottomShield.activeSelf)
        {
            //bottomShield.frameCounter = topShield.frameCounter;
        }
        */
        currentLane = !currentLane;
    }
    public void EnableShield()
    {
        bottomShield.SetActive(!currentLane);
        topShield.SetActive(currentLane);
        //topShield.frameCounter = 60;
        //topShield.frameCounter = 60;
    }
    public void DisableShield()
    {
        bottomShield.SetActive(false);
        topShield.SetActive(false);
        //topShield.frameCounter = 0;
        //topShield.frameCounter = 0;
    }
    // Adds to the player amount of souls
    public void ChangeSouls(int amount)
    {
        souls += amount;
    }

    // Shoots a bullet with a cooldown
    public float SpawnBullet(float cooldown, GameObject projectile, Vector3 shootingPoint)
    {
        // Cooldown before shooting
        if (cooldown <= Time.time)
        {
            Instantiate(projectile, shootingPoint, Quaternion.identity);
            return Time.time + shootingCD;
        }
        return cooldown;
    }
}
