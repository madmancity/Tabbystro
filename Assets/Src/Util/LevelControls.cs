using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControls : MonoBehaviour
{
    public static float timeRemaining;
    public static int mistakes;
    public static int completedOrders;
    public static int maxTime;

    // Start is called before the first frame update
    void Start()
    {
        mistakes = 0;
        completedOrders = 0;
        if (SceneManager.GetActiveScene().name.Equals("MainLevel"))
        {
            maxTime = 60;
        }
        else if (SceneManager.GetActiveScene().name.Equals("MainLevel1"))
        {
            maxTime = 90;
        }
        else if (SceneManager.GetActiveScene().name.Equals("MainLevel2"))
        {
            maxTime = 120;
        }
        else if (SceneManager.GetActiveScene().name.Equals("Freeplay"))
        {
            maxTime = 6000000;
            mistakes = -500000;
        }
        timeRemaining = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (mistakes >= 3)
        {
            // End level with failure status
            SceneManager.LoadScene("Lose");
        }
        else if (timeRemaining <= 0)
        {
            // End level with success status
            SceneManager.LoadScene("Win");
        }
    }
}
