using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKick : MonoBehaviour
{
    private GameObject ScriptHolder;

    private GameObject Teams;

    // Start is called before the first frame update
    void Start()
    {
        if(ScriptHolder == null){
            ScriptHolder = GameObject.Find("ScriptHolder");
        }

        if(Teams == null){
            Teams = GameObject.Find("Teams");
        }

        Teams.GetComponent<Teams>().GoalKick();

        ScriptHolder.GetComponent<ScriptHolder>().ResetPlayersPos();

        DestroyGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyGameObject()
    {
        Debug.Log("Destroying goal kick area");
        Destroy(gameObject);
    }
}
