using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulCollect : MonoBehaviour
{
    [SerializeField] private int value;
    
    [SerializeField] private TMP_Text soulsDisplay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Da el alma al jugador
        GameManager.Instance.ChangeSouls(value);
        soulsDisplay.text = GameManager.Instance.souls.ToString();
        Destroy(gameObject);
    }
}
