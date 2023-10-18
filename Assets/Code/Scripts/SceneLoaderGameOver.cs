using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderGameOver : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log("ok");
        SceneManager.LoadScene(sceneName);
    }
}
