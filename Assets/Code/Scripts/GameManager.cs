using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int souls;
    [SerializeField] private TMP_Text soulsDisplay;

    void Awake()
    {
        // Only one GameManager on scene.
        if (!instance) instance = this;
        else { Destroy(gameObject); return; }
        souls = 0;
    }

    private void OnGUI()
    {
        soulsDisplay.text = souls.ToString();
    }

    public void ChangeSouls(int amount)
    {
        souls += amount;
    }

}
