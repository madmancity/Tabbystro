using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

public class DialogueParser : MonoBehaviour
{
    List<dLine> lines;
    List<Sprite> images;
    struct dLine 
    {
        public string name;
        public string cont;
        public int pose;

        public dLine(string ne, string ct, int pe)
        {
            name = ne;
            cont = ct;
            pose = pe;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        lines = new List<dLine>();
        LoadDialogue("dialogue1.txt");
        images = new List<Sprite>();
        LoadImages(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void LoadImages()
    {
        for(int i = 0; i < lines.Count; i++)
        {
            string imageName = lines[i].name;
            Sprite image = (Sprite) Resources.Load(imageName, typeof(Sprite));
            if(!images.Contains (image))
            {
                images.Add (image);
            }
        }
    }

    public string GetName(int linenum)
    {
        if(linenum < lines.Count)
            return lines [linenum].name;
        return "";
    }

    public string GetCont(int linenum)
    {
        if(linenum < lines.Count)
            return lines [linenum].cont;
        return "";
    }

    public int GetPose(int linenum)
    {
        if (linenum < lines.Count)
            return lines[linenum].pose;
            //return images[lines [linenum].pose];
        return 0;
    }

    public static MemoryStream GenerateStreamFromString(string value)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
    }

    // Change filepath for your version of the unity project
    void LoadDialogue(string fname)
    {
        string f = "Tutorial/" + fname;
        string line;

        string dump = @"Pierre2, ""Bonjour, oui."",0
Pierre, ""I知 Chef Pierre oui."",0 
Pierre2, ""Mew must be the one, oui?"",0 
Pierre2, ""Meow知 meant to teach you to run a cafe, oui!"",0
Pierre, ""Meow promise Meow has lots of experience."",0 
Pierre2, ""So trust in Meow, oui."",0
Pierre, ""..."",0
Pierre2, ""Mew値l call this place Tabbystro, oui?"",0
Pierre2, ""Great, oui!"",0
Pierre, ""Let痴 get started, oui!"",0
Pierre, ""This is mewr workspace oui."",1
Pierre, ""Mewr ingredients are located at the bottom oui."",2
Pierre, ""Use them by dragging them into the open space on the board oui."",3
Pierre, ""Mew can combine two ingredients to make something new oui."",4
Pierre, ""But beware, combining two ingredients that should not go together will create a disastrous dish called muck oui."",5
Pierre, ""Get three muck and mew値l have to close down due to health violations oui."",6
Pierre, ""Mew'll have to watch out for the timer too, and try not to mess up the orders oui."",7
Pierre, ""A combination of three wrong orders or mush will get mew fired, oui."",0
Pierre, ""If mew serve 5 customers correctly, Meow'll hire you full time oui."",0
Pierre, ""At the top of the workspace is where mew値l receive customer orders oui."",8
Pierre, ""Customers aren稚 very specific here in Purris, so mew値l have to take it upon mewrself to find the hidden clues in their words oui."",0
Pierre, ""If mew forget Meow teachings, Meow'll be here to give another tutorial oui."",-1
Pierre, ""Good luck oui."",0
Pierre, """",-2";
        
        StreamReader rdr = new StreamReader(GenerateStreamFromString(dump), System.Text.Encoding.UTF8, true);

        using(rdr)
        {
            do
            {
                line = rdr.ReadLine();
                if(line != null)
                {
                    string[] line_values = SplitCsvLine(line);
                    dLine line_entry = new dLine(line_values[0], line_values[1], int.Parse(line_values[2]) );
                    lines.Add(line_entry);
                }
                
            }
            while(line != null);
                rdr.Close();
        }
    }
    string[] SplitCsvLine(string line)
    {
            string pattern = @"
            (?!\s*$)
            \s*
            (?:
              '(?<val>[^'\\]*(?:\\[\S\s][^'\\]*)*)'
            | ""(?<val>[^""\\]*(?:\\[\S\s][^""\\]*)*)""
            | (?<val>[^,'""\s\\]*(?:\s+[^,'""\s\\]+)*)
            )
            \s*
            (?:.|$)
            "; 

            string[] values = (from Match m in Regex.Matches(line, pattern,
            RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace |
            RegexOptions.Multiline) select m.Groups[1].Value).ToArray();

            return values;
    }
}
