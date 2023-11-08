using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Scriptable", menuName = "Player Scriptable")]
public class PlayerScriptable : ScriptableObject
{
    [DoNotSerialize] public GameObject playerObject;
    [DoNotSerialize] public Rigidbody2D playerRB;
    public GameObject fireballPrefab;
    public float fireballCD;

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
