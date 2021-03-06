using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagrivaldosMoves : MonoBehaviour
{
    public Text movesText;

    private GameObject Teams = null;

    // Start is called before the first frame update
    void Start()
    {
        if(Teams == null){
            Teams = GameObject.Find("Teams");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Teams.GetComponent<Teams>().player_turn_UI){
            movesText.text = "Mov.: " + Teams.GetComponent<Teams>().getMovesLeft().ToString();
        }
        else{
            movesText.text = "Mov.: -";
        }
    }
}
