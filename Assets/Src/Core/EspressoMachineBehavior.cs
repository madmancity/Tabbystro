using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachineBehavior : MonoBehaviour
{
    public GameObject panel;
    public string[] inventory;

    // Start is called before the first frame update
    void Start()
    {
        if (inventory.Length > 0)
        {
            panel.SetActive(true);

        }
        else
        {
            panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
