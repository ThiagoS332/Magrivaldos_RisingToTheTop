using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTeamNameUI : MonoBehaviour
{
    private string shrek2 = "Shrek 2";

    //private string santos = "Santos MC Fishers";

    //private string congaz = "Congaz";

    public Font shrek2Font;

    //public Font santosFont;

    //public Font congazFont;

    private Color shrek2Color;

    //private Color santosColor;

    //private Color congazColor;

    public Text teamNameText;

    // Start is called before the first frame update
    void Start()
    {
        shrek2Color = new Color(0.33725f,0.74901f,0.16078f,1f); // RGBA : (86,122,26,255)

        //santosColor = new Color(0f,0.73333f,0.83137f,1f); // RGBA : (0,187,212,255)

        //congazColor = new Color(0.26274f,0.33725f,0.74901f,1f); // RGBA : (67,86,191,255)

        // place Shrek 2 font and name
        teamNameText.text = shrek2;
        teamNameText.GetComponent<Text>().font = shrek2Font;
        teamNameText.fontSize = 14;
        teamNameText.color = shrek2Color;
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKey("s")){
            // place Shrek 2 font and name
            teamNameText.text = shrek2;
            teamNameText.GetComponent<Text>().font = shrek2Font;
            teamNameText.fontSize = 14;
            //teamNameText.color = new Color(135,191,41,255);
            teamNameText.color = shrek2Color;
        }
        else if(Input.GetKey("m")){
            // place Santos MC Fishers font and name
            teamNameText.text = santos;
            teamNameText.GetComponent<Text>().font = santosFont;
            teamNameText.fontSize = 14;
            teamNameText.color = santosColor;
        }
        else if(Input.GetKey("c")){
            // place Congaz font and name
            teamNameText.text = congaz;
            teamNameText.GetComponent<Text>().font = congazFont;
            teamNameText.fontSize = 14;
            teamNameText.color = congazColor;
        }*/
    }
}
