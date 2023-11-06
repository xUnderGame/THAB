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
}
