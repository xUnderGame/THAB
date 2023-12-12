using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerScriptable player;
    [HideInInspector] public readonly float globalCD = 0.5f;
    [HideInInspector] public TMP_Text soulsDisplay;
    [HideInInspector] public TMP_Text distanceDisplay;
    [HideInInspector] public float gameSpeed;
    [HideInInspector] public float spawningGap;

    public bool currentLane;
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
        StartCoroutine(BackToPosition());
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
            meters += 1f;
            distanceDisplay.text = Instance.meters.ToString();
            yield return new WaitForSeconds(1 / gameSpeed);
        }
    }

    IEnumerator BackToPosition()
    {
        while (true)
        {
            if (player.playerObject.transform.position.x < -8)
            {
                yield return new WaitForSeconds(3 / gameSpeed);
                do
                {
                    player.playerRB.AddForce(Vector2.right * 8, ForceMode2D.Impulse);
                    yield return new WaitForSeconds(0.005f);
                } while (player.playerObject.transform.position.x < -8);
            }
            else if (player.playerObject.transform.position.x > -8)
            {
                player.playerObject.transform.position = new Vector3(-8, player.playerObject.transform.position.y, player.playerObject.transform.position.z);
            }
            yield return null;
        }
    }


    // Adds to the player amount of souls
    public void ChangeSouls(int amount, bool forceSet = false)
    {
        if (forceSet) souls = amount;
        else souls += amount;
    }
}