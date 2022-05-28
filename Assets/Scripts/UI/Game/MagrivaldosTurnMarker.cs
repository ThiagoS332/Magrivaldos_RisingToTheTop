using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagrivaldosTurnMarker : MonoBehaviour
{
    public Image image_UI;
    
    public Sprite turnActivate;

    public Sprite turnDeactivate;

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
            image_UI.sprite = turnActivate;
        }
        else{
            image_UI.sprite = turnDeactivate;
        }
    }
}
