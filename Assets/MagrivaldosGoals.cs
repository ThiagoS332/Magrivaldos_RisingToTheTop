using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagrivaldosGoals : MonoBehaviour
{
    public Text goalsText;

    private GameObject Goal = null;

    // Start is called before the first frame update
    void Start()
    {
        if(Goal == null){
            Goal = GameObject.Find("RightGoalArea");
        }
    }

    // Update is called once per frame
    void Update()
    {

        goalsText.text = Goal.GetComponent<Goal>().magrivaldosScoredGoals.ToString();

    }
}
