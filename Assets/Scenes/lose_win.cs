using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lose_win : MonoBehaviour
{
    public void replay()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void easy()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void med()
    {
        SceneManager.LoadScene("MainLevel1");
    }

    public void hard()
    {
        SceneManager.LoadScene("MainLevel2");
    }

    public void exit()
    {
        Application.Quit();
    }

    public void menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void tutorial()
    {
        SceneManager.LoadScene("TutOpen");
    }

    public void freeplay()
    {
        SceneManager.LoadScene("Freeplay");
    }
}
