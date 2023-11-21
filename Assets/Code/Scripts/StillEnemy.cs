using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillEnemy : MonoBehaviour
{
    private Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<Gun>();
        gun.cooldown = Time.time;
        gun.projectile = Resources.Load<GameObject>("Projectiles/Feather");
    }

    void FixedUpdate()
    {
        // Makes the enemy shoot every x seconds, depending on the cooldown 
        gun.cooldown = gun.Shoot(gun.cooldown, gun.projectile, gameObject.transform.GetChild(0).transform.position, gun.projectile.transform);
    }
}
