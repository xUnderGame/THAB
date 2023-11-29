using System;
using System.Collections;
using System.Collections.Generic;
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
        if (isShieldEnabled) DisableShield();
        else Kill(go);
    }

    // Kills the player and optionally shows the game over GUI
    public void Kill(GameObject go) {
        Debug.Log("Player was killed!");

        // // Play death animation
        //playerObject.GetComponent<Animator>().Play("Death");
        
        // Toggle GameOver GUI
        ToggleGameOverGUI();
    }

    // Toggles ON/OFF the game over GUI
    public void ToggleGameOverGUI() {
        // Time.timeScale = 0;
        // throw new NotImplementedException();
    }

    // Turns on the player shield
    public void EnableShield() { isShieldEnabled = true; shield.SetActive(true); }

    // Turns off the player shield
    public void DisableShield() { isShieldEnabled = false; shield.SetActive(false); }
}
