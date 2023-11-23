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
    [DoNotSerialize] public float gameSpeed;
    [DoNotSerialize] public float spawningGap;

    private bool currentLane;
    public int souls;

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
        spawningGap = 15;
        gameSpeed = 1f;
        souls = 0;

        // Game speed corroutine, can change later
        // StartCoroutine(SpeedUp(1.5f));
    }

    // Enumerator for the corroutine
    IEnumerator SpeedUp(float maxSpeed)
    {
        while (gameSpeed < maxSpeed)
        {
            gameSpeed += 0.10f;
            yield return new WaitForSeconds(2f);
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
            player.playerObject.transform.position = new Vector3(-6f, 2.5f, 4f);
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
}

// Kill interface
interface ICollission {
    public void Kill();
}