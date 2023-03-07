using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{
    public TMP_Text timerField;

    // Update is called once per frame
    void Update()
    {
        int minutes = (int)(LevelControls.timeRemaining / 60);
        int seconds = (int)(LevelControls.timeRemaining % 60);
        if (minutes < 0 || seconds < 0)
        {
            minutes = 0;
            seconds = 0;
        }
        timerField.text = $"{minutes}:{(seconds < 10 ? $"0{seconds}" : $"{seconds}")}";
    }
}
