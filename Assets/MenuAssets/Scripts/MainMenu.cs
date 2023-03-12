using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("MenuLvl");
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }
}