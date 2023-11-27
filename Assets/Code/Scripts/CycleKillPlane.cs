using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CycleKillPlane : MonoBehaviour
{
    [DoNotSerialize] public List<GameObject> planes;

    void Start() { planes = GameObject.FindGameObjectsWithTag("Fire").ToList(); }

    void FixedUpdate() { planes.ForEach(plane => { if (plane.transform.position.x <= -80f) plane.transform.position = new Vector3(40f, plane.transform.position.y, plane.transform.position.z); }); }
}
