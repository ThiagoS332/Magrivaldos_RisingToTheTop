using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

    private AudioSource refereeWhistle;

    // Start is called before the first frame update
    void Start()
    {
        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            ballRB = ballObj.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void PlayRefereeWhistle(){
        refereeWhistle.Play();
    }*/

    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3 ballPosEntrance = ballObj.transform.position;

        if(other.gameObject.name == "Ball"){
            if(ballPosEntrance.x > 0){
                Debug.Log("Gol do Magrivaldos");
            }
            else{
                Debug.Log("Gol dos Outros");
            }
            
            //PlayRefereeWhistle();

            ballObj.transform.position = new Vector3(0f, 0f, 0f);

            ballRB.velocity = new Vector2(0f, 0f);
            ballRB.angularVelocity = 0f;
        }
    }
}
