using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControls : MonoBehaviour
{
    public static float timeRemaining;
    public static int mistakes;
    public static int completedOrders;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 120; // 120 seconds
        mistakes = 0;
        completedOrders = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0 || mistakes >= 3)
        {
            // End level with failure status
        }
        else if (completedOrders >= 5)
        {
            // End level with success status
        }
    }
}
