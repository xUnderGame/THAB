using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void MenuShop()
    {
        SceneManager.LoadScene(3);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(4);
    }

    public void CloseApp() {
        Application.Quit();
    }
}
