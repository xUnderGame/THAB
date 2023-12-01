using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour, IInteractable
{
    [SerializeField] private int value;
    public void Interact() {
        // Da el alma al jugador
        GameManager.Instance.ChangeSouls(value);
        GameManager.Instance.soulsDisplay.text = GameManager.Instance.souls.ToString();
        Destroy(gameObject);
    }
}
