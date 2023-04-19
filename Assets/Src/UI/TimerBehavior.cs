using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehavior : MonoBehaviour
{
    Image RingBar;
    public float maxTime = 21f;
    public float halfTime = 14f;
    public float crunchTime = 7f;
    float timeLeft;
    public Image Customer;
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
    if (timeLeft <= halfTime){
        RingBar.color = Color.yellow;
        Customer.sprite = Resources.Load<Sprite>("Sprites/Gabriella Profile");
    }
    if (timeLeft <= crunchTime){
        RingBar.color = Color.red;
        Customer.sprite = Resources.Load<Sprite>("Sprites/Boris");
    } 
    if (timeLeft <= 0f) {
        LevelControls.mistakes++;
    }
    } 
}
