using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    [HideInInspector] public GameObject projectile;
    [HideInInspector] public float cooldown;

    // Shoots a bullet with a cooldown
    public float Shoot(float cooldown, GameObject projectile, Vector3 shootingPoint, GameObject parent, Transform rotateFragment = null)
    {
        // Cooldown before shooting
        if (cooldown <= Time.time)
        {
            GameObject fragment = Instantiate(projectile, shootingPoint, Quaternion.identity);
            fragment.layer = parent.layer;
            if (rotateFragment) fragment.transform.rotation = rotateFragment.rotation;
            return Time.time + GameManager.Instance.globalCD;
        }
        return cooldown;
    }
}
