using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerScriptable player;
    
    private readonly float shootingCD = 0.75f;
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
        currentLane = false; // (Starts as bottom lane)
        player.top.SetActive(false);
        souls = 0;
    }

    // Swap current lanes
    public void SwapLane() 
    {
        player.bottom.SetActive(currentLane);
        player.top.SetActive(!currentLane);
        currentLane = !currentLane;
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
