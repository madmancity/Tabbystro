using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsBehavior : MonoBehaviour
{
    public GameObject[] lives;
    private int localMistakes;

    // Start is called before the first frame update
    void Start()
    {
        localMistakes = LevelControls.mistakes;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelControls.mistakes == -1)
        {
            localMistakes = 0;
            LevelControls.mistakes = 0;
            foreach (GameObject life in lives)
            {
                life.SetActive(true);
            }
        }
        else if (localMistakes != LevelControls.mistakes)
        {
            localMistakes = LevelControls.mistakes;
            lives[3 - localMistakes].SetActive(false);
        }
    }
}
