using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerScriptable player;
    public GameObject fireballPrefab;
    [DoNotSerialize] public TMP_Text soulsDisplay;

    private readonly float shootingCD = 0.75f;
    private bool currentLane;
    public int souls;

    void Awake()
    {
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }

        // Scriptables
        player.playerObject = GameObject.Find("Player");
        player.fireballCD = Time.time;

        // Setting stuff up
        soulsDisplay = GameObject.Find("SoulsDisplay").GetComponent<TMP_Text>();
        souls = 0;
    }

    // Swap current lanes
    public void SwapLane() 
    {
        currentLane = !currentLane;

        // Change to top lane
        if (currentLane) {
            player.playerRB.mass = 0.7f;
            player.playerObject.layer = 7; // Top layer
            player.playerObject.transform.localScale = new Vector3(1f, 1f, 1f);
            player.playerObject.transform.position = new Vector3(-5f, 2.5f, 4f);
        } 

        // Change to bottom lane
        else {
            player.playerRB.mass = 0.5f;
            player.playerObject.layer = 6; // Bottom layer
            player.playerObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            player.playerObject.transform.position = new Vector3(-8f, -5f, 0f);
        }
    }

    // Adds to the player amount of souls
    public void ChangeSouls(int amount, bool forceSet = false)
    {
        if (forceSet) souls = amount;
        else souls += amount;
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
