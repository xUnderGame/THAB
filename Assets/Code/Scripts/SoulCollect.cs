using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollect : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.souls += 1;
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            //Da el alma al jugador
            Destroy(gameObject);
        }
    }
}
