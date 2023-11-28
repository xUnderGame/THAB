using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerScriptable player;
    [DoNotSerialize] public readonly float globalCD = 0.5f;
    [DoNotSerialize] public TMP_Text soulsDisplay;
    [DoNotSerialize] public TMP_Text distanceDisplay;
    [DoNotSerialize] public float gameSpeed;
    [DoNotSerialize] public float spawningGap;

    private bool currentLane;
    public int souls;
    public float meters;

    void Awake()
    {
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }

        // Scriptables
        player.playerObject = GameObject.Find("Player");
        player.shield = player.playerObject.transform.Find("Shield").gameObject;
        player.isShieldEnabled = false;

        // Setting stuff up
        player.DisableShield();
        soulsDisplay = GameObject.Find("SoulsDisplay").GetComponent<TMP_Text>();
        distanceDisplay = GameObject.Find("DistanceDisplay").GetComponent<TMP_Text>();
        spawningGap = 18f;
        gameSpeed = 1f;
        souls = 0;

        // Game speed corroutine, can change later
        StartCoroutine(SpeedUp(1.2f));
        StartCoroutine(Distance());
    }

    // Enumerator for the corroutine
    IEnumerator SpeedUp(float maxSpeed)
    {
        while (gameSpeed < maxSpeed)
        {
            yield return new WaitForSeconds(2f);
            gameSpeed += 0.02f;
            spawningGap -= 0.4f;
            Debug.Log($"Speedup! gameSpeed: {gameSpeed}, spawningGap: {spawningGap}");
        }
    }

    IEnumerator Distance()
    {
        while (true)
        {
            meters += 1f * gameSpeed;
            distanceDisplay.text = Instance.meters.ToString();
            yield return new WaitForSeconds(1);
        }
    } 


    // Swap current lanes
    public void SwapLane() 
    {
        currentLane = !currentLane;

        // Change to top lane
        if (currentLane) {
            player.playerRB.mass = 0.6f;
            player.playerObject.layer = 7; // Top layer
            player.playerObject.transform.localScale = new Vector3(1f, 1f, 1f);
            player.playerObject.transform.position = new Vector3(-6f, 11f, 4f);
        } 

        // Change to bottom lane
        else {
            player.playerRB.mass = 0.5f;
            player.playerObject.layer = 6; // Bottom layer
            player.playerObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            player.playerObject.transform.position = new Vector3(-8f, 11f, 0f);
        }
    }

    // Adds to the player amount of souls
    public void ChangeSouls(int amount, bool forceSet = false)
    {
        if (forceSet) souls = amount;
        else souls += amount;
    }
}

// Kill interface (REMOVE LATER REMOVE LATER REMOVE LATER REMOVE LATER)
interface ICollission {
    public void Kill();
}