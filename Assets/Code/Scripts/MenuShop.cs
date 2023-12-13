using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuShop : MonoBehaviour
{
    public void Start()
    {
        GetTotalSouls();
    }

    public void GetTotalSouls()
    {
        
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}
