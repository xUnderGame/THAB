using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerScriptable player;
    [HideInInspector] public readonly float globalCD = 0.5f;
    [HideInInspector] public TMP_Text soulsDisplay;
    [HideInInspector] public TMP_Text distanceDisplay;
    [HideInInspector] public GameObject livesDisplay;
    [HideInInspector] public float gameSpeed;
    [HideInInspector] public float spawningGap;
    [HideInInspector] public GameObject UI;
    [HideInInspector] public GameObject shopUI;
    [HideInInspector] public List<IngameItem> ingameItems;
    [HideInInspector] public PlayerMovement pm;
    [HideInInspector] public Transform putoSuelo;
    [HideInInspector] public int maxLives;
    [HideInInspector] public int lives;

    public bool currentLane;
    public IngameShopBehaviour currentShop;
    public int souls;
    public float meters;
    public float score;

    private readonly int maxFallSpd = -50;
    
    void Awake()
    {
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        // Scriptables
        player.playerObject = GameObject.Find("Player");
        pm = player.playerObject.GetComponent<PlayerMovement>();
        pm = player.playerObject.GetComponent<PlayerMovement>();
        player.shield = player.playerObject.transform.Find("Shield").gameObject;
        player.isShieldEnabled = false;

        // UI
        UI = GameObject.Find("Game UI");
        soulsDisplay = UI.transform.Find("Displays").Find("SoulsDisplay").GetComponent<TMP_Text>();
        distanceDisplay = UI.transform.Find("Displays").Find("DistanceDisplay").GetComponent<TMP_Text>();
        shopUI = UI.transform.Find("Ingame Shop").gameObject;
        livesDisplay = UI.transform.Find("Displays").Find("LivesDisplay").gameObject;

        // Grounded check
        putoSuelo = player.playerObject.transform.Find("PutoSuelo").transform;
        Physics2D.IgnoreCollision(player.playerObject.GetComponent<Collider2D>(), putoSuelo.gameObject.GetComponent<Collider2D>());
        
        // Setting stuff up
        Resources.LoadAll<GameObject>("Items").ToList().ForEach(item => { ingameItems.Add(item.GetComponent<IngameItem>()); });
        player.DisableShield();
        spawningGap = 18f;
        gameSpeed = 1f;
        maxLives = 7;
        lives = 7;
        souls = 0;

        // Game speed corroutine, can change later
        StartCoroutine(SpeedUp(1.2f));
        StartCoroutine(Distance());
        StartCoroutine(BackToPosition());
        StartCoroutine(ScoreByMeters());
    }
    // Enumerator for the corroutine
    IEnumerator SpeedUp(float maxSpeed)
    {
        while (gameSpeed < maxSpeed)
        {
            yield return new WaitForSeconds(2f);
            gameSpeed += 0.025f;
            spawningGap -= 0.4f;
            // Debug.Log($"Speedup! gameSpeed: {gameSpeed}, spawningGap: {spawningGap}");
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

    IEnumerator BackToPosition()
    {
        while (true)
        {
            if (player.playerObject.transform.position.x < -8)
            {
                yield return new WaitForSeconds(3 / gameSpeed);
                do
                {
                    if(pm.IsGrounded()) player.playerRB.AddForce(Vector2.right * 1.25f, ForceMode2D.Impulse);
                    yield return new WaitForSeconds(0.02f);
                } while (player.playerObject.transform.position.x < -8);
            }
            else if (player.playerObject.transform.position.x > -8)
            {
                player.playerObject.transform.position = new Vector3(-8, player.playerObject.transform.position.y, player.playerObject.transform.position.z);
            }
            yield return null;
        }
    }

    IEnumerator ScoreByMeters()
    {
        while (true)
        {
            score += 1f;
            distanceDisplay.text = $"{score} m";
            yield return new WaitForSeconds(5/gameSpeed);
        }
    }


    // Adds to the player amount of souls
    public void ChangeSouls(int amount, bool forceSet = false)
    {
        if (forceSet) souls = amount;
        else souls += amount;
    }

    public void Update()
    {
        // Velocity cap
        if(player.playerRB.velocity.y < maxFallSpd)
        {
            player.playerRB.velocity = new Vector2(player.playerRB.velocity.x, maxFallSpd);
        }
        Debug.Log(score);
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
        Destroy(gameObject); // Destroys GameManager after leaving the game scene
    }
}