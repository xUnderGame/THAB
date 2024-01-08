using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuShop : MonoBehaviour
{
    /*public Image image;
    
    public TextMeshPro totalSouls;
    public TextMeshPro improveForcefieldSouls;
    //public TextMeshPro improveMagnetSouls;

    public void Start()
    {
        GetTotalSouls();
        SetPrice();
    }

    public void ImprovePowerUp()
    {
        //totalSouls.text -= 
    }

    //Get own souls from json
    public void GetTotalSouls() => totalSouls.text = Convert.ToString(userData.souls);

    //Set the price of next level
    public void SetPrice()
    {
        switch (SetNextLevel(JsonManager.Instance.userData.upgrades.forcefield.currentLevel))
        {
            case "L1":
                improveForcefieldSouls.text = Convert.ToString(l1.nextUpgradeCost);
                //image.color.a = 255;
                break;
            case "L2":
                improveForcefieldSouls.text = Convert.ToString(l2.nextUpgradeCost);
                break;
            case "L3":
                improveForcefieldSouls.text = Convert.ToString(l3.nextUpgradeCost);
                break;
            case "L4":
                improveForcefieldSouls.text = Convert.ToString(l4.nextUpgradeCost);
                break;
            case "L5":
                improveForcefieldSouls.text = Convert.ToString(l5.nextUpgradeCost);
                break;
            default:
                break;
        }
    }

    //Set the next level
    public string SetNextLevel(int currentLevel)
    {
        return "L" + (currentLevel + 1);
    }

    // Return to menu
    public void Return() => SceneManager.LoadScene(0);*/
}
