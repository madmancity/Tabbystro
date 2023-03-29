using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehavior : MonoBehaviour
{
    Image RingBar;
    public float maxTime = 21f;
    float timeLeft;

    void Start () {
        RingBar = GetComponent<Image> ();
        timeLeft = maxTime;
    } 
    // Update is called once per frame
    void Update()
    {
    if (timeLeft > 0) {
        timeLeft -= Time.deltaTime;
        RingBar.fillAmount = timeLeft / maxTime;
    } else {
        Time.timeScale = 0;
    }
   // if (timeLeft == );
    }
}
