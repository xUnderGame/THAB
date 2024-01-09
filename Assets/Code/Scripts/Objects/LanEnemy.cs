using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanEnemy : LaneBehaviour, IDamageable
{
    public GameObject child;
    private double swapLCD;
    private bool flag;
    public double delay;
    // Start is called before the first frame update
    void Start()
    {
        swapLCD = Time.time + delay;
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((swapLCD<=Time.time)&&flag)
        {
            bool lane;
            if (gameObject.layer == 6) lane = false;
            else lane = true;
            SwapLane(lane,
                GetComponent<Rigidbody2D>(),gameObject);
            child.layer = gameObject.layer;
            flag = false;
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
