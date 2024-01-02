using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuShop : MonoBehaviour
{
    public UserData userData;

    public Text totalSouls;

    public void Start()
    {
        GetTotalSouls();
    }

    public void GetTotalSouls() => totalSouls.text = Convert.ToString(userData.souls);

    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}
