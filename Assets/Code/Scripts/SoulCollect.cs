using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollect : MonoBehaviour
{
    [SerializeField] private int value;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Da el alma al jugador
        GameManager.Instance.ChangeSouls(value);
        GameManager.Instance.soulsDisplay.text = GameManager.Instance.souls.ToString();
        Destroy(gameObject);
    }
}
