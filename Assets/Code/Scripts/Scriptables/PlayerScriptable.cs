using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Scriptable", menuName = "Player Scriptable")]
public class PlayerScriptable : ScriptableObject, IDamageable
{
    public bool isShieldEnabled;
    [DoNotSerialize] public GameObject playerObject;
    [DoNotSerialize] public Rigidbody2D playerRB;
    [DoNotSerialize] public GameObject shield;

    // Checks if you are allowed to kill the player or disable the shield
    public void HurtPlayer(GameObject go) {
        GameManager.Instance.bonus = 1;
        if (isShieldEnabled) DisableShield();
        else Kill(go);
    }

    // Kills the player and optionally shows the game over GUI
    public void Kill(GameObject go) {
        GameManager.Instance.lives--;
        GameManager.Instance.achievements.Ach2DoubleDeath();
        
        // Gameover condition
        if (GameManager.Instance.lives < 1)
        {
            JsonManager.Instance.userData.souls = GameManager.Instance.souls;
            JsonManager.Instance.SaveDataJSON(JsonManager.Instance.userData);
            ToggleGameOverGUI();
            return;
        }

        // Respawn on current lane
        GameManager.Instance.player.playerObject.transform.position = new Vector3(GameManager.Instance.player.playerObject.transform.position.x + 2f, 12.5f, GameManager.Instance.player.playerObject.transform.position.z);

        // Change number of remaining lives
        GameManager.Instance.livesDisplay.GetComponent<Lifebar>().Lives(false);
    }

    // Toggles ON/OFF the game over GUI
    public void ToggleGameOverGUI() {
        GameManager.Instance.LoadScene("Game Over");
    }

    // Turns on the player shield
    public void EnableShield() { isShieldEnabled = true; shield.SetActive(true); }

    // Turns off the player shield
    public void DisableShield() { isShieldEnabled = false; shield.SetActive(false); }
}
