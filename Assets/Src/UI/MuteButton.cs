using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuteButton : MonoBehaviour
{
    public void mute()
    {
        AudioListener.volume = 0;
    }

    public void unmute()
    {
        AudioListener.volume = 1;
    }

    public void menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quit()
    {
        Application.Quit();
    } 
}
