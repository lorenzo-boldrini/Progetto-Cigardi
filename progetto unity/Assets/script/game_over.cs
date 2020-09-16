using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_over : MonoBehaviour
{
    public GameObject Monster;
    public GameObject GameOverScreen;
    public GameObject Yboy;

    bool CanRestart;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Monster.SetActive(false);
            GameOverScreen.SetActive(true);
            Yboy.SetActive(false);
            CanRestart = true;
        }
    }

    private void Update()
    {
        if (CanRestart)
        {
            if (Input.GetButton("Restart"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetButton("Cancel"))
            {
                Application.Quit();
            }
        }
    }
}
