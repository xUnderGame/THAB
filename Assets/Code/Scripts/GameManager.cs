using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerScriptable player;
    [HideInInspector] public readonly float globalCD = 0.5f;
    [HideInInspector] public TMP_Text soulsDisplay;
    [HideInInspector] public TMP_Text distanceDisplay;
    [HideInInspector] public GameObject lifebar;
    [HideInInspector] public float gameSpeed;
    [HideInInspector] public float spawningGap;
    [HideInInspector] public GameObject UI;
    [HideInInspector] public GameObject shopUI;
    [HideInInspector] public PlayerMovement pm;

    public bool currentLane;
    public IngameShopBehaviour currentShop;
    public bool alive;
    public int souls;
    public int lives;
    public float meters;

    void Awake()
    {
        alive = true;
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        // Scriptables
        player.playerObject = GameObject.Find("Player");
        pm = player.playerObject.GetComponent<PlayerMovement>();
        player.shield = player.playerObject.transform.Find("Shield").gameObject;
        player.isShieldEnabled = false;

        // Setting stuff up
        player.DisableShield();
        soulsDisplay = GameObject.Find("SoulsDisplay").GetComponent<TMP_Text>();
        distanceDisplay = GameObject.Find("DistanceDisplay").GetComponent<TMP_Text>();
        UI = GameObject.Find("Game UI");
        shopUI = UI.transform.Find("Ingame Shop").gameObject;
        spawningGap = 18f;
        gameSpeed = 1f;
        souls = 0;
        lives = 7;

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
            gameSpeed += 0.025f;
            spawningGap -= 0.4f;
            Debug.Log($"Speedup! gameSpeed: {gameSpeed}, spawningGap: {spawningGap}");
        }
    }

    IEnumerator Distance()
    {
        while (true)
        {
            meters += 1f;
            distanceDisplay.text = $"{meters} m";
            yield return new WaitForSeconds(1 / gameSpeed);
        }
    }

    // Adds to the player amount of souls
    public void ChangeSouls(int amount, bool forceSet = false)
    {
        if (forceSet) souls = amount;
        else souls += amount;
    }

    public void EnableShopGUI() { shopUI.SetActive(true); }

    public void DisableShopGUI()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}