using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerScriptable player;
    public GameObject shieldObject;
    public GameObject fireballPrefab;
    [DoNotSerialize] public TMP_Text soulsDisplay;
    [DoNotSerialize] public float gameSpeed;
    [DoNotSerialize] public float spawningGap;

    private readonly float globalCD = 0.5f;
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
        player.fireballCD = Time.time;

        // Setting stuff up
        DisableShield();
        soulsDisplay = GameObject.Find("SoulsDisplay").GetComponent<TMP_Text>();
        spawningGap = 15;
        gameSpeed = 0.5f;
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

    // Turns on the player shield
    public void EnableShield() { player.isShieldEnabled = true; player.shield.SetActive(true); }

    // Turns off the player shield
    public void DisableShield() { player.isShieldEnabled = false; player.shield.SetActive(false); }
    
    // Adds to the player amount of souls
    public void ChangeSouls(int amount, bool forceSet = false)
    {
        if (forceSet) souls = amount;
        else souls += amount;
    }

    // Shoots a bullet with a cooldown
    public float SpawnBullet(float cooldown, GameObject projectile, Vector3 shootingPoint, Transform rotateFragment = null)
    {
        // Cooldown before shooting
        if (cooldown <= Time.time)
        {
            GameObject fragment = Instantiate(projectile, shootingPoint, Quaternion.identity);
            if (rotateFragment) fragment.transform.rotation = rotateFragment.rotation;
            return Time.time + globalCD;
        }
        return cooldown;
    }

    // Kills an enemy
    public void KillEnemy(GameObject enemy) {
        Debug.Log($"{enemy.name} was killed!");
        Destroy(enemy);
    }
}