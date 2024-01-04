using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UserData;

public class MenuShop : MonoBehaviour
{
    public UserData userData;

    public Image image;
    
    public TextMeshPro totalSouls;
    public TextMeshPro improveForcefieldSouls;
    //public TextMeshPro improveMagnetSouls;

    public UserData.Forcefield forcefield = new UserData.Forcefield();

    public UserData.L1 l1 = new UserData.L1();
    public UserData.L2 l2 = new UserData.L2();
    public UserData.L3 l3 = new UserData.L3();
    public UserData.L4 l4 = new UserData.L4();
    public UserData.L5 l5 = new UserData.L5();


    public void Start()
    {
        GetTotalSouls();
        forcefield.currentLevel = 1;
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
        switch (SetNextLevel(forcefield.currentLevel))
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
    public void Return() => SceneManager.LoadScene(0);
}
