using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool moving;

    private GameObject Teams;

    private Vector2 vector_zero = new Vector2(0f, 0f);

    public bool getMoving(){
        return this.moving;
    }

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
        if(this.GetComponent<Rigidbody2D>().velocity == vector_zero){
            moving = false;
        }
        else{
            moving = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(Teams.GetComponent<Teams>().getPlayerTurn()){
            if(col.collider.tag == "Team_1")
            {
                //Debug.Log ("Os MAGRIVALDOS estãos com a bola");

                gameObject.tag = "BallTeam_1";
            }
            else if(col.collider.tag == "Team_2")
            {
                //Debug.Log ("Os outros estãos com a bola");

                gameObject.tag = "MagrivaldosLostBall";
            }
        }
        else{
            if(col.collider.tag == "Team_1")
            {
                //Debug.Log ("Os MAGRIVALDOS estãos com a bola");

                gameObject.tag = "EnemyLostBall";
            }
            else if(col.collider.tag == "Team_2")
            {
                //Debug.Log ("Os outros estãos com a bola");

                gameObject.tag = "BallTeam_2";
            }
        }
    }
}
