using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Scriptable", menuName = "Player Scriptable")]
public class PlayerScriptable : ScriptableObject
{
    public bool isShieldEnabled;
    [DoNotSerialize] public GameObject playerObject;
    [DoNotSerialize] public Rigidbody2D playerRB;
    [DoNotSerialize] public GameObject shield;

    public GameObject fireballPrefab;
    public float fireballCD;

    // Checks if you are allowed to kill the player or disable the shield
    public void HurtPlayer(Collider2D collision, string tag = null) {
        if (!collision.name.Contains("Fireball") && !GameManager.Instance.player.isShieldEnabled) GameManager.Instance.player.Kill();
        else if (!collision.name.Contains("Fireball") && GameManager.Instance.player.isShieldEnabled) GameManager.Instance.DisableShield();
    }

    // Kills the player and optionally shows the game over GUI
    public void Kill(bool showGUI = false) {
        Debug.Log("Player was killed!");

        // //Play death animation
        //playerObject.GetComponent<Animator>().Play("Death");
        
        // Toggle GameOver GUI
        if (showGUI) ToggleGameOverGUI();
    }

    // Toggles ON/OFF the game over GUI
    public void ToggleGameOverGUI() {
        Time.timeScale = 0;
        throw new NotImplementedException();
    }
}
