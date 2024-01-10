using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanEnemy : LaneBehaviour, IDamageable
{
    public GameObject child;
    private GameObject playerChase;
    private bool jumped;

    // Start is called before the first frame update
    void Start()
    {
        playerChase = GameObject.Find("Player");
        jumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerChase.layer != gameObject.layer) && (!jumped))
        {
            SwapLane(!GameManager.Instance.currentLane,
                GetComponent<Rigidbody2D>(),
                gameObject);
            jumped = true;
        }
    }

    // Collision actions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable test)) test?.Kill(gameObject);
    }

    public void Kill(GameObject go)
    {
        Debug.Log($"{gameObject.name} was killed!");
        Destroy(gameObject);
    }
}
