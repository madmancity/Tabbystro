using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextBox : MonoBehaviour
{
    DialogueParser parser;

    public Image sourceImg;
    public TMP_Text textbox;
    public string dialogue;
    public string name;
    public Sprite pose;
    int linenum;
    //Add CustomStyle name after customstyle for character nameplate
    // Start is called before the first frame update
    void Start()
    {
        parser = GameObject.Find("DialogueParserObj").GetComponent<DialogueParser>();
        textbox.text = "[Press Space to begin tutorial]";
        linenum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            textbox.text = parser.GetCont(linenum) + "\n[Press Space to continue]";
            int imgi = parser.GetPose(linenum);
            if (imgi != 0)
            {
                switch (imgi)
                {
                    case -2:
                        SceneManager.LoadScene("MainMenu");
                        break;
                    case 0:
                        break;
                    default:
                        sourceImg.sprite = Resources.Load<Sprite>($"Tutorial/{imgi}");
                        break;
                }
            }
            linenum++;
        }
    }
}
