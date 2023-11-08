using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    private float shootCD;

    void Start() { shootCD = Time.time; }

    void FixedUpdate()
    {
        // Makes the enemy shoot every x seconds, depending on the cooldown 
        shootCD = GameManager.Instance.SpawnBullet(shootCD, projectile, gameObject.transform.GetChild(0).transform.position, projectile.transform);
    }
}
