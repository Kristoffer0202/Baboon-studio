using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() // Starter spillet
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame() // lukker spillet
    {
        Debug.Log("QUIT");
        Application.Quit();

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
