using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerScriptable player;
    [HideInInspector] public readonly float globalCD = 0.5f;
    [HideInInspector] public Text soulsDisplay;
    [HideInInspector] public Text distanceDisplay;
    [HideInInspector] public GameObject lifebar;
    [HideInInspector] public float gameSpeed;
    [HideInInspector] public float spawningGap;
    [HideInInspector] public GameObject UI;
    [HideInInspector] public GameObject shopUI;
    [HideInInspector] public PlayerMovement pm;
    [HideInInspector] public Transform putoSuelo;

    public bool currentLane;
    public IngameShopBehaviour currentShop;
    public bool alive;
    public int souls;
    public int lives;
    public float meters;

    private readonly int maxFallSpd = -50;
    
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
        pm = player.playerObject.GetComponent<PlayerMovement>();
        player.shield = player.playerObject.transform.Find("Shield").gameObject;
        player.isShieldEnabled = false;

        // UI
        UI = GameObject.Find("Game UI");
        soulsDisplay = UI.transform.Find("SoulsDisplay").GetComponent<Text>();
        distanceDisplay = UI.transform.Find("DistanceDisplay").GetComponent<Text>();
        shopUI = UI.transform.Find("Ingame Shop").gameObject;

        // Grounded check
        putoSuelo = player.playerObject.transform.Find("PutoSuelo").transform;
        Physics2D.IgnoreCollision(player.playerObject.GetComponent<Collider2D>(), putoSuelo.gameObject.GetComponent<Collider2D>());
        
        // Setting stuff up
        player.DisableShield();
        spawningGap = 18f;
        gameSpeed = 1f;
        souls = 0;
        lives = 7;

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


    // Adds to the player amount of souls
    public void ChangeSouls(int amount, bool forceSet = false)
    {
        if (forceSet) souls = amount;
        else souls += amount;
    }

    public void Update()
    {
        if(player.playerRB.velocity.y < maxFallSpd)
        {
            player.playerRB.velocity = new Vector2(player.playerRB.velocity.x, maxFallSpd);
        }
        Debug.Log(player.playerRB.velocity.y);
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