using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanEnemy : LaneBehaviour, IDamageable
{
    public GameObject self;
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwapLane(!GameManager.Instance.currentLane,
                GetComponent<Rigidbody2D>(),
                self);
            child.layer = self.layer;
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
