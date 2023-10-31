using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    private float shootCD;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        shootCD = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        shootCD = GameManager.Instance.SpawnBullet(shootCD, projectile, gameObject.transform.GetChild(0).transform.position);
    }
}
