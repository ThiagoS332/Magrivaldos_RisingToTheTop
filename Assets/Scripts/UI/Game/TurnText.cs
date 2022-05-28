using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnText : MonoBehaviour
{
    public Text turnText;

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
        turnText.text = "Turno " + Teams.GetComponent<Teams>().turns.ToString();
    }
}
