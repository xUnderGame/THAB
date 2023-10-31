using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Scriptable", menuName = "Player Scriptable")]
public class PlayerScriptable : ScriptableObject
{
    public GameObject fireballPrefab;
    public float fireballCD;
    [DoNotSerialize] public GameObject top;
    [DoNotSerialize] public GameObject bottom;
}
