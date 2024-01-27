using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    private bool[] achTracker = new bool[5];
    private string[] achNames = new string[5] { "Survivor", "Consolation Prize", "Persistent", "Gift Ticket", "Marksman" };

    private string[] achDescs = new string[5] { "You travelled 150 m", "You died twice in quick succession", "You travelled 25 m without changing lanes", "You spent 100 souls in the shop", "You shot 3 enemies" };
    private float travel;
    private float traveLane;
    private int shot;
    private bool respawned;
    public float quickDeath;
    // Start is called before the first frame update
    void Start()
    {
        Array.Fill(achTracker, false);
        respawned = false;
        travel = 0;
        traveLane = 0;
        quickDeath = 0;
        shot = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!achTracker[0]) {
            //to be replaced with exact distance travelled
            if (travel+150 < GameManager.Instance.meters) {
                achTracker[0] = true;
                GameManager.Instance.EnablePopUp(achNames[0], achDescs[0]);
            }
        }
        if (!achTracker[1] && respawned) {
            if(quickDeath+3 < GameManager.Instance.meters)
            {
                quickDeath = GameManager.Instance.meters;
                respawned = false;
            }
        }
        if (!achTracker[2])
        {
            //to be replaced with exact distance travelled
            if (traveLane+25<GameManager.Instance.meters)
            {
                achTracker[2] = true;
                GameManager.Instance.EnablePopUp(achNames[2], achDescs[2]);
            }
            if (Input.GetKey(KeyCode.V)) traveLane = GameManager.Instance.meters;
        }
    }
    public void Ach2DoubleDeath()
    {
        if (respawned && !achTracker[1])
        {
            achTracker[1] = true;
            GameManager.Instance.EnablePopUp(achNames[1], achDescs[1]);
        }
        else respawned = true;
    }
    public void Ach4Whaled()//call from shop if spent a lot of money
    {
        if (!achTracker[3])
        {
                achTracker[3] = true;
                GameManager.Instance.EnablePopUp(achNames[3], achDescs[3]);
        }
    }
    public void Ach5Shoot()
    {
        if (!achTracker[4])
        {
            if (shot < 3) shot++;
            else
            {
                achTracker[3] = true;
                GameManager.Instance.EnablePopUp(achNames[3], achDescs[3]);
            }
        }
    }
}
