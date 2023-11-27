using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    [DoNotSerialize] public GameObject projectile;
    [DoNotSerialize] public float cooldown;

    // Shoots a bullet with a cooldown
    public float Shoot(float cooldown, GameObject projectile, Vector3 shootingPoint, Transform rotateFragment = null)
    {
        // Cooldown before shooting
        if (cooldown <= Time.time)
        {
            GameObject fragment = Instantiate(projectile, shootingPoint, Quaternion.identity);
            if (rotateFragment) fragment.transform.rotation = rotateFragment.rotation;
            return Time.time + GameManager.Instance.globalCD;
        }
        return cooldown;
    }
}
