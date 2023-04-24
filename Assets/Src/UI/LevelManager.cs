using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagment;

public class LevelManager : MonoBehaviour
{
    public string sceneName;

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
