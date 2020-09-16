using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_controller : MonoBehaviour
{
   public void PlayGame()
    {
        Debug.Log("play");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
