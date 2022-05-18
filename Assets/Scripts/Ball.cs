using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Team_1")
        {
            Debug.Log ("Os MAGRIVALDOS estãos com a bola");

            gameObject.tag = "BallTeam_1";
        }
        else if(col.collider.tag == "Team_2")
        {
            Debug.Log ("Os outros estãos com a bola");

            gameObject.tag = "BallTeam_2";
        }
    }
}
