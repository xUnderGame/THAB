using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollect : MonoBehaviour
{
    [SerializeField] private int value;
    

    private GameManager soulManager;

    private void Start()
    {
        soulManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.souls += 1;
        if (collision.CompareTag("Player"))
        {
            //Da el alma al jugador
            soulManager.ChangeSouls(value);
        }
            Destroy(gameObject);
    }
}
